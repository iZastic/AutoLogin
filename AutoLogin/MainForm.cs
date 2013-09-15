using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutoLogin
{
    public partial class MainForm : Form
    {
        // Bring the process to foreground
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        // Force window position and size
        [DllImport("user32.dll", EntryPoint = "PostMessageA")]
        private extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        // Convert char to virtual-key code
        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);
        // Post message to process
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        string data;
        Account ActiveAccount;
        bool Is64bit = Environment.Is64BitOperatingSystem;
        Options options;
        public Crypto crypto;
        public static List<Account> ACCOUNTS;
        public static Settings SETTINGS;
        public static string PATH;
        public static string PASSWORD = "password";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Finds the path where program is executing
            PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            options = new Options();
            crypto = new Crypto();
            ACCOUNTS = new List<Account>();

            // Check for settings file or creat default settings
            if (File.Exists(PATH + @"\settings.xml"))
            {
                LoadSettings();
            }
            else
            {
                SETTINGS = new Settings();
            }
            // If data file exists load it
            if (File.Exists(PATH + @"\data.al"))
            {
                data = File.ReadAllText(PATH + @"\data.al");
            }
            // Check to see if we have the right path for WoW
            if (!File.Exists(SETTINGS.WowPath + @"\Wow.exe"))
            {
                MessageBox.Show("Could not find Wow.exe." + Environment.NewLine + "Please browse to your World of Warcraft folder.");
                new SettingsForm().ShowDialog(this);
            }
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new AddEditForm().ShowMe(this, 0);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ActiveAccount != null)
            {
                new AddEditForm().ShowMe(this, 1, ActiveAccount);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (ActiveAccount != null)
            {
                if (MessageBox.Show("Remove this account?", "", MessageBoxButtons.YesNo)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    ACCOUNTS.Remove(ActiveAccount);
                    ActiveAccount = null;
                    refreshList();
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // TODO: show the settings form
            new SettingsForm().ShowDialog(this);
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (ActiveAccount != null)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = SETTINGS.WowPath + (ActiveAccount.Client == "32bit" ? @"\Wow.exe" : (Is64bit ? @"\Wow-64.exe" : @"\Wow.exe")),
                        Arguments = (ActiveAccount.Client == "32bit" ? "-noautolaunch64bit " : "")
                    }
                };
                SetWTFAndStart(process, ActiveAccount);
                Login(process, ActiveAccount);
            }
        }

        private void btnLaunchAll_Click(object sender, EventArgs e)
        {
            if (ACCOUNTS.Count > 0)
            {
                foreach (Account account in ACCOUNTS)
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = SETTINGS.WowPath + (account.Client == "32bit" ? @"\Wow.exe" : (Is64bit ? @"\Wow-64.exe" : @"\Wow.exe")),
                            Arguments = (account.Client == "32bit" ? "-noautolaunch64bit " : "")
                        }
                    };
                    SetWTFAndStart(process, account);
                    Login(process, account);
                }
            }
        }

        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAccounts.DataSource != null)
            {
                foreach (Account account in ACCOUNTS)
                {
                    if (account.ToString() == lstAccounts.SelectedItem.ToString())
                    {
                        ActiveAccount = account;
                        break;
                    }
                }
                if (ActiveAccount.Client == "32bit")
                {
                    rdo32bit.Checked = true;
                }
                else
                {
                    rdo64bit.Checked = true;
                }
            }
        }

        private void rdo32bit_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveAccount != null)
            {
                ActiveAccount.Client = "32bit";
            }
        }

        private void rdo64bit_CheckedChanged(object sender, EventArgs e)
        {
            if (ActiveAccount != null)
            {
                ActiveAccount.Client = "64bit";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void LoadSettings()
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(Settings));
                StreamReader file = new StreamReader(PATH + @"\settings.xml");
                SETTINGS = (Settings)reader.Deserialize(file);
                lstAccounts.DataSource = ACCOUNTS;
                file.Close();
                if (SETTINGS.HasPassword)
                {
                    new PasswordForm().GetPassword(this);
                }
            }
            catch (Exception) { }
        }

        private void LoadData()
        {
            if (data != null && data.Length > 0)
            {
                try
                {
                    XmlSerializer reader = new XmlSerializer(typeof(List<Account>));
                    List<Account> accs = new List<Account>();
                    ACCOUNTS = (List<Account>)reader.Deserialize(crypto.Decrypt(data, PASSWORD));
                    lstAccounts.DataSource = ACCOUNTS;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Password!");
                }
            }
        }

        private void SaveData()
        {
            if (ACCOUNTS.Count > 0)
            {
                XmlSerializer writer = new XmlSerializer(typeof(List<Account>));
                MemoryStream xml = new MemoryStream();
                writer.Serialize(xml, ACCOUNTS);
                File.WriteAllText(PATH + @"\data.al", crypto.Encrypt(xml.ToArray(), PASSWORD));
            }
        }

        public void refreshList()
        {
            SaveData();
            lstAccounts.DataSource = null;
            lstAccounts.DataSource = ACCOUNTS;
        }

        private void SetWTFAndStart(Process process, Account account)
        {
            options.SetAccount(account);

            // Find or create directory
            if (!Directory.Exists(SETTINGS.WowPath + @"\WTF"))
            {
                Directory.CreateDirectory(SETTINGS.WowPath + @"\WTF");
            }
            else
            {
                if (File.Exists(SETTINGS.WowPath + @"\WTF\Config.wtf"))
                {
                    // Modify current config file
                    File.WriteAllLines(SETTINGS.WowPath + @"\WTF\Config.wtf",
                        File.ReadLines(SETTINGS.WowPath + @"\WTF\Config.wtf").
                            Where(line => !line.Contains("readTOS")).
                            Where(line => !line.Contains("readEULA")).
                            Where(line => !line.Contains("accountName")).
                            Where(line => !line.Contains("gxWindow")).
                            Where(line => !line.Contains("hwDetect")).
                            Where(line => !line.Contains("gxMaximize")).
                            Where(line => !line.Contains("accountList")).
                            Where(line => !line.Contains("graphicsQuality")).
                            Where(line => !line.Contains(account.SetRealm ? "realmName" : "null")).
                            Where(line => !line.Contains(account.Windowed ? "gxResolution" : "null")).
                            Where(line => !line.Contains(account.LowDetail ? "gxApi" : "null")).
                            Where(line => !line.Contains(account.SetCharacter ? "lastCharacterIndex" : "null")).
                            ToList());
                    File.AppendAllLines(SETTINGS.WowPath + @"\WTF\Config.wtf", options.CompiledList());
                }
                else
                {
                    // Create new config file
                    File.WriteAllLines(SETTINGS.WowPath + @"\WTF\Config.wtf", options.CompiledList());
                }
            }

            Thread.Sleep(250);
            process.Start();
            Thread.Sleep(400);
        }

        private void Login(Process p, Account a)
        {
            // Run this in a new thread so AutoLogin is not frozen
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                uint WM_KEYDOWN = 0x0100;
                uint WM_KEYUP = 0x0101;
                int VK_RETURN = 0x0D;
                Process process = p;
                Account account = a;

                do // Keep repeating till window is idle
                {
                    process.WaitForInputIdle();
                    process.Refresh();
                } while (process.MainWindowHandle.ToInt32() == 0);

                // Sleep for a little to give the insides time to load
                Thread.Sleep(account.Windowed ? 600 : 1500);
                // Send the password one key at a time
                for (int i = 0; i < account.Password.Length; i++)
                {
                    PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VkKeyScan(account.Password.ToCharArray()[i])), IntPtr.Zero);
                    Thread.Sleep(30);
                }
                // Hit enter to log in
                PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);

                if (account.EnterWorld)
                {
                    Thread.Sleep(15000);
                    PostMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_RETURN), IntPtr.Zero);
                    PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);
                }

                Thread.CurrentThread.Abort();
            }).Start();
        }
    }
}

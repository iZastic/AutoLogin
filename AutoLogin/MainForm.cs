using System;
using System.Collections.Generic;
using System.Deployment.Application;
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
        private const int SW_RESTORE = 9;
        // Post message to process
        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        // Restore window after hidden to taskbar
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            // Finds the path where program is executing
            PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Add version to title
            this.Text += " v" + version.Major + '.' + version.Minor + '.' + version.Build;
            tskIcon.Text += " v" + version.Major + '.' + version.Minor + '.' + version.Build;

            // Check for settings file or create default settings
            if (File.Exists(PATH + @"\settings.xml"))
            {
                LoadSettings();
            }
            else
            {
                SETTINGS = new Settings();
            }

            // Check for updates
            CheckUpdate(version);

            options = new Options();
            crypto = new Crypto();
            ACCOUNTS = new List<Account>();

            // If data file exists load it
            if (File.Exists(PATH + @"\data.al"))
            {
                data = File.ReadAllText(PATH + @"\data.al");
            }

            // Look for wow.exe
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
                if (lstAccounts.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show("Remove this account?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        ACCOUNTS.Remove(ActiveAccount);
                    }
                }
                else if (lstAccounts.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("Remove these accounts?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                        == System.Windows.Forms.DialogResult.Yes)
                    {
                        for (int i = 0; i < lstAccounts.SelectedItems.Count; i++)
                        {
                            ACCOUNTS.Remove(ACCOUNTS.Find(a => a.Name == lstAccounts.SelectedItems[i].ToString()));
                        }
                    }
                }
                ActiveAccount = null;
                refreshList();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Open settings form
            new SettingsForm().ShowMe(this);
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            // Minimize if option is set
            if (SETTINGS.Minimize)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            // For the ActiveAccount: create a process, set config, login
            if (ActiveAccount != null)
            {
                if (lstAccounts.SelectedItems.Count == 1)
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
                else
                {
                    if (lstAccounts.SelectedItems.Count > 1)
                    {
                        LaunchSelected();
                    }
                    else
                    {
                        MessageBox.Show("Please select at least 1 account");
                    }
                }
            }
        }

        private void btnLaunchAll_Click(object sender, EventArgs e)
        {
            // Minimize if option is set
            if (SETTINGS.Minimize)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            // For each account: create local account, create a process, set config, login
            if (ACCOUNTS.Count > 0)
            {
                if (lstAccounts.SelectedItems.Count <= 1)
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
                else
                {
                    LaunchSelected();
                }
            }
        }

        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAccounts.DataSource != null)
            {
                // Set active account when 1 account clicked
                if (lstAccounts.SelectedItems.Count == 1)
                {
                    ActiveAccount = ACCOUNTS.Find(a => a.Name == lstAccounts.SelectedItem.ToString());

                    // Set the 32bit/64bit selection
                    if (ActiveAccount.Client == "32bit")
                    {
                        rdo32bit.Checked = true;
                    }
                    else
                    {
                        rdo64bit.Checked = true;
                    }
                }

                // If more than one account is selected disable the edit button and 32bit/64bit selection
                if (lstAccounts.SelectedItems.Count > 1)
                {
                    rdo32bit.Enabled = false;
                    rdo64bit.Enabled = false;
                    btnEdit.Enabled = false;
                }
                else
                {
                    rdo32bit.Enabled = true;
                    rdo64bit.Enabled = true;
                    btnEdit.Enabled = true;
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // If minimized and setting, Hide, is true
            if (FormWindowState.Minimized == this.WindowState && SETTINGS.Hide)
            {
                LoadLaunchAccounts();
                tskIcon.Visible = true;
                tskIcon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState && SETTINGS.Hide)
            {
                tskIcon.Visible = false;
            }
        }

        private void tskIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Show form and restore focus
            this.Show();
            ShowWindow(this.Handle, SW_RESTORE);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings and account data when closing
            SaveSettings();
            SaveData();
        }

        private void launchAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // For each account: create local account, create a process, set config, login
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

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show window and bring to front
            ShowWindow(this.Handle, SW_RESTORE);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit as if hitting the red X
            Application.Exit();
        }

        private void CheckUpdate(Version version)
        {
            if (SETTINGS.AutoUpdate)
            {
                // If file not found or no internet connection this will fail showing no error
                try
                {
                    // Load update version from online xml version file and see if it's new than our version
                    Version v = new Version(XDocument.Load("http://izastic.twomini.com/autologin/update.xml").Root.Element("Version").Value);
                    if (v > version)
                    {
                        new UpdateForm().ShowMe(this, v);
                    }
                }
                catch{}
            }
        }

        private void LaunchSelected()
        {
            // For each account selected: create local account, create a process, set config, login
            for (int i = 0; i < lstAccounts.SelectedItems.Count; i++)
            {
                Account account = ACCOUNTS.Find(a => a.Name == lstAccounts.SelectedItems[i].ToString());
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

        public void SaveSettings()
        {
            SaveSelected();

            // Save the XML settings file
            XmlSerializer writer = new XmlSerializer(typeof(Settings));
            StreamWriter file = new StreamWriter(MainForm.PATH + @"\settings.xml");
            writer.Serialize(file, MainForm.SETTINGS);
            file.Close();
        }

        private void LoadSettings()
        {
            try
            {
                // Load the XML settings file
                XmlSerializer reader = new XmlSerializer(typeof(Settings));
                StreamReader file = new StreamReader(PATH + @"\settings.xml");
                SETTINGS = (Settings)reader.Deserialize(file);
                file.Close();
                if (SETTINGS.HasPassword)
                {
                    new PasswordForm().GetPassword(this);
                }
            }
            catch (Exception) { }
        }

        private void SaveSelected()
        {
            // Save currently selected accounts in SETTINGS
            if (lstAccounts.SelectedItems.Count > 0)
            {
                SETTINGS.Selected = new int[lstAccounts.SelectedItems.Count];
                for (int i = 0; i < SETTINGS.Selected.Length; i++)
                {
                    SETTINGS.Selected[i] = lstAccounts.SelectedIndices[i];
                }
            }
            else
            {
                SETTINGS.Selected = new int[] { 0 };
            }
        }

        private void SaveData()
        {
            // If there are accounts
            if (ACCOUNTS.Count > 0)
            {
                // Create XML data with accounts information and save encrypted file
                XmlSerializer writer = new XmlSerializer(typeof(List<Account>));
                MemoryStream xml = new MemoryStream();
                writer.Serialize(xml, ACCOUNTS);
                File.WriteAllText(PATH + @"\data.al", crypto.Encrypt(xml.ToArray(), PASSWORD));
            }
        }

        private void LoadData()
        {
            // If was data loaded
            if (data != null && data.Length > 0)
            {
                try
                {
                    // Decrypt and load XML accounts information
                    XmlSerializer reader = new XmlSerializer(typeof(List<Account>));
                    List<Account> accs = new List<Account>();
                    ACCOUNTS = (List<Account>)reader.Deserialize(crypto.Decrypt(data, PASSWORD));
                    // Load accounts into lstAccounts
                    lstAccounts.DataSource = ACCOUNTS;
                    SelectAccounts();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Password!");
                }
            }
        }

        public void refreshList(int addEdit = 1)
        {
            // Save account data
            SaveData();

            if (addEdit == 0)
            {
                // Refresh lstAccounts
                SaveSelected();
                lstAccounts.DataSource = null;
                lstAccounts.DataSource = ACCOUNTS;
                SelectAccounts();
            }
            else
            {
                int l = lstAccounts.SelectedIndex;
                // Refresh lstAccounts
                lstAccounts.DataSource = null;
                lstAccounts.DataSource = ACCOUNTS;
                if (lstAccounts.Items.Count-1 >= l)
                {
                    lstAccounts.SelectedIndex = l;
                }
            }
        }

        private void SelectAccounts()
        {
            if (SETTINGS.Selected.Length > 0)
            {
                // Select previously selected accounts
                for (int i = 0; i < SETTINGS.Selected.Length; i++)
                {
                    lstAccounts.SetSelected(SETTINGS.Selected[i], true);
                }
                // 0 is selected by default, so unselect it if needed
                if (SETTINGS.Selected[0] != 0)
                {
                    lstAccounts.SetSelected(0, false);
                }
            }
        }

        private void SetWTFAndStart(Process process, Account account)
        {
            // Get options for account
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

            // Sleep to give harddrive time to save file
            Thread.Sleep(250);
            process.Start();
            // Sleep long enough for Wow to read file
            Thread.Sleep(400);
        }

        private void Login(Process p, Account a)
        {
            // Run this in a new thread so AutoLogin is not frozen
            new Thread(() =>
            {
                try
                {
                    // Run in background
                    Thread.CurrentThread.IsBackground = true;

                    // Set keycodes
                    uint WM_KEYDOWN = 0x0100;
                    uint WM_KEYUP = 0x0101;
                    uint WM_CHAR = 0x0102;
                    int VK_RETURN = 0x0D;

                    // Set local account/password, otherwise it uses the account/password that changes each time Login() is called
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
                        PostMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(account.Password[i]), IntPtr.Zero);
                        Thread.Sleep(30);
                    }
                    // Hit enter to log in
                    PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);

                    // Wait 15 seconds and press Enter
                    if (account.EnterWorld)
                    {
                        Thread.Sleep(15000);
                        PostMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_RETURN), IntPtr.Zero);
                        PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);
                    }

                    Thread.CurrentThread.Abort();
                }
                catch
                {
                    // Crash probably due to process closing
                    Thread.CurrentThread.Abort();
                }
            }).Start();
        }

        private void LoadLaunchAccounts()
        {
            // Clear the Lauch of old accounts incase they've changed
            tskMenuLaunch.DropDownItems.Clear();
            if (ACCOUNTS.Count > 0)
            {
                // Create menu item
                ToolStripMenuItem menuItem;
                // Setup and add menu item for each account
                foreach (Account account in ACCOUNTS)
                {
                    menuItem = new ToolStripMenuItem();
                    menuItem.Text = account.Name;
                    menuItem.Click += new EventHandler(MinimizedLaunch);
                    tskMenuLaunch.DropDownItems.Add(menuItem);
                }
            }
        }

        private void MinimizedLaunch(object sender, EventArgs e)
        {
            Account account = ACCOUNTS.Find(a => a.Name == sender.ToString());
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        // Imports user32.dll so I bring the wow client to foreground
        [DllImport("user32.dll")]
        private static extern
            bool SetForegroundWindow(IntPtr hWnd);

        string data;
        Account ActiveAccount;
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
                        FileName = SETTINGS.WowPath + (ActiveAccount.Client == "32bit" ? @"\Wow.exe" : @"\Wow-64.exe"),
                        Arguments = (ActiveAccount.Client == "32bit" ? @"-noautolaunch64bit" : "")
                    }
                };
                if (ActiveAccount.Client == "32bit")
                {
                    process.Start();
                }
                else
                {
                    process.Start();
                }
                process.WaitForInputIdle();
                Login(process);
            }
        }

        private void btnLaunchAll_Click(object sender, EventArgs e)
        {
            if (ACCOUNTS.Count > 0)
            {
                List<AllLogin> loginAccounts = new List<AllLogin>();
                foreach (Account account in ACCOUNTS)
                {
                    AllLogin AL = new AllLogin();
                    AL.AllAccount = account;
                    AL.AllProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = SETTINGS.WowPath + (account.Client == "32bit" ? @"\Wow.exe" : @"\Wow-64.exe"),
                            Arguments = (account.Client == "32bit" ? "-noautolaunch64bit" : "")
                        }
                    };
                    AL.AllProcess.Start();
                    loginAccounts.Add(AL);
                }
                while (true)
                {
                    for (int i = loginAccounts.Count-1; i >= 0; i--)
                    {
                        if (loginAccounts[i].AllProcess.WaitForInputIdle(15000))
                        {
                            Login(loginAccounts[i].AllProcess, loginAccounts[i].AllAccount);
                            loginAccounts.Remove(loginAccounts[i]);
                        }
                    }
                    if (loginAccounts.Count <= 0)
                    {
                        break;
                    }
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

        private void Login(Process process, Account account = null)
        {
            if (account == null)
            {
                account = ActiveAccount;
            }
            SetForegroundWindow(process.MainWindowHandle);
            Thread.Sleep(300);
            SendKeys.SendWait(account.Email);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(300);
            SendKeys.SendWait(account.Password);
            SendKeys.SendWait("~");
            if (account.Multiple)
            {
                Thread.Sleep(1600);
                for (int i = 0; i < account.SelectedAccount; i++)
                {
                    SendKeys.SendWait("{DOWN}");
                }
                SendKeys.SendWait("~");
            }
        }
    }
}

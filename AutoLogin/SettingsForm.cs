using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AutoLogin
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            txtWowPath.Text = MainForm.SETTINGS.WowPath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtWowPath.Text + @"\Wow.exe"))
            {
                MessageBox.Show("Could not find Wow.exe." + Environment.NewLine + "Please browse to your World of Warcraft folder.");
            }
            else
            {
                MainForm.SETTINGS.WowPath = txtWowPath.Text;
                XmlSerializer writer = new XmlSerializer(typeof(Settings));
                StreamWriter file = new StreamWriter(MainForm.PATH + @"\settings.xml");
                writer.Serialize(file, MainForm.SETTINGS);
                file.Close();
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtWowPath.Text = fbd.SelectedPath;
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            new PasswordForm().ShowDialog(this);
        }
    }
}

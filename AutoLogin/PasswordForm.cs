using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AutoLogin
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            if (!MainForm.SETTINGS.HasPassword)
            {
                txtCurrent.Text = MainForm.PASSWORD;
            }
        }

        public void GetPassword(MainForm mForm)
        {
            label2.Visible = false;
            label3.Visible = false;
            txtNew.Visible = false;
            txtConfirm.Visible = false;
            btnClose.Visible = false;
            btnSave.Visible = false;
            btnSubmit.Visible = true;
            this.AcceptButton = btnSubmit;
            this.Text = "Enter your password";
            this.Height = 110;
            this.ShowDialog(mForm);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCurrent.Text == MainForm.PASSWORD)
            {
                if (txtNew.Text == txtConfirm.Text)
                {
                    if (txtCurrent.Text == txtNew.Text)
                    {
                        MessageBox.Show("New password cannot be \nthe same as the old one!");
                    }
                    else
                    {
                        XmlSerializer writer = new XmlSerializer(typeof(Settings));
                        StreamWriter file = new StreamWriter(MainForm.PATH + @"\settings.xml");
                        writer.Serialize(file, MainForm.SETTINGS);
                        file.Close();
                        MainForm.PASSWORD = txtNew.Text;
                        MainForm.SETTINGS.HasPassword = true;
                        MessageBox.Show("Password changed successfully!");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match!");
                }
            }
            else
            {
                MessageBox.Show("Current password incorrect!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            MainForm.PASSWORD = txtCurrent.Text;
            this.Close();
        }
    }
}

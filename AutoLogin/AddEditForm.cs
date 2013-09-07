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

namespace AutoLogin
{
    public partial class AddEditForm : Form
    {
        Account account;
        MainForm mForm;

        public AddEditForm()
        {
            InitializeComponent();
        }

        public void ShowMe(MainForm parent, int addEdit, Account account = null)
        {
            if (addEdit == 0)
            {
                this.Text = "New Account";
            }
            else
            {
                this.Text = "Edit Account";
            }
            this.account = account;
            mForm = parent;
            this.ShowDialog(parent);
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            if (account != null)
            {
                txtName.Text = account.Name;
                txtEmail.Text = account.Email;
                txtPassword.Text = account.Password;
                chkMultiple.Checked = account.Multiple;
                if (account.Multiple)
                {
                    if (account.NumberAccounts > 2)
                    {
                        for (int i = 2; i < account.NumberAccounts; ++i)
                        {
                            numAccounts.Value += 1;
                        }
                    }
                    lstAccounts.SelectedIndex = account.SelectedAccount;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (account == null)
            {
                account = new Account();
                account.Client = "32bit";
                MainForm.ACCOUNTS.Add(account);
            }
            account.Name = txtName.Text;
            account.Email = txtEmail.Text;
            account.Password = txtPassword.Text;
            account.Multiple = chkMultiple.Checked;
            account.NumberAccounts = chkMultiple.Checked ? (int)numAccounts.Value : 0;
            account.SelectedAccount = chkMultiple.Checked ? lstAccounts.SelectedIndex : 0;
            mForm.refreshList();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkMultiple_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMultiple.Checked)
            {
                this.Width = 415;
                lstAccounts.SelectedIndex = 0;
            }
            else
            {
                this.Width = 250;
            }
        }

        private void numAccounts_ValueChanged(object sender, EventArgs e)
        {
            if (numAccounts.Value > lstAccounts.Items.Count)
            {
                lstAccounts.Items.Add("Account " + numAccounts.Value);
            }
            else
            {
                lstAccounts.Items.RemoveAt(lstAccounts.Items.Count - 1);
            }
        }
    }
}

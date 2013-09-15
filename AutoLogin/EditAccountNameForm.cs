using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLogin
{
    public partial class EditAccountNameForm : Form
    {
        AddEditForm form;
        int index;

        public EditAccountNameForm()
        {
            InitializeComponent();
        }

        public void ShowMe(AddEditForm form, int index)
        {
            this.form = form;
            this.index = index;
            txtAccountName.Text = form.lstAccounts.SelectedItem.ToString();
            this.ShowDialog(form);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            form.lstAccounts.Items[index] = txtAccountName.Text;
            this.Close();
        }
    }
}

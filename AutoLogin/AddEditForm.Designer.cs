namespace AutoLogin
{
    partial class AddEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditForm));
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkMultiple = new System.Windows.Forms.CheckBox();
            this.numAccounts = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkWindowed = new System.Windows.Forms.CheckBox();
            this.drpResolution = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.drpRealm = new System.Windows.Forms.ComboBox();
            this.chkRealm = new System.Windows.Forms.CheckBox();
            this.chkCharacter = new System.Windows.Forms.CheckBox();
            this.lstCharacter = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnterWorld = new System.Windows.Forms.CheckBox();
            this.lstAccounts = new System.Windows.Forms.ListBox();
            this.chkLowDetail = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numAccounts)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(73, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 20);
            this.txtName.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(73, 38);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 20);
            this.txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(73, 64);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(150, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Password";
            // 
            // chkMultiple
            // 
            this.chkMultiple.AutoSize = true;
            this.chkMultiple.Location = new System.Drawing.Point(15, 93);
            this.chkMultiple.Name = "chkMultiple";
            this.chkMultiple.Size = new System.Drawing.Size(170, 17);
            this.chkMultiple.TabIndex = 3;
            this.chkMultiple.Text = "Multiple accounts on this email";
            this.chkMultiple.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMultiple.UseVisualStyleBackColor = true;
            this.chkMultiple.CheckedChanged += new System.EventHandler(this.chkMultiple_CheckedChanged);
            // 
            // numAccounts
            // 
            this.numAccounts.Enabled = false;
            this.numAccounts.Location = new System.Drawing.Point(183, 90);
            this.numAccounts.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numAccounts.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numAccounts.Name = "numAccounts";
            this.numAccounts.ReadOnly = true;
            this.numAccounts.Size = new System.Drawing.Size(40, 20);
            this.numAccounts.TabIndex = 4;
            this.numAccounts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numAccounts.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numAccounts.ValueChanged += new System.EventHandler(this.numAccounts_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 91);
            this.label5.TabIndex = 16;
            this.label5.Text = "Select position \r\nof account\r\n\r\nDouble click\r\nto rename\r\n\r\nCase Sensitive";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkWindowed
            // 
            this.chkWindowed.AutoSize = true;
            this.chkWindowed.Location = new System.Drawing.Point(238, 14);
            this.chkWindowed.Name = "chkWindowed";
            this.chkWindowed.Size = new System.Drawing.Size(77, 17);
            this.chkWindowed.TabIndex = 6;
            this.chkWindowed.Text = "Windowed";
            this.chkWindowed.UseVisualStyleBackColor = true;
            this.chkWindowed.CheckedChanged += new System.EventHandler(this.chkWindowed_CheckedChanged);
            // 
            // drpResolution
            // 
            this.drpResolution.DropDownHeight = 150;
            this.drpResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpResolution.DropDownWidth = 100;
            this.drpResolution.Enabled = false;
            this.drpResolution.FormattingEnabled = true;
            this.drpResolution.IntegralHeight = false;
            this.drpResolution.Location = new System.Drawing.Point(316, 12);
            this.drpResolution.Name = "drpResolution";
            this.drpResolution.Size = new System.Drawing.Size(68, 21);
            this.drpResolution.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 34);
            this.panel1.TabIndex = 18;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(285, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(175, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // drpRealm
            // 
            this.drpRealm.DropDownHeight = 150;
            this.drpRealm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpRealm.Enabled = false;
            this.drpRealm.FormattingEnabled = true;
            this.drpRealm.IntegralHeight = false;
            this.drpRealm.Location = new System.Drawing.Point(316, 39);
            this.drpRealm.Name = "drpRealm";
            this.drpRealm.Size = new System.Drawing.Size(144, 21);
            this.drpRealm.TabIndex = 9;
            // 
            // chkRealm
            // 
            this.chkRealm.AutoSize = true;
            this.chkRealm.Location = new System.Drawing.Point(238, 41);
            this.chkRealm.Name = "chkRealm";
            this.chkRealm.Size = new System.Drawing.Size(75, 17);
            this.chkRealm.TabIndex = 8;
            this.chkRealm.Text = "Set Realm";
            this.chkRealm.UseVisualStyleBackColor = true;
            this.chkRealm.CheckedChanged += new System.EventHandler(this.chkRealm_CheckedChanged);
            // 
            // chkCharacter
            // 
            this.chkCharacter.AutoSize = true;
            this.chkCharacter.Enabled = false;
            this.chkCharacter.Location = new System.Drawing.Point(238, 66);
            this.chkCharacter.Name = "chkCharacter";
            this.chkCharacter.Size = new System.Drawing.Size(91, 17);
            this.chkCharacter.TabIndex = 10;
            this.chkCharacter.Text = "Set Character";
            this.chkCharacter.UseVisualStyleBackColor = true;
            this.chkCharacter.CheckedChanged += new System.EventHandler(this.chkCharacter_CheckedChanged);
            // 
            // lstCharacter
            // 
            this.lstCharacter.Enabled = false;
            this.lstCharacter.FormattingEnabled = true;
            this.lstCharacter.Items.AddRange(new object[] {
            "Slot 1",
            "Slot 2",
            "Slot 3",
            "Slot 4",
            "Slot 5",
            "Slot 6",
            "Slot 7",
            "Slot 8",
            "Slot 9",
            "Slot 10",
            "Slot 11"});
            this.lstCharacter.Location = new System.Drawing.Point(336, 67);
            this.lstCharacter.Name = "lstCharacter";
            this.lstCharacter.Size = new System.Drawing.Size(124, 147);
            this.lstCharacter.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(250, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 65);
            this.label4.TabIndex = 17;
            this.label4.Text = "Select position \r\nof character\r\n\r\nCharacter Name\r\nis not important\r\n";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkEnterWorld
            // 
            this.chkEnterWorld.AutoSize = true;
            this.chkEnterWorld.Enabled = false;
            this.chkEnterWorld.Location = new System.Drawing.Point(238, 194);
            this.chkEnterWorld.Name = "chkEnterWorld";
            this.chkEnterWorld.Size = new System.Drawing.Size(82, 17);
            this.chkEnterWorld.TabIndex = 12;
            this.chkEnterWorld.Text = "Enter World";
            this.chkEnterWorld.UseVisualStyleBackColor = true;
            // 
            // lstAccounts
            // 
            this.lstAccounts.Enabled = false;
            this.lstAccounts.FormattingEnabled = true;
            this.lstAccounts.Items.AddRange(new object[] {
            "WoW1",
            "WoW1"});
            this.lstAccounts.Location = new System.Drawing.Point(118, 116);
            this.lstAccounts.Name = "lstAccounts";
            this.lstAccounts.Size = new System.Drawing.Size(105, 95);
            this.lstAccounts.TabIndex = 19;
            this.lstAccounts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAccounts_MouseDoubleClick);
            // 
            // chkLowDetail
            // 
            this.chkLowDetail.AutoSize = true;
            this.chkLowDetail.Enabled = false;
            this.chkLowDetail.Location = new System.Drawing.Point(391, 14);
            this.chkLowDetail.Name = "chkLowDetail";
            this.chkLowDetail.Size = new System.Drawing.Size(76, 17);
            this.chkLowDetail.TabIndex = 20;
            this.chkLowDetail.Text = "Low Detail";
            this.chkLowDetail.UseVisualStyleBackColor = true;
            // 
            // AddEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 251);
            this.ControlBox = false;
            this.Controls.Add(this.chkLowDetail);
            this.Controls.Add(this.lstAccounts);
            this.Controls.Add(this.chkEnterWorld);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstCharacter);
            this.Controls.Add(this.chkCharacter);
            this.Controls.Add(this.drpRealm);
            this.Controls.Add(this.chkRealm);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.drpResolution);
            this.Controls.Add(this.chkWindowed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numAccounts);
            this.Controls.Add(this.chkMultiple);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddEditForm";
            this.Load += new System.EventHandler(this.AddEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numAccounts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkMultiple;
        private System.Windows.Forms.NumericUpDown numAccounts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkWindowed;
        private System.Windows.Forms.ComboBox drpResolution;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox drpRealm;
        private System.Windows.Forms.CheckBox chkRealm;
        private System.Windows.Forms.CheckBox chkCharacter;
        private System.Windows.Forms.ListBox lstCharacter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkEnterWorld;
        public System.Windows.Forms.ListBox lstAccounts;
        private System.Windows.Forms.CheckBox chkLowDetail;
    }
}
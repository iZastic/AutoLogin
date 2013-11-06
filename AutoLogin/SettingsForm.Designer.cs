namespace AutoLogin
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPassword = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWowPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.chkMinimize = new System.Windows.Forms.CheckBox();
            this.chkHide = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(12, 83);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(115, 83);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPassword
            // 
            this.btnPassword.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPassword.Location = new System.Drawing.Point(218, 83);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(164, 23);
            this.btnPassword.TabIndex = 8;
            this.btnPassword.Text = "Change Password";
            this.btnPassword.UseVisualStyleBackColor = true;
            this.btnPassword.Click += new System.EventHandler(this.btnPassword_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "WoW Path";
            // 
            // txtWowPath
            // 
            this.txtWowPath.Location = new System.Drawing.Point(78, 12);
            this.txtWowPath.Name = "txtWowPath";
            this.txtWowPath.Size = new System.Drawing.Size(247, 20);
            this.txtWowPath.TabIndex = 10;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(331, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(51, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkUpdate
            // 
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpdate.Location = new System.Drawing.Point(121, 38);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(151, 17);
            this.chkUpdate.TabIndex = 12;
            this.chkUpdate.Text = "Check for updates on start";
            this.chkUpdate.UseVisualStyleBackColor = true;
            // 
            // chkMinimize
            // 
            this.chkMinimize.AutoSize = true;
            this.chkMinimize.Location = new System.Drawing.Point(65, 60);
            this.chkMinimize.Name = "chkMinimize";
            this.chkMinimize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkMinimize.Size = new System.Drawing.Size(116, 17);
            this.chkMinimize.TabIndex = 13;
            this.chkMinimize.Text = "Minimize on launch";
            this.chkMinimize.UseVisualStyleBackColor = true;
            // 
            // chkHide
            // 
            this.chkHide.AutoSize = true;
            this.chkHide.Location = new System.Drawing.Point(206, 60);
            this.chkHide.Name = "chkHide";
            this.chkHide.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkHide.Size = new System.Drawing.Size(125, 17);
            this.chkHide.TabIndex = 14;
            this.chkHide.Text = "Hide when minimized";
            this.chkHide.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 115);
            this.ControlBox = false;
            this.Controls.Add(this.chkHide);
            this.Controls.Add(this.chkMinimize);
            this.Controls.Add(this.chkUpdate);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtWowPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPassword);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWowPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.CheckBox chkMinimize;
        private System.Windows.Forms.CheckBox chkHide;
    }
}
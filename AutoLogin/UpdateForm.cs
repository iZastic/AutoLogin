using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoLogin
{
    public partial class UpdateForm : Form
    {
        Version version;
        WebClient webClient;
        string fileName;

        public UpdateForm()
        {
            InitializeComponent();
        }

        public void ShowMe(MainForm parent, Version version)
        {
            this.version = version;
            this.ShowDialog(parent);
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            fileName = "AutoLogin_v" + version + ".exe";

            using (var webClient = new System.Net.WebClient())
            {
                webClient.DownloadFile("http://izastic.twomini.com/autologin/changelog", MainForm.PATH + @"\changelog");
            }

            string[] changelog = File.ReadAllLines(MainForm.PATH + @"\changelog");
            foreach (string line in changelog)
            {
                txtChangeLog.Text += line + Environment.NewLine;
            }
            File.Delete(MainForm.PATH + @"\changelog");
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                // Update the progressbar percentage only when the value is not the same (to avoid updating the control constantly)
                if (prgPercent.Value != e.ProgressPercentage)
                {
                    prgPercent.Value = e.ProgressPercentage;
                }

                // Show the percentage on our label (update only if the value isn't the same to avoid updating the control constantly)
                if (lblPercent.Text != e.ProgressPercentage.ToString() + "%")
                {
                    lblPercent.Text = e.ProgressPercentage.ToString() + "%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // Delete the incomplete file if the download is canceled
                File.Delete(MainForm.PATH + @"\" + fileName);
                MessageBox.Show("Canceled");
            }
            else
            {
                MessageBox.Show("Download completed!");
                File.AppendAllText("update.cmd", @"
                    @echo off
                    ping -n 2 127.0.0.1 > NUL
                    del AutoLogin.exe
                    rename " + fileName + @" AutoLogin.exe
                    start AutoLogin.exe
                    del update.cmd
                    exit");
                Process.Start(MainForm.PATH + @"\" + "update.cmd");
                Application.ExitThread();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (webClient != null)
            {
                webClient.CancelAsync();
                webClient = null;
            }
            else
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            webClient = new WebClient();
            // Create the event handlers we need
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            // Start downloading the file
            webClient.DownloadFileAsync(new Uri("http://izastic.twomini.com/autologin/" + fileName), MainForm.PATH + @"\" + fileName);
        }
    }
}

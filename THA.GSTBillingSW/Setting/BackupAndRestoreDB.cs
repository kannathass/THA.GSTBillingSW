using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Setting
{
    public partial class BackupAndRestoreDB : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        public BackupAndRestoreDB()
        {
            InitializeComponent();
        }

        private void buttonBackupToLocation_Click(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog folderDialog = new FolderBrowserDialog();

                folderDialog.Description = "Select a location to save database";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxBackupToLocation.Text = folderDialog.SelectedPath;
                    buttonBackup.Enabled = true;
                }

                folderDialog = null;

            }

            catch (System.ArgumentException ae)
            {
                BAL.LogHelper.WriteDebugLog(ae.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxBackupToLocation.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please provide a location to save database", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    cmd = new SqlCommand("BACKUP DATABASE [" + con.Database + "] TO DISK='" + textBoxBackupToLocation.Text + "\\THAgstBilling-" + DateTime.Now.ToString("dd/MM/yyyy--HH-mm-ss") + ".bak'", con);

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    buttonBackup.Enabled = false;
                    textBoxBackupToLocation.Clear();
                    MessageBox.Show("Database backup completed successfully", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRestoreFromLocation_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog fileDialog = new OpenFileDialog();

                fileDialog.Title = "Database Restore";
                fileDialog.Filter = "THA billing backup|THAgstBilling*.bak";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxRestoreFromLocation.Text = fileDialog.FileName;
                    buttonRestore.Enabled = true;
                }

                fileDialog = null;

            }

            catch (System.ArgumentException ae)
            {
                BAL.LogHelper.WriteDebugLog(ae.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxRestoreFromLocation.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please provide database location to restore", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string dbName = con.Database;

                    cmd = new SqlCommand("ALTER DATABASE [" + dbName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", con);

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new SqlCommand("USE MASTER RESTORE DATABASE [" + dbName + "] FROM DISK='" + textBoxRestoreFromLocation.Text + "' WITH REPLACE", con);

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new SqlCommand("ALTER DATABASE [" + dbName + "] SET MULTI_USER", con);

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    buttonRestore.Enabled = false;
                    textBoxRestoreFromLocation.Clear();
                    MessageBox.Show("Database restoration completed successfully", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {
            //Updates "THAgstBilling" objects if any
            string updateStatus = BAL.DeployDB.RunSqlScript("\\Scripts\\scriptUpdates.sql");
            if (updateStatus == "Completed")
                MessageBox.Show("Update Completed.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (updateStatus == "CompletedWithError")
                MessageBox.Show("Update Completed with Error.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Update failed. Please contact Deployment@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}

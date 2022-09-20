using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Imports
{
    public partial class ImportExcel : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;

        public ImportExcel()
        {
            InitializeComponent();
            comboBoxImportFor.SelectedIndex = 0;
        }

        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            if (comboBoxImportFor.SelectedIndex > 0)
            {
                openFileDialog1.Filter = "Excel Files(*.xls;*.xlsx;)|*.xls;*.xlsx;";
                openFileDialog1.ShowDialog();
            }
            else
                MessageBox.Show("Please Select an Import Category", "Import Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public DataTable GetDataTableFromExcel(string path, bool hasHeader)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage(new FileInfo(path)))
            {
                //using (var stream = File.OpenRead(path))
                //{
                //    pck.Load(stream);
                //}
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            try
            {
                DataTable dt = GetDataTableFromExcel(filePath, radioButtonYes.Checked);

                //Populate DataGridView.
                dataGridViewList.DataSource = dt;
            }
            catch (InvalidOperationException ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            buttonImport.Enabled = true;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (comboBoxImportFor.Text == "State Master")
            {
                saveStateList();
            }
            else if (comboBoxImportFor.Text == "Item List")
            {
                saveItemList();
            }
            else if (comboBoxImportFor.Text == "Unit List")
            {
                saveUnitList();
            }
        }

        private void clearData()
        {
            comboBoxImportFor.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveUnitList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
                    {
                        cmd = new SqlCommand(@"INSERT INTO [dbo].[MasterUoM]
           ([Code]
           ,[UoM]
           ,[UoMStatus])
     VALUES
           (@Code
           ,@UoM
           ,@UoMStatus)", con);

                        cmd.Parameters.AddWithValue("@Code", Convert.ToString(dataGridViewList[0, i].Value));
                        cmd.Parameters.AddWithValue("@UoM", Convert.ToString(dataGridViewList[1, i].Value));
                        cmd.Parameters.AddWithValue("@UoMStatus", Convert.ToString(dataGridViewList[2, i].Value));

                        using (cmd)
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Unit Master Import completed successfully", "Import Unit List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearData();
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveItemList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
                    {
                        cmd = new SqlCommand(@"INSERT INTO [MasterState]([Code],[Name],StateStatus) VALUES(@Code, @Name, @StateStatus)", con);

                        cmd.Parameters.AddWithValue("@Code", Convert.ToString(dataGridViewList[0, i].Value));
                        cmd.Parameters.AddWithValue("@Name", Convert.ToString(dataGridViewList[1, i].Value));
                        cmd.Parameters.AddWithValue("@StateStatus", Convert.ToString(dataGridViewList[2, i].Value));

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Item List Import completed successfully", "Import Item List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearData();
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveStateList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
                    {
                        cmd = new SqlCommand(@"INSERT INTO [MasterState]([Code],[Name],StateStatus) VALUES(@Code, @Name, @StateStatus)", con);

                        cmd.Parameters.AddWithValue("@Code", Convert.ToString(dataGridViewList[0, i].Value));
                        cmd.Parameters.AddWithValue("@Name", Convert.ToString(dataGridViewList[1, i].Value));
                        cmd.Parameters.AddWithValue("@StateStatus", Convert.ToString(dataGridViewList[2, i].Value));

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("State Master Import completed successfully", "Import State List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearData();
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabelDownloadTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strBackupLocation = browseDownloadLocation();
            if (strBackupLocation != null)
            {
                if (comboBoxImportFor.Text == "State Master")
                {
                    downloadTemplate(strBackupLocation, "StateList.xlsx", @Application.StartupPath + @"\Imports\ExcelTemplates\", "StateList.xlsx");
                }
                else if (comboBoxImportFor.Text == "Item List")
                {
                    downloadTemplate(strBackupLocation, "ItemList.xlsx", @Application.StartupPath + @"\Imports\ExcelTemplates\", "ItemList.xlsx");
                }
                else if (comboBoxImportFor.Text == "Unit List")
                {
                    downloadTemplate(strBackupLocation, "UoM.xlsx", @Application.StartupPath + @"\Imports\ExcelTemplates\", "UoM.xlsx");
                }
            }
        }

        private void downloadTemplate(string destinationPath, string destinationFileName, string sourcePath, string sourceFileName)
        {
            //if (File.Exists(Application.StartupPath + @"\Imports\ExcelTemplates\StateList.xlsx"))
            //{
            //    sourcePath = @Application.StartupPath + @"\Imports\ExcelTemplates\StateList.xlsx";
            //}

            try
            {
                if (!File.Exists(string.Concat(sourcePath, sourceFileName)))
                {
                    return;
                }
                //string destinationPath = @"G:\ProjectBO\ForFutureAnalysis";
                //string sourceFileName = "startingStock.xml";
                //string destinationFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"; // Don't mind this. I did this because I needed to name the copied files with respect to time.
                string sourceFile = System.IO.Path.Combine(sourcePath, sourceFileName);
                string destinationFile = System.IO.Path.Combine(destinationPath, destinationFileName);

                if (!System.IO.Directory.Exists(destinationPath))
                {
                    System.IO.Directory.CreateDirectory(destinationPath);
                }
                System.IO.File.Copy(sourceFile, destinationFile, true);

                MessageBox.Show("StateList.xlsx downloaded successfully", "Import Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string browseDownloadLocation()
        {
            string strBackupLocation = string.Empty;
            try
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();

                folderDialog.Description = "Select a location to save template";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    strBackupLocation = folderDialog.SelectedPath;
                }

                folderDialog = null;
            }

            catch (System.ArgumentException ae)
            {
                BAL.LogHelper.WriteDebugLog(ae.Message.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.Message.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return strBackupLocation;
        }

        private void CreateStockEntry(Int16 stockID, decimal stockInHand, int CompanyID)
        {
            if (stockInHand <= 0)
                return;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Trans_MasterStockList"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyID", CompanyID);
                    cmd.Parameters.AddWithValue("@StockID", stockID);
                    cmd.Parameters.AddWithValue("@StockInHand", stockInHand);
                    cmd.Parameters.AddWithValue("@mode", "InsertStockItem");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void comboBoxImportFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewList.DataSource = null;
            buttonImport.Enabled = false;
        }
    }
}

using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Export
{
    public partial class ITReturn : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        BAL.GSTReturn gstReturn = new BAL.GSTReturn();
        Int16 CompanyId;
        string TargetFileNameWithPath;
        DateTime FromDate;
        DateTime ToDate;
        PopupControl.ProgressPopup alert;
        public ITReturn()
        {
            InitializeComponent();
            loadCompany();
            //loadFinancialYears();
            //dateTimePickerYear.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            dateTimePickerYear.Value = new DateTime(DateTime.Now.Year, DateTime.Today.AddMonths(-1).Month, 1);

            //setFromToDateTime();
            comboBoxFileFormat.SelectedIndex = 0;
            listBoxReturnType.SelectedIndex = 0;
        }

        private void loadFinancialYears()
        {
            comboBoxFinancialYear.DataSource = BAL.GenericFucntions.GetFinancialYears();
            comboBoxFinancialYear.SelectedIndex = 0;
        }

        private void setFromToDateTime()
        {
            //dateTimePickerMonth.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimePickerFromDate.Value = dateTimePickerMonth.Value;
            dateTimePickerToDate.Value = dateTimePickerMonth.Value.AddMonths(1).AddDays(-1);
        }

        private void backgroundWorkerTHA_DoWork(object sender, DoWorkEventArgs e)
        {
            // This is the Second Release of the Article:
            // http://www.codeproject.com/Articles/680421/Create-Read-Edit-Advance-Excel-2007-2010-Report-in

            // template file
            FileInfo templateFile = new FileInfo(@Application.StartupPath + @"\Export\ExcelTemplates\GSTR1_Excel_Workbook_Template-V1.3.xlsx");
            // Making a new file based on template
            FileInfo newFile = new FileInfo(TargetFileNameWithPath);

            // If there is any file having same name as newFile, then delete it first
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(TargetFileNameWithPath);
            }

            using (DataSet ds = gstReturn.BindDataSetGSTR1(FromDate, ToDate, CompanyId, ConfigurationManager.AppSettings["SalesInvoiceWiseTaxCalc"].ToString()))
            {
                int index = 1;
                int toatalRecordCount = ds.Tables[0].Rows.Count + ds.Tables[1].Rows.Count + ds.Tables[2].Rows.Count + ds.Tables[3].Rows.Count + 10;

                using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
                {
                    // Openning b2b Worksheet of the template file
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["b2b"];

                    int rowID = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //Progress bar updation - Start
                        if (backgroundWorkerTHA.CancellationPending)
                        {
                            return;
                        }
                        backgroundWorkerTHA.ReportProgress(index++ * 100 / toatalRecordCount);
                        //Progress bar updation - End

                        int columnId = rowID + 5;
                        worksheet.Cells["A" + columnId].Value = Convert.ToString(dr["CustomerGSTN"]);
                        worksheet.Cells["B" + columnId].Value = Convert.ToString(dr["InvoiceNumber"]);
                        worksheet.Cells["C" + columnId].Value = Convert.ToDateTime(dr["InvoiceDate"]);
                        worksheet.Cells["D" + columnId].Value = Convert.ToDecimal(dr["InvoiceTotalAmount"]);
                        worksheet.Cells["E" + columnId].Value = Convert.ToString(dr["CustomerState"]);
                        worksheet.Cells["F" + columnId].Value = "N";
                        worksheet.Cells["G" + columnId].Value = "Regular";
                        worksheet.Cells["H" + columnId].Value = "";
                        worksheet.Cells["I" + columnId].Value = Convert.ToDecimal(dr["TaxRate"]);
                        worksheet.Cells["J" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
                        worksheet.Cells["K" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
                        rowID++;
                    }
                    worksheet = package.Workbook.Worksheets["b2cl"];

                    rowID = 0;
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        //Progress bar updation - Start
                        if (backgroundWorkerTHA.CancellationPending)
                        {
                            return;
                        }
                        backgroundWorkerTHA.ReportProgress(index++ * 100 / toatalRecordCount);
                        //Progress bar updation - End

                        int columnId = rowID + 5;
                        worksheet.Cells["A" + columnId].Value = Convert.ToString(dr["InvoiceNumber"]);
                        worksheet.Cells["B" + columnId].Value = Convert.ToDateTime(dr["InvoiceDate"]);
                        worksheet.Cells["C" + columnId].Value = Convert.ToDecimal(dr["InvoiceTotalAmount"]);
                        worksheet.Cells["D" + columnId].Value = Convert.ToString(dr["CustomerState"]);
                        worksheet.Cells["E" + columnId].Value = Convert.ToDecimal(dr["TaxRate"]);
                        worksheet.Cells["F" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
                        worksheet.Cells["G" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
                        worksheet.Cells["H" + columnId].Value = "";
                        rowID++;
                    }

                    worksheet = package.Workbook.Worksheets["b2cs"];

                    rowID = 0;
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        //Progress bar updation - Start
                        if (backgroundWorkerTHA.CancellationPending)
                        {
                            return;
                        }
                        backgroundWorkerTHA.ReportProgress(index++ * 100 / toatalRecordCount);
                        //Progress bar updation - End

                        int columnId = rowID + 5;
                        worksheet.Cells["A" + columnId].Value = "OE";
                        worksheet.Cells["B" + columnId].Value = Convert.ToString(dr["CustomerState"]);
                        worksheet.Cells["C" + columnId].Value = Convert.ToDecimal(dr["TaxRate"]);
                        worksheet.Cells["D" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
                        worksheet.Cells["E" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
                        worksheet.Cells["F" + columnId].Value = "";
                        rowID++;
                    }

                    worksheet = package.Workbook.Worksheets["hsn"];

                    rowID = 0;
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        //Progress bar updation - Start
                        if (backgroundWorkerTHA.CancellationPending)
                        {
                            return;
                        }
                        backgroundWorkerTHA.ReportProgress(index++ * 100 / toatalRecordCount);
                        //Progress bar updation - End

                        int columnId = rowID + 5;
                        worksheet.Cells["A" + columnId].Value = Convert.ToString(dr["HsnSac"]);
                        worksheet.Cells["B" + columnId].Value = Convert.ToString(dr["ItemDescription"]);
                        worksheet.Cells["C" + columnId].Value = Convert.ToString(dr["UoM"]);
                        worksheet.Cells["D" + columnId].Value = Convert.ToDecimal(dr["TotalQty"]);
                        worksheet.Cells["E" + columnId].Value = Convert.ToDecimal(dr["TaxableAmount"]);
                        worksheet.Cells["F" + columnId].Value = Convert.ToDecimal(dr["InvoiceTotalAmount"]);
                        worksheet.Cells["G" + columnId].Value = Convert.ToDecimal(dr["TotalIGSTAmount"]);
                        worksheet.Cells["H" + columnId].Value = Convert.ToDecimal(dr["TotalCGSTAmount"]);
                        worksheet.Cells["I" + columnId].Value = Convert.ToDecimal(dr["TotalSGSTAmount"]);
                        worksheet.Cells["J" + columnId].Value = Convert.ToDecimal(dr["CESSAmount"]);
                        rowID++;
                    }

                    // Switch the page layout view back to the normal
                    worksheet.View.PageLayoutView = false;

                    backgroundWorkerTHA.ReportProgress(99);
                    // Save our new workbook, we are done
                    package.Save();
                    backgroundWorkerTHA.ReportProgress(100);
                }
            }
        }

        private void backgroundWorkerTHA_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Pass the progress to AlertForm label and progressbar
            alert.Message = "In progress, please wait... " + e.ProgressPercentage.ToString() + "%";
            alert.ProgressValue = e.ProgressPercentage;
        }

        private void backgroundWorkerTHA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Cancelled == true)
            //{
            //    LabelProcessing.Text = "Canceled!";
            //}
            if (e.Error != null)
            {
                BAL.LogHelper.WriteDebugLog(e.Error.ToString());
                MessageBox.Show("Error: " + e.Error.Message, "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Thread.Sleep(100);
                //Let's open our new Excel file and shut down this application.
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(TargetFileNameWithPath);
                p.Start();
            }
            //Close the AlertForm
            alert.Close();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerTHA.IsBusy)
                return;
            if (dateTimePickerFromDate.Value.Date > dateTimePickerToDate.Value.Date)
            {
                MessageBox.Show("From Date should be less than To Date.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TargetFileNameWithPath = browseDownloadLocation();
            if (TargetFileNameWithPath == string.Empty)
                return;

            CompanyId = Convert.ToInt16(comboBoxCompany.SelectedValue);
            FromDate = dateTimePickerFromDate.Value.Date;
            ToDate = dateTimePickerToDate.Value.Date;

            // create a new instance of the alert form
            alert = new PopupControl.ProgressPopup();
            // event handler for the Cancel button in AlertForm
            alert.Canceled += new EventHandler<EventArgs>(buttonCancelProgressPopup_Click);
            alert.Show();

            // Start the asynchronous operation.
            backgroundWorkerTHA.RunWorkerAsync();
        }

        private void buttonCancelProgressPopup_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerTHA.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorkerTHA.CancelAsync();
                // Close the AlertForm
                alert.Close();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //loadFinancialYears();
            //setFromToDateTime();
            dateTimePickerMonth.Value = new DateTime(DateTime.Now.Year, DateTime.Today.AddMonths(-1).Month, 1);

            dateTimePickerYear.Value = new DateTime(DateTime.Now.Year, DateTime.Today.AddMonths(-1).Month, 1);
            comboBoxFileFormat.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePickerYear_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerMonth.Value = new DateTime(dateTimePickerYear.Value.Date.Year, dateTimePickerYear.Value.Date.Month, 1);
            ////setFromToDateTime();
        }

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            setFromToDateTime();
        }

        private void loadCompany()
        {
            comboBoxCompany.ValueMember = "ID";
            comboBoxCompany.DisplayMember = "CompanyName";
            comboBoxCompany.DataSource = masterSelection.GetCompanyList();

            comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private string browseDownloadLocation()
        {
            string strBackupLocation = string.Empty;
            try
            {
                //FolderBrowserDialog folderDialog = new FolderBrowserDialog();

                //folderDialog.Description = "Select a location to save template";

                //if (folderDialog.ShowDialog() == DialogResult.OK)
                //{
                //    strBackupLocation = folderDialog.SelectedPath;
                //}

                //folderDialog = null;
                // Prompt the user to enter a path/filename to save an example Excel file to

                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.FileName = string.Format("GSTR1_Excel_Workbook_{0}_{1}.xlsx", dateTimePickerYear.Text, dateTimePickerMonth.Text);

                    saveFileDialog1.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.OverwritePrompt = false;
                    //  If the user hit Cancel, then abort!
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        strBackupLocation = saveFileDialog1.FileName;
                }
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

    }
}

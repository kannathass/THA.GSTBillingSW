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

namespace THA.GSTBillingSW.Transaction
{
    public partial class PurchaseInvoiceList : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        Int32 ID = 0;
        //int CompanyId = 0;
        //int CustomerId = 0;
        //string InvoiceNumber = "";
        bool isIGSTApplicable = false;
        string selectedCompanyGSTN = string.Empty;

        public PurchaseInvoiceList()
        {
            InitializeComponent();
            loadCompany();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonNewPurchaseInvoice.Enabled = false;
            }
            dateTimePickerInvoiceFromDate.Value = DateTime.Today.AddDays(-7);
            dateTimePickerInvoiceToDate.Value = DateTime.Today;
            filterInvoices();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void dateTimePickerInvoiceToDate_ValueChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void dateTimePickerInvoiceFromDate_ValueChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void buttonPreviewInvoice_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("PurchaseInvoice", Convert.ToInt16(comboBoxCompany.SelectedValue));
                reportViewer.MdiParent = this.MdiParent;
                //reportViewer.WindowState = FormWindowState.Maximized;
                reportViewer.InvoiceID = ID;
                if (isIGSTApplicable)
                {
                    reportViewer.isIGSTApplicable = true;
                }
                reportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Invoice found!", "Purchase Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxCompany.SelectedIndex != 0)
            //{
            //    buttonConsolidatedReport.Enabled = true;
            //    buttonDetailedReport.Enabled = true;
            //}
            //else
            //{
            //    buttonConsolidatedReport.Enabled = false;
            //    buttonDetailedReport.Enabled = false;
            //}
            //if (comboBoxCompany.SelectedIndex != 0)
            filterInvoices();
            selectedCompanyGSTN = BAL.GenericValidation.GetGSTNAvailability(Convert.ToInt32(comboBoxCompany.SelectedValue));
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                //ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells[0].Value.ToString());
                //setGSTCategory(Convert.ToString(dataGridViewList.Rows[e.RowIndex].Cells[2].Value));

                ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                setGSTCategory(Convert.ToString(dataGridViewList.Rows[e.RowIndex].Cells["Customer Name | State"].Value));
                //comboBoxCompany.SelectedValue = dataGridViewList.Rows[e.RowIndex].Cells["CompanyId"].Value;
                //enablePreviewInvoice();
            }
            else
            {
                ClearData();
            }
        }

        private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                PurchaseInvoice purchaseInvoice = new PurchaseInvoice();
                purchaseInvoice.ID = ID;
                purchaseInvoice.CompanyGSTN = selectedCompanyGSTN;
                purchaseInvoice.MdiParent = this.MdiParent;
                purchaseInvoice.Show();
            }
        }

        private void buttonNewPurchaseInvoice_Click(object sender, EventArgs e)
        {
            //if (comboBoxCompany.SelectedIndex != 0)
            //{
            PurchaseInvoice purchaseInvoice = new PurchaseInvoice();
            //PurchaseInvoice.ID = ID;
            purchaseInvoice.CompanyId = Convert.ToInt16(comboBoxCompany.SelectedValue);
            purchaseInvoice.CompanyGSTN = selectedCompanyGSTN;
            //MdiParent.form mas = new MasterForm();
            purchaseInvoice.MdiParent = this.MdiParent;
            purchaseInvoice.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Please select company!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void ClearData()
        {
            ID = 0;
            //CompanyId = 0;
            //CustomerId = 0;
            //buttonPreviewInvoice.Enabled = false;
            //InvoiceNumber = "";
        }


        //private void loadCompany()
        //{
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        con.Open();

        //        DataTable dt = new DataTable();
        //        adapt = new SqlDataAdapter(" select 0 ID, '-Select-' CompanyName " +
        //            " UNION " +
        //            "select ID, [CompanyName] + ' | ' + [State] CompanyName from MasterCompany Where Del_State = 0", con);

        //        using (adapt)
        //        {
        //            adapt.Fill(dt);

        //            comboBoxCompany.DataSource = dt;
        //            comboBoxCompany.ValueMember = "ID";
        //            comboBoxCompany.DisplayMember = "CompanyName";
        //        }
        //    }
        //    comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;

        //    if (comboBoxCompany.Items.Count == 2)
        //        comboBoxCompany.SelectedIndex = 1;
        //}

        private void loadCompany()
        {
            comboBoxCompany.ValueMember = "ID";
            comboBoxCompany.DisplayMember = "CompanyName";
            comboBoxCompany.DataSource = masterSelection.GetCompanyList();

            comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (comboBoxCompany.Items.Count > 0)
                comboBoxCompany.SelectedIndex = 0;
        }


        private void filterInvoices()
        {
            string filterQuery = string.Empty;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable dt = new DataTable();
                //if (comboBoxCompany.SelectedIndex > 0)
                //{
                //    filterQuery = " SELECT PurchaseInv.ID, InvoiceNumber [Invoice #], CustomerName [Customer Name],InvoiceDate [Invoice Date], " +
                //" InvoiceTotalAmount[Total Amount], InvoiceStatus[Invoice Status], PONumber PONumber " +
                //" FROM TranPurchaseInvoice PurchaseInv inner join MasterCustomer Cust on PurchaseInv.CustomerID = Cust.ID " +
                //" Where PurchaseInv.Del_State = 0 and PurchaseInv.CompanyId=@CompanyId " +
                //" and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue  or PONumber like @FilterValue) " +
                //" and InvoiceDate between @FromDate and @ToDate ";
                filterQuery = " SELECT PurchaseInv.ID, InvoiceNumber [Invoice #], [CustomerName] + ' | ' + [State] [Customer Name | State],InvoiceDate [Invoice Date], " +
            " RoundedOffInvoiceTotalAmount [Total Amount], InvoiceStatus[Invoice Status], PONumber [PO Number], CompanyId " +
            " FROM TranPurchaseInvoice PurchaseInv inner join MasterCustomer Cust on PurchaseInv.CustomerID = Cust.ID " +
            " Where PurchaseInv.Del_State = 0 and PurchaseInv.CompanyId=@CompanyId " +
            " and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue  or PONumber like @FilterValue or State like @FilterValue) " +
            " and InvoiceDate between @FromDate and @ToDate " +
            " order by ID desc ";
                adapt = new SqlDataAdapter(filterQuery, con);
                adapt.SelectCommand.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                //}
                //else
                //{
                //    filterQuery = " SELECT PurchaseInv.ID, InvoiceNumber [Invoice #], [CustomerName] + ' | ' + [State] [Customer Name | State],InvoiceDate [Invoice Date], " +
                //" InvoiceTotalAmount[Total Amount], InvoiceStatus[Invoice Status], PONumber [PO Number], CompanyId " +
                //" FROM TranPurchaseInvoice PurchaseInv inner join MasterCustomer Cust on PurchaseInv.CustomerID = Cust.ID " +
                //" Where PurchaseInv.Del_State = 0 " +
                //" and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue or State like @FilterValue) " +
                //" and InvoiceDate between @FromDate and @ToDate " +
                //" order by ID desc ";
                //    adapt = new SqlDataAdapter(filterQuery, con);
                //}
                adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                adapt.SelectCommand.Parameters.AddWithValue("@FromDate", dateTimePickerInvoiceFromDate.Value.Date);
                adapt.SelectCommand.Parameters.AddWithValue("@ToDate", dateTimePickerInvoiceToDate.Value.Date);
                adapt.Fill(dt);
                using (adapt)
                {
                    dataGridViewList.DataSource = dt;
                }
                dataGridViewList.Columns["ID"].Visible = false;
                dataGridViewList.Columns["CompanyId"].Visible = false;
                ClearData();
            }
        }

        private void setGSTCategory(string customerDetail)
        {
            if (getStateName(comboBoxCompany.Text, '|') == getStateName(customerDetail, '|'))
            {
                isIGSTApplicable = false;
            }
            else
            {
                isIGSTApplicable = true;
            }
        }

        private string getStateName(string str, char separator)
        {
            var splitStr = str.Split(separator);

            if (splitStr.Length == 2) // throw error ?
            {
                return splitStr[1].ToUpper();
            }
            else
            {
                return string.Empty;
            }
        }

        //private void enablePreviewInvoice()
        //{
        //    if (comboBoxCompany.SelectedIndex > 0)
        //    {
        //        buttonPreviewInvoice.Enabled = true;
        //    }
        //}

        private void buttonConsolidatedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("PurchaseInvoiceConsolidated", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonDetailedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("PurchaseInvoiceDetailed", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PurchaseInvoice purchaseInvoice = new PurchaseInvoice();
                purchaseInvoice.ID = ID;
                purchaseInvoice.CompanyGSTN = selectedCompanyGSTN;
                purchaseInvoice.MdiParent = this.MdiParent;
                purchaseInvoice.Show();

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift == true)
            {
                comboBoxCompany.Select();
            }
        }

        private void comboBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.AcceptButton = buttonNewSalesInvoice;
                buttonNewPurchaseInvoice_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dataGridViewList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                setGSTCategory(Convert.ToString(dataGridViewList.Rows[e.RowIndex].Cells["Customer Name | State"].Value));
                //comboBoxCompany.SelectedValue = dataGridViewList.Rows[e.RowIndex].Cells["CompanyId"].Value;
                //enablePreviewInvoice();
            }
            else
            {
                ClearData();
            }
        }
    }
}

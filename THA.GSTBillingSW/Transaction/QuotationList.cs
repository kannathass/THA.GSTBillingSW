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
    public partial class QuotationList : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        Int32 ID = 0;
        bool isIGSTApplicable = false;
        public QuotationList()
        {
            InitializeComponent();
            dateTimePickerInvoiceToDate.Value = DateTime.Today;
            dateTimePickerInvoiceFromDate.Value = DateTime.Today.AddDays(-7);
            loadCompany();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonNewEntry.Enabled = false;
            }

            filterInvoices();
        }

        private void QuotationList_Load(object sender, EventArgs e)
        {

        }

        private void buttonNewEntry_Click(object sender, EventArgs e)
        {
            QuotationEntry quotationEntry = new QuotationEntry();
            quotationEntry.CompanyId = Convert.ToInt16(comboBoxCompany.SelectedValue);
            quotationEntry.MdiParent = this.MdiParent;
            quotationEntry.Show();
        }

        private void buttonConsolidatedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("QuotationConsolidated", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonDetailedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("QuotationDetailed", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("Quotation", Convert.ToInt16(comboBoxCompany.SelectedValue));
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
                MessageBox.Show("No Quotation found!", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            filterInvoices();

        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxCompany.SelectedIndex != -1)
            filterInvoices();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            filterInvoices();

        }

        private void dateTimePickerInvoiceFromDate_ValueChanged(object sender, EventArgs e)
        {

            filterInvoices();

        }

        private void dateTimePickerInvoiceToDate_ValueChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void comboBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.AcceptButton = buttonNewSalesInvoice;
                buttonNewEntry_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                setGSTCategory(Convert.ToString(dataGridViewList.Rows[e.RowIndex].Cells["Customer Name | State"].Value));
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
                QuotationEntry quotationEntry = new QuotationEntry();
                quotationEntry.ID = ID;
                quotationEntry.MdiParent = this.MdiParent;
                quotationEntry.Show();
            }
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QuotationEntry quotationEntry = new QuotationEntry();
                quotationEntry.ID = ID;
                quotationEntry.MdiParent = this.MdiParent;
                quotationEntry.Show();

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift == true)
            {
                comboBoxCompany.Select();
            }
        }

        private void dataGridViewList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                setGSTCategory(Convert.ToString(dataGridViewList.Rows[e.RowIndex].Cells["Customer Name | State"].Value));
            }
            else
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            ID = 0;
        }

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
            //if (Convert.ToInt32(((DataRowView)comboBoxCompany.SelectedValue)["ID"]) >= 0)

            if (Convert.ToInt16(comboBoxCompany.SelectedValue) >= 0)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    filterQuery = @"select * from (SELECT Quotation.ID, QuotationNumber [Quotation #], 
                Cust.CustomerName + ' | ' + [State] [Customer Name | State],QuotationDate [Quotation Date], 
                RoundedOffQuotationTotalAmount [Total Amount], QuotationStatus[Quotation Status], PONumber [PO Number], CompanyId 
                FROM TranQuotation Quotation inner join MasterCustomer Cust on Quotation.CustomerID = Cust.ID 
                Where Quotation.Del_State = 0 and Quotation.CompanyId=@CompanyId 
                and (QuotationNumber like @FilterValue or Cust.CustomerName like @FilterValue  or PONumber like @FilterValue or State like @FilterValue) 
                and QuotationDate between @FromDate and @ToDate 
                and customerId<>0
                    union
                SELECT Quotation.ID, QuotationNumber [Quotation #], 
                Quotation.CustomerName + ' | ' + [CustomerState] [Customer Name | State],QuotationDate [Quotation Date], 
                RoundedOffQuotationTotalAmount [Total Amount], QuotationStatus[Quotation Status], PONumber [PO Number], CompanyId 
                FROM TranQuotation Quotation 
                Where Quotation.Del_State = 0 and Quotation.CompanyId=@CompanyId 
                and (QuotationNumber like @FilterValue or CustomerName like @FilterValue  or PONumber like @FilterValue or CustomerState like @FilterValue) 
                and QuotationDate between @FromDate and @ToDate 
                and customerId=0
                ) tt
                order by ID desc";
                    adapt = new SqlDataAdapter(filterQuery, con);
                    adapt.SelectCommand.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));

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
    }
}

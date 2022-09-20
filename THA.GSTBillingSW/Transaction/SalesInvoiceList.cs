using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace THA.GSTBillingSW.Transaction
{
    public partial class SalesInvoiceList : Form
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
        public SalesInvoiceList()
        {
            InitializeComponent();
            loadCompany();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonNewSalesInvoice.Enabled = false;
            }
            dateTimePickerInvoiceFromDate.Value = DateTime.Today.AddDays(-7);
            dateTimePickerInvoiceToDate.Value = DateTime.Today;
            filterInvoices();

        }

        private void buttonNewSalesInvoice_Click(object sender, EventArgs e)
        {
            //if (comboBoxCompany.SelectedIndex != 0)
            //{
            SalesInvoice salesInvoice = new SalesInvoice();
            //salesInvoice.ID = ID;
            salesInvoice.CompanyId = Convert.ToInt16(comboBoxCompany.SelectedValue);
            salesInvoice.CompanyGSTN = selectedCompanyGSTN;
            //MdiParent.form mas = new MasterForm();
            salesInvoice.MdiParent = this.MdiParent;
            salesInvoice.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Please select a company!", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
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


        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
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

        //private void enablePreviewInvoice()
        //{
        //    if (comboBoxCompany.SelectedIndex > 0)
        //    {
        //        buttonPreviewInvoice.Enabled = true;
        //    }
        //}
        //private void dataGridViewList_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.WhiteSmoke;
        //    dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Green;
        //}

        //private void dataGridViewList_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Green;
        //    dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.WhiteSmoke;
        //}

        private void ClearData()
        {
            ID = 0;
            //CompanyId = 0;
            //CustomerId = 0;
            //buttonPreviewInvoice.Enabled = false;
            //InvoiceNumber = "";
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            filterInvoices();
            //DisplayData();
        }

        //public void RefreshSalesInvoiceGridView()
        //{
        //    filterInvoices();
        //}

        private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SalesInvoice salesInvoice = new SalesInvoice();
                salesInvoice.ID = ID;
                salesInvoice.CompanyGSTN = selectedCompanyGSTN;
                salesInvoice.MdiParent = this.MdiParent;
                salesInvoice.Show();
            }
        }

        //private void loadCompany()
        //{
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        con.Open();

        //        DataTable dt = new DataTable();
        //        adapt = new SqlDataAdapter(@"select 0 ID, '-Select-' CompanyName 
        //             UNION 
        //            select ID, [CompanyName] + ' | ' + [State] CompanyName from MasterCompany Where Del_State = 0", con);

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

        //    //if (comboBoxCompany.Items.Count > 1)
        //    comboBoxCompany.SelectedIndex = 0;
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


        private void textBoxSearch_TextChanged(object sender, EventArgs e)
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

        private void filterInvoices()
        {
            //if (comboBoxCompany.Items.Count == 0)
            //    return;
            string filterQuery = string.Empty;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable dt = new DataTable();

                //filterQuery = " SELECT SalesInv.ID, InvoiceNumber [Invoice #], [CustomerName] + ' | ' + [State] [Customer Name | State],InvoiceDate [Invoice Date], " +
                //" InvoiceTotalAmount[Total Amount], InvoiceStatus[Invoice Status], PONumber [PO Number], CompanyId " +
                //" FROM TranSalesInvoice SalesInv inner join MasterCustomer Cust on SalesInv.CustomerID = Cust.ID " +
                //" Where SalesInv.Del_State = 0 and SalesInv.CompanyId=@CompanyId " +
                //" and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue or State like @FilterValue) " +
                //" and InvoiceDate between @FromDate and @ToDate " +
                //" order by ID desc ";

                filterQuery = @"select * from (SELECT Invoice.ID, InvoiceNumber [Invoice #], 
                Cust.CustomerName + ' | ' + [State] [Customer Name | State],InvoiceDate [Invoice Date], 
                RoundedOffInvoiceTotalAmount [Total Amount], InvoiceStatus[Invoice Status], PONumber [PO Number], CompanyId
                FROM TranSalesInvoice Invoice inner join MasterCustomer Cust on Invoice.CustomerID = Cust.ID 
                Where Invoice.Del_State = 0 and Invoice.CompanyId=@CompanyId 
                and (InvoiceNumber like @FilterValue or Cust.CustomerName like @FilterValue  or PONumber like @FilterValue or State like @FilterValue) 
                and InvoiceDate between @FromDate and @ToDate 
                and customerId<>0
                    union
                SELECT Invoice.ID, InvoiceNumber [Invoice #], 
                Invoice.CustomerName + ' | ' + [CustomerState] [Customer Name | State],InvoiceDate [Invoice Date], 
                RoundedOffInvoiceTotalAmount [Total Amount], InvoiceStatus[Invoice Status], PONumber [PO Number], CompanyId 
                FROM TranSalesInvoice Invoice 
                Where Invoice.Del_State = 0 and Invoice.CompanyId=@CompanyId 
                and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue  or PONumber like @FilterValue or CustomerState like @FilterValue) 
                and InvoiceDate between @FromDate and @ToDate 
                and customerId=0
                ) tt
                order by ID desc";

                adapt = new SqlDataAdapter(filterQuery, con);
                adapt.SelectCommand.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);

                adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                adapt.SelectCommand.Parameters.AddWithValue("@FromDate", dateTimePickerInvoiceFromDate.Value.Date);
                adapt.SelectCommand.Parameters.AddWithValue("@ToDate", dateTimePickerInvoiceToDate.Value.Date);
                adapt.Fill(dt);
                using (adapt)
                {
                    dataGridViewList.DataSource = dt;
                }
                //if (dataGridViewList.Rows.Count > 0)
                //{
                dataGridViewList.Columns["ID"].Visible = false;
                dataGridViewList.Columns["CompanyId"].Visible = false;
                ClearData();
                //}
            }
        }

        private void buttonPreviewInvoice_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("SalesInvoice", Convert.ToInt16(comboBoxCompany.SelectedValue));
                reportViewer.MdiParent = this.MdiParent;
                reportViewer.WindowState = FormWindowState.Maximized;
                reportViewer.InvoiceID = ID;
                if (isIGSTApplicable)
                {
                    reportViewer.isIGSTApplicable = true;
                }
                reportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Invoice found!", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void buttonConsolidatedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("SalesInvoiceConsolidated", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            //reportViewer.WindowState = FormWindowState.Maximized;
            //if (isIGSTApplicable)
            //{
            //    reportViewer.isIGSTApplicable = true;
            //}
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonDetailedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("SalesInvoiceDetailed", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.AcceptButton = buttonPreviewInvoice;
                //buttonPreviewInvoice_Click(sender, e);
                SalesInvoice salesInvoice = new SalesInvoice();
                salesInvoice.ID = ID;
                salesInvoice.CompanyGSTN = selectedCompanyGSTN;
                salesInvoice.MdiParent = this.MdiParent;
                salesInvoice.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift == true)
            {
                comboBoxCompany.Select();
            }
            //else
            //{

            //}
        }

        private void comboBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.AcceptButton = buttonNewSalesInvoice;
                buttonNewSalesInvoice_Click(sender, e);
                e.Handled = true;

            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

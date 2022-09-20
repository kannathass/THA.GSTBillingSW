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
    public partial class PaymentCollectionList : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        Int32 ID = 0;

        public PaymentCollectionList()
        {
            InitializeComponent();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonNewCollection.Enabled = false;
            }
            dateTimePickerFromDate.Value = DateTime.Today.AddDays(-7);
            dateTimePickerToDate.Value = DateTime.Today;
            comboBoxReportType.SelectedIndex = 0;
            loadCompany();
            loadAgent();
            loadCustomerByAgent();
            //filterInvoices();
        }

        private void PaymentCollectionList_Load(object sender, EventArgs e)
        {

        }

        private void buttonNewCollection_Click(object sender, EventArgs e)
        {
            PaymentCollection paymentCollection = new PaymentCollection();
            paymentCollection.CompanyId = Convert.ToInt16(comboBoxCompany.SelectedValue);
            paymentCollection.AgentId = Convert.ToInt32(comboBoxAgent.SelectedValue);
            paymentCollection.CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);
            paymentCollection.MdiParent = this.MdiParent;
            paymentCollection.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            filterInvoices();
        }

        #region Methods

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
            if (comboBoxCompany.SelectedIndex >= 0)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    if (comboBoxCustomer.SelectedIndex > 0)
                    {
                        filterQuery = @"SELECT Pay.ID, ReceiptNumber [Receipt #], [CustomerName] + ' | ' + [State] [Customer Name | State],ReceiptDate [Receipt Date], 
            TotalPayment[Total Payment]
            FROM TranPaymentCollection Pay inner join MasterCustomer Cust on Pay.CustomerID = Cust.ID
            Where Pay.Del_State = 0 and Pay.CompanyId = @CompanyId and Pay.CustomerId = @CustomerId
            and(ReceiptNumber like @FilterValue or CustomerName like @FilterValue or State like @FilterValue)
            and ReceiptDate between @FromDate and @ToDate
            order by ID desc ";
                    }
                    else
                    {
                        filterQuery = @"SELECT Pay.ID, ReceiptNumber [Receipt #], [CustomerName] + ' | ' + [State] [Customer Name | State],ReceiptDate [Receipt Date], 
            TotalPayment[Total Payment]
            FROM TranPaymentCollection Pay inner join MasterCustomer Cust on Pay.CustomerID = Cust.ID
            Where Pay.Del_State = 0 and Pay.CompanyId = @CompanyId
            and(ReceiptNumber like @FilterValue or CustomerName like @FilterValue or State like @FilterValue)
            and ReceiptDate between @FromDate and @ToDate
            order by ID desc ";
                    }
                    adapt = new SqlDataAdapter(filterQuery, con);
                    if (comboBoxCustomer.SelectedIndex >= 0)
                    {
                        adapt.SelectCommand.Parameters.AddWithValue("@CustomerId", comboBoxCustomer.SelectedValue);
                    }
                    adapt.SelectCommand.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);

                    adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                    adapt.SelectCommand.Parameters.AddWithValue("@FromDate", dateTimePickerFromDate.Value.Date);
                    adapt.SelectCommand.Parameters.AddWithValue("@ToDate", dateTimePickerToDate.Value.Date);
                    adapt.Fill(dt);
                    using (adapt)
                    {
                        dataGridViewList.DataSource = dt;
                    }
                    //dataGridViewList.Columns["ID"].Visible = false;
                    ClearData();
                }
            }
        }

        private void ClearData()
        {
            ID = 0;
        }
        #endregion

        private void buttonConsolidatedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("PaymentCollectionConsolidated", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonDetailedReport_Click(object sender, EventArgs e)
        {
            //Report.ReportViewer reportViewer = new Report.ReportViewer("PurchaseInvoiceDetailed", Convert.ToInt16(comboBoxCompany.SelectedValue));
            //reportViewer.MdiParent = this.MdiParent;
            //reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            //reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            //reportViewer.FilterValue = textBoxSearch.Text.Trim();
            //reportViewer.Show();
        }

        private void buttonPreviewInvoice_Click(object sender, EventArgs e)
        {
            //if (ID != 0)
            //{
            //    Report.ReportViewer reportViewer = new Report.ReportViewer("PurchaseInvoice", Convert.ToInt16(comboBoxCompany.SelectedValue));
            //    reportViewer.MdiParent = this.MdiParent;
            //    reportViewer.InvoiceID = ID;
            //    reportViewer.Show();
            //}
            //else
            //{
            //    MessageBox.Show("No Invoice found!", "Purchase Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void comboBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonNewCollection_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells["ID"].Value.ToString());
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
                PaymentCollection paymentCollection = new PaymentCollection();
                paymentCollection.ID = ID;
                paymentCollection.MdiParent = this.MdiParent;
                paymentCollection.Show();
            }
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PaymentCollection paymentCollection = new PaymentCollection();
                paymentCollection.ID = ID;
                paymentCollection.MdiParent = this.MdiParent;
                paymentCollection.Show();

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
            }
            else
            {
                ClearData();
            }
        }

        private void dateTimePickerFromDate_ValueChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void dateTimePickerToDate_ValueChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void loadCustomerByAgent()
        {
            comboBoxCustomer.ValueMember = "ID";
            comboBoxCustomer.DisplayMember = "CustomerName";
            comboBoxCustomer.DataSource = masterSelection.GetCustomerListByAgent(Convert.ToInt32(comboBoxAgent.SelectedValue), "Buyer");

            comboBoxCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void loadAgent()
        {
            comboBoxAgent.ValueMember = "ID";
            comboBoxAgent.DisplayMember = "CustomerName";
            comboBoxAgent.DataSource = masterSelection.GetAgentList("Agent");

            comboBoxAgent.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxAgent.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void comboBoxAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCustomerByAgent();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (comboBoxReportType.SelectedIndex == 0)
            {
                MessageBox.Show("Select a report type", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxReportType.Text == "Partywise Payment")
            {
                Report.ReportViewerPayment reportViewer = new Report.ReportViewerPayment("Partywise Payment", Convert.ToInt16(comboBoxCompany.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxAgent.SelectedValue), textBoxSearch.Text.Trim());
                reportViewer.MdiParent = this.MdiParent;

                reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
                reportViewer.ToDate = dateTimePickerToDate.Value.Date;
                reportViewer.Show();
            }
            //Agentwise
            else if (comboBoxReportType.Text == "Agentwise Payment")
            {
                Report.ReportViewerPayment reportViewer = new Report.ReportViewerPayment("Agentwise Payment", Convert.ToInt16(comboBoxCompany.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxAgent.SelectedValue), textBoxSearch.Text.Trim());
                reportViewer.MdiParent = this.MdiParent;

                reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
                reportViewer.ToDate = dateTimePickerToDate.Value.Date;
                reportViewer.Show();
            }
            else if (comboBoxReportType.Text == "Placewise Payment")
            {
                Report.ReportViewerPayment reportViewer = new Report.ReportViewerPayment("Placewise Payment", Convert.ToInt16(comboBoxCompany.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxAgent.SelectedValue), textBoxSearch.Text.Trim());
                reportViewer.MdiParent = this.MdiParent;

                reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
                reportViewer.ToDate = dateTimePickerToDate.Value.Date;
                reportViewer.Show();
            }
            else if (comboBoxReportType.Text == "Partywise Pending Payment")
            {
                Report.ReportViewerPayment reportViewer = new Report.ReportViewerPayment("Partywise Pending Payment", Convert.ToInt16(comboBoxCompany.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxAgent.SelectedValue), textBoxSearch.Text.Trim());
                reportViewer.MdiParent = this.MdiParent;

                reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
                reportViewer.ToDate = dateTimePickerToDate.Value.Date;
                reportViewer.Show();
            }
            else if (comboBoxReportType.Text == "Agentwise Pending Payment")
            {
                Report.ReportViewerPayment reportViewer = new Report.ReportViewerPayment("Agentwise Pending Payment", Convert.ToInt16(comboBoxCompany.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxAgent.SelectedValue), textBoxSearch.Text.Trim());
                reportViewer.MdiParent = this.MdiParent;

                reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
                reportViewer.ToDate = dateTimePickerToDate.Value.Date;
                reportViewer.Show();
            }
            else if (comboBoxReportType.Text == "Placewise Pending Payment")
            {
                Report.ReportViewerPayment reportViewer = new Report.ReportViewerPayment("Placewise Pending Payment", Convert.ToInt16(comboBoxCompany.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxAgent.SelectedValue), textBoxSearch.Text.Trim());
                reportViewer.MdiParent = this.MdiParent;

                reportViewer.FromDate = dateTimePickerFromDate.Value.Date;
                reportViewer.ToDate = dateTimePickerToDate.Value.Date;
                reportViewer.Show();
            }
        }
    }
}

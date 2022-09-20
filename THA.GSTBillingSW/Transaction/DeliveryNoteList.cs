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
    public partial class DeliveryNoteList : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlDataAdapter adapt;
        Int32 ID = 0;
        bool isIGSTApplicable = false;
        string selectedCompanyGSTN = string.Empty;
        public DeliveryNoteList()
        {
            InitializeComponent();
            loadCompany();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonNewDeliveryNote.Enabled = false;
            }
            dateTimePickerInvoiceFromDate.Value = DateTime.Today.AddDays(-7);
            dateTimePickerInvoiceToDate.Value = DateTime.Today;
            filterInvoices();
        }

        private void buttonNewDeliveryNote_Click(object sender, EventArgs e)
        {
            DeliveryNote deliveryNote = new DeliveryNote();
            //salesInvoice.ID = ID;
            deliveryNote.CompanyId = Convert.ToInt16(comboBoxCompany.SelectedValue);
            //MdiParent.form mas = new MasterForm();
            deliveryNote.CompanyGSTN = selectedCompanyGSTN;
            deliveryNote.MdiParent = this.MdiParent;
            deliveryNote.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void buttonConsolidatedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("DeliveryNoteConsolidated", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonDetailedReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewer reportViewer = new Report.ReportViewer("DeliveryNoteDetailed", Convert.ToInt16(comboBoxCompany.SelectedValue));
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.FromDate = dateTimePickerInvoiceFromDate.Value.Date;
            reportViewer.ToDate = dateTimePickerInvoiceToDate.Value.Date;
            reportViewer.FilterValue = textBoxSearch.Text.Trim();
            reportViewer.Show();
        }

        private void buttonPreviewDeliveryNote_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("DeliveryNote", Convert.ToInt16(comboBoxCompany.SelectedValue));
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
                MessageBox.Show("No Delivery Note found!", "Delivery Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterInvoices();
            selectedCompanyGSTN = BAL.GenericValidation.GetGSTNAvailability(Convert.ToInt32(comboBoxCompany.SelectedValue));
        }

        private void comboBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonNewDeliveryNote_Click(sender, e);
                e.Handled = true;
            }
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
                DeliveryNote deliveryNote = new DeliveryNote();
                deliveryNote.ID = ID;
                deliveryNote.CompanyGSTN = selectedCompanyGSTN;
                deliveryNote.MdiParent = this.MdiParent;
                deliveryNote.Show();
            }
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DeliveryNote deliveryNote = new DeliveryNote();
                deliveryNote.ID = ID;
                deliveryNote.MdiParent = this.MdiParent;
                deliveryNote.Show();
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

                filterQuery = @"SELECT Note.ID, NoteNumber [Note #], 
                Cust.CustomerName + ' | ' + [State] [Customer Name | State],NoteDate [Note Date], 
                RoundedOffNoteTotalAmount [Total Amount], NoteStatus[Note Status], ReferenceNumber [Ref #], CompanyId 
                FROM TranDeliveryNote Note inner join MasterCustomer Cust on Note.CustomerID = Cust.ID 
                Where Note.Del_State = 0 and Note.CompanyId=@CompanyId 
                and (NoteNumber like @FilterValue or Cust.CustomerName like @FilterValue  or ReferenceNumber like @FilterValue or State like @FilterValue) 
                and NoteDate between @FromDate and @ToDate 
                and customerId<>0
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
                dataGridViewList.Columns["ID"].Visible = false;
                dataGridViewList.Columns["CompanyId"].Visible = false;
                ClearData();
            }
        }

        private void ClearData()
        {
            ID = 0;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Transaction
{
    public partial class PaymentCollection : Form
    {
        private SqlDataAdapter adapt;
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        BAL.CustomFieldSelection customFieldSelection = new BAL.CustomFieldSelection();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        SqlCommand cmd;
        public Int32 ID { get; set; }
        public Int16 CompanyId { get; set; }
        public Int32 CustomerId { get; set; }
        public Int32 AgentId { get; set; }

        bool flagDisplayData;
        public PaymentCollection()
        {
            InitializeComponent();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonSave.Enabled = false;
                buttonDelete.Enabled = false;
            }
            comboBoxPaymentMode.DataSource = BAL.GenericFucntions.SplitString(ConfigurationManager.AppSettings["PaymentMode"].ToString(), "|");
            loadCompany();
            loadAgent();
            loadCustomerByAgent();
        }

        private void PaymentCollection_Load(object sender, EventArgs e)
        {

            dataGridViewColumnWidthSetting();
            if (ID != 0)
            {
                DisplayData();
            }
            else
            {
                comboBoxCompany.SelectedValue = CompanyId;
                comboBoxAgent.SelectedValue = AgentId;
                comboBoxCustomer.SelectedValue = CustomerId;
                setAutoSequenceNumber();
            }
            getCustomFields();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("PaymentCollection", Convert.ToInt16(comboBoxCompany.SelectedValue));
                reportViewer.MdiParent = this.MdiParent;
                reportViewer.InvoiceID = ID;
                reportViewer.Show();
            }
            else
            {
                MessageBox.Show("No Payment found!", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID != 0)
                {
                    if (comboBoxCompany.Text.Trim() != "")
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            deleteItem();
                            MessageBox.Show("Record Deleted Successfully", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Details!", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No Payment found!", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCustomer.SelectedIndex != 0)
                {
                    if (dataGridViewList.Rows.Count == 0)
                    {
                        DialogResult dialogResultNoItems = MessageBox.Show("No invoice available in this payment. Do you want to save this transaction still?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResultNoItems == DialogResult.No)
                        {
                            return;
                        }
                    }

                    ID = saveReceiptHeader();
                    saveReceiptItems();

                    MessageBox.Show("Record Saved Successfully", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Provide Details!", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData && comboBoxCustomer.SelectedIndex != 0)
            {
                dataGridViewList.Rows.Clear();
                filterInvoices();
            }
        }

        private void dataGridViewList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.WhiteSmoke;
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Green;
        }

        private void dataGridViewList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Green;
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.WhiteSmoke;
        }

        private void dataGridViewList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1 && e.ColumnIndex != -1 && !flagDisplayData)
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dataGridViewList.Columns[e.ColumnIndex].Name == "AmountReceived" || dataGridViewList.Columns[e.ColumnIndex].Name == "DiscountGiven" || dataGridViewList.Columns[e.ColumnIndex].Name == "InterestReceived")
                {
                    if (!flagDisplayData)
                    {
                        dataGridViewList["Balance", e.RowIndex].Value =
                            Convert.ToDecimal(dataGridViewList["BalanceBeforeUpdate", e.RowIndex].Value)
                            - Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["AmountReceived", e.RowIndex].Value)) ? "0" : dataGridViewList["AmountReceived", e.RowIndex].Value)
                            - Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["DiscountGiven", e.RowIndex].Value)) ? "0" : dataGridViewList["DiscountGiven", e.RowIndex].Value)
                            ;
                    }
                    calcTotalReceiptValues();
                }
            }
        }

        private void dataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setRowNumber();
        }

        private void dataGridViewList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            calcTotalReceiptValues();
            setRowNumber();
        }

        #region Methods
        private void loadCompany()
        {
            comboBoxCompany.ValueMember = "ID";
            comboBoxCompany.DisplayMember = "CompanyName";
            comboBoxCompany.DataSource = masterSelection.GetCompanyList();

            comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void loadCustomerByAgent()
        {
            comboBoxCustomer.ValueMember = "ID";
            comboBoxCustomer.DisplayMember = "CustomerName";
            //comboBoxCustomer.DataSource = masterSelection.GetCustomerList("Buyer");
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

        private void ClearData()
        {
            ID = 0;
            setAutoSequenceNumber();
            comboBoxAgent.SelectedIndex = 0;
            comboBoxCustomer.SelectedIndex = 0;
            comboBoxPaymentMode.SelectedIndex = 0;
            dateTimePickerReceiptDate.Value = DateTime.Now;
            textBoxReceiptNumber.Text = string.Empty;
            textBoxInvoiceAmount.Text = string.Empty;
            textBoxBalance.Text = string.Empty;
            textBoxAmountReceived.Text = string.Empty;
            textBoxDiscount.Text = string.Empty;
            textBoxInterest.Text = string.Empty;
            textBoxPayment.Text = string.Empty;
            textBoxReference.Text = string.Empty;
            textBoxSearch.Text = string.Empty;
            textBoxNotes.Text = string.Empty;
            dataGridViewList.Rows.Clear();
        }

        private void dataGridViewColumnWidthSetting()
        {
            dataGridViewList.Columns["InvoiceNumber"].Width = 100;
            dataGridViewList.Columns["InvoiceDate"].Width = 85;
            dataGridViewList.Columns["InvoiceAmount"].Width = 80;
            dataGridViewList.Columns["Balance"].Width = 80;
            dataGridViewList.Columns["AmountReceived"].Width = 80;
            dataGridViewList.Columns["DiscountGiven"].Width = 80;
            dataGridViewList.Columns["InterestReceived"].Width = 80;
            dataGridViewList.Columns["CFItem1"].Width = 80;
            dataGridViewList.Columns["CFItem2"].Width = 80;
            dataGridViewList.Columns["CFItem3"].Width = 80;
            dataGridViewList.Columns["CFItem4"].Width = 80;
        }

        private void getCustomFields()
        {
            bindCustomFieldItems(customFieldSelection.GetCustomFields(Convert.ToInt16(comboBoxCompany.SelectedValue), "Payment Collection"));
        }

        private void bindCustomFieldItems(Entities.CustomFields customFields)
        {
            if (customFields != null)
            {
                if (customFields.CF1 != string.Empty)
                {
                    dataGridViewList.Columns["CFItem1"].Visible = true;
                    dataGridViewList.Columns["CFItem1"].HeaderText = customFields.CF1;
                }
                else
                {
                    dataGridViewList.Columns["CFItem1"].Visible = false;
                }
                if (customFields.CF2 != string.Empty)
                {
                    dataGridViewList.Columns["CFItem2"].Visible = true;
                    dataGridViewList.Columns["CFItem2"].HeaderText = customFields.CF2;
                }
                else
                {
                    dataGridViewList.Columns["CFItem2"].Visible = false;
                }
                if (customFields.CF3 != string.Empty)
                {
                    dataGridViewList.Columns["CFItem3"].Visible = true;
                    dataGridViewList.Columns["CFItem3"].HeaderText = customFields.CF3;
                }
                else
                {
                    dataGridViewList.Columns["CFItem3"].Visible = false;
                }
                if (customFields.CF4 != string.Empty)
                {
                    dataGridViewList.Columns["CFItem4"].Visible = true;
                    dataGridViewList.Columns["CFItem4"].HeaderText = customFields.CF4;
                }
                else
                {
                    dataGridViewList.Columns["CFItem4"].Visible = false;
                }
            }
            else
            {
                dataGridViewList.Columns["CFItem1"].Visible = false;
                dataGridViewList.Columns["CFItem2"].Visible = false;
                dataGridViewList.Columns["CFItem3"].Visible = false;
                dataGridViewList.Columns["CFItem4"].Visible = false;
            }
        }

        private void setAutoSequenceNumber()
        {
            Int64 receiptNumber = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "GetAutoSequenceNumber_Accounts";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                cmd.Parameters.AddWithValue("@DocumentType", "Payment Collection");

                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        receiptNumber = (Int64)reader["AutoNumber"];
                    }
                }
            }
            labelReceiptID.Text = receiptNumber.ToString();
        }


        private void deleteItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "Trans_Payment_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeaderId", ID);
                cmd.Parameters.AddWithValue("@mode", "PaymentCollection-Delete");
                cmd.ExecuteNonQuery();
            }
        }

        private void DisplayData()
        {
            flagDisplayData = true;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand(@"SELECT ID,[CompanyID],[ReceiptNumber],[ReceiptDate]
      ,[CustomerID],[PaymentMode],[PaymentReference]
      ,[TotalPayment],[PaidToAccountID],[Notes]
        FROM [dbo].[TranPaymentCollection] 
        where ID = @ID and Del_State=0", con);
                cmd.Parameters.AddWithValue("@ID", ID);

                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        labelReceiptID.Text = Convert.ToString(reader["ID"]);
                        comboBoxCompany.SelectedValue = Convert.ToInt16(reader["CompanyID"]);
                        textBoxReceiptNumber.Text = Convert.ToString(reader["ReceiptNumber"]);
                        dateTimePickerReceiptDate.Value = Convert.ToDateTime(reader["ReceiptDate"]);
                        comboBoxCustomer.SelectedValue = Convert.ToInt32(reader["CustomerId"]);
                        comboBoxPaymentMode.Text = Convert.ToString(reader["PaymentMode"]);
                        textBoxReference.Text = Convert.ToString(reader["PaymentReference"]);
                        textBoxNotes.Text = Convert.ToString(reader["Notes"]);
                    }
                }
                DataTable dtInvoiceList = new DataTable();

                using (SqlCommand cmd = new SqlCommand(@"
SELECT [InvoiceID],[InvoiceNumber]
      ,[InvoiceDate],[InvoiceAmount],[Balance]
      ,[AmountReceived],[Discount],[Interest]
      ,[CFItem1],[CFItem2],[CFItem3],[CFItem4]
  FROM [dbo].[TranPaymentCollectionList]
  where HeaderID=@ID and Del_State=0 order by ID "))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (dtInvoiceList)
                        {
                            sda.Fill(dtInvoiceList);
                        }
                    }
                }
                dataGridViewList.Rows.Clear();
                using (dtInvoiceList)
                {
                    Int16 i = 0;
                    foreach (DataRow drow in dtInvoiceList.Rows)
                    {
                        dataGridViewList.Rows.Add();

                        dataGridViewList["InvoiceId", i].Value = (Int64)drow["InvoiceId"];
                        dataGridViewList["InvoiceNumber", i].Value = (string)drow["InvoiceNumber"];
                        dataGridViewList["InvoiceDate", i].Value = Convert.ToDateTime(drow["InvoiceDate"]).ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        dataGridViewList["InvoiceAmount", i].Value = (decimal)drow["InvoiceAmount"];
                        dataGridViewList["Balance", i].Value = (decimal)drow["Balance"];
                        dataGridViewList["AmountReceived", i].Value = (decimal)drow["AmountReceived"];
                        dataGridViewList["BalanceBeforeUpdate", i].Value = (decimal)drow["Balance"] + (decimal)drow["AmountReceived"];
                        dataGridViewList["DiscountGiven", i].Value = (decimal)drow["Discount"];
                        dataGridViewList["InterestReceived", i].Value = (decimal)drow["Interest"];
                        dataGridViewList["CFItem1", i].Value = (string)drow["CFItem1"];
                        dataGridViewList["CFItem2", i].Value = (string)drow["CFItem2"];
                        dataGridViewList["CFItem3", i].Value = (string)drow["CFItem3"];
                        dataGridViewList["CFItem4", i].Value = (string)drow["CFItem4"];

                        i++;
                    }
                }
            }
            flagDisplayData = false;
        }

        private void fillDGVlist(SqlConnection con, string invoiceQuery)
        {
            DataTable dtInvoiceList = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@ID", ID);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (dtInvoiceList)
                    {

                        sda.Fill(dtInvoiceList);
                    }
                }
            }
            using (dtInvoiceList)
            {
                Int16 i = 0;
                foreach (DataRow dr in dtInvoiceList.Rows)
                {
                    dataGridViewList.Rows.Add();

                    dataGridViewList["InvoiceId", i].Value = (int)dr["InvoiceId"];
                    dataGridViewList["InvoiceNumber", i].Value = (string)dr["InvoiceNumber"];
                    dataGridViewList["InvoiceDate", i].Value = (string)dr["InvoiceDate"];
                    dataGridViewList["InvoiceAmount", i].Value = (string)dr["InvoiceAmount"];
                    dataGridViewList["Balance", i].Value = (decimal)dr["Balance"];
                    dataGridViewList["BalanceBeforeUpdate", i].Value = (decimal)dr["Balance"];
                    dataGridViewList["AmountReceived", i].Value = (decimal)dr["AmountReceived"];
                    dataGridViewList["DiscountGiven", i].Value = (decimal)dr["Discount"];
                    dataGridViewList["InterestReceived", i].Value = (decimal)dr["Interest"];
                    dataGridViewList["CFItem1", i].Value = (string)dr["CFItem1"];
                    dataGridViewList["CFItem2", i].Value = (string)dr["CFItem2"];
                    dataGridViewList["CFItem3", i].Value = (string)dr["CFItem3"];
                    dataGridViewList["CFItem4", i].Value = (string)dr["CFItem4"];

                    i++;
                }
            }
        }

        private void filterInvoices()
        {
            string filterQuery = string.Empty;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable dt = new DataTable();
                filterQuery = @"select Sales.ID, Sales.InvoiceNumber, Sales.InvoiceDate, Sales.RoundedOffInvoiceTotalAmount
, Sales.RoundedOffInvoiceTotalAmount- (SUM(ISNULL(Payment.AmountReceived, 0)) + SUM(ISNULL(Payment.Discount,0))) Balance
, SUM(ISNULL(Payment.AmountReceived, 0)) AmountReceived
, SUM(ISNULL(Payment.Discount,0)) DiscountGiven
, SUM(ISNULL(Payment.Interest,0)) InterestReceived 
from (select * from TranSalesInvoice where Del_State=0) Sales left join (select * from TranPaymentCollectionList where Del_State=0) Payment
on Sales.ID=Payment.InvoiceID 
group by Sales.ID, Sales.InvoiceNumber, Sales.InvoiceDate, Sales.RoundedOffInvoiceTotalAmount
, Sales.CompanyID, Sales.CustomerID,Sales.InvoiceNumber 
having Sales.RoundedOffInvoiceTotalAmount> SUM(ISNULL(Payment.AmountReceived,0)) and Sales.RoundedOffInvoiceTotalAmount > 0 ";
                if ((checkBoxFilterAllInvoices.Checked && textBoxSearch.Text != string.Empty) || (!checkBoxFilterAllInvoices.Checked && textBoxSearch.Text != string.Empty && comboBoxCustomer.SelectedIndex == 0))
                {
                    //filterQuery = filterQuery + " where CompanyID=@CompanyID and InvoiceNumber like @FilterValue and Del_State=0 order by InvoiceDate,ID ";
                    filterQuery = filterQuery + @" and Sales.CompanyID=@CompanyID and Sales.InvoiceNumber like @FilterValue
order by InvoiceDate,ID ";
                }
                else if (checkBoxFilterAllInvoices.Checked && textBoxSearch.Text == string.Empty)
                {
                    filterQuery = filterQuery + @" and Sales.CompanyID=@CompanyID 
order by InvoiceDate,ID ";
                }
                else
                {
                    filterQuery = filterQuery + @" and Sales.CompanyID=@CompanyID 
and Sales.CustomerID=@CustomerId and Sales.InvoiceNumber like @FilterValue
order by InvoiceDate,ID ";
                }

                adapt = new SqlDataAdapter(filterQuery, con);
                adapt.SelectCommand.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                adapt.SelectCommand.Parameters.AddWithValue("@CustomerId", comboBoxCustomer.SelectedValue);

                adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                adapt.Fill(dt);
                dataGridViewList.Rows.Clear();
                using (dt)
                {
                    Int16 i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridViewList.Rows.Add();

                        dataGridViewList["InvoiceId", i].Value = (Int64)dr["ID"];
                        dataGridViewList["InvoiceNumber", i].Value = (string)dr["InvoiceNumber"];
                        dataGridViewList["InvoiceDate", i].Value = Convert.ToDateTime(dr["InvoiceDate"]).ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        dataGridViewList["InvoiceAmount", i].Value = (Int64)dr["RoundedOffInvoiceTotalAmount"];
                        dataGridViewList["Balance", i].Value = (decimal)dr["Balance"];
                        dataGridViewList["BalanceBeforeUpdate", i].Value = (decimal)dr["Balance"];
                        //dataGridViewList["AmountReceived", i].Value = Convert.ToDecimal(dr["AmountReceived"]);
                        dataGridViewList["AmountReceived", i].Value = 0;
                        dataGridViewList["DiscountGiven", i].Value = (decimal)dr["DiscountGiven"];
                        dataGridViewList["InterestReceived", i].Value = (decimal)dr["InterestReceived"];

                        i++;
                    }
                }

                dataGridViewList.Columns["InvoiceId"].Visible = false;
            }
        }

        private Int32 saveReceiptHeader()
        {
            Int32 identityInserted = 0;
            calcTotalReceiptValues();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                if (ID == 0)
                {
                    cmd = new SqlCommand(@"INSERT INTO [dbo].[TranPaymentCollection]
           ([CompanyID],[ReceiptNumber],[ReceiptDate]
           ,[CustomerID],[PaymentMode],[PaymentReference]
           ,[TotalPayment],[PaidToAccountID],[Notes]
           ,[CreatedBy],[CreatedOn], ReceiptId) OUTPUT inserted.ID
     VALUES
           (@CompanyID, @ReceiptNumber, @ReceiptDate
           , @CustomerID, @PaymentMode, @PaymentReference
           , @TotalPayment, @PaidToAccountID, @Notes
           , @CreatedBy, getDate(), @ReceiptId)", con);
                }
                else
                {
                    cmd = new SqlCommand(@"
    delete [TranPaymentCollectionList] Where HeaderID=@ID

    UPDATE [dbo].[TranPaymentCollection]
    SET [CompanyID] = @CompanyID
      ,[ReceiptNumber] = @ReceiptNumber
      ,[ReceiptDate] = @ReceiptDate
      ,[CustomerID] = @CustomerID
      ,[PaymentMode] = @PaymentMode
      ,[PaymentReference] = @PaymentReference
      ,[TotalPayment] = @TotalPayment
      ,[PaidToAccountID] = @PaidToAccountID
      ,[Notes] = @Notes
      ,[CreatedBy] = @CreatedBy
      ,[CreatedOn] = GETDATE() 
	OUTPUT inserted.ID
	WHERE ID = @ID", con);
                }

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@ReceiptNumber", Convert.ToString(textBoxReceiptNumber.Text).Trim());
                cmd.Parameters.AddWithValue("@ReceiptDate", dateTimePickerReceiptDate.Value);
                cmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(comboBoxCustomer.SelectedValue));
                cmd.Parameters.AddWithValue("@PaymentMode", Convert.ToString(comboBoxPaymentMode.SelectedValue));
                cmd.Parameters.AddWithValue("@PaymentReference", Convert.ToString(textBoxReference.Text).Trim());
                cmd.Parameters.AddWithValue("@TotalPayment", Convert.ToDecimal(String.IsNullOrEmpty(textBoxAmountReceived.Text) ? "0" : textBoxAmountReceived.Text));
                cmd.Parameters.AddWithValue("@PaidToAccountID", 0);
                cmd.Parameters.AddWithValue("@Notes", Convert.ToString(textBoxNotes.Text).Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Entities.AuthenticationDetail.UserName);
                cmd.Parameters.AddWithValue("@ReceiptId", Convert.ToInt32(labelReceiptID.Text));

                identityInserted = Convert.ToInt32(cmd.ExecuteScalar());

            }
            return identityInserted;
        }

        private void saveReceiptItems()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                for (int i = 0; i < dataGridViewList.Rows.Count; i++)
                {
                    decimal invoiceAmount = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["InvoiceAmount", i].Value)) ? "0" : dataGridViewList["InvoiceAmount", i].Value);
                    decimal balanceAmount = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Balance", i].Value)) ? "0" : dataGridViewList["Balance", i].Value);
                    decimal amountReceived = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["AmountReceived", i].Value)) ? "0" : dataGridViewList["AmountReceived", i].Value);
                    decimal discountGiven = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["DiscountGiven", i].Value)) ? "0" : dataGridViewList["DiscountGiven", i].Value);
                    decimal interestReceived = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["InterestReceived", i].Value)) ? "0" : dataGridViewList["InterestReceived", i].Value);
                    string cfItem1 = Convert.ToString(dataGridViewList["CFItem1", i].Value);
                    string cfItem2 = Convert.ToString(dataGridViewList["CFItem2", i].Value);
                    string cfItem3 = Convert.ToString(dataGridViewList["CFItem3", i].Value);
                    string cfItem4 = Convert.ToString(dataGridViewList["CFItem4", i].Value);

                    if (amountReceived == 0 && discountGiven == 0 && interestReceived == 0 && cfItem1 == string.Empty && cfItem2 == string.Empty && cfItem3 == string.Empty && cfItem4 == string.Empty)
                    {
                        continue;
                    }
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Trans_PaymentList_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@HeaderID", ID);
                    cmd.Parameters.AddWithValue("@InvoiceID", Convert.ToInt32(dataGridViewList["InvoiceId", i].Value));
                    cmd.Parameters.AddWithValue("@InvoiceNumber", Convert.ToString(dataGridViewList["InvoiceNumber", i].Value));
                    cmd.Parameters.AddWithValue("@InvoiceDate", Convert.ToDateTime(dataGridViewList["InvoiceDate", i].Value));
                    cmd.Parameters.AddWithValue("@InvoiceAmount", invoiceAmount);
                    cmd.Parameters.AddWithValue("@Balance", balanceAmount);
                    cmd.Parameters.AddWithValue("@AmountReceived", amountReceived);
                    cmd.Parameters.AddWithValue("@Discount", discountGiven);
                    cmd.Parameters.AddWithValue("@Interest", interestReceived);
                    cmd.Parameters.AddWithValue("@CFItem1", cfItem1);
                    cmd.Parameters.AddWithValue("@CFItem2", cfItem2);
                    cmd.Parameters.AddWithValue("@CFItem3", cfItem3);
                    cmd.Parameters.AddWithValue("@CFItem4", cfItem4);
                    cmd.Parameters.AddWithValue("@mode", "PaymentCollection-Insert");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void calcTotalReceiptValues()
        {
            decimal totalInvoiceAmount = 0;
            decimal totalBalance = 0;

            decimal totalAmountReceived = 0;
            decimal totalDiscount = 0;
            decimal totalInterestReceived = 0;

            for (int i = 0; i < dataGridViewList.Rows.Count; i++)
            {
                totalInvoiceAmount = totalInvoiceAmount + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["InvoiceAmount", i].Value)) ? "0" : dataGridViewList["InvoiceAmount", i].Value);
                totalBalance = totalBalance + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Balance", i].Value)) ? "0" : dataGridViewList["Balance", i].Value);

                totalAmountReceived = totalAmountReceived + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["AmountReceived", i].Value)) ? "0" : dataGridViewList["AmountReceived", i].Value);
                totalDiscount = totalDiscount + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["DiscountGiven", i].Value)) ? "0" : dataGridViewList["DiscountGiven", i].Value);
                totalInterestReceived = totalInterestReceived + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["InterestReceived", i].Value)) ? "0" : dataGridViewList["InterestReceived", i].Value);
            }
            textBoxInvoiceAmount.Text = Convert.ToString(Math.Round(totalInvoiceAmount, 2, MidpointRounding.AwayFromZero));
            textBoxBalance.Text = Convert.ToString(Math.Round(totalBalance, 2, MidpointRounding.AwayFromZero));

            textBoxAmountReceived.Text = Convert.ToString(Math.Round(totalAmountReceived, 2, MidpointRounding.AwayFromZero));
            textBoxDiscount.Text = Convert.ToString(Math.Round(totalDiscount, 2, MidpointRounding.AwayFromZero));
            textBoxInterest.Text = Convert.ToString(Math.Round(totalInterestReceived, 2, MidpointRounding.AwayFromZero));
        }

        private void setRowNumber()
        {
            foreach (DataGridViewRow row in dataGridViewList.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        #endregion

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void checkBoxFilterAllInvoices_CheckedChanged(object sender, EventArgs e)
        {
            filterInvoices();
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            decimal payment = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(textBoxPayment.Text)) ? "0" : textBoxPayment.Text);

            for (int i = 0; i < dataGridViewList.Rows.Count; i++)
            {
                decimal balance = Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["BalanceBeforeUpdate", i].Value)) ? "0" : dataGridViewList["BalanceBeforeUpdate", i].Value);
                if (payment >= balance)
                {
                    dataGridViewList["AmountReceived", i].Value = balance;
                    payment = payment - balance;
                }
                else
                {
                    dataGridViewList["AmountReceived", i].Value = payment;
                    payment = 0;
                }
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                ID = BAL.GenericValidation.GetLatestIdentity_PaymentCollection();
            }
            else
            {
                ID = BAL.GenericValidation.GetPreviousIdentity_PaymentCollection(ID);
            }
            DisplayData();
            //MessageBox.Show("No record found", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Int32 id = BAL.GenericValidation.GetNextIdentity_PaymentCollection(ID);
            if (id != 0)
            {
                ID = id;
                DisplayData();
            }
            else
                MessageBox.Show("No record found!", "Payment Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void comboBoxAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCustomerByAgent();
        }
    }
}
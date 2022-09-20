using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;

namespace THA.GSTBillingSW.Transaction
{
    public partial class SalesInvoice : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        BAL.CustomFieldSelection customFieldSelection = new BAL.CustomFieldSelection();
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        string quickCustomerInfoDisplay = ConfigurationManager.AppSettings["QuickCustomerInfoDisplay"].ToString();
        string percentBasedDiscount = ConfigurationManager.AppSettings["PercentBasedDiscount"].ToString();
        string negativeStockAllowed = ConfigurationManager.AppSettings["NegativeStockAllowed"].ToString();
        string invoicewiseTaxCalc = ConfigurationManager.AppSettings["SalesInvoiceWiseTaxCalc"].ToString();
        SqlCommand cmd;
        public Int32 ID { get; set; }
        public Int16 CompanyId { get; set; }
        public string CompanyGSTN { get; set; }
        bool isIGSTApplicable = false;
        //Setting for CESS applicability
        bool isCESSApllicable = false;

        bool autoSequenceStatus;
        bool newCustomerRegistrationFlag;
        bool flagDisplayData;
        string customerStateBeforeUpdate = string.Empty;
        public SalesInvoice()
        {
            InitializeComponent();
            if (quickCustomerInfoDisplay == "0")
            {
                tabControlInvoiceInfo.TabPages.Remove(tabPageCustomerDetail);
            }
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonSave.Enabled = false;
                buttonDelete.Enabled = false;
                buttonCancelInvoice.Enabled = false;
            }
            loadCompany();
            loadCustomer();
            loadAgent();
            loadState();
            loadDiscountBasedOnSettings();
            loadCategoryBasedOnSettings();
        }

        private void loadDiscountBasedOnSettings()
        {
            if (percentBasedDiscount == "1")
            {
                radioButtonPercentBasedDiscount.Checked = true;
            }
            else
            {
                radioButtonAmountBasedDiscount.Checked = true;
            }
        }

        private void loadCategoryBasedOnSettings()
        {
            if (ConfigurationManager.AppSettings["SalesInvoiceCategory"].ToString() == "B2B")
            {
                radioButtonB2B.Checked = true;
            }
            else
            {
                radioButtonB2C.Checked = true;
            }
        }

        private void SalesInvoice_Load(object sender, EventArgs e)
        {
            //Checking CESS applicability
            if (!isCESSApllicable)
            {
                dataGridViewList.Columns["CESSPercent"].Visible = false;
                dataGridViewList.Columns["CESSValue"].Visible = false;
                labelTotalCESSAmount.Visible = false;
                textBoxTotalCESSAmount.Visible = false;

                labelCESSPercent.Visible = false;
                textBoxCESSPercent.Visible = false;
            }
            dataGridViewColumnWidthSetting();
            if (ID != 0)
            {
                DisplayData();
                setAutoSequenceNumber("ExistingInvoice");
            }
            else
            {
                comboBoxCompany.SelectedValue = CompanyId;
                setAutoSequenceNumber("NewInvoice");
                textBoxTermsAndConditions.Text = customFieldSelection.GetTermsAndConditions(Convert.ToInt16(comboBoxCompany.SelectedValue), "Sales Invoice");
            }

            getCustomFields();
        }


        private void buttonRefreshCustomer_Click(object sender, EventArgs e)
        {
            loadCustomer();
        }

        private void getCustomFields()
        {
            bindCustomFields(customFieldSelection.GetCustomFields(Convert.ToInt16(comboBoxCompany.SelectedValue), "Sales Invoice"));
            bindCustomFieldItems(customFieldSelection.GetCustomFields(Convert.ToInt16(comboBoxCompany.SelectedValue), "Sales Invoice Items"));
        }

        private void setAutoCompletion(string customFieldName, TextBox tbCustom)
        {
            if (ConfigurationManager.AppSettings[string.Format("SalesInvoiceAutoComplete{0}", customFieldName)].ToString() == "1")
            {
                tbCustom.AutoCompleteCustomSource = customFieldSelection.GetCustomFieldData(customFieldName, "Sales Invoice");
                tbCustom.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tbCustom.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
        }

        private void bindCustomFields(Entities.CustomFields customFields)
        {
            if (customFields != null)
            {
                if (customFields.CF1 != string.Empty)
                {
                    labelCF1.Text = customFields.CF1;
                    labelCF1.Visible = true;
                    textBoxCF1.Visible = true;
                    setAutoCompletion("CF1", textBoxCF1);
                }
                else
                {
                    labelCF1.Visible = false;
                    textBoxCF1.Visible = false;
                    textBoxCF1.Text = string.Empty;
                }

                if (customFields.CF2 != string.Empty)
                {
                    labelCF2.Text = customFields.CF2;
                    labelCF2.Visible = true;
                    textBoxCF2.Visible = true;
                    setAutoCompletion("CF2", textBoxCF2);
                }
                else
                {
                    labelCF2.Visible = false;
                    textBoxCF2.Visible = false;
                    textBoxCF2.Text = string.Empty;
                }

                if (customFields.CF3 != string.Empty)
                {
                    labelCF3.Text = customFields.CF3;
                    labelCF3.Visible = true;
                    textBoxCF3.Visible = true;
                    setAutoCompletion("CF3", textBoxCF3);
                }
                else
                {
                    labelCF3.Visible = false;
                    textBoxCF3.Visible = false;
                    textBoxCF3.Text = string.Empty;
                }

                if (customFields.CF4 != string.Empty)
                {
                    labelCF4.Text = customFields.CF4;
                    labelCF4.Visible = true;
                    textBoxCF4.Visible = true;
                    setAutoCompletion("CF4", textBoxCF4);
                }
                else
                {
                    labelCF4.Visible = false;
                    textBoxCF4.Visible = false;
                    textBoxCF4.Text = string.Empty;
                }

                if (customFields.CF5 != string.Empty)
                {
                    labelCF5.Text = customFields.CF5;
                    labelCF5.Visible = true;
                    textBoxCF5.Visible = true;
                    setAutoCompletion("CF5", textBoxCF5);
                }
                else
                {
                    labelCF5.Visible = false;
                    textBoxCF5.Visible = false;
                    textBoxCF5.Text = string.Empty;
                }

                if (customFields.CF6 != string.Empty)
                {
                    labelCF6.Text = customFields.CF6;
                    labelCF6.Visible = true;
                    textBoxCF6.Visible = true;
                    setAutoCompletion("CF6", textBoxCF6);
                }
                else
                {
                    labelCF6.Visible = false;
                    textBoxCF6.Visible = false;
                    textBoxCF6.Text = string.Empty;
                }
            }
            else
            {
                labelCF1.Visible = false;
                textBoxCF1.Visible = false;
                textBoxCF1.Text = string.Empty;

                labelCF2.Visible = false;
                textBoxCF2.Visible = false;
                textBoxCF2.Text = string.Empty;

                labelCF3.Visible = false;
                textBoxCF3.Visible = false;
                textBoxCF3.Text = string.Empty;

                labelCF4.Visible = false;
                textBoxCF4.Visible = false;
                textBoxCF4.Text = string.Empty;

                labelCF5.Visible = false;
                textBoxCF5.Visible = false;
                textBoxCF5.Text = string.Empty;

                labelCF6.Visible = false;
                textBoxCF6.Visible = false;
                textBoxCF6.Text = string.Empty;
            }
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
            }
            else
            {
                dataGridViewList.Columns["CFItem1"].Visible = false;
                dataGridViewList.Columns["CFItem2"].Visible = false;
            }
        }

        private void setAutoSequenceNumber(string invoiceMode)
        {
            string invoiceNumber = string.Empty;
            string idPrifix = string.Empty;
            string idSuffix = string.Empty;
            using (SqlConnection con = new SqlConnection(conString))
            {

                con.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "GetAutoSequenceNumber";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                cmd.Parameters.AddWithValue("@DocumentType", "Sales Invoice");
                cmd.Parameters.AddWithValue("@mode", invoiceMode);
                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            invoiceNumber = (string)(reader["InvoiceNumber"] != DBNull.Value ? reader["InvoiceNumber"] : "");
                            autoSequenceStatus = (bool)reader["DocumentIdResetFlag"];
                            idPrifix = (string)reader["IdPrefix"];
                            idSuffix = (string)reader["IdSuffix"];
                        }
                    }
                }
            }
            if (invoiceNumber == "ManualNewInvoice")
            {
                textBoxInvoiceNumber.Text = string.Empty;
                textBoxInvoiceNumber.Enabled = true;
                textBoxInvoiceNumber.Select();
            }
            else if (invoiceNumber == "ManualExistingInvoice")
            {
                textBoxInvoiceNumber.Enabled = true;
                textBoxInvoiceNumber.Select();
            }
            else if (invoiceNumber == "AutoExistingInvoice")
            {
                textBoxInvoiceNumber.Enabled = false;
                dateTimePickerInvoiceDate.Select();
            }
            else
            {
                textBoxInvoiceNumber.Text = string.Concat(idPrifix, invoiceNumber, idSuffix);
                textBoxInvoiceNumber.Enabled = false;
                dateTimePickerInvoiceDate.Select();
            }
        }

        private void updateAutoSequenceNumberStatus()
        {
            if (autoSequenceStatus)
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "GetAutoSequenceNumber";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                    cmd.Parameters.AddWithValue("@DocumentType", "Sales Invoice");
                    cmd.Parameters.AddWithValue("@mode", "UpdateResetFlag");
                    using (cmd)
                    {
                        object o = cmd.ExecuteNonQuery();
                    }
                }
        }

        private void dataGridViewColumnWidthSetting()
        {
            dataGridViewList.Columns["ItemDescription"].Width = 120;
            dataGridViewList.Columns["ItemCustomDescription"].Width = 80;
            dataGridViewList.Columns["ItemType"].Width = 60;
            dataGridViewList.Columns["HsnSac"].Width = 60;
            dataGridViewList.Columns["Quantity"].Width = 60;
            dataGridViewList.Columns["UoM"].Width = 60;
            dataGridViewList.Columns["RatePerItem"].Width = 60;
            dataGridViewList.Columns["DiscountPercent"].Width = 60;
            dataGridViewList.Columns["Discount"].Width = 60;
            dataGridViewList.Columns["TaxableValue"].Width = 60;
            dataGridViewList.Columns["CGSTPercent"].Width = 60;
            dataGridViewList.Columns["CGSTValue"].Width = 60;
            dataGridViewList.Columns["SGSTPercent"].Width = 60;
            dataGridViewList.Columns["SGSTValue"].Width = 60;
            dataGridViewList.Columns["IGSTPercent"].Width = 60;
            dataGridViewList.Columns["IGSTValue"].Width = 60;
            dataGridViewList.Columns["CESSPercent"].Width = 60;
            dataGridViewList.Columns["CESSValue"].Width = 60;
            dataGridViewList.Columns["Total"].Width = 60;
            dataGridViewList.Columns["CFItem1"].Width = 60;
            dataGridViewList.Columns["CFItem2"].Width = 60;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCustomer.SelectedIndex != 0 || textBoxCustomer.Text.Trim() != string.Empty)
                {
                    if (dataGridViewList.Rows.Count == 1)
                    {
                        DialogResult dialogResultNoItems = MessageBox.Show("No items added in this invoice. Do you want to save this invoice still?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResultNoItems == DialogResult.No)
                        {
                            return;
                        }
                    }
                    //TO DO : Financial Year wise validation
                    if (BAL.GenericValidation.IsInvoiceNumberAlreadyExist(textBoxInvoiceNumber.Text.Trim(), Convert.ToInt16(comboBoxCompany.SelectedValue), ID, "SalesInvoice"))
                    {
                        DialogResult dialogResult1 = MessageBox.Show("Invoice Number '" + textBoxInvoiceNumber.Text.Trim() + "' already exists. Do you want to save it still?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult1 != DialogResult.Yes)
                        {
                            textBoxInvoiceNumber.Select();
                            return;
                        }
                    }
                    ID = saveInvoiceHeader();
                    saveInvoiceItems();
                    updateAutoSequenceNumberStatus();

                    DialogResult dialogResult = MessageBox.Show("Record Saved Successfully. Do you want to preview this invoice?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        buttonReport_Click(sender, e);
                    }
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Provide Details!", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveInvoiceItems()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Trans_InvoiceItem_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@InvoiceHeaderID", ID);

                    cmd.Parameters.AddWithValue("@ItemID", Convert.ToInt16(dataGridViewList["ItemId", i].Value));
                    cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@ItemDescription", Convert.ToString(dataGridViewList["ItemDescription", i].Value));
                    cmd.Parameters.AddWithValue("@ItemCustomDescription", Convert.ToString(dataGridViewList["ItemCustomDescription", i].Value));
                    cmd.Parameters.AddWithValue("@ItemType", Convert.ToString(dataGridViewList["ItemType", i].Value));
                    cmd.Parameters.AddWithValue("@HsnSac", Convert.ToString(dataGridViewList["HSNSAC", i].Value));
                    cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(dataGridViewList["Quantity", i].Value));
                    cmd.Parameters.AddWithValue("@UoM", Convert.ToString(dataGridViewList["UoM", i].Value));
                    cmd.Parameters.AddWithValue("@RatePerItem", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["RatePerItem", i].Value)) ? "0" : dataGridViewList["RatePerItem", i].Value));
                    cmd.Parameters.AddWithValue("@DiscountPercent", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["DiscountPercent", i].Value)) ? "0" : dataGridViewList["DiscountPercent", i].Value));
                    cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Discount", i].Value)) ? "0" : dataGridViewList["Discount", i].Value));
                    cmd.Parameters.AddWithValue("@TaxableValue", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["TaxableValue", i].Value)) ? "0" : dataGridViewList["TaxableValue", i].Value));
                    cmd.Parameters.AddWithValue("@CGSTPercent", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CGSTPercent", i].Value)) ? "0" : dataGridViewList["CGSTPercent", i].Value));
                    cmd.Parameters.AddWithValue("@CGSTValue", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CGSTValue", i].Value)) ? "0" : dataGridViewList["CGSTValue", i].Value));
                    cmd.Parameters.AddWithValue("@SGSTPercent", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["SGSTPercent", i].Value)) ? "0" : dataGridViewList["SGSTPercent", i].Value));
                    cmd.Parameters.AddWithValue("@SGSTValue", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["SGSTValue", i].Value)) ? "0" : dataGridViewList["SGSTValue", i].Value));
                    cmd.Parameters.AddWithValue("@IGSTPercent", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["IGSTPercent", i].Value)) ? "0" : dataGridViewList["IGSTPercent", i].Value));
                    cmd.Parameters.AddWithValue("@IGSTValue", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["IGSTValue", i].Value)) ? "0" : dataGridViewList["IGSTValue", i].Value));
                    cmd.Parameters.AddWithValue("@CESSPercent", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CESSPercent", i].Value)) ? "0" : dataGridViewList["CESSPercent", i].Value));
                    cmd.Parameters.AddWithValue("@CESSValue", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CESSValue", i].Value)) ? "0" : dataGridViewList["CESSValue", i].Value));
                    cmd.Parameters.AddWithValue("@TotalValue", Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Total", i].Value)) ? "0" : dataGridViewList["Total", i].Value));
                    cmd.Parameters.AddWithValue("@CFItem1", Convert.ToString(dataGridViewList["CFItem1", i].Value));
                    cmd.Parameters.AddWithValue("@CFItem2", Convert.ToString(dataGridViewList["CFItem2", i].Value));
                    if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                        cmd.Parameters.AddWithValue("@StockCompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    else
                        cmd.Parameters.AddWithValue("@StockCompanyID", 0);
                    cmd.Parameters.AddWithValue("@mode", "SalesInvoice-Insert");

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private Int32 saveInvoiceHeader()
        {
            Int32 identityInserted = 0;
            calcTotalInvoiceValues();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                if (ID == 0)
                {
                    //Weight - CF1
                    //ModeOfTransport - CF2
                    //LRNumber - CF3
                    //BaleNumber - CF4
                    //AgentName - CF5

                    cmd = new SqlCommand(@"INSERT INTO [dbo].[TranSalesInvoice]
          (CompanyID, InvoiceNumber, InvoiceDate, InvoiceDueDate, CustomerID, AgentID
          , CustomerName, CustomerAddress, ContactPerson, CustomerMobile
          , CustomerPhone, CustomerEmail, CustomerState, PONumber
          , PODate, CF4, CF2, CF3, CF1, CGSTAmount
          , SGSTAmount, IGSTAmount, CESSAmount, TaxableAmount
          , InvoiceTotalAmount, InvoiceStatus, CustomerNotes
          , isCessApplicable, TermsAndCondition
          , CF5, CF6, CreatedBy, CreatedOn
        , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
        , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount
        , TaxableAmountWithDisc,Category, RoundedOffInvoiceTotalAmount)
          OUTPUT inserted.ID
    VALUES
          (@CompanyID, @InvoiceNumber, @InvoiceDate, @InvoiceDueDate, @CustomerID, @AgentID
          , @CustomerName, @CustomerAddress, @ContactPerson, @CustomerMobile
          , @CustomerPhone, @CustomerEmail, @CustomerState, @PONumber
          , @PODate, @CF4, @CF2, @CF3, @CF1, @CGSTAmount
          , @SGSTAmount, @IGSTAmount, @CESSAmount, @TaxableAmount
          , @InvoiceTotalAmount, @InvoiceStatus, @CustomerNotes
          , @isCessApplicable,@TermsAndCondition
          , @CF5,@CF6, @CreatedBy, getDate()
        , @CESSPercentTotal, @SGSTPercentTotal, @CGSTPercentTotal
        , @IGSTPercentTotal, @DiscountPercentTotal, @DiscountAmount
        , @TaxableAmountWithDisc,@Category, ROUND(@InvoiceTotalAmount,0))", con);
                }
                else
                {
                    //Reset items stock before update
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Trans_Invoice_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@InvoiceHeaderId", ID);
                    cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                        cmd.Parameters.AddWithValue("@StockCompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    else
                        cmd.Parameters.AddWithValue("@StockCompanyID", 0);
                    cmd.Parameters.AddWithValue("@mode", "SalesInvoice-Reset");
                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new SqlCommand(@"UPDATE [dbo].[TranSalesInvoice]
           SET [CompanyID] = @CompanyID
           ,InvoiceNumber = @InvoiceNumber
           ,InvoiceDate = @InvoiceDate
           ,InvoiceDueDate = @InvoiceDueDate
           ,CustomerID = @CustomerID
           ,AgentID = @AgentID
            , CustomerName=@CustomerName
            , CustomerAddress=@CustomerAddress
            , ContactPerson=@ContactPerson
            , CustomerMobile=@CustomerMobile
            , CustomerPhone=@CustomerPhone
            , CustomerEmail=@CustomerEmail
            , CustomerState=@CustomerState
           ,PONumber = @PONumber
           ,PODate = @PODate
           ,CF4 = @CF4
           ,CF2 = @CF2
           ,CF3 = @CF3
           ,CF1 = @CF1
           ,CGSTAmount = @CGSTAmount
           ,SGSTAmount = @SGSTAmount
           ,IGSTAmount = @IGSTAmount
           ,CESSAmount = @CESSAmount
           ,TaxableAmount = @TaxableAmount
           ,InvoiceTotalAmount = @InvoiceTotalAmount
           ,InvoiceStatus = @InvoiceStatus
           ,CustomerNotes = @CustomerNotes
            ,isCessApplicable =@isCessApplicable 
           ,TermsAndCondition = @TermsAndCondition
           ,CF5 = @CF5
           ,CF6 = @CF6
           ,CreatedBy = @CreatedBy
           ,CreatedOn = getDate()
            , CESSPercentTotal=@CESSPercentTotal
            , SGSTPercentTotal=@SGSTPercentTotal
            , CGSTPercentTotal=@CGSTPercentTotal
            , IGSTPercentTotal=@IGSTPercentTotal
            , DiscountPercentTotal=@DiscountPercentTotal
            , DiscountAmount=@DiscountAmount
            , TaxableAmountWithDisc=@TaxableAmountWithDisc
            , Category=@Category
            , RoundedOffInvoiceTotalAmount = ROUND(@InvoiceTotalAmount,0)
                OUTPUT inserted.ID
           WHERE ID = @ID", con);
                }

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@InvoiceNumber", Convert.ToString(textBoxInvoiceNumber.Text).Trim());
                cmd.Parameters.AddWithValue("@InvoiceDate", dateTimePickerInvoiceDate.Value);
                cmd.Parameters.AddWithValue("@InvoiceDueDate", dateTimePickerDueDate.Value);
                cmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(comboBoxCustomer.SelectedValue));
                cmd.Parameters.AddWithValue("@AgentID", Convert.ToInt32(comboBoxAgent.SelectedValue));

                cmd.Parameters.AddWithValue("@CustomerName", Convert.ToString(textBoxCustomer.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerAddress", Convert.ToString(textBoxBillingAddress.Text.Trim()));
                cmd.Parameters.AddWithValue("@ContactPerson", Convert.ToString(textBoxContactPerson.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerMobile", Convert.ToString(textBoxMobile.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerPhone", Convert.ToString(textBoxPhone.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerEmail", Convert.ToString(textBoxEmail.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerState", Convert.ToString(comboBoxState.Text));

                cmd.Parameters.AddWithValue("@PONumber", Convert.ToString(textBoxPONumber.Text).Trim());
                if (textBoxPONumber.Text.Trim() != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@poDate", dateTimePickerPODate.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@poDate", Convert.ToDateTime("1900-01-01"));
                }
                cmd.Parameters.AddWithValue("@CF4", Convert.ToString(textBoxCF4.Text).Trim());
                cmd.Parameters.AddWithValue("@CF2", Convert.ToString(textBoxCF2.Text).Trim());
                cmd.Parameters.AddWithValue("@CF3", Convert.ToString(textBoxCF3.Text).Trim());
                cmd.Parameters.AddWithValue("@CF1", Convert.ToString(textBoxCF1.Text).Trim());
                cmd.Parameters.AddWithValue("@CGSTAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalCGSTAmount.Text) ? "0" : textBoxTotalCGSTAmount.Text));
                cmd.Parameters.AddWithValue("@SGSTAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalSGSTAmount.Text) ? "0" : textBoxTotalSGSTAmount.Text));
                cmd.Parameters.AddWithValue("@IGSTAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalIGSTAmount.Text) ? "0" : textBoxTotalIGSTAmount.Text));
                cmd.Parameters.AddWithValue("@CESSAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalCESSAmount.Text) ? "0" : textBoxTotalCESSAmount.Text));
                cmd.Parameters.AddWithValue("@TaxableAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTaxableAmount.Text) ? "0" : textBoxTaxableAmount.Text));
                cmd.Parameters.AddWithValue("@InvoiceTotalAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxInvoiceTotalAmount.Text) ? "0" : textBoxInvoiceTotalAmount.Text));
                cmd.Parameters.AddWithValue("@InvoiceStatus", Convert.ToString("").Trim());
                cmd.Parameters.AddWithValue("@CustomerNotes", Convert.ToString(textBoxCustomerNotes.Text).Trim());
                cmd.Parameters.AddWithValue("@isCessApplicable", checkBoxIsCESSApplicable.Checked);
                cmd.Parameters.AddWithValue("@TermsAndCondition", Convert.ToString(textBoxTermsAndConditions.Text).Trim());
                cmd.Parameters.AddWithValue("@CF5", Convert.ToString(textBoxCF5.Text).Trim());
                cmd.Parameters.AddWithValue("@CF6", Convert.ToString(textBoxCF6.Text).Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Entities.AuthenticationDetail.UserName);

                cmd.Parameters.AddWithValue("@CESSPercentTotal", Convert.ToDecimal(String.IsNullOrEmpty(textBoxCESSPercent.Text) ? "0" : textBoxCESSPercent.Text));
                cmd.Parameters.AddWithValue("@SGSTPercentTotal", Convert.ToDecimal(String.IsNullOrEmpty(textBoxSGSTPercent.Text) ? "0" : textBoxSGSTPercent.Text));
                cmd.Parameters.AddWithValue("@CGSTPercentTotal", Convert.ToDecimal(String.IsNullOrEmpty(textBoxCGSTPercent.Text) ? "0" : textBoxCGSTPercent.Text));
                cmd.Parameters.AddWithValue("@IGSTPercentTotal", Convert.ToDecimal(String.IsNullOrEmpty(textBoxIGSTPercent.Text) ? "0" : textBoxIGSTPercent.Text));
                cmd.Parameters.AddWithValue("@DiscountPercentTotal", Convert.ToDecimal(String.IsNullOrEmpty(textBoxDiscountPercent.Text) ? "0" : textBoxDiscountPercent.Text));
                cmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalDiscountAmount.Text) ? "0" : textBoxTotalDiscountAmount.Text));
                cmd.Parameters.AddWithValue("@TaxableAmountWithDisc", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTaxableAmountWithDisc.Text) ? "0" : textBoxTaxableAmountWithDisc.Text));
                cmd.Parameters.AddWithValue("@Category", radioButtonB2B.Checked ? "B2B" : "B2C");

                using (cmd)
                {
                    identityInserted = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return identityInserted;
        }

        //Need to do workout : Extension method 
        //public static string ZeroIfEmpty(this string s)
        //{
        //    return string.IsNullOrEmpty(s) ? "0" : s;
        //}

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID != 0)
                {
                    if (comboBoxCompany.Text.Trim() != "" && comboBoxCustomer.Text.Trim() != "")
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            deleteItem();
                            MessageBox.Show("Record Deleted Successfully", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //DisplayData();
                            ClearData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Details!", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No Invoice found!", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "Trans_Invoice_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceHeaderId", ID);
                cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                    cmd.Parameters.AddWithValue("@StockCompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                else
                    cmd.Parameters.AddWithValue("@StockCompanyID", 0);
                cmd.Parameters.AddWithValue("@mode", "SalesInvoice-Delete");
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("SalesInvoice", Convert.ToInt16(comboBoxCompany.SelectedValue));
                reportViewer.MdiParent = this.MdiParent;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayData()
        {
            flagDisplayData = true;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand(@"SELECT [CompanyID],[InvoiceNumber],[InvoiceDate],[InvoiceDueDate]
                     ,[CustomerID],AgentID,Invoice.CustomerName,CustomerAddress,Invoice.ContactPerson
                    ,CustomerMobile,CustomerPhone,CustomerEmail,CustomerState
                     ,[PONumber],[PODate],[CGSTAmount],[SGSTAmount] 
                     ,[IGSTAmount],[CESSAmount],[TaxableAmount],[InvoiceTotalAmount],[InvoiceStatus] 
                     ,[CustomerNotes],TermsAndCondition, isCessApplicable,[CF1],[CF2],[CF3],[CF4],CF5, CF6
                    , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
                    , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount, TaxableAmountWithDisc, Category
                     FROM [TranSalesInvoice] Invoice where Invoice.ID = @ID and Invoice.Del_State=0", con);
                cmd.Parameters.AddWithValue("@ID", ID);

                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            //Weight - CF1
                            //ModeOfTransport - CF2
                            //LRNumber - CF3
                            //BaleNumber - CF4
                            //AgentName - CF5
                            comboBoxCompany.SelectedValue = Convert.ToInt16(reader["CompanyID"]);
                            textBoxInvoiceNumber.Text = Convert.ToString(reader["InvoiceNumber"]);
                            dateTimePickerInvoiceDate.Value = Convert.ToDateTime(reader["InvoiceDate"]);
                            dateTimePickerDueDate.Value = Convert.ToDateTime(reader["InvoiceDueDate"]);
                            comboBoxCustomer.SelectedValue = Convert.ToInt32(reader["CustomerId"]);
                            comboBoxAgent.SelectedValue = Convert.ToInt32(reader["AgentId"]);
                            textBoxCustomer.Text = Convert.ToString(reader["CustomerName"]);
                            textBoxBillingAddress.Text = Convert.ToString(reader["CustomerAddress"]);
                            textBoxContactPerson.Text = Convert.ToString(reader["ContactPerson"]);
                            textBoxMobile.Text = Convert.ToString(reader["CustomerMobile"]);
                            textBoxPhone.Text = Convert.ToString(reader["CustomerPhone"]);
                            textBoxEmail.Text = Convert.ToString(reader["CustomerEmail"]);
                            comboBoxState.Text = Convert.ToString(reader["CustomerState"]);
                            textBoxPONumber.Text = Convert.ToString(reader["PONumber"]);
                            dateTimePickerPODate.Value = Convert.ToDateTime(reader["PODate"]);
                            textBoxTotalCGSTAmount.Text = Convert.ToString(reader["CGSTAmount"]);
                            textBoxTotalSGSTAmount.Text = Convert.ToString(reader["SGSTAmount"]);
                            textBoxTotalIGSTAmount.Text = Convert.ToString(reader["IGSTAmount"]);
                            textBoxTotalCESSAmount.Text = Convert.ToString(reader["CESSAmount"]);
                            textBoxTaxableAmount.Text = Convert.ToString(reader["TaxableAmount"]);
                            textBoxInvoiceTotalAmount.Text = Convert.ToString(reader["InvoiceTotalAmount"]);
                            textBoxCustomerNotes.Text = Convert.ToString(reader["CustomerNotes"]);
                            textBoxTermsAndConditions.Text = Convert.ToString(reader["TermsAndCondition"]);
                            checkBoxIsCESSApplicable.Checked = Convert.ToBoolean(reader["isCessApplicable"]);
                            textBoxCF1.Text = Convert.ToString(reader["CF1"]);
                            textBoxCF2.Text = Convert.ToString(reader["CF2"]);
                            textBoxCF3.Text = Convert.ToString(reader["CF3"]);
                            textBoxCF4.Text = Convert.ToString(reader["CF4"]);
                            textBoxCF5.Text = Convert.ToString(reader["CF5"]);
                            textBoxCF6.Text = Convert.ToString(reader["CF6"]);

                            textBoxCESSPercent.Text = Convert.ToString(reader["CESSPercentTotal"]);
                            textBoxSGSTPercent.Text = Convert.ToString(reader["SGSTPercentTotal"]);
                            textBoxCGSTPercent.Text = Convert.ToString(reader["CGSTPercentTotal"]);
                            textBoxIGSTPercent.Text = Convert.ToString(reader["IGSTPercentTotal"]);
                            textBoxDiscountPercent.Text = Convert.ToString(reader["DiscountPercentTotal"]);
                            textBoxTotalDiscountAmount.Text = Convert.ToString(reader["DiscountAmount"]);
                            textBoxTaxableAmountWithDisc.Text = Convert.ToString(reader["TaxableAmountWithDisc"]);
                            if (Convert.ToString(reader["Category"]) == "B2B")
                            {
                                radioButtonB2B.Checked = true;
                            }
                            else
                            {
                                radioButtonB2C.Checked = true;
                            }
                        }
                    }

                    DataSet dsInvoiceItems = new DataSet();

                    using (SqlCommand cmd = new SqlCommand(@"SELECT IVItem.ItemId, IVItem.ItemDescription
                    ,IVItem.ItemCustomDescription,IVItem.ItemType,IVItem.HsnSac,[Qty] 
                    ,IVItem.UoM,[RatePerItem],isnull(IVItem.DiscountPercent,0) DiscountPercent,[Discount],[TaxableValue] 
                    ,CGSTPercent,[CGSTValue],SGSTPercent,[SGSTValue],IGSTPercent,[IGSTValue] 
                    ,CESSPercent,[CESSValue],[TotalValue], isnull(Stock.StockInHand,0) StockInHand
                    ,IsTaxIncluded,CFItem1,CFItem2
                    FROM [TranSalesInvoiceItems] IVItem 
                    Inner join MasterItem Item on IVItem.ItemID = Item.ID
                    left join MasterStockList Stock 
                    on IVItem.ItemID=Stock.ID and Stock.CompanyID=@CompanyID where SalesInvoiceHeaderID = @ID and IVItem.Del_State=0 order by IVItem.ID "))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                            cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                        else
                            cmd.Parameters.AddWithValue("@CompanyID", 0);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (dsInvoiceItems)
                            {
                                sda.Fill(dsInvoiceItems);
                            }
                        }
                    }
                    using (dsInvoiceItems)
                    {
                        Int16 i = 0;
                        foreach (DataRow dr in dsInvoiceItems.Tables[0].Rows)
                        {
                            dataGridViewList.Rows.Add();

                            dataGridViewList["ItemId", i].Value = (int)dr["ItemId"];
                            dataGridViewList["ItemDescription", i].Value = (string)dr["ItemDescription"];
                            dataGridViewList["ItemCustomDescription", i].Value = (string)dr["ItemCustomDescription"];
                            dataGridViewList["ItemType", i].Value = (string)dr["ItemType"];
                            dataGridViewList["HsnSac", i].Value = (string)dr["HsnSac"];
                            dataGridViewList["UoM", i].Value = (string)dr["UoM"];
                            dataGridViewList["Quantity", i].Value = (decimal)dr["Qty"];

                            if (!isUomOthers((string)dr["UoM"]))
                            {
                                dataGridViewList["ItemStockInHand", i].Value = (decimal)dr["StockInHand"];
                                dataGridViewList["BUQty", i].Value = (decimal)dr["Qty"];
                            }
                            dataGridViewList["RatePerItem", i].Value = (decimal)dr["RatePerItem"];
                            dataGridViewList["DiscountPercent", i].Value = Convert.ToDecimal(dr["DiscountPercent"]);
                            dataGridViewList["Discount", i].Value = (decimal)dr["Discount"];
                            dataGridViewList["TaxableValue", i].Value = (decimal)dr["TaxableValue"];
                            dataGridViewList["CGSTPercent", i].Value = (decimal)dr["CGSTPercent"];
                            dataGridViewList["CGSTValue", i].Value = (decimal)dr["CGSTValue"];
                            dataGridViewList["SGSTPercent", i].Value = (decimal)dr["SGSTPercent"];
                            dataGridViewList["SGSTValue", i].Value = (decimal)dr["SGSTValue"];
                            dataGridViewList["IGSTPercent", i].Value = (decimal)dr["IGSTPercent"];
                            dataGridViewList["IGSTValue", i].Value = (decimal)dr["IGSTValue"];
                            dataGridViewList["CESSPercent", i].Value = (decimal)dr["CESSPercent"];
                            dataGridViewList["CESSValue", i].Value = (decimal)dr["CESSValue"];
                            dataGridViewList["Total", i].Value = (decimal)dr["TotalValue"];

                            dataGridViewList["IsTaxIncluded", i].Value = (bool)dr["IsTaxIncluded"];
                            dataGridViewList["CFItem1", i].Value = (string)dr["CFItem1"];
                            dataGridViewList["CFItem2", i].Value = (string)dr["CFItem2"];
                            i++;
                        }
                    }
                }
            }
            flagDisplayData = false;
        }

        private void ClearData()
        {
            isIGSTApplicable = false;
            ID = 0;
            setAutoSequenceNumber("NewInvoice");
            comboBoxCustomer.SelectedIndex = 0;
            //textBoxInvoiceNumber.Text = "";
            dateTimePickerInvoiceDate.Value = DateTime.Now;
            dateTimePickerDueDate.Value = DateTime.Now;

            textBoxCustomer.Text = string.Empty;
            textBoxBillingAddress.Text = string.Empty;
            textBoxContactPerson.Text = string.Empty;
            textBoxMobile.Text = string.Empty;
            textBoxPhone.Text = string.Empty;
            textBoxEmail.Text = string.Empty;

            textBoxPONumber.Text = "";
            textBoxCF6.Text = "";
            textBoxCF5.Text = "";
            textBoxCF2.Text = "";
            textBoxCF4.Text = "";
            textBoxCF1.Text = "";
            textBoxTotalCGSTAmount.Text = "";
            textBoxTotalSGSTAmount.Text = "";
            textBoxTotalIGSTAmount.Text = "";
            textBoxTotalCESSAmount.Text = "";
            textBoxTaxableAmount.Text = "";
            textBoxInvoiceTotalAmount.Text = "";
            textBoxCustomerNotes.Text = "";
            //textBoxTermsAndConditions.Text = "";
            textBoxCF3.Text = "";
            dataGridViewList.Rows.Clear();

            textBoxCESSPercent.Clear();
            textBoxSGSTPercent.Clear();
            textBoxCGSTPercent.Clear();
            textBoxIGSTPercent.Clear();
            textBoxDiscountPercent.Clear();
            textBoxTotalDiscountAmount.Clear();
            textBoxTaxableAmountWithDisc.Clear();
        }

        private void loadCompany()
        {
            comboBoxCompany.ValueMember = "ID";
            comboBoxCompany.DisplayMember = "CompanyName";
            comboBoxCompany.DataSource = masterSelection.GetCompanyList();

            comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void loadCustomer()
        {
            comboBoxCustomer.ValueMember = "ID";
            comboBoxCustomer.DisplayMember = "CustomerName";
            comboBoxCustomer.DataSource = masterSelection.GetCustomerList("Buyer");

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

        private void fillItemInGridRow()
        {
            int rowIndex = dataGridViewList.Rows.Count - 1;
            PopupControl.GenericPopup popup = new PopupControl.GenericPopup();
            popup.PopupQueryString = @"select Item.ID
                                    ,[Group].GroupName [Group Under]
                                    ,[ItemDescription] [Item Name]
                                    ,isnull(Stock.StockInHand,0) Stock
                                    ,UoMDisplayName [UoM]
                                    ,[Size]
                                    ,[HsnSac] [HSN/SAC]
                                    ,[SellingPrice] [Rate]
                                    ,[PurchasePrice] 
                                    ,[DiscountPercent]
                                    ,DiscountAmount
                                    ,TaxRatePercent
                                    ,ItemType
                                    ,IsTaxIncluded
                                    from MasterItem Item inner join 
                                    vw_MasterGroup [Group] on Item.GroupIDUnder=[Group].ID
                                    left join MasterStockList Stock on Item.ID =Stock.ItemID and Stock.CompanyID={0} 
                                    Where Item.Del_State = 0 and ItemStatus='Active'
                                    and ([Group].GroupName like @FilterValue or ItemDescription like @FilterValue or Size like @FilterValue or UoM like @FilterValue or HsnSac like @FilterValue) 
                                    order by ID";
            if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                popup.PopupQueryString = string.Format(popup.PopupQueryString, Convert.ToInt16(comboBoxCompany.SelectedValue));
            else
                popup.PopupQueryString = string.Format(popup.PopupQueryString, 0);

            popup.Text = "Select Item";
            popup.Width = 710;
            popup.dataGridViewList.Width = 700;
            DialogResult dialogResult = popup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                dataGridViewList.Rows.Add();

                //0,Item.ID
                //1,[Group].GroupName[Group Under]
                //2,[ItemDescription]
                //3,StockInHand Stock
                //4,[UoM]
                //5,[Size]
                //6,[HsnSac] [HSN/SAC]
                //7,[SellingPrice]
                //8,[PurchasePrice] 
                //9,[DiscountPercent]
                //10,DiscountAmount
                //11,TaxRatePercent
                //12,ItemType
                //13,IsTaxIncluded
                dataGridViewList["ItemId", rowIndex].Value = popup.PopupItemId;
                dataGridViewList["ItemDescription", rowIndex].Value = popup.PopupItemProperty2;
                dataGridViewList["ItemType", rowIndex].Value = popup.PopupItemProperty12;
                dataGridViewList["HsnSac", rowIndex].Value = popup.PopupItemProperty6;
                dataGridViewList["Quantity", rowIndex].Value = 1;
                if (!isUomOthers(popup.PopupItemProperty4))
                {
                    dataGridViewList["BUQty", rowIndex].Value = 0;
                    dataGridViewList["ItemStockInHand", rowIndex].Value = popup.PopupItemProperty3;
                }

                dataGridViewList["UoM", rowIndex].Value = popup.PopupItemProperty4;
                dataGridViewList["IsTaxIncluded", rowIndex].Value = Convert.ToBoolean(popup.PopupItemProperty13);
                if (Convert.ToBoolean(dataGridViewList["IsTaxIncluded", rowIndex].Value) == true)
                {
                    dataGridViewList["RatePerItem", rowIndex].Value = BAL.GenericFucntions.calcTaxInclusiveRate(Convert.ToDecimal(popup.PopupItemProperty7), Convert.ToDecimal(popup.PopupItemProperty11));
                }
                else
                {
                    dataGridViewList["RatePerItem", rowIndex].Value = popup.PopupItemProperty7;
                }
                if (invoicewiseTaxCalc == "0")
                {
                    if (radioButtonPercentBasedDiscount.Checked)
                    {
                        dataGridViewList["DiscountPercent", rowIndex].Value = popup.PopupItemProperty9;
                    }
                    else
                    {
                        dataGridViewList["Discount", rowIndex].Value = popup.PopupItemProperty10;
                    }
                    if (CompanyGSTN != string.Empty)
                    {
                        if (isIGSTApplicable == false)
                        {
                            dataGridViewList["SGSTPercent", rowIndex].Value = Convert.ToDecimal(popup.PopupItemProperty11) / 2;
                            dataGridViewList["CGSTPercent", rowIndex].Value = Convert.ToDecimal(popup.PopupItemProperty11) / 2;
                        }
                        else
                        {
                            dataGridViewList["IGSTPercent", rowIndex].Value = popup.PopupItemProperty11;
                        }
                    }
                    else
                    {
                        dataGridViewList["SGSTPercent", rowIndex].Value = 0;
                        dataGridViewList["CGSTPercent", rowIndex].Value = 0;
                        dataGridViewList["IGSTPercent", rowIndex].Value = 0;
                    }
                }
                dataGridViewList.Select();
                dataGridViewList["ItemCustomDescription", dataGridViewList.Rows.Count - 2].Selected = true;
            }
            popup.Dispose();
        }

        private bool isUomOthers(string uom)
        {
            if (uom.ToLower().Contains("other"))
                return true;
            else
                return false;
        }

        private void taxChangesInGrid()
        {
            string customerStateCurrent = BAL.GenericFucntions.GetStateFromCustomerName(comboBoxCustomer.Text, '|');
            if (!newCustomerRegistrationFlag)
            {
                dataGridViewList.Enabled = true;
                if (CompanyGSTN != string.Empty)
                {

                    if (comboBoxCustomer.SelectedIndex != 0)
                    {
                        if (customerStateBeforeUpdate != customerStateCurrent)
                        {
                            dataGridViewList.Rows.Clear();
                            if (BAL.GenericFucntions.GetStateFromCustomerName(comboBoxCompany.Text, '|') == customerStateCurrent)
                            {
                                gridViewStructureForCGSTSGST();
                            }
                            else
                            {
                                gridViewStructureForIGST();
                            }
                        }
                    }
                    else
                    {
                        dataGridViewList.Rows.Clear();
                        if (BAL.GenericFucntions.GetStateFromCustomerName(comboBoxCompany.Text, '|') == comboBoxState.Text.ToUpper())
                        {
                            gridViewStructureForCGSTSGST();
                        }
                        else
                        {
                            gridViewStructureForIGST();
                        }
                    }

                }
                else
                {
                    disableTaxStructure();
                }

                if (quickCustomerInfoDisplay == "0")
                {
                    if (comboBoxCustomer.SelectedIndex == 0)
                    {
                        dataGridViewList.Rows.Clear();
                        dataGridViewList.Enabled = false;
                    }
                }
            }
            customerStateBeforeUpdate = customerStateCurrent;
        }

        private void gridViewStructureForCGSTSGST()
        {
            isIGSTApplicable = false;

            textBoxTotalCGSTAmount.Visible = true;
            textBoxTotalSGSTAmount.Visible = true;
            textBoxTotalIGSTAmount.Visible = false;

            labelTotalCGSTAmount.Visible = true;
            labelTotalSGSTAmount.Visible = true;
            labelTotalIGSTAmount.Visible = false;
            if (invoicewiseTaxCalc == "1")
            {
                dataGridViewList.Columns["CGSTPercent"].Visible = false;
                dataGridViewList.Columns["CGSTValue"].Visible = false;
                dataGridViewList.Columns["SGSTPercent"].Visible = false;
                dataGridViewList.Columns["SGSTValue"].Visible = false;
                dataGridViewList.Columns["IGSTPercent"].Visible = false;
                dataGridViewList.Columns["IGSTValue"].Visible = false;

                dataGridViewList.Columns["Discount"].Visible = false;
                dataGridViewList.Columns["DiscountPercent"].Visible = false;

                dataGridViewList.Columns["Total"].Visible = false;

                textBoxCGSTPercent.Visible = true;
                textBoxSGSTPercent.Visible = true;
                textBoxIGSTPercent.Visible = false;

                labelCGSTPercent.Visible = true;
                labelSGSTPercent.Visible = true;
                labelIGSTPercent.Visible = false;

                textBoxCGSTPercent.Text = (Convert.ToDecimal(ConfigurationManager.AppSettings["SalesInvoiceWiseTax"].ToString()) / 2).ToString();
                textBoxSGSTPercent.Text = textBoxCGSTPercent.Text;
                textBoxIGSTPercent.Text = string.Empty;
            }
            else
            {
                dataGridViewList.Columns["CGSTPercent"].Visible = true;
                dataGridViewList.Columns["CGSTValue"].Visible = true;
                dataGridViewList.Columns["SGSTPercent"].Visible = true;
                dataGridViewList.Columns["SGSTValue"].Visible = true;
                dataGridViewList.Columns["IGSTPercent"].Visible = false;
                dataGridViewList.Columns["IGSTValue"].Visible = false;

                dataGridViewList.Columns["Discount"].Visible = true;
                dataGridViewList.Columns["DiscountPercent"].Visible = true;

                textBoxCGSTPercent.Visible = false;
                textBoxSGSTPercent.Visible = false;
                textBoxIGSTPercent.Visible = false;

                labelCGSTPercent.Visible = false;
                labelSGSTPercent.Visible = false;
                labelIGSTPercent.Visible = false;

                textBoxCGSTPercent.Text = string.Empty;
                textBoxSGSTPercent.Text = string.Empty;
                textBoxIGSTPercent.Text = string.Empty;

                textBoxTaxableAmountWithDisc.Text = string.Empty;
                textBoxTaxableAmountWithDisc.Visible = false;
                labelTaxableAmountWithDiscount.Visible = false;
            }

        }

        private void gridViewStructureForIGST()
        {

            isIGSTApplicable = true;

            textBoxTotalCGSTAmount.Visible = false;
            textBoxTotalSGSTAmount.Visible = false;
            textBoxTotalIGSTAmount.Visible = true;

            labelTotalCGSTAmount.Visible = false;
            labelTotalSGSTAmount.Visible = false;
            labelTotalIGSTAmount.Visible = true;

            if (invoicewiseTaxCalc == "1")
            {
                dataGridViewList.Columns["CGSTPercent"].Visible = false;
                dataGridViewList.Columns["CGSTValue"].Visible = false;
                dataGridViewList.Columns["SGSTPercent"].Visible = false;
                dataGridViewList.Columns["SGSTValue"].Visible = false;
                dataGridViewList.Columns["IGSTPercent"].Visible = false;
                dataGridViewList.Columns["IGSTValue"].Visible = false;

                dataGridViewList.Columns["Discount"].Visible = false;
                dataGridViewList.Columns["DiscountPercent"].Visible = false;

                dataGridViewList.Columns["Total"].Visible = false;

                textBoxCGSTPercent.Visible = false;
                textBoxSGSTPercent.Visible = false;
                textBoxIGSTPercent.Visible = true;
                textBoxDiscountPercent.Visible = true;

                labelCGSTPercent.Visible = false;
                labelSGSTPercent.Visible = false;
                labelIGSTPercent.Visible = true;
                labelDiscountPercent.Visible = true;

                textBoxCGSTPercent.Text = string.Empty;
                textBoxSGSTPercent.Text = string.Empty;
                textBoxIGSTPercent.Text = (Convert.ToDecimal(ConfigurationManager.AppSettings["SalesInvoiceWiseTax"].ToString())).ToString();
            }
            else
            {
                dataGridViewList.Columns["CGSTPercent"].Visible = false;
                dataGridViewList.Columns["CGSTValue"].Visible = false;
                dataGridViewList.Columns["SGSTPercent"].Visible = false;
                dataGridViewList.Columns["SGSTValue"].Visible = false;
                dataGridViewList.Columns["IGSTPercent"].Visible = true;
                dataGridViewList.Columns["IGSTValue"].Visible = true;

                dataGridViewList.Columns["Discount"].Visible = true;
                dataGridViewList.Columns["DiscountPercent"].Visible = true;

                textBoxCGSTPercent.Visible = false;
                textBoxSGSTPercent.Visible = false;
                textBoxIGSTPercent.Visible = false;
                textBoxDiscountPercent.Visible = false;

                labelCGSTPercent.Visible = false;
                labelSGSTPercent.Visible = false;
                labelIGSTPercent.Visible = false;
                labelDiscountPercent.Visible = false;

                textBoxCGSTPercent.Text = string.Empty;
                textBoxSGSTPercent.Text = string.Empty;
                textBoxIGSTPercent.Text = string.Empty;

                textBoxTaxableAmountWithDisc.Text = string.Empty;
                textBoxTaxableAmountWithDisc.Visible = false;
                labelTaxableAmountWithDiscount.Visible = false;
            }

        }

        //private void disableTaxStructure()
        //{
        //    //if (CompanyGSTN == string.Empty)
        //    //{
        //    dataGridViewList.Columns["CGSTPercent"].Visible = false;
        //    dataGridViewList.Columns["CGSTValue"].Visible = false;
        //    dataGridViewList.Columns["SGSTPercent"].Visible = false;
        //    dataGridViewList.Columns["SGSTValue"].Visible = false;
        //    dataGridViewList.Columns["IGSTPercent"].Visible = false;
        //    dataGridViewList.Columns["IGSTValue"].Visible = false;

        //    textBoxTotalCGSTAmount.Visible = false;
        //    textBoxTotalSGSTAmount.Visible = false;
        //    textBoxTotalIGSTAmount.Visible = false;

        //    labelTotalCGSTAmount.Visible = false;
        //    labelTotalSGSTAmount.Visible = false;
        //    labelTotalIGSTAmount.Visible = false;

        //    textBoxCGSTPercent.Visible = false;
        //    textBoxSGSTPercent.Visible = false;
        //    textBoxIGSTPercent.Visible = false;

        //    labelCGSTPercent.Visible = false;
        //    labelSGSTPercent.Visible = false;
        //    labelIGSTPercent.Visible = false;

        //    textBoxCGSTPercent.Text = string.Empty;
        //    textBoxSGSTPercent.Text = string.Empty;
        //    textBoxIGSTPercent.Text = string.Empty;

        //    checkBoxIsCESSApplicable.Enabled = false;
        //    //}
        //}

        private void disableTaxStructure()
        {
            textBoxTotalCGSTAmount.Visible = false;
            textBoxTotalSGSTAmount.Visible = false;
            textBoxTotalIGSTAmount.Visible = false;

            labelTotalCGSTAmount.Visible = false;
            labelTotalSGSTAmount.Visible = false;
            labelTotalIGSTAmount.Visible = false;

            if (invoicewiseTaxCalc == "1")
            {
                dataGridViewList.Columns["CGSTPercent"].Visible = false;
                dataGridViewList.Columns["CGSTValue"].Visible = false;
                dataGridViewList.Columns["SGSTPercent"].Visible = false;
                dataGridViewList.Columns["SGSTValue"].Visible = false;
                dataGridViewList.Columns["IGSTPercent"].Visible = false;
                dataGridViewList.Columns["IGSTValue"].Visible = false;

                dataGridViewList.Columns["Discount"].Visible = false;
                dataGridViewList.Columns["DiscountPercent"].Visible = false;

                dataGridViewList.Columns["Total"].Visible = false;

                textBoxCGSTPercent.Visible = false;
                textBoxSGSTPercent.Visible = false;
                textBoxIGSTPercent.Visible = false;
                textBoxDiscountPercent.Visible = true;

                labelCGSTPercent.Visible = false;
                labelSGSTPercent.Visible = false;
                labelIGSTPercent.Visible = false;
                labelDiscountPercent.Visible = true;

                textBoxCGSTPercent.Text = string.Empty;
                textBoxSGSTPercent.Text = string.Empty;
                textBoxIGSTPercent.Text = string.Empty;
            }
            else
            {
                dataGridViewList.Columns["CGSTPercent"].Visible = false;
                dataGridViewList.Columns["CGSTValue"].Visible = false;
                dataGridViewList.Columns["SGSTPercent"].Visible = false;
                dataGridViewList.Columns["SGSTValue"].Visible = false;
                dataGridViewList.Columns["IGSTPercent"].Visible = false;
                dataGridViewList.Columns["IGSTValue"].Visible = false;

                dataGridViewList.Columns["Discount"].Visible = true;
                dataGridViewList.Columns["DiscountPercent"].Visible = true;

                textBoxCGSTPercent.Visible = false;
                textBoxSGSTPercent.Visible = false;
                textBoxIGSTPercent.Visible = false;
                textBoxDiscountPercent.Visible = false;

                labelCGSTPercent.Visible = false;
                labelSGSTPercent.Visible = false;
                labelIGSTPercent.Visible = false;
                labelDiscountPercent.Visible = false;

                textBoxCGSTPercent.Text = string.Empty;
                textBoxSGSTPercent.Text = string.Empty;
                textBoxIGSTPercent.Text = string.Empty;

                textBoxTaxableAmountWithDisc.Text = string.Empty;
                textBoxTaxableAmountWithDisc.Visible = false;
                labelTaxableAmountWithDiscount.Visible = false;
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
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && !flagDisplayData)
            {
                //Need to do : Check visibility of column and if it is true then do calculation
                if (ConfigurationManager.AppSettings["SalesInvoiceQtyBasedOnCustomField1And2"].ToString() == "1" && (dataGridViewList.Columns[e.ColumnIndex].Name == "CFItem1" || dataGridViewList.Columns[e.ColumnIndex].Name == "CFItem2"))
                {
                    dataGridViewList["Quantity", e.RowIndex].Value =
                        Convert.ToDecimal(dataGridViewList["CFItem1", e.RowIndex].Value)
                    * Convert.ToDecimal(dataGridViewList["CFItem2", e.RowIndex].Value);
                }
                else if (dataGridViewList.Columns[e.ColumnIndex].Name == "Quantity")
                {
                    if (negativeStockAllowed == "0" && Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) >
                        (Convert.ToDecimal(dataGridViewList["ItemStockInHand", e.RowIndex].Value) +
                        Convert.ToDecimal(dataGridViewList["BUQty", e.RowIndex].Value))
                        )
                    {
                        MessageBox.Show(string.Format("You can enter Quantity upto '{0}' for the item '{1}'",
                            (Convert.ToDecimal(dataGridViewList["ItemStockInHand", e.RowIndex].Value) + Convert.ToDecimal(dataGridViewList["BUQty", e.RowIndex].Value))
                            , dataGridViewList["ItemDescription", e.RowIndex].Value.ToString())
                            , "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewList["Quantity", e.RowIndex].Value = dataGridViewList["BUQty", e.RowIndex].Value;
                        return;
                    }
                    else if (negativeStockAllowed == "0")
                    {
                        decimal actualStockInHand = stockInHand(Convert.ToDecimal(dataGridViewList["ItemStockInHand", e.RowIndex].Value),
                             Convert.ToInt16(dataGridViewList["ItemId", e.RowIndex].Value));
                        if (actualStockInHand > 0)
                        {
                            MessageBox.Show(string.Format("Cumulative Quantity for the item '{1}' exceeds the available stock '{0}' ",
                               actualStockInHand, dataGridViewList["ItemDescription", e.RowIndex].Value.ToString())
                                , "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dataGridViewList["Quantity", e.RowIndex].Value = dataGridViewList["BUQty", e.RowIndex].Value;
                            return;
                        }
                    }
                    dataGridViewList["TaxableValue", e.RowIndex].Value =
                        Math.Round((Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) *
                        Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value)) -
                        Convert.ToDecimal(dataGridViewList["Discount", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);

                    dataGridViewList["Discount", e.RowIndex].Value = BAL.GenericFucntions.calcDiscountAmountFromPercent(Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) *
                    Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value), Convert.ToDecimal(dataGridViewList["DiscountPercent", e.RowIndex].Value));
                }
                else if (dataGridViewList.Columns[e.ColumnIndex].Name == "RatePerItem")
                {
                    dataGridViewList["TaxableValue", e.RowIndex].Value =
                        Math.Round((Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) *
                        Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value)) -
                        Convert.ToDecimal(dataGridViewList["Discount", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);

                    dataGridViewList["Discount", e.RowIndex].Value = BAL.GenericFucntions.calcDiscountAmountFromPercent(Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) *
                    Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value), Convert.ToDecimal(dataGridViewList["DiscountPercent", e.RowIndex].Value));
                }
                else if (dataGridViewList.Columns[e.ColumnIndex].Name == "DiscountPercent")
                {
                    if (radioButtonPercentBasedDiscount.Checked == true)
                    {
                        dataGridViewList["Discount", e.RowIndex].Value = BAL.GenericFucntions.calcDiscountAmountFromPercent(Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) *
                        Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value), Convert.ToDecimal(dataGridViewList["DiscountPercent", e.RowIndex].Value));
                    }
                }
                else if (dataGridViewList.Columns[e.ColumnIndex].Name == "Discount")
                {
                    dataGridViewList["TaxableValue", e.RowIndex].Value =
                        Math.Round((Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) *
                        Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value)) -
                        Convert.ToDecimal(dataGridViewList["Discount", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);
                    if (radioButtonAmountBasedDiscount.Checked == true)
                    {
                        dataGridViewList["DiscountPercent", e.RowIndex].Value = BAL.GenericFucntions.calcDiscountPercentFromAmount(Convert.ToDecimal(dataGridViewList["Quantity", e.RowIndex].Value) * Convert.ToDecimal(dataGridViewList["RatePerItem", e.RowIndex].Value)
              , Convert.ToDecimal(dataGridViewList["Discount", e.RowIndex].Value));
                    }
                }

                if (dataGridViewList.Columns[e.ColumnIndex].Name == "SGSTPercent" || dataGridViewList.Columns[e.ColumnIndex].Name == "TaxableValue")
                {
                    if (isIGSTApplicable == false)
                    {
                        dataGridViewList["SGSTValue", e.RowIndex].Value =
                        Math.Round((Convert.ToDecimal(dataGridViewList["TaxableValue", e.RowIndex].Value) / 100) *
                        Convert.ToDecimal(dataGridViewList["SGSTPercent", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);
                        calcTotalSGSTValue();
                    }
                }
                if (dataGridViewList.Columns[e.ColumnIndex].Name == "CGSTPercent" || dataGridViewList.Columns[e.ColumnIndex].Name == "TaxableValue")
                {
                    if (isIGSTApplicable == false)
                    {
                        dataGridViewList["CGSTValue", e.RowIndex].Value =
                        Math.Round((Convert.ToDecimal(dataGridViewList["TaxableValue", e.RowIndex].Value) / 100) *
                        Convert.ToDecimal(dataGridViewList["CGSTPercent", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);
                        calcTotalCGSTValue();
                    }
                }
                if (dataGridViewList.Columns[e.ColumnIndex].Name == "IGSTPercent" || dataGridViewList.Columns[e.ColumnIndex].Name == "TaxableValue")
                {
                    if (isIGSTApplicable == true)
                    {
                        dataGridViewList["IGSTValue", e.RowIndex].Value =
                            Math.Round((Convert.ToDecimal(dataGridViewList["TaxableValue", e.RowIndex].Value) / 100) *
                            Convert.ToDecimal(dataGridViewList["IGSTPercent", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);
                        calcTotalIGSTValue();
                    }
                }
                if (dataGridViewList.Columns[e.ColumnIndex].Name == "CESSPercent" || dataGridViewList.Columns[e.ColumnIndex].Name == "TaxableValue")
                {
                    if (checkBoxIsCESSApplicable.Checked)
                    {
                        dataGridViewList["CESSValue", e.RowIndex].Value =
                        Math.Round((Convert.ToDecimal(dataGridViewList["TaxableValue", e.RowIndex].Value) / 100) *
                        Convert.ToDecimal(dataGridViewList["CESSPercent", e.RowIndex].Value), 2, MidpointRounding.AwayFromZero);
                        calcTotalCESSValue();
                    }
                }
                dataGridViewList["Total", e.RowIndex].Value =
                    Convert.ToDecimal(dataGridViewList["TaxableValue", e.RowIndex].Value)
                    + Convert.ToDecimal(dataGridViewList["SGSTValue", e.RowIndex].Value)
                    + Convert.ToDecimal(dataGridViewList["CGSTValue", e.RowIndex].Value)
                    + Convert.ToDecimal(dataGridViewList["IGSTValue", e.RowIndex].Value)
                    + Convert.ToDecimal(dataGridViewList["CESSValue", e.RowIndex].Value);
                calcTotalInvoiceValues();
            }
        }

        private void dataGridViewList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            calcTotalInvoiceValues();
            setRowNumber();
        }
        /// <summary>
        /// Vefies itemwise quantity against available stock .. to do need to sent total stock as an output
        /// </summary>
        private decimal stockInHand(decimal stockInHand, Int16 itemId)
        {
            decimal totalItemQty = 0;
            stockInHand = getActualStockInHand(stockInHand, itemId);
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                if (Convert.ToInt16(dataGridViewList["ItemId", i].Value) == itemId)
                {
                    totalItemQty = totalItemQty + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Quantity", i].Value)) ? "0" : dataGridViewList["Quantity", i].Value);
                    if (totalItemQty > stockInHand)
                    {
                        return stockInHand;
                    }
                }
            }
            return 0;
        }

        private decimal getActualStockInHand(decimal StockFromDB, Int16 itemId)
        {
            decimal actualStockInHand = StockFromDB;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                if (Convert.ToInt16(dataGridViewList["ItemId", i].Value) == itemId)
                {
                    actualStockInHand = actualStockInHand + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["BUQty", i].Value)) ? "0" : dataGridViewList["BUQty", i].Value);
                }
            }
            return actualStockInHand;
        }

        private void calcTotalCGSTValue()
        {
            decimal totalGSTValue = 0;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                totalGSTValue = totalGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CGSTValue", i].Value)) ? "0" : dataGridViewList["CGSTValue", i].Value);
            }
            textBoxTotalCGSTAmount.Text = Convert.ToString(Math.Round(totalGSTValue, 2, MidpointRounding.AwayFromZero));
        }

        private void calcTotalSGSTValue()
        {
            decimal totalGSTValue = 0;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                totalGSTValue = totalGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["SGSTValue", i].Value)) ? "0" : dataGridViewList["SGSTValue", i].Value);
            }
            textBoxTotalSGSTAmount.Text = Convert.ToString(Math.Round(totalGSTValue, 2, MidpointRounding.AwayFromZero));
        }

        private void calcTotalIGSTValue()
        {
            decimal totalGSTValue = 0;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                totalGSTValue = totalGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["IGSTValue", i].Value)) ? "0" : dataGridViewList["IGSTValue", i].Value);
            }
            textBoxTotalIGSTAmount.Text = Convert.ToString(Math.Round(totalGSTValue, 2, MidpointRounding.AwayFromZero));
        }

        private void calcTotalCESSValue()
        {
            decimal totalGSTValue = 0;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                totalGSTValue = totalGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CESSValue", i].Value)) ? "0" : dataGridViewList["CESSValue", i].Value);
            }
            textBoxTotalCESSAmount.Text = Convert.ToString(Math.Round(totalGSTValue, 2, MidpointRounding.AwayFromZero));
        }

        private void calcTotalInvoiceValues()
        {

            decimal totalGSTValue = 0;
            decimal totalTaxableValue = 0;

            decimal totalCGSTValue = 0;
            decimal totalSGSTValue = 0;
            decimal totalIGSTValue = 0;
            decimal totalCESSValue = 0;
            decimal totalDiscountValue = 0;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                totalGSTValue = totalGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Total", i].Value)) ? "0" : dataGridViewList["Total", i].Value);
                totalTaxableValue = totalTaxableValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["TaxableValue", i].Value)) ? "0" : dataGridViewList["TaxableValue", i].Value);
                if (invoicewiseTaxCalc == "0")
                {
                    totalCGSTValue = totalCGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CGSTValue", i].Value)) ? "0" : dataGridViewList["CGSTValue", i].Value);
                    totalSGSTValue = totalSGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["SGSTValue", i].Value)) ? "0" : dataGridViewList["SGSTValue", i].Value);
                    totalIGSTValue = totalIGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["IGSTValue", i].Value)) ? "0" : dataGridViewList["IGSTValue", i].Value);
                    totalCESSValue = totalCESSValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CESSValue", i].Value)) ? "0" : dataGridViewList["CESSValue", i].Value);

                    totalDiscountValue = totalDiscountValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Discount", i].Value)) ? "0" : dataGridViewList["Discount", i].Value);
                }
            }
            textBoxTaxableAmount.Text = Convert.ToString(Math.Round(totalTaxableValue, 2, MidpointRounding.AwayFromZero));
            if (invoicewiseTaxCalc == "0")
            {
                textBoxTotalCGSTAmount.Text = Convert.ToString(Math.Round(totalCGSTValue, 2, MidpointRounding.AwayFromZero));
                textBoxTotalSGSTAmount.Text = Convert.ToString(Math.Round(totalSGSTValue, 2, MidpointRounding.AwayFromZero));
                textBoxTotalIGSTAmount.Text = Convert.ToString(Math.Round(totalIGSTValue, 2, MidpointRounding.AwayFromZero));
                textBoxTotalCESSAmount.Text = Convert.ToString(Math.Round(totalCESSValue, 2, MidpointRounding.AwayFromZero));
                textBoxInvoiceTotalAmount.Text = Convert.ToString(Math.Round(totalGSTValue, 2, MidpointRounding.AwayFromZero));

                textBoxTotalDiscountAmount.Text = Convert.ToString(Math.Round(totalDiscountValue, 2, MidpointRounding.AwayFromZero));
            }

        }

        private void buttonCancelInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID != 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CancelItem();
                        MessageBox.Show("Invoice canceled successfully", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayData();
                        ClearData();
                    }
                }
                else
                {
                    MessageBox.Show("No Invoice found!", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand("Update [TranSalesInvoice] set InvoiceStatus='Canceled' Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void checkBoxIsCESSApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsCESSApplicable.Checked)
            {
                if (invoicewiseTaxCalc == "1")
                {
                    dataGridViewList.Columns["CESSPercent"].Visible = false;
                    dataGridViewList.Columns["CESSValue"].Visible = false;
                    labelTotalCESSAmount.Visible = true;
                    textBoxTotalCESSAmount.Visible = true;
                    labelCESSPercent.Visible = true;
                    textBoxCESSPercent.Visible = true;
                }
                else
                {
                    dataGridViewList.Columns["CESSPercent"].Visible = true;
                    dataGridViewList.Columns["CESSValue"].Visible = true;
                    labelTotalCESSAmount.Visible = true;
                    textBoxTotalCESSAmount.Visible = true;
                    labelCESSPercent.Visible = false;
                    textBoxCESSPercent.Visible = false;
                }
            }
            else
            {
                dataGridViewList.Columns["CESSPercent"].Visible = false;
                dataGridViewList.Columns["CESSValue"].Visible = false;
                labelTotalCESSAmount.Visible = false;
                textBoxTotalCESSAmount.Visible = false;
                labelCESSPercent.Visible = false;
                textBoxCESSPercent.Visible = false;
            }
        }

        private void displayPendingQuotation()
        {
            PopupControl.GenericPopup popup = new PopupControl.GenericPopup();
            popup.PopupQueryString = @"select * from (
            SELECT Quotation.ID,QuotationNumber [Quotation #]
            ,cust.CustomerName [Customer Name],Cust.State [Customer State]
            ,QuotationDate [Quotation Date], [QuotationDueDate] [Quotation Due Date],isCessApplicable
            ,[CustomerID],[CompanyID], '' [CustomerAddress],'' [ContactPerson],'' [CustomerMobile],'' [CustomerPhone],'' [CustomerEmail]
            FROM TranQuotation Quotation inner join MasterCustomer Cust on Quotation.CustomerID = Cust.ID 
            Where Quotation.Del_State = 0 and Quotation.CompanyId={0} 
            and (QuotationNumber like @FilterValue or Cust.CustomerName like @FilterValue or State like @FilterValue or QuotationDate like @FilterValue) 
            and customerId<>0
            union
            SELECT Quotation.ID,QuotationNumber [Quotation #]
            ,CustomerName [Customer Name],CustomerState [Customer State]
            ,QuotationDate [Quotation Date], [QuotationDueDate] [Quotation Due Date],isCessApplicable
            ,[CustomerID],[CompanyID],  [CustomerAddress],[ContactPerson],[CustomerMobile],[CustomerPhone],[CustomerEmail]
            FROM TranQuotation Quotation 
            Where Quotation.Del_State = 0 and Quotation.CompanyId={0} 
            and (QuotationNumber like @FilterValue or CustomerName like @FilterValue or CustomerState like @FilterValue or QuotationDate like @FilterValue) 
            and customerId=0
            ) tt
            order by ID desc";
            popup.PopupQueryString = string.Format(popup.PopupQueryString, Convert.ToInt16(comboBoxCompany.SelectedValue));
            popup.Text = "Select Quotation";
            DialogResult dialogResult = popup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                textBoxPONumber.Text = popup.PopupItemProperty1;
                dateTimePickerPODate.Value = Convert.ToDateTime(popup.PopupItemProperty4);
                checkBoxIsCESSApplicable.Checked = Convert.ToBoolean(popup.PopupItemProperty6);
                if (popup.PopupItemProperty7 != "0")
                {
                    comboBoxCustomer.SelectedValue = popup.PopupItemProperty7;
                }
                else
                {
                    textBoxCustomer.Text = popup.PopupItemProperty2;
                    textBoxBillingAddress.Text = popup.PopupItemProperty9;
                    textBoxContactPerson.Text = popup.PopupItemProperty10;
                    textBoxMobile.Text = popup.PopupItemProperty11;
                    textBoxPhone.Text = popup.PopupItemProperty12;
                    textBoxEmail.Text = popup.PopupItemProperty13;
                    comboBoxState.Text = popup.PopupItemProperty3;
                }
                displayQuotationItems(popup.PopupItemId);
            }
            popup.Dispose();
        }

        private void displayQuotationItems(Int32 quotationId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                DataSet dsInvoiceItems = new DataSet();

                using (SqlCommand cmd = new SqlCommand("SELECT ItemId, [ItemDescription],[ItemCustomDescription],ItemType,[HsnSac],[Qty] " +
     " ,[UoM],[RatePerItem],isnull(DiscountPercent,0) DiscountPercent,[Discount],[TaxableValue] " +
     " ,CGSTPercent,[CGSTValue],SGSTPercent,[SGSTValue],IGSTPercent,[IGSTValue] " +
     " ,CESSPercent,[CESSValue],[TotalValue] " +
        " FROM [TranQuotationItems] where HeaderID = @ID and Del_State=0 order by ID "))
                {
                    cmd.Parameters.AddWithValue("@ID", quotationId);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {

                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (dsInvoiceItems)
                        {
                            sda.Fill(dsInvoiceItems);
                        }
                    }
                }
                using (dsInvoiceItems)
                {
                    Int16 i = 0;
                    foreach (DataRow dr in dsInvoiceItems.Tables[0].Rows)
                    {
                        dataGridViewList.Rows.Add();

                        dataGridViewList["ItemId", i].Value = (int)dr["ItemId"];
                        dataGridViewList["ItemDescription", i].Value = (string)dr["ItemDescription"];
                        dataGridViewList["ItemCustomDescription", i].Value = (string)dr["ItemCustomDescription"];
                        dataGridViewList["ItemType", i].Value = (string)dr["ItemType"];
                        dataGridViewList["HsnSac", i].Value = (string)dr["HsnSac"];
                        dataGridViewList["Quantity", i].Value = (decimal)dr["Qty"];
                        dataGridViewList["UoM", i].Value = (string)dr["UoM"];
                        dataGridViewList["RatePerItem", i].Value = (decimal)dr["RatePerItem"];
                        dataGridViewList["DiscountPercent", i].Value = Convert.ToDecimal(dr["DiscountPercent"]);
                        dataGridViewList["Discount", i].Value = (decimal)dr["Discount"];
                        dataGridViewList["TaxableValue", i].Value = (decimal)dr["TaxableValue"];
                        dataGridViewList["CGSTPercent", i].Value = (decimal)dr["CGSTPercent"];
                        dataGridViewList["CGSTValue", i].Value = (decimal)dr["CGSTValue"];
                        dataGridViewList["SGSTPercent", i].Value = (decimal)dr["SGSTPercent"];
                        dataGridViewList["SGSTValue", i].Value = (decimal)dr["SGSTValue"];
                        dataGridViewList["IGSTPercent", i].Value = (decimal)dr["IGSTPercent"];
                        dataGridViewList["IGSTValue", i].Value = (decimal)dr["IGSTValue"];
                        dataGridViewList["CESSPercent", i].Value = (decimal)dr["CESSPercent"];
                        dataGridViewList["CESSValue", i].Value = (decimal)dr["CESSValue"];
                        dataGridViewList["Total", i].Value = (decimal)dr["TotalValue"];
                        i++;
                    }
                }
            }
        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            taxChangesInGrid();
        }

        private void comboBoxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            taxChangesInGrid();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonClearQuickCustomerInfo_Click(object sender, EventArgs e)
        {
            clearQuickCustomerInfo();
        }

        private void clearQuickCustomerInfo()
        {
            textBoxCustomer.Text = string.Empty;
            textBoxBillingAddress.Text = string.Empty;
            textBoxContactPerson.Text = string.Empty;
            textBoxMobile.Text = string.Empty;
            textBoxPhone.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            comboBoxState.SelectedIndex = 0;
        }

        private void loadState()
        {
            comboBoxState.ValueMember = "Code";
            comboBoxState.DisplayMember = "Name";
            comboBoxState.DataSource = masterSelection.GetStateList();
            //comboBoxState.SelectedIndex = 0;
        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            taxChangesInGrid();
        }

        private void buttonRegisterCustomer_Click(object sender, EventArgs e)
        {
            createNewCustomer();
        }


        private void createNewCustomer()
        {
            if (textBoxCustomer.Text.Trim() != string.Empty)
            {
                newCustomerRegistrationFlag = true;
                Int32 identityInserted = 0;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    cmd = new SqlCommand(@"INSERT INTO [MasterCustomer] 
                                    ([CustomerName],[BillingAddress],[State]
                                    ,[ContactPerson],[Email],[Phone],[Mobile],[CustomerStatus],CustomerType)
                                        output inserted.ID
                                     values(@CustomerName,@BillingAddress,@State
                                    ,@ContactPerson,@Email,@Phone,@Mobile,@CustomerStatus,@CustomerType)
                                    ", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@CustomerName", Convert.ToString(textBoxCustomer.Text).Trim());
                    cmd.Parameters.AddWithValue("@BillingAddress", Convert.ToString(textBoxBillingAddress.Text).Trim());
                    cmd.Parameters.AddWithValue("@State", Convert.ToString(comboBoxState.Text).Trim());
                    cmd.Parameters.AddWithValue("@ContactPerson", Convert.ToString(textBoxContactPerson.Text).Trim());
                    cmd.Parameters.AddWithValue("@Email", Convert.ToString(textBoxEmail.Text).Trim());
                    cmd.Parameters.AddWithValue("@Phone", Convert.ToString(textBoxPhone.Text).Trim());
                    cmd.Parameters.AddWithValue("@Mobile", Convert.ToString(textBoxMobile.Text).Trim());
                    cmd.Parameters.AddWithValue("@CustomerStatus", "Active");
                    cmd.Parameters.AddWithValue("@CustomerType", "Buyer");
                    using (cmd)
                    {
                        identityInserted = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                clearQuickCustomerInfo();
                loadCustomer();
                comboBoxCustomer.SelectedValue = identityInserted;
                tabPageInvoiceDetail.Select();
                newCustomerRegistrationFlag = false;
            }
        }

        private void radioButtonPercentBasedDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPercentBasedDiscount.Checked)
            {
                dataGridViewList.Columns["DiscountPercent"].ReadOnly = false;
                dataGridViewList.Columns["Discount"].ReadOnly = true;

                if (invoicewiseTaxCalc == "1")
                {
                    textBoxDiscountPercent.Enabled = true;
                    textBoxTotalDiscountAmount.Enabled = false;
                }
                else
                {
                    textBoxDiscountPercent.Enabled = false;
                    textBoxTotalDiscountAmount.Enabled = false;
                }
            }
        }

        private void radioButtonAmountBasedDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAmountBasedDiscount.Checked)
            {
                dataGridViewList.Columns["Discount"].ReadOnly = false;
                dataGridViewList.Columns["DiscountPercent"].ReadOnly = true;
                if (invoicewiseTaxCalc == "1")
                {
                    textBoxDiscountPercent.Enabled = false;
                    textBoxTotalDiscountAmount.Enabled = true;
                }
                else
                {
                    textBoxDiscountPercent.Enabled = false;
                    textBoxTotalDiscountAmount.Enabled = false;
                }
            }
        }

        private void SalesInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt && e.KeyCode == Keys.I) || e.KeyCode == Keys.F12)
            {
                if (comboBoxCustomer.SelectedIndex != 0 || textBoxCustomer.Text.Trim() != string.Empty)
                {
                    fillItemInGridRow();
                }
                else
                {
                    MessageBox.Show("Please choose a customer before item selection", "Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxCustomer.Select();
                }
            }
            else if (e.Alt == true && e.KeyCode == Keys.Q)
            {
                displayPendingQuotation();
                //e.Handled = true;
            }
        }

        private void buttonRefreshTermsAndCondition_Click(object sender, EventArgs e)
        {
            textBoxTermsAndConditions.Text = customFieldSelection.GetTermsAndConditions(Convert.ToInt16(comboBoxCompany.SelectedValue), "Sales Invoice");
        }

        private void SalesInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ID != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Exit Sales Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                e.Cancel = (dialogResult == DialogResult.No);
            }
        }

        private decimal getTotalTaxForItem(int rowInd)
        {
            decimal totalTax = 0;
            if (isIGSTApplicable == false)
            {
                totalTax = Convert.ToDecimal(dataGridViewList["SGSTPercent", rowInd].Value) + Convert.ToDecimal(dataGridViewList["CGSTPercent", rowInd].Value);
            }
            else
            {
                totalTax = Convert.ToDecimal(dataGridViewList["IGSTPercent", rowInd].Value);
            }
            return totalTax;
        }

        private decimal getItemRateFromUser()
        {
            decimal itemRate = 0;
            PopupControl.RatePopup popup = new PopupControl.RatePopup();

            popup.Text = "Item Rate";
            DialogResult dialogResult = popup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                itemRate = Convert.ToDecimal(popup.ItemRate);
            }
            return itemRate;
        }

        private string calcTaxableAmountWithDiscount(string strToalAmount, string strDiscountAmount)
        {

            decimal decTotalAmount = 0;
            decimal decDiscountAmount = 0;

            if (strToalAmount != string.Empty && strDiscountAmount != string.Empty)
            {
                decTotalAmount = Convert.ToDecimal(strToalAmount);
                decDiscountAmount = Convert.ToDecimal(strDiscountAmount);
            }
            else if (strDiscountAmount != string.Empty)
            {
                decTotalAmount = 0;
                decDiscountAmount = Convert.ToDecimal(strDiscountAmount);
            }
            else if (strToalAmount != string.Empty)
            {
                decTotalAmount = Convert.ToDecimal(strToalAmount);
                decDiscountAmount = 0;
            }
            else
            {
                decTotalAmount = 0;
                decDiscountAmount = 0;
            }

            return Convert.ToString(decTotalAmount - decDiscountAmount);
        }

        private void calcTotalInvoiceValueInvoiceWise()
        {
            textBoxInvoiceTotalAmount.Text =
                Convert.ToString(Convert.ToDecimal(String.IsNullOrEmpty(textBoxTaxableAmountWithDisc.Text) ? "0" : textBoxTaxableAmountWithDisc.Text) +
                Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalIGSTAmount.Text) ? "0" : textBoxTotalIGSTAmount.Text) +
                Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalCGSTAmount.Text) ? "0" : textBoxTotalCGSTAmount.Text) +
                Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalSGSTAmount.Text) ? "0" : textBoxTotalSGSTAmount.Text) +
                Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalCESSAmount.Text) ? "0" : textBoxTotalCESSAmount.Text));
        }

        private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (radioButtonPercentBasedDiscount.Checked == true)
                {
                    if (textBoxTaxableAmount.Text != string.Empty && textBoxDiscountPercent.Text != string.Empty)
                    {
                        textBoxTotalDiscountAmount.Text = Convert.ToString(BAL.GenericFucntions.calcDiscountAmountFromPercent
                            (Convert.ToDecimal(textBoxTaxableAmount.Text), Convert.ToDecimal(textBoxDiscountPercent.Text)));
                    }
                    else
                    {
                        textBoxTotalDiscountAmount.Text = string.Empty;
                    }
                }
            }
        }

        private void textBoxTotalDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (radioButtonAmountBasedDiscount.Checked == true)
                {
                    if (textBoxTaxableAmount.Text != string.Empty && textBoxTotalDiscountAmount.Text != string.Empty)
                    {
                        textBoxDiscountPercent.Text = Convert.ToString(BAL.GenericFucntions.calcDiscountPercentFromAmount
                            (Convert.ToDecimal(textBoxTaxableAmount.Text), Convert.ToDecimal(textBoxTotalDiscountAmount.Text)));
                    }
                    else
                    {
                        textBoxDiscountPercent.Text = string.Empty;
                    }
                }
                if (invoicewiseTaxCalc == "1")
                {
                    textBoxTaxableAmountWithDisc.Text = calcTaxableAmountWithDiscount(textBoxTaxableAmount.Text, textBoxTotalDiscountAmount.Text);
                }
                calcTotalInvoiceValueInvoiceWise();
            }
        }

        private void textBoxIGSTPercent_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (isIGSTApplicable)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxIGSTPercent.Text != string.Empty)
                    {
                        textBoxTotalIGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxIGSTPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                    else
                    {
                        textBoxTotalIGSTAmount.Text = string.Empty;
                    }
                    calcTotalInvoiceValueInvoiceWise();
                }
            }
        }

        private void textBoxCGSTPercent_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (!isIGSTApplicable)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxCGSTPercent.Text != string.Empty)
                    {
                        textBoxTotalCGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxCGSTPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                    else
                    {
                        textBoxTotalCGSTAmount.Text = string.Empty;
                    }
                }

                calcTotalInvoiceValueInvoiceWise();
            }
        }

        private void textBoxSGSTPercent_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (!isIGSTApplicable)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxSGSTPercent.Text != string.Empty)
                    {
                        textBoxTotalSGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxSGSTPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                    else
                    {
                        textBoxTotalSGSTAmount.Text = string.Empty;
                    }
                    calcTotalInvoiceValueInvoiceWise();
                }
            }
        }

        private void textBoxCESSPercent_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (checkBoxIsCESSApplicable.Checked)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxCESSPercent.Text != string.Empty)
                    {
                        textBoxTotalCESSAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxCESSPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                    else
                    {
                        textBoxTotalCESSAmount.Text = string.Empty;
                    }
                    calcTotalInvoiceValueInvoiceWise();
                }
            }
        }

        private void textBoxTaxableAmount_TextChanged(object sender, EventArgs e)
        {
            if (invoicewiseTaxCalc == "1")
            {
                textBoxTaxableAmountWithDisc.Text = calcTaxableAmountWithDiscount(textBoxTaxableAmount.Text, textBoxTotalDiscountAmount.Text);
            }
        }

        private void textBoxTaxableAmountWithDisc_TextChanged(object sender, EventArgs e)
        {
            if (!flagDisplayData)
            {
                if (isIGSTApplicable)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxIGSTPercent.Text != string.Empty)
                    {
                        textBoxTotalIGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxIGSTPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                }
                else if (!isIGSTApplicable)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxCGSTPercent.Text != string.Empty)
                    {
                        textBoxTotalCGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxCGSTPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxSGSTPercent.Text != string.Empty)
                    {
                        textBoxTotalSGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxSGSTPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                }
                if (checkBoxIsCESSApplicable.Checked)
                {
                    if (textBoxTaxableAmountWithDisc.Text != string.Empty && textBoxCESSPercent.Text != string.Empty)
                    {
                        textBoxTotalCGSTAmount.Text = Convert.ToString(
                        Math.Round((Convert.ToDecimal(textBoxTaxableAmountWithDisc.Text) / 100) *
                        Convert.ToDecimal(textBoxCESSPercent.Text), 2, MidpointRounding.AwayFromZero)
                    );
                    }
                }

                calcTotalInvoiceValueInvoiceWise();
            }

        }

        private void setRowNumber()
        {
            foreach (DataGridViewRow row in dataGridViewList.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void dataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setRowNumber();
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.R)
            {
                decimal itemRate = getItemRateFromUser();
                if (Convert.ToBoolean(dataGridViewList["IsTaxIncluded", dataGridViewList.CurrentRow.Index].Value) == true)
                {
                    //dataGridViewList["RatePerItem", dataGridViewList.CurrentRow.Index].Value = BAL.GenericFucntions.calcTaxInclusiveRate(Convert.ToDecimal(popup.PopupItemProperty7), Convert.ToDecimal(popup.PopupItemProperty11));
                    dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells["RatePerItem"].Value = BAL.GenericFucntions.calcTaxInclusiveRate(itemRate, getTotalTaxForItem(dataGridViewList.CurrentRow.Index));
                }
                else
                {
                    dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells["RatePerItem"].Value = itemRate;
                }
            }
        }

    }
}

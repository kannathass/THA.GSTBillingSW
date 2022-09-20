using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Transaction
{
    public partial class QuotationEntry : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        BAL.CustomFieldSelection customFieldSelection = new BAL.CustomFieldSelection();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        string quickCustomerInfoDisplay = ConfigurationManager.AppSettings["QuickCustomerInfoDisplay"].ToString();
        string percentBasedDiscount = ConfigurationManager.AppSettings["PercentBasedDiscount"].ToString();
        SqlCommand cmd;
        public Int32 ID { get; set; }
        public Int16 CompanyId { get; set; }
        bool isIGSTApplicable = false;
        //Setting for CESS applicability
        bool isCESSApllicable = false;
        bool autoSequenceStatus;
        bool flagDisplayData;
        string customerStateBeforeUpdate = string.Empty;
        public QuotationEntry()
        {
            InitializeComponent();
            if (quickCustomerInfoDisplay == "0")
            {
                tabControlQuotationInfo.TabPages.Remove(tabPageCustomerDetail);
            }
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonSave.Enabled = false;
                buttonDelete.Enabled = false;
                buttonCancelInvoice.Enabled = false;
            }

            loadCompany();
            loadCustomer();
            loadState();
            loadDiscountBasedOnSettings();
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

        private void QuotationEntry_Load(object sender, EventArgs e)
        {
            //Checking CESS applicability
            if (!isCESSApllicable)
            {
                dataGridViewList.Columns["CESSPercent"].Visible = false;
                dataGridViewList.Columns["CESSValue"].Visible = false;
                labelTotalCESSAmount.Visible = false;
                textBoxTotalCESSAmount.Visible = false;
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
                textBoxTermsAndConditions.Text = customFieldSelection.GetTermsAndConditions(Convert.ToInt16(comboBoxCompany.SelectedValue), "Quotation");
            }
            getCustomFields();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCustomer.SelectedIndex != 0 || textBoxCustomer.Text.Trim() != string.Empty)
                {
                    if (dataGridViewList.Rows.Count == 1)
                    {
                        DialogResult dialogResultNoItems = MessageBox.Show("No items added in this Quotation. Do you want to save this Quotation still?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResultNoItems == DialogResult.No)
                        {
                            return;
                        }
                    }

                    ID = saveInvoiceHeader();
                    saveInvoiceItems();
                    updateAutoSequenceNumberStatus();

                    DialogResult dialogResult = MessageBox.Show("Record Saved Successfully. Do you want to preview this Quotation?", "Quotation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        buttonReport_Click(sender, e);
                    }
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Provide Details!", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Report.ReportViewer reportViewer = new Report.ReportViewer("Quotation", Convert.ToInt16(comboBoxCompany.SelectedValue));
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
                MessageBox.Show("No Quotation found!", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Quotation canceled successfully", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayData();
                        ClearData();
                    }
                }
                else
                {
                    MessageBox.Show("No Quotation found!", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                            MessageBox.Show("Record Deleted Successfully", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //DisplayData();
                            ClearData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Details!", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No Quotation found!", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxIsCESSApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsCESSApplicable.Checked)
            {
                dataGridViewList.Columns["CESSPercent"].Visible = true;
                dataGridViewList.Columns["CESSValue"].Visible = true;
                labelTotalCESSAmount.Visible = true;
                textBoxTotalCESSAmount.Visible = true;
            }
            else
            {
                dataGridViewList.Columns["CESSPercent"].Visible = false;
                dataGridViewList.Columns["CESSValue"].Visible = false;
                labelTotalCESSAmount.Visible = false;
                textBoxTotalCESSAmount.Visible = false;
            }
        }

        private void comboBoxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            taxChangesInGrid();
        }

        private void buttonClearQuickCustomerInfo_Click(object sender, EventArgs e)
        {
            textBoxCustomer.Text = string.Empty;
            textBoxBillingAddress.Text = string.Empty;
            textBoxContactPerson.Text = string.Empty;
            textBoxMobile.Text = string.Empty;
            textBoxPhone.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            comboBoxState.SelectedIndex = 0;
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
                if (dataGridViewList.Columns[e.ColumnIndex].Name == "Quantity")
                {
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
                calcTotalTotalInvoiceValues();
            }
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

        private void calcTotalTotalInvoiceValues()
        {
            decimal totalGSTValue = 0;
            decimal totalTaxableValue = 0;

            decimal totalCGSTValue = 0;
            decimal totalSGSTValue = 0;
            decimal totalIGSTValue = 0;
            decimal totalCESSValue = 0;
            for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
            {
                totalGSTValue = totalGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["Total", i].Value)) ? "0" : dataGridViewList["Total", i].Value);
                totalTaxableValue = totalTaxableValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["TaxableValue", i].Value)) ? "0" : dataGridViewList["TaxableValue", i].Value);

                totalCGSTValue = totalCGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CGSTValue", i].Value)) ? "0" : dataGridViewList["CGSTValue", i].Value);
                totalSGSTValue = totalSGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["SGSTValue", i].Value)) ? "0" : dataGridViewList["SGSTValue", i].Value);
                totalIGSTValue = totalIGSTValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["IGSTValue", i].Value)) ? "0" : dataGridViewList["IGSTValue", i].Value);
                totalCESSValue = totalCESSValue + Convert.ToDecimal(String.IsNullOrEmpty(Convert.ToString(dataGridViewList["CESSValue", i].Value)) ? "0" : dataGridViewList["CESSValue", i].Value);
            }
            textBoxInvoiceTotalAmount.Text = Convert.ToString(Math.Round(totalGSTValue, 2, MidpointRounding.AwayFromZero));
            textBoxTaxableAmount.Text = Convert.ToString(Math.Round(totalTaxableValue, 2, MidpointRounding.AwayFromZero));

            textBoxTotalCGSTAmount.Text = Convert.ToString(Math.Round(totalCGSTValue, 2, MidpointRounding.AwayFromZero));
            textBoxTotalSGSTAmount.Text = Convert.ToString(Math.Round(totalSGSTValue, 2, MidpointRounding.AwayFromZero));
            textBoxTotalIGSTAmount.Text = Convert.ToString(Math.Round(totalIGSTValue, 2, MidpointRounding.AwayFromZero));
            textBoxTotalCESSAmount.Text = Convert.ToString(Math.Round(totalCESSValue, 2, MidpointRounding.AwayFromZero));
        }

        private void taxChangesInGrid()
        {
            string customerStateCurrent = BAL.GenericFucntions.GetStateFromCustomerName(comboBoxCustomer.Text, '|');
            dataGridViewList.Enabled = true;
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
            //if (comboBoxCustomer.SelectedIndex == 0 && textBoxCustomer.Text.Trim() != string.Empty)
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
            if (quickCustomerInfoDisplay == "0")
            {
                if (comboBoxCustomer.SelectedIndex == 0)
                {
                    dataGridViewList.Rows.Clear();
                    dataGridViewList.Enabled = false;
                }
            }
            //if (comboBoxCustomer.SelectedIndex == 0 && textBoxCustomer.Text.Trim() == string.Empty)
            //{
            //    dataGridViewList.Enabled = false;
            //}
            customerStateBeforeUpdate = customerStateCurrent;
        }

        private void gridViewStructureForCGSTSGST()
        {
            dataGridViewList.Columns["CGSTPercent"].Visible = true;
            dataGridViewList.Columns["CGSTValue"].Visible = true;
            dataGridViewList.Columns["SGSTPercent"].Visible = true;
            dataGridViewList.Columns["SGSTValue"].Visible = true;
            dataGridViewList.Columns["IGSTPercent"].Visible = false;
            dataGridViewList.Columns["IGSTValue"].Visible = false;
            isIGSTApplicable = false;

            textBoxTotalCGSTAmount.Visible = true;
            textBoxTotalSGSTAmount.Visible = true;
            textBoxTotalIGSTAmount.Visible = false;

            labelTotalCGSTAmount.Visible = true;
            labelTotalSGSTAmount.Visible = true;
            labelTotalIGSTAmount.Visible = false;
        }

        private void gridViewStructureForIGST()
        {
            dataGridViewList.Columns["CGSTPercent"].Visible = false;
            dataGridViewList.Columns["CGSTValue"].Visible = false;
            dataGridViewList.Columns["SGSTPercent"].Visible = false;
            dataGridViewList.Columns["SGSTValue"].Visible = false;
            dataGridViewList.Columns["IGSTPercent"].Visible = true;
            dataGridViewList.Columns["IGSTValue"].Visible = true;
            isIGSTApplicable = true;

            textBoxTotalCGSTAmount.Visible = false;
            textBoxTotalSGSTAmount.Visible = false;
            textBoxTotalIGSTAmount.Visible = true;

            labelTotalCGSTAmount.Visible = false;
            labelTotalSGSTAmount.Visible = false;
            labelTotalIGSTAmount.Visible = true;
        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            taxChangesInGrid();
        }

        private void CancelItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand("Update [TranQuotation] set InvoiceStatus='Canceled' Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void getCustomFields()
        {
            bindCustomFields(customFieldSelection.GetCustomFields(Convert.ToInt16(comboBoxCompany.SelectedValue), "Quotation"));
            bindCustomFieldItems(customFieldSelection.GetCustomFields(Convert.ToInt16(comboBoxCompany.SelectedValue), "Quotation Items"));
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
                }
                else
                {
                    labelCF5.Visible = false;
                    textBoxCF5.Visible = false;
                    textBoxCF5.Text = string.Empty;
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
                cmd.Parameters.AddWithValue("@DocumentType", "Quotation");
                cmd.Parameters.AddWithValue("@mode", invoiceMode);
                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            invoiceNumber = (string)reader["InvoiceNumber"];
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
            }
            else if (invoiceNumber == "ManualExistingInvoice")
            {
                textBoxInvoiceNumber.Enabled = true;
            }
            else if (invoiceNumber == "AutoExistingInvoice")
            {
                textBoxInvoiceNumber.Enabled = false;
            }
            else
            {
                textBoxInvoiceNumber.Text = string.Concat(idPrifix, invoiceNumber, idSuffix);
                textBoxInvoiceNumber.Enabled = false;
            }
        }

        private void updateAutoSequenceNumberStatus()
        {
            if (autoSequenceStatus)
                using (SqlConnection con = new SqlConnection(conString))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "GetAutoSequenceNumber";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                    cmd.Parameters.AddWithValue("@DocumentType", "Quotation");
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

        private void saveInvoiceItems()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                for (int i = 0; i < dataGridViewList.Rows.Count - 1; i++)
                {
                    cmd = new SqlCommand("INSERT INTO [TranQuotationItems] " +
           " ([HeaderID],[ItemID],[ItemDescription],[ItemCustomDescription],[ItemType] " +
           " ,[HsnSac],[Qty],[UoM],[RatePerItem],DiscountPercent,[Discount] " +
           " ,[TaxableValue],[CGSTPercent],[CGSTValue] " +
           " ,[SGSTPercent],[SGSTValue],[IGSTPercent] " +
           " ,[IGSTValue],[CESSPercent],[CESSValue],[TotalValue], CFItem1, CFItem2) " +
     " VALUES  " +
           " (@HeaderID, @ItemID, @ItemDescription, @ItemCustomDescription, @ItemType " +
           " , @HsnSac, @Qty, @UoM, @RatePerItem,@DiscountPercent, @Discount " +
           " , @TaxableValue, @CGSTPercent, @CGSTValue " +
           " , @SGSTPercent, @SGSTValue, @IGSTPercent " +
           " , @IGSTValue, @CESSPercent, @CESSValue, @TotalValue, @CFItem1, @CFItem2)", con);

                    cmd.Parameters.AddWithValue("@HeaderID", ID);

                    cmd.Parameters.AddWithValue("@ItemID", Convert.ToInt16(dataGridViewList["ItemId", i].Value));
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
            calcTotalTotalInvoiceValues();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                if (ID == 0)
                {
                    cmd = new SqlCommand("INSERT INTO [TranQuotation] " +
           " (CompanyID " +
           " ,QuotationNumber " +
           " ,QuotationDate " +
           " ,QuotationDueDate " +
           " ,CustomerID " +
           " ,CustomerName " +
           " ,CustomerAddress " +
           " ,ContactPerson " +
           " ,CustomerMobile " +
           " ,CustomerPhone " +
           " ,CustomerEmail " +
           " ,CustomerState " +
           " ,PONumber " +
           " ,PODate " +
           " ,CGSTAmount " +
           " ,SGSTAmount " +
           " ,IGSTAmount " +
           " ,CESSAmount " +
           " ,TaxableAmount " +
           " ,QuotationTotalAmount " +
           " ,QuotationStatus " +
           " ,CustomerNotes " +
           " ,TermsAndCondition " +
           " ,isCessApplicable " +
           " ,CF1 " +
           " ,CF2 " +
           " ,CF3 " +
           " ,CF4 " +
           " , CF5" +
           " , CreatedBy, CreatedOn, RoundedOffQuotationTotalAmount)" +
           " OUTPUT inserted.ID " +
     " VALUES " +
           " (@CompanyID " +
           " ,@QuotationNumber " +
           " ,@QuotationDate " +
           " ,@QuotationDueDate " +
           " ,@CustomerID " +
           " ,@CustomerName " +
           " ,@CustomerAddress " +
           " ,@ContactPerson " +
           " ,@CustomerMobile " +
           " ,@CustomerPhone " +
           " ,@CustomerEmail " +
           " ,@CustomerState " +
           " ,@PONumber " +
           " ,@PODate " +
           " ,@CGSTAmount " +
           " ,@SGSTAmount " +
           " ,@IGSTAmount " +
           " ,@CESSAmount " +
           " ,@TaxableAmount " +
           " ,@QuotationTotalAmount " +
           " ,@QuotationStatus " +
           " ,@CustomerNotes " +
           " ,@TermsAndCondition " +
           " ,@isCessApplicable " +
           " ,@CF1 " +
           " ,@CF2 " +
           " ,@CF3 " +
           " ,@CF4 " +
           " ,@CF5 " +
           " ,@CreatedBy, getDate(), ROUND(@QuotationTotalAmount,0))", con);
                }
                else
                {
                    cmd = new SqlCommand(" Delete TranQuotationItems where HeaderID=@ID " +
                        " UPDATE [TranQuotation] " +
          " SET [CompanyID] = @CompanyID " +
          " ,QuotationNumber = @QuotationNumber " +
          " ,QuotationDate = @QuotationDate " +
          " ,QuotationDueDate = @QuotationDueDate " +
          " ,CustomerID = @CustomerID " +
           " ,CustomerName = @CustomerName " +
           " ,CustomerAddress = @CustomerAddress " +
           " ,ContactPerson=@ContactPerson " +
           " ,CustomerMobile=@CustomerMobile " +
           " ,CustomerPhone=@CustomerPhone " +
           " ,CustomerEmail=@CustomerEmail " +
           " ,CustomerState=@CustomerState " +
          " ,PONumber = @PONumber " +
          " ,PODate = @PODate " +
          " ,CGSTAmount = @CGSTAmount " +
          " ,SGSTAmount = @SGSTAmount " +
          " ,IGSTAmount = @IGSTAmount " +
          " ,CESSAmount = @CESSAmount " +
          " ,TaxableAmount = @TaxableAmount " +
          " ,QuotationTotalAmount = @QuotationTotalAmount " +
          " ,QuotationStatus = @QuotationStatus " +
          " ,CustomerNotes = @CustomerNotes " +
          " ,TermsAndCondition = @TermsAndCondition " +
          " ,isCessApplicable = @isCessApplicable " +
          " ,CF1 = @CF1 " +
          " ,CF2 = @CF2 " +
          " ,CF3 = @CF3 " +
          " ,CF4 = @CF4 " +
          " ,CF5 = @CF5 " +
          " ,CreatedBy = @CreatedBy " +
          " ,CreatedOn = getDate() " +
          " ,RoundedOffQuotationTotalAmount = ROUND(@QuotationTotalAmount,0) " +
               " OUTPUT inserted.ID " +
          " WHERE ID = @ID", con);
                }

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@QuotationNumber", Convert.ToString(textBoxInvoiceNumber.Text).Trim());
                cmd.Parameters.AddWithValue("@QuotationDate", dateTimePickerInvoiceDate.Value);
                cmd.Parameters.AddWithValue("@QuotationDueDate", dateTimePickerDueDate.Value);
                //if (comboBoxCustomer.SelectedIndex != 0)
                //{
                //    cmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(comboBoxCustomer.SelectedValue));
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(comboBoxCustomer.SelectedValue));
                //}
                cmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(comboBoxCustomer.SelectedValue));
                cmd.Parameters.AddWithValue("@CustomerName", Convert.ToString(textBoxCustomer.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerAddress", Convert.ToString(textBoxBillingAddress.Text.Trim()));
                cmd.Parameters.AddWithValue("@ContactPerson", Convert.ToString(textBoxContactPerson.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerMobile", Convert.ToString(textBoxMobile.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerPhone", Convert.ToString(textBoxPhone.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerEmail", Convert.ToString(textBoxEmail.Text.Trim()));
                cmd.Parameters.AddWithValue("@CustomerState", Convert.ToString(comboBoxState.Text));

                cmd.Parameters.AddWithValue("@PONumber", Convert.ToString(textBoxPONumber.Text).Trim());
                cmd.Parameters.AddWithValue("@poDate", dateTimePickerPODate.Value);
                cmd.Parameters.AddWithValue("@CGSTAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalCGSTAmount.Text) ? "0" : textBoxTotalCGSTAmount.Text));
                cmd.Parameters.AddWithValue("@SGSTAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalSGSTAmount.Text) ? "0" : textBoxTotalSGSTAmount.Text));
                cmd.Parameters.AddWithValue("@IGSTAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalIGSTAmount.Text) ? "0" : textBoxTotalIGSTAmount.Text));
                cmd.Parameters.AddWithValue("@CESSAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTotalCESSAmount.Text) ? "0" : textBoxTotalCESSAmount.Text));
                cmd.Parameters.AddWithValue("@TaxableAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxTaxableAmount.Text) ? "0" : textBoxTaxableAmount.Text));
                cmd.Parameters.AddWithValue("@QuotationTotalAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxInvoiceTotalAmount.Text) ? "0" : textBoxInvoiceTotalAmount.Text));
                cmd.Parameters.AddWithValue("@QuotationStatus", Convert.ToString("").Trim());
                cmd.Parameters.AddWithValue("@CustomerNotes", Convert.ToString(textBoxCustomerNotes.Text).Trim());
                cmd.Parameters.AddWithValue("@TermsAndCondition", Convert.ToString(textBoxTermsAndConditions.Text).Trim());
                cmd.Parameters.AddWithValue("@isCessApplicable", checkBoxIsCESSApplicable.Checked);

                cmd.Parameters.AddWithValue("@CF1", Convert.ToString(textBoxCF1.Text).Trim());
                cmd.Parameters.AddWithValue("@CF2", Convert.ToString(textBoxCF2.Text).Trim());
                cmd.Parameters.AddWithValue("@CF3", Convert.ToString(textBoxCF3.Text).Trim());
                cmd.Parameters.AddWithValue("@CF4", Convert.ToString(textBoxCF4.Text).Trim());
                cmd.Parameters.AddWithValue("@CF5", Convert.ToString(textBoxCF5.Text).Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Entities.AuthenticationDetail.UserName);

                using (cmd)
                {
                    identityInserted = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return identityInserted;
        }

        private void deleteItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand("Update [TranQuotationItems] set Del_State=1, Del_Date=GetDate() Where HeaderID=@ID " +
                    " Update [TranQuotation] set Del_State=1, Del_Date=GetDate() Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DisplayData()
        {
            flagDisplayData = true;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand(@"SELECT [CompanyID],[QuotationNumber],[QuotationDate],[QuotationDueDate]
                     ,[CustomerID],Quotation.CustomerName,CustomerAddress,Quotation.ContactPerson,CustomerMobile,CustomerPhone,CustomerEmail,CustomerState
                     ,[PONumber],[PODate],[CGSTAmount],[SGSTAmount] 
                     ,[IGSTAmount],[CESSAmount],[TaxableAmount],[QuotationTotalAmount],[QuotationStatus] 
                     ,[CustomerNotes],TermsAndCondition, isCessApplicable,[CF1],[CF2],[CF3],[CF4],CF5 
                     FROM [TranQuotation] Quotation where Quotation.ID = @ID and Quotation.Del_State=0", con);
                cmd.Parameters.AddWithValue("@ID", ID);

                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            comboBoxCompany.SelectedValue = Convert.ToInt16(reader["CompanyID"]);
                            textBoxInvoiceNumber.Text = Convert.ToString(reader["QuotationNumber"]);
                            dateTimePickerInvoiceDate.Value = Convert.ToDateTime(reader["QuotationDate"]);
                            dateTimePickerDueDate.Value = Convert.ToDateTime(reader["QuotationDueDate"]);
                            comboBoxCustomer.SelectedValue = Convert.ToInt32(reader["CustomerId"]);
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
                            textBoxInvoiceTotalAmount.Text = Convert.ToString(reader["QuotationTotalAmount"]);
                            textBoxCustomerNotes.Text = Convert.ToString(reader["CustomerNotes"]);
                            textBoxTermsAndConditions.Text = Convert.ToString(reader["TermsAndCondition"]);
                            checkBoxIsCESSApplicable.Checked = Convert.ToBoolean(reader["isCessApplicable"]);
                            textBoxCF1.Text = Convert.ToString(reader["CF1"]);
                            textBoxCF2.Text = Convert.ToString(reader["CF2"]);
                            textBoxCF3.Text = Convert.ToString(reader["CF3"]);
                            textBoxCF4.Text = Convert.ToString(reader["CF4"]);
                            textBoxCF5.Text = Convert.ToString(reader["CF5"]);
                        }
                    }

                    DataSet dsInvoiceItems = new DataSet();

                    using (SqlCommand cmd = new SqlCommand(@"SELECT ItemId, IVItem.[ItemDescription]
                            ,[ItemCustomDescription],IVItem.ItemType,IVItem.[HsnSac],[Qty] 
                            ,IVItem.[UoM],[RatePerItem],isnull(IVItem.DiscountPercent,0) DiscountPercent,[Discount],[TaxableValue] 
                            ,CGSTPercent,[CGSTValue],SGSTPercent,[SGSTValue],IGSTPercent,[IGSTValue] 
                            ,CESSPercent,[CESSValue],[TotalValue] 
                            ,IsTaxIncluded,CFItem1,CFItem2
                            FROM [TranQuotationItems] IVItem
                            Inner join MasterItem Item on IVItem.ItemID = Item.ID
                            where HeaderID = @ID and IVItem.Del_State=0 order by IVItem.ID "))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);

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
            dateTimePickerInvoiceDate.Value = DateTime.Now;
            dateTimePickerDueDate.Value = DateTime.Now;
            textBoxPONumber.Text = "";
            textBoxCustomer.Text = string.Empty;
            textBoxBillingAddress.Text = string.Empty;
            textBoxContactPerson.Text = string.Empty;
            textBoxMobile.Text = string.Empty;
            textBoxPhone.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
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
            textBoxCF3.Text = "";
            dataGridViewList.Rows.Clear();
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

        private void loadState()
        {
            comboBoxState.ValueMember = "Code";
            comboBoxState.DisplayMember = "Name";
            comboBoxState.DataSource = masterSelection.GetStateList();
            //comboBoxState.SelectedIndex = 0;
        }

        private decimal calcDiscountPercentFromValue(decimal totalValue, decimal discountValue)
        {
            if (discountValue == 0)
            {
                return 0;
            }
            return Math.Round(totalValue / discountValue, 2, MidpointRounding.AwayFromZero);
        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (quickCustomerInfoDisplay == "1")
            {
                if (Convert.ToInt16(comboBoxCustomer.SelectedIndex) == 0)
                {
                    if (comboBoxCustomer.Focused == true)
                        tabControlQuotationInfo.SelectedTab = tabPageCustomerDetail;
                    tabPageCustomerDetail.Enabled = true;
                }
                else
                {
                    tabPageCustomerDetail.Enabled = false;
                }
            }

            taxChangesInGrid();
        }

        private void radioButtonPercentBasedDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPercentBasedDiscount.Checked)
            {
                dataGridViewList.Columns["DiscountPercent"].ReadOnly = false;
                dataGridViewList.Columns["Discount"].ReadOnly = true;
            }
        }

        private void radioButtonAmountBasedDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAmountBasedDiscount.Checked)
            {
                dataGridViewList.Columns["Discount"].ReadOnly = false;
                dataGridViewList.Columns["DiscountPercent"].ReadOnly = true;
            }
        }

        private void dataGridViewList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            calcTotalTotalInvoiceValues();
            setRowNumber();
        }

        private void QuotationEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt && e.KeyCode == Keys.I) || e.KeyCode == Keys.F12)
            {
                if (comboBoxCustomer.SelectedIndex != 0 || textBoxCustomer.Text.Trim() != string.Empty)
                {
                    fillItemInGridRow();
                }
                else
                {
                    MessageBox.Show("Please choose a customer before item selection", "Quotation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxCustomer.Select();
                }
            }
        }

        private void fillItemInGridRow()
        {
            int rowIndex = dataGridViewList.Rows.Count - 1;
            PopupControl.GenericPopup popup = new PopupControl.GenericPopup();
            popup.PopupQueryString = @"select Item.ID
                ,[Group].GroupName [Group Under]
                ,[ItemDescription] [Item Name]
                ,isnull(Stock.StockInHand,0) Stock
                ,[UoM]
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
                Where Item.Del_State = 0   and ItemStatus='Active'
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
                if (radioButtonPercentBasedDiscount.Checked)
                {
                    dataGridViewList["DiscountPercent", rowIndex].Value = popup.PopupItemProperty9;
                }
                else
                {
                    dataGridViewList["Discount", rowIndex].Value = popup.PopupItemProperty10;
                }
                if (isIGSTApplicable == false)
                {
                    dataGridViewList["SGSTPercent", rowIndex].Value = Convert.ToDecimal(popup.PopupItemProperty11) / 2;
                    dataGridViewList["CGSTPercent", rowIndex].Value = Convert.ToDecimal(popup.PopupItemProperty11) / 2;
                }
                else
                {
                    dataGridViewList["IGSTPercent", rowIndex].Value = popup.PopupItemProperty11;
                }

                dataGridViewList.Select();
                dataGridViewList["ItemCustomDescription", dataGridViewList.Rows.Count - 2].Selected = true;
            }

            popup.Dispose();
        }

        private void buttonRefreshTermsAndCondition_Click(object sender, EventArgs e)
        {
            textBoxTermsAndConditions.Text = customFieldSelection.GetTermsAndConditions(Convert.ToInt16(comboBoxCompany.SelectedValue), "Quotation");
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.R)
            {
                decimal itemRate = getItemRateFromUser();
                if (Convert.ToBoolean(dataGridViewList["IsTaxIncluded", dataGridViewList.CurrentRow.Index].Value) == true)
                {
                    dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells["RatePerItem"].Value = BAL.GenericFucntions.calcTaxInclusiveRate(itemRate, getTotalTaxForItem(dataGridViewList.CurrentRow.Index));
                }
                else
                {
                    dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells["RatePerItem"].Value = itemRate;
                }
            }
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

        private void dataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setRowNumber();
        }

        private void setRowNumber()
        {
            foreach (DataGridViewRow row in dataGridViewList.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void buttonRefreshCustomer_Click(object sender, EventArgs e)
        {
            loadCustomer();
        }
    }
}


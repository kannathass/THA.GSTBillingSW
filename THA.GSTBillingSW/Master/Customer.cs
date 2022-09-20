using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace THA.GSTBillingSW.Master
{
    public partial class Customer : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        string GSTNBeforeUpdate = string.Empty;

        public Customer()
        {
            InitializeComponent();
            DisplayData();
            loadState();
            comboBoxCustomerType.DataSource = BAL.GenericFucntions.SplitString(ConfigurationManager.AppSettings["CustomerType"].ToString(), "|");
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonSave.Enabled = false;
                buttonDelete.Enabled = false;
            }
            comboBoxStatus.SelectedIndex = 0;
            textBoxCustomer.Select();
        }

        private void loadState()
        {
            comboBoxState.ValueMember = "Code";
            comboBoxState.DisplayMember = "Name";
            comboBoxState.DataSource = masterSelection.GetStateList();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCustomer.Text.Trim() != "")
                {
                    //If GSTN number is empty then allowed to save. Validation only when GSTN available
                    if (textBoxGSTN.Text != string.Empty)
                    {
                        if (!BAL.GenericValidation.IsValidGSTN(textBoxGSTN.Text))
                        {
                            MessageBox.Show("GSTN is invalid.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxGSTN.Select();
                            return;
                        }
                        string gstnNo = string.Empty;
                        //This condition occurs when new customer added or updating customer with modified gstn 
                        if (GSTNBeforeUpdate != textBoxGSTN.Text)
                        {
                            gstnNo = BAL.GenericValidation.GetCustomerGSTNCount(textBoxGSTN.Text);
                        }
                        if (gstnNo != string.Empty)
                        {
                            MessageBox.Show(string.Format("GSTN number '{0}' is already existing for the customer '{1}'. Duplicate GSTN is not permitted.", textBoxGSTN.Text, gstnNo), "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxGSTN.Select();
                            return;
                        }
                    }

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        if (ID == 0)
                        {
                            cmd = new SqlCommand("	INSERT INTO [dbo].[MasterCustomer] " +
               " ([CustomerName],[BillingAddress],[ShippingAddress],[State],[PinCode],[PAN],[GSTN],[GSTNPortalUserName] " +
               " ,[ContactPerson],[Email],[Phone],[Mobile],[Website],[BankName],[Branch],[AccountNumber] " +
               " ,[IFSC],[CustomerStatus],[Comments],CustomerType, StateCode) " +
         " VALUES " +
               " (@CustomerName, @BillingAddress, @ShippingAddress, @State, @PinCode, @PAN, @GSTN, @GSTNPortalUserName " +
               " , @ContactPerson, @Email, @Phone, @Mobile, @Website, @BankName, @Branch, @AccountNumber " +
               " , @IFSC, @CustomerStatus, @Comments,@CustomerType, @StateCode)", con);
                        }
                        else
                        {
                            cmd = new SqlCommand("UPDATE [dbo].[MasterCustomer] " +
       " SET CustomerName = @CustomerName " +
         ",BillingAddress = @BillingAddress " +
         ",ShippingAddress = @ShippingAddress " +
         ",State = @State " +
         ",PinCode = @PinCode " +
         ",PAN = @PAN " +
         ",GSTN = @GSTN " +
         ",GSTNPortalUserName = @GSTNPortalUserName " +
         ",ContactPerson = @ContactPerson " +
         ",Email = @Email " +
         ",Phone = @Phone " +
         ",Mobile = @Mobile " +
         ",Website = @Website " +
         ",BankName = @BankName " +
         ",Branch = @Branch " +
         ",AccountNumber = @AccountNumber " +
         ",IFSC = @IFSC " +
         ",CustomerStatus = @CustomerStatus " +
         ",Comments = @Comments " +
         ",CustomerType=@CustomerType " +
         ",StateCode = @StateCode " +
     " WHERE ID = @ID", con);
                        }
                        con.Open();
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@CustomerName", Convert.ToString(textBoxCustomer.Text).Trim());
                        cmd.Parameters.AddWithValue("@BillingAddress", Convert.ToString(textBoxBillingAddress.Text).Trim());
                        cmd.Parameters.AddWithValue("@ShippingAddress", Convert.ToString(textBoxShippingAddress.Text).Trim());
                        cmd.Parameters.AddWithValue("@State", Convert.ToString(comboBoxState.Text).Trim());
                        cmd.Parameters.AddWithValue("@PinCode", Convert.ToString(textBoxPinCode.Text).Trim());
                        cmd.Parameters.AddWithValue("@PAN", Convert.ToString(textBoxPAN.Text).Trim());
                        cmd.Parameters.AddWithValue("@GSTN", Convert.ToString(textBoxGSTN.Text).Trim());
                        cmd.Parameters.AddWithValue("@GSTNPortalUserName", Convert.ToString(textBoxGSTNPortalUserName.Text).Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", Convert.ToString(textBoxContactPerson.Text).Trim());
                        cmd.Parameters.AddWithValue("@Email", Convert.ToString(textBoxEmail.Text).Trim());
                        cmd.Parameters.AddWithValue("@Phone", Convert.ToString(textBoxPhone.Text).Trim());
                        cmd.Parameters.AddWithValue("@Mobile", Convert.ToString(textBoxMobile.Text).Trim());
                        cmd.Parameters.AddWithValue("@Website", Convert.ToString(textBoxWebsite.Text).Trim());
                        cmd.Parameters.AddWithValue("@BankName", Convert.ToString(textBoxBankName.Text).Trim());
                        cmd.Parameters.AddWithValue("@Branch", Convert.ToString(textBoxBranch.Text).Trim());
                        cmd.Parameters.AddWithValue("@AccountNumber", Convert.ToString(textBoxAccountNumber.Text).Trim());
                        cmd.Parameters.AddWithValue("@IFSC", Convert.ToString(textBoxIFSC.Text).Trim());
                        cmd.Parameters.AddWithValue("@CustomerStatus", Convert.ToString(comboBoxStatus.Text).Trim());
                        cmd.Parameters.AddWithValue("@Comments", Convert.ToString(textBoxComments.Text).Trim());
                        cmd.Parameters.AddWithValue("@CustomerType", Convert.ToString(comboBoxCustomerType.Text).Trim());
                        cmd.Parameters.AddWithValue("@StateCode", Convert.ToString(comboBoxState.SelectedValue).Trim());
                        using (cmd)
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Record Saved Successfully", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Provide Details!", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (textBoxCustomer.Text.Trim() != "")
                {
                    if (BAL.GenericValidation.GetCustomerTransactionCount(Convert.ToString(ID)) == 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            deleteItem();
                            MessageBox.Show("Record Deleted Successfully", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DisplayData();
                            ClearData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transactions are exist for the selected Customer. You can not delete it.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Details!", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand("Update MasterCustomer set Del_State=1, Del_Date=GetDate() Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void DisplayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SqlDataAdapter(@"select ID,CustomerName [Customer Name],[BillingAddress],[ShippingAddress]
                    ,[State],[PinCode] [Pin Code],[PAN],[GSTN],[GSTNPortalUserName]
                    ,ContactPerson [Contact Person],[Email],[Phone],[Mobile],[Website],[BankName],[Branch],[AccountNumber] 
                    ,[IFSC],[CustomerStatus],[Comments],CustomerType
                    from MasterCustomer Where Del_State = 0
                    and (CustomerName like @FilterValue or State like @FilterValue or CustomerType like @FilterValue) order by customerName", con);
                    adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());

                    adapt.Fill(dt);
                    using (adapt)
                    {
                        dataGridViewCompany.DataSource = dt;
                    }
                    dataGridViewCompany.Columns["ID"].Visible = false;
                    dataGridViewCompany.Columns["BillingAddress"].Visible = false;
                    dataGridViewCompany.Columns["ShippingAddress"].Visible = false;
                    dataGridViewCompany.Columns["PAN"].Visible = false;
                    dataGridViewCompany.Columns["GSTN"].Visible = false;
                    dataGridViewCompany.Columns["GSTNPortalUserName"].Visible = false;
                    dataGridViewCompany.Columns["WebSite"].Visible = false;
                    dataGridViewCompany.Columns["BankName"].Visible = false;
                    dataGridViewCompany.Columns["Branch"].Visible = false;
                    dataGridViewCompany.Columns["AccountNumber"].Visible = false;
                    dataGridViewCompany.Columns["IFSC"].Visible = false;
                    dataGridViewCompany.Columns["Comments"].Visible = false;
                    //dataGridViewCompany.Columns["CustomerType"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearData()
        {
            ID = 0;
            textBoxCustomer.Text = "";
            textBoxBillingAddress.Text = "";
            textBoxShippingAddress.Text = "";
            comboBoxState.SelectedIndex = 0;
            textBoxPinCode.Text = "";
            textBoxPAN.Text = "";
            textBoxGSTN.Text = "";
            textBoxGSTNPortalUserName.Text = "";
            textBoxContactPerson.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            textBoxMobile.Text = "";
            textBoxWebsite.Text = "";
            textBoxBankName.Text = "";
            textBoxBranch.Text = "";
            textBoxAccountNumber.Text = "";
            textBoxIFSC.Text = "";
            comboBoxStatus.SelectedIndex = 0;
            comboBoxCustomerType.SelectedIndex = 0;
            textBoxComments.Text = "";
            GSTNBeforeUpdate = string.Empty;
        }

        private void dataGridViewCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewCompany.Rows[e.RowIndex].Cells[0].Value);
                textBoxCustomer.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxBillingAddress.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxShippingAddress.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBoxState.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxPinCode.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxPAN.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBoxGSTN.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[7].Value.ToString();
                GSTNBeforeUpdate = textBoxGSTN.Text;
                textBoxGSTNPortalUserName.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBoxContactPerson.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[9].Value.ToString();
                textBoxEmail.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[10].Value.ToString();
                textBoxPhone.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[11].Value.ToString();
                textBoxMobile.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[12].Value.ToString();
                textBoxWebsite.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[13].Value.ToString();
                textBoxBankName.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[14].Value.ToString();
                textBoxBranch.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[15].Value.ToString();
                textBoxAccountNumber.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[16].Value.ToString();
                textBoxIFSC.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[17].Value.ToString();
                comboBoxStatus.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[18].Value.ToString();
                textBoxComments.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[19].Value.ToString();
                comboBoxCustomerType.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[20].Value.ToString();
            }
            else
            {
                ClearData();
            }
        }

        private void checkBoxSameAsBillingAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSameAsBillingAddress.Checked)
            {
                textBoxShippingAddress.Text = textBoxBillingAddress.Text;
            }
            else
            {
                textBoxShippingAddress.Text = string.Empty;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}

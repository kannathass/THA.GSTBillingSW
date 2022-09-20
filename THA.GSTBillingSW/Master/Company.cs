using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Master
{
    public partial class Company : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;

        public Company()
        {
            InitializeComponent();
            loadState();
            DisplayData();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonSave.Enabled = false;
                buttonDelete.Enabled = false;
                buttonSettings.Enabled = false;
            }
            textBoxCompany.Select();
        }

        private void loadState()
        {
            comboBoxState.ValueMember = "Code";
            comboBoxState.DisplayMember = "Name";
            comboBoxState.DataSource = masterSelection.GetStateList();
        }

        private void Company_Load(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCompany.Text.Trim() != "")
                {
                    //If GSTN number is empty then allowed to save. Validation only when GSTN available
                    if (textBoxGSTN.Text != string.Empty && !BAL.GenericValidation.IsValidGSTN(textBoxGSTN.Text))
                    {
                        MessageBox.Show("GSTN is invalid.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxGSTN.Select();

                        return;
                    }

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        if (ID == 0)
                        {
                            cmd = new SqlCommand("INSERT INTO [MasterCompany] ([CompanyName],Address, State, PinCode, " +
                                                " [PAN],[GSTN], GSTNPortalUserName,Email,Phone, Mobile, Website, " +
                                                " BankName, Branch, AccountNumber, IFSC, StateCode, CompanyDisplayName ) " +
                                                " VALUES(@CompanyName, @Address, @State,@PinCode, " +
                                                " @PAN, @GSTN, @GSTNPortalUserName,@Email,@Phone,@Mobile,@Website, " +
                                                " @BankName, @Branch, @AccountNumber, @IFSC, @StateCode, @CompanyDisplayName )", con);
                        }
                        else
                        {
                            cmd = new SqlCommand("UPDATE [dbo].[MasterCompany] " +
                                                " SET CompanyName = @CompanyName " +
                                                ",Address = @Address " +
                                                ",State = @State " +
                                                ",PinCode = @PinCode " +
                                                ",PAN = @PAN " +
                                                ",GSTN = @GSTN " +
                                                ",GSTNPortalUserName = @GSTNPortalUserName " +
                                                ",Email = @Email " +
                                                ",Phone = @Phone " +
                                                ",Mobile = @Mobile " +
                                                ",Website = @Website " +
                                                ",BankName = @BankName " +
                                                ",Branch = @Branch " +
                                                ",AccountNumber = @AccountNumber " +
                                                ",IFSC = @IFSC " +
                                                ",StateCode = @StateCode " +
                                                ",CompanyDisplayName = @CompanyDisplayName " +
                                                " WHERE ID = @ID", con);
                        }

                        cmd.Parameters.AddWithValue("@ID", ID);

                        cmd.Parameters.AddWithValue("@CompanyName", Convert.ToString(textBoxCompany.Text).Trim());
                        cmd.Parameters.AddWithValue("@Address", Convert.ToString(textBoxAddress.Text).Trim());
                        cmd.Parameters.AddWithValue("@State", Convert.ToString(comboBoxState.Text).Trim());
                        cmd.Parameters.AddWithValue("@PinCode", Convert.ToString(textBoxPinCode.Text).Trim());
                        cmd.Parameters.AddWithValue("@PAN", Convert.ToString(textBoxPAN.Text).Trim());
                        cmd.Parameters.AddWithValue("@GSTN", Convert.ToString(textBoxGSTN.Text).Trim());
                        cmd.Parameters.AddWithValue("@GSTNPortalUserName", Convert.ToString(textBoxGSTNPortalUserName.Text).Trim());
                        cmd.Parameters.AddWithValue("@Email", Convert.ToString(textBoxEmail.Text).Trim());
                        cmd.Parameters.AddWithValue("@Phone", Convert.ToString(textBoxPhone.Text).Trim());
                        cmd.Parameters.AddWithValue("@Mobile", Convert.ToString(textBoxMobile.Text).Trim());
                        cmd.Parameters.AddWithValue("@Website", Convert.ToString(textBoxWebsite.Text).Trim());
                        cmd.Parameters.AddWithValue("@BankName", Convert.ToString(textBoxBankName.Text).Trim());
                        cmd.Parameters.AddWithValue("@Branch", Convert.ToString(textBoxBranch.Text).Trim());
                        cmd.Parameters.AddWithValue("@AccountNumber", Convert.ToString(textBoxAccountNumber.Text).Trim());
                        cmd.Parameters.AddWithValue("@IFSC", Convert.ToString(textBoxIFSC.Text).Trim());
                        cmd.Parameters.AddWithValue("@StateCode", Convert.ToString(comboBoxState.SelectedValue).Trim());
                        cmd.Parameters.AddWithValue("@CompanyDisplayName", Convert.ToString(textBoxDisplayName.Text).Trim());
                        con.Open();

                        using (cmd)
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Record Saved Successfully", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please Provide Details!", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SqlDataAdapter("select ID,[CompanyName] [Company Name],Address, State,PinCode [Pin Code],[PAN],[GSTN], " +
                        " GSTNPortalUserName, Email, Phone, Mobile, Website, " +
                        " BankName, Branch, AccountNumber, IFSC, CompanyDisplayName " +
                        " from MasterCompany Where Del_State = 0 order by CompanyName", con);
                    adapt.Fill(dt);
                    using (adapt)
                    {
                        dataGridViewCompany.DataSource = dt;
                    }
                    dataGridViewCompany.Columns["ID"].Visible = false;
                    dataGridViewCompany.Columns["Address"].Visible = false;
                    dataGridViewCompany.Columns["GSTNPortalUserName"].Visible = false;
                    dataGridViewCompany.Columns["WebSite"].Visible = false;
                    dataGridViewCompany.Columns["BankName"].Visible = false;
                    dataGridViewCompany.Columns["Branch"].Visible = false;
                    dataGridViewCompany.Columns["AccountNumber"].Visible = false;
                    dataGridViewCompany.Columns["IFSC"].Visible = false;
                    dataGridViewCompany.Columns["CompanyDisplayName"].Visible = false;
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
            textBoxCompany.Text = "";
            textBoxDisplayName.Text = string.Empty;
            textBoxAddress.Text = "";
            comboBoxState.SelectedIndex = 0;
            textBoxPinCode.Text = "";
            textBoxPAN.Text = "";
            textBoxGSTN.Text = "";
            textBoxGSTNPortalUserName.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            textBoxMobile.Text = "";
            textBoxWebsite.Text = "";
            textBoxBankName.Text = "";
            textBoxBranch.Text = "";
            textBoxAccountNumber.Text = "";
            textBoxIFSC.Text = "";
        }

        //private void dataGridViewCompany_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    dataGridViewCompany[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.WhiteSmoke;
        //    dataGridViewCompany[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Green;
        //}

        //private void dataGridViewCompany_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    dataGridViewCompany[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Green;
        //    dataGridViewCompany[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.WhiteSmoke;
        //}

        private void dataGridViewCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewCompany.Rows[e.RowIndex].Cells[0].Value);
                textBoxCompany.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxAddress.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBoxState.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxPinCode.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxPAN.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxGSTN.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBoxGSTNPortalUserName.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBoxEmail.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBoxPhone.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[9].Value.ToString();
                textBoxMobile.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[10].Value.ToString();
                textBoxWebsite.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[11].Value.ToString();
                textBoxBankName.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[12].Value.ToString();
                textBoxBranch.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[13].Value.ToString();
                textBoxAccountNumber.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[14].Value.ToString();
                textBoxIFSC.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[15].Value.ToString();
                textBoxDisplayName.Text = dataGridViewCompany.Rows[e.RowIndex].Cells[16].Value.ToString();
            }
            else
            {
                ClearData();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCompany.Text.Trim() != "")
                {
                    if (BAL.GenericValidation.GetCompanyTransactionCount(Convert.ToString(ID)) == 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            deleteItem();
                            MessageBox.Show("Record Deleted Successfully", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DisplayData();
                            ClearData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transactions are exist for the selected Company. You can not delete it.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Details!", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                cmd = new SqlCommand("Update MasterCompany set Del_State=1, Del_Date=GetDate() Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Setting.InvoiceSettings formSetting = new Setting.InvoiceSettings(ID);
                formSetting.MdiParent = this.MdiParent;
                formSetting.Show();
            }
            else
            {
                MessageBox.Show("Please select a comapny to open setting", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

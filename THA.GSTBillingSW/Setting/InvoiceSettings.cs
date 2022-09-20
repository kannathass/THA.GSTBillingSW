using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Setting
{
    public partial class InvoiceSettings : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        //int ID = 0;
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        string docSerialStartNo;
        string docIdPrefix;
        string docIdSuffix;
        bool docIdAutoSeqNo;
        string imagename = string.Empty;

        string cf1 = string.Empty;
        string cf2 = string.Empty;
        string cf3 = string.Empty;
        string cf4 = string.Empty;
        string cf5 = string.Empty;
        string cf6 = string.Empty;

        DateTime financialYearStart;
        DateTime financialYearEnd;

        public InvoiceSettings(int CompanyId)
        {
            InitializeComponent();
            loadCompany();
            comboBoxCompany.SelectedValue = CompanyId;
            bindLogo();
            getActiveFinancialYear();
        }

        private void bindFinancialYear()
        {


            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                cmd = new SqlCommand("select StartDate, EndDate from SettingFinancialYear where IsActive = 1 and CompanyId = @CompanyId", con);

                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));

                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        if (reader.Read())
                        {
                            dateTimePickerYearStartDate.Value = reader.GetDateTime(0);
                            dateTimePickerYearEndDate.Value = reader.GetDateTime(1);
                        }
                        else
                        {
                            dateTimePickerYearStartDate.Value = DateTime.Today;
                            dateTimePickerYearEndDate.Value = DateTime.Today;
                        }
                    }
                }
            }
        }

        private void buttonSaveDocSettings_Click(object sender, EventArgs e)
        {
            if (comboBoxDocumentType.Text != string.Empty && textBoxSeriesStart.Text.Trim() != string.Empty)
            {
                saveSettings();
                saveSettingsDocumentIdResetDetail();
                MessageBox.Show("Record Saved Successfully", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
            }
            else
            {
                MessageBox.Show("Please provide Details!", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDocumentSettings();
        }

        private void saveSettings()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                cmd = new SqlCommand("delete SettingDocument WHERE CompanyId = @CompanyId and DocumentType=@DocumentType " +
                    "INSERT INTO [SettingDocument] " +
            " ([IsAutoDocumentSequanceNumber] " +
                " ,[CompanyId] " +
                " ,[DocumentType] " +
                " ,[IdPrefix] " +
                " ,[IdSuffix] " +
                " ,[IdSeriesStart] " +
                " ,[DocumentIdResetFlag]) " +
          " VALUES " +
                " (@IsAutoDocumentSequanceNumber " +
                " , @CompanyId " +
                " , @DocumentType " +
                " , @IdPrefix " +
                " , @IdSuffix " +
                " , @IdSeriesStart " +
                " , @DocumentIdResetFlag)", con);

                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentType.Text).Trim());

                cmd.Parameters.AddWithValue("@IsAutoDocumentSequanceNumber", Convert.ToBoolean(checkBoxIsAutoSequenceNumber.CheckState));
                cmd.Parameters.AddWithValue("@IdPrefix", Convert.ToString(textBoxPrefix.Text).Trim());
                cmd.Parameters.AddWithValue("@IdSuffix", Convert.ToString(textBoxSuffix.Text).Trim());
                cmd.Parameters.AddWithValue("@IdSeriesStart", Convert.ToString(textBoxSeriesStart.Text).Trim());

                cmd.Parameters.AddWithValue("@DocumentIdResetFlag", Convert.ToBoolean(checkBoxIsAutoSequenceNumber.Checked));

                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void saveTerms()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                cmd = new SqlCommand(@"delete SettingTerms WHERE CompanyId = @CompanyId and DocumentType=@DocumentType 
                    INSERT INTO [SettingTerms] 
             (CompanyId,DocumentType,[TermsAndConditions]) VALUES (@CompanyId,@DocumentType,@TermsAndConditions)", con);

                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentTypeTerms.Text).Trim());

                cmd.Parameters.AddWithValue("@TermsAndConditions", Convert.ToString(textBoxTermsAndConditions.Text).Trim());

                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void saveSettingsDocumentIdResetDetail()
        {
            //if (buttonSaveDocSettings.Text == "Update" && (docIdAutoSeqNo == false && checkBoxIsAutoSequenceNumber.Checked == true))
            //{

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                Int32 maxDocumentIdWhileResetting = 0;
                if (comboBoxDocumentType.Text == "Sales Invoice")
                {
                    cmd = new SqlCommand("select max(id) MaxDocumentId from [TranSalesInvoice]", con);
                }
                else if (comboBoxDocumentType.Text == "Purchase Invoice")
                {
                    cmd = new SqlCommand("select max(id) MaxDocumentId from [TranPurchaseInvoice]", con);
                }
                else if (comboBoxDocumentType.Text == "Quotation")
                {
                    cmd = new SqlCommand("select max(id) MaxDocumentId from [TranQuotation]", con);
                }
                else if (comboBoxDocumentType.Text == "Delivery Note")
                {
                    cmd = new SqlCommand("select max(id) MaxDocumentId from [TranDeliveryNote]", con);
                }
                else if (comboBoxDocumentType.Text == "Receipt Note")
                {
                    cmd = new SqlCommand("select max(id) MaxDocumentId from [TranReceiptNote]", con);
                }
                using (cmd)
                {
                    var maxDoucmentId = Convert.ToString(cmd.ExecuteScalar());
                    if (maxDoucmentId != string.Empty)
                        maxDocumentIdWhileResetting = Convert.ToInt32(maxDoucmentId);
                    else
                        maxDocumentIdWhileResetting = 0;
                }

                cmd = new SqlCommand("delete SettingDocumentIdResetDetail WHERE CompanyId = @CompanyId and DocumentType=@DocumentType " +
                    "INSERT INTO [SettingDocumentIdResetDetail] " +
            " ([CompanyId] " +
                " ,[DocumentType] " +
                " ,[MaxDocumentIdWhileResettingSequence] ) " +
          " VALUES " +
                " (@CompanyId " +
                " , @DocumentType " +
                " , @MaxDocumentIdWhileResettingSequence )", con);

                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentType.Text).Trim());

                cmd.Parameters.AddWithValue("@MaxDocumentIdWhileResettingSequence", maxDocumentIdWhileResetting);

                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            //}
        }


        private void saveLogo()
        {
            try
            {


                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    //if (ID == 0)
                    //{
                    cmd = new SqlCommand("delete SettingLogo WHERE CompanyId = @CompanyId " +
                        " INSERT INTO [SettingLogo] " +
           " ([CompanyId] " +
           " ,CompanyLogo) " +
     " VALUES " +
           " (@CompanyId " +
           " , @CompanyLogo)", con);
                    //            }
                    //            else
                    //            {
                    //                cmd = new SqlCommand("UPDATE [SettingDocument] " +
                    // " SET [CompanyId] = @CompanyId " +
                    //  " ,[CompanyLogo] = @CompanyLogo " +
                    //" WHERE CompanyId = @CompanyId", con);

                    cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));

                    //if (imagename != "")
                    //{

                    //fs = new FileStream(imagename, FileMode.Open, FileAccess.Read);

                    //a byte array to read the image
                    using (FileStream fs = new FileStream(imagename, FileMode.Open, FileAccess.Read))
                    {
                        byte[] picbyte = new byte[fs.Length];
                        fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
                        SqlParameter picparameter = new SqlParameter();

                        picparameter.SqlDbType = SqlDbType.Image;

                        picparameter.ParameterName = "CompanyLogo";

                        picparameter.Value = picbyte;
                        cmd.Parameters.Add(picparameter);
                    }

                    //}
                    //else
                    //{

                    //}

                    //cmd.Parameters.AddWithValue("@CompanyLogo", Convert.ToString(textBoxTermsAndConditions.Text).Trim());

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void bindLogo()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                //if (ID == 0)
                //{
                cmd = new SqlCommand("Select CompanyLogo from SettingLogo Where CompanyId=@CompanyId", con);

                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));

                //FileStream FS1 = new FileStream("image.jpg", FileMode.Create);

                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            byte[] byteImage = (byte[])reader[0];

                            //FS1.Write(byteImage, 0, byteImage.Length);

                            //FS1.Close();

                            //FS1 = null;

                            pictureBoxLogo.Image = GetDataToImage(byteImage);

                            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBoxLogo.Refresh();
                        }
                    }
                }
            }
        }

        private void bindDocumentSettings()
        {
            if (comboBoxDocumentType.Text != string.Empty)
            {

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    //if (ID == 0)
                    //{
                    cmd = new SqlCommand("SELECT [IdPrefix],[IdSeriesStart],[IsAutoDocumentSequanceNumber], [DocumentIdResetFlag],[IdSuffix] " +
      "FROM [SettingDocument] where CompanyId = @CompanyId and DocumentType = @DocumentType", con);

                    cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentType.Text));

                    //FileStream FS1 = new FileStream("image.jpg", FileMode.Create);

                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            if (reader.Read())
                            {
                                //while (reader.Read())
                                //{
                                textBoxPrefix.Text = reader.GetString(0);
                                docIdPrefix = textBoxPrefix.Text;
                                textBoxSeriesStart.Text = Convert.ToString(reader[1]);
                                docSerialStartNo = textBoxSeriesStart.Text;
                                checkBoxIsAutoSequenceNumber.Checked = reader.GetBoolean(2);
                                docIdAutoSeqNo = checkBoxIsAutoSequenceNumber.Checked;
                                textBoxSuffix.Text = reader.GetString(4);
                                docIdSuffix = textBoxSuffix.Text;
                                buttonSaveDocSettings.Text = "Update";
                                buttonSaveDocSettings.Enabled = false;
                                //}
                            }
                            else
                            {
                                docIdPrefix = string.Empty;
                                docIdSuffix = string.Empty;
                                docSerialStartNo = string.Empty;
                                docIdAutoSeqNo = false;
                                checkBoxIsAutoSequenceNumber.Checked = false;
                                textBoxPrefix.Text = "";
                                textBoxSuffix.Text = "";
                                textBoxSeriesStart.Text = "";
                                buttonSaveDocSettings.Text = "Save";
                                buttonSaveDocSettings.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                docIdPrefix = string.Empty;
                docIdSuffix = string.Empty;
                docSerialStartNo = string.Empty;
                docIdAutoSeqNo = false;
                checkBoxIsAutoSequenceNumber.Checked = false;
                textBoxPrefix.Text = "";
                textBoxSuffix.Text = "";
                textBoxSeriesStart.Text = "";
                buttonSaveDocSettings.Text = "Save";
                buttonSaveDocSettings.Enabled = true;
            }

        }

        private void bindTerms()
        {
            if (comboBoxDocumentTypeTerms.Text != string.Empty)
            {

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    //if (ID == 0)
                    //{
                    cmd = new SqlCommand("SELECT [TermsAndConditions] FROM [SettingTerms] where CompanyId = @CompanyId and DocumentType = @DocumentType", con);

                    cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentTypeTerms.Text));

                    //FileStream FS1 = new FileStream("image.jpg", FileMode.Create);

                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            if (reader.Read())
                            {
                                textBoxTermsAndConditions.Text = reader.GetString(0);
                            }
                            else
                            {
                                textBoxTermsAndConditions.Text = "";
                            }
                        }
                    }
                }
            }
            else
            {
                textBoxTermsAndConditions.Text = "";
            }

        }

        public Image GetDataToImage(byte[] pData)
        {
            Image image = null;
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                image = imgConverter.ConvertFrom(pData) as Image;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return image;
        }

        private void ClearData()
        {
            comboBoxDocumentType.SelectedIndex = 0;
            checkBoxIsAutoSequenceNumber.Checked = false;
            textBoxPrefix.Text = "";
            textBoxSuffix.Text = "";
            textBoxSeriesStart.Text = "";
            buttonSaveDocSettings.Text = "Save";
        }

        private void ClearTerms()
        {
            comboBoxDocumentTypeTerms.SelectedIndex = 0;
            textBoxTermsAndConditions.Text = "";
        }

        private void checkBoxIsAutoSequenceNumber_CheckedChanged(object sender, EventArgs e)
        {
            checkDocSettingUpdates();
        }

        private void buttonOpenLogo_Click(object sender, EventArgs e)
        {
            try
            {

                FileDialog fileDialog = new OpenFileDialog();

                //specify your own initial directory

                //fileDialog.InitialDirectory = @":D\";
                fileDialog.Title = "Select Logo";
                //this will allow only those file extensions to be added

                //fileDialog.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fileDialog.Filter = "Images (*.ICO;*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.ICO;*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {

                    imagename = fileDialog.FileName;
                    Bitmap newimg = new Bitmap(imagename);

                    pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;

                    pictureBoxLogo.Image = (Image)newimg;

                }

                fileDialog = null;

            }

            catch (System.ArgumentException ae)
            {
                imagename = string.Empty;
                MessageBox.Show(ae.Message.ToString());
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCompany()
        {
            comboBoxCompany.ValueMember = "ID";
            comboBoxCompany.DisplayMember = "CompanyName";
            comboBoxCompany.DataSource = masterSelection.GetCompanyList();

            comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void buttonSaveLogo_Click(object sender, EventArgs e)
        {
            if (pictureBoxLogo.Image != null)
            {
                saveLogo();
                MessageBox.Show("Logo Saved Successfully", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please provide Detail!", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxSeriesStart_TextChanged(object sender, EventArgs e)
        {
            checkDocSettingUpdates();
        }


        private void textBoxPrefix_TextChanged(object sender, EventArgs e)
        {
            checkDocSettingUpdates();
        }

        private void textBoxSuffix_TextChanged(object sender, EventArgs e)
        {
            checkDocSettingUpdates();
        }

        private void checkDocSettingUpdates()
        {
            if (buttonSaveDocSettings.Text == "Update"
                && (docIdAutoSeqNo != (Boolean)checkBoxIsAutoSequenceNumber.Checked
                || docSerialStartNo != textBoxSeriesStart.Text.Trim()
                || docIdPrefix != textBoxPrefix.Text.Trim()
                || docIdSuffix != textBoxSuffix.Text.Trim()
                ))
            {
                buttonSaveDocSettings.Enabled = true;
            }
            else if (buttonSaveDocSettings.Text == "Update")
            {
                buttonSaveDocSettings.Enabled = false;
            }
        }

        private void buttonDeleteLogo_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxLogo.Image != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        deleteItem();
                        MessageBox.Show("Logo Deleted Successfully", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No logo found", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                cmd = new SqlCommand("Delete SettingLogo Where CompanyId=@CompanyId ", con);
                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            pictureBoxLogo.Image = null;
            pictureBoxLogo.Refresh();
        }

        private void buttonSaveCustomFields_Click(object sender, EventArgs e)
        {
            if (comboBoxDocumentTypeCustomFields.Text != string.Empty)
            {
                saveCustomFields();
                MessageBox.Show("Record Saved Successfully", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearDataCustomFields();
            }
            else
            {
                MessageBox.Show("Please select Document Type", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxDocumentTypeCustomFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDCustomFieldSettings();
        }


        private void saveCustomFields()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                cmd = new SqlCommand("delete SettingCustomFields WHERE CompanyId = @CompanyId and DocumentType=@DocumentType " +
                    "INSERT INTO [dbo].[SettingCustomFields] " +
           " ([CompanyId],[DocumentType] " +
           " ,[CF1],[CF2],[CF3] " +
           " ,[CF4],[CF5],[CF6]) " +
     " VALUES " +
           " (@CompanyId, @DocumentType " +
           " , @CF1, @CF2, @CF3 " +
           " , @CF4, @CF5, @CF6)", con);

                cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentTypeCustomFields.Text).Trim());
                cmd.Parameters.AddWithValue("@CF1", Convert.ToString(textBoxCustomField1.Text).Trim());
                cmd.Parameters.AddWithValue("@CF2", Convert.ToString(textBoxCustomField2.Text).Trim());
                cmd.Parameters.AddWithValue("@CF3", Convert.ToString(textBoxCustomField3.Text).Trim());
                cmd.Parameters.AddWithValue("@CF4", Convert.ToString(textBoxCustomField4.Text).Trim());
                cmd.Parameters.AddWithValue("@CF5", Convert.ToString(textBoxCustomField5.Text).Trim());
                cmd.Parameters.AddWithValue("@CF6", Convert.ToString(textBoxCustomField6.Text).Trim());

                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void bindDCustomFieldSettings()
        {
            if (comboBoxDocumentTypeCustomFields.Text != string.Empty)
            {

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    cmd = new SqlCommand("select [CF1],[CF2],[CF3],[CF4],[CF5],[CF6] " +
    " From SettingCustomFields where CompanyId = @CompanyId and DocumentType = @DocumentType", con);

                    cmd.Parameters.AddWithValue("@CompanyId", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@DocumentType", Convert.ToString(comboBoxDocumentTypeCustomFields.Text));

                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            if (reader.Read())
                            {
                                textBoxCustomField1.Text = Convert.ToString(reader["CF1"]);
                                cf1 = textBoxCustomField1.Text;
                                textBoxCustomField2.Text = Convert.ToString(reader["CF2"]);
                                cf2 = textBoxCustomField2.Text;
                                textBoxCustomField3.Text = Convert.ToString(reader["CF3"]);
                                cf3 = textBoxCustomField3.Text;
                                textBoxCustomField4.Text = Convert.ToString(reader["CF4"]);
                                cf4 = textBoxCustomField4.Text;
                                textBoxCustomField5.Text = Convert.ToString(reader["CF5"]);
                                cf5 = textBoxCustomField5.Text;
                                textBoxCustomField6.Text = Convert.ToString(reader["CF6"]);
                                cf6 = textBoxCustomField6.Text;

                                buttonSaveCustomFields.Text = "Update";
                                buttonSaveCustomFields.Enabled = false;
                            }
                            else
                            {
                                textBoxCustomField1.Text = string.Empty;
                                textBoxCustomField2.Text = string.Empty;
                                textBoxCustomField3.Text = string.Empty;
                                textBoxCustomField4.Text = string.Empty;
                                textBoxCustomField5.Text = string.Empty;
                                textBoxCustomField6.Text = string.Empty;
                                cf1 = string.Empty;
                                cf2 = string.Empty;
                                cf3 = string.Empty;
                                cf4 = string.Empty;
                                cf5 = string.Empty;
                                cf6 = string.Empty;

                                buttonSaveCustomFields.Text = "Save";
                                buttonSaveCustomFields.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                textBoxCustomField1.Text = string.Empty;
                textBoxCustomField2.Text = string.Empty;
                textBoxCustomField3.Text = string.Empty;
                textBoxCustomField4.Text = string.Empty;
                textBoxCustomField5.Text = string.Empty;
                textBoxCustomField6.Text = string.Empty;
                cf1 = string.Empty;
                cf2 = string.Empty;
                cf3 = string.Empty;
                cf4 = string.Empty;
                cf5 = string.Empty;
                cf6 = string.Empty;

                buttonSaveCustomFields.Text = "Save";
                buttonSaveCustomFields.Enabled = true;
            }
        }


        private void ClearDataCustomFields()
        {
            comboBoxDocumentTypeCustomFields.SelectedIndex = 0;
            textBoxCustomField1.Text = string.Empty;
            textBoxCustomField2.Text = string.Empty;
            textBoxCustomField3.Text = string.Empty;
            textBoxCustomField4.Text = string.Empty;
            textBoxCustomField5.Text = string.Empty;
            textBoxCustomField6.Text = string.Empty;
            cf1 = string.Empty;
            cf2 = string.Empty;
            cf3 = string.Empty;
            cf4 = string.Empty;
            cf5 = string.Empty;
            cf6 = string.Empty;

            buttonSaveCustomFields.Text = "Save";
            buttonSaveCustomFields.Enabled = true;
        }


        private void checkCustomFieldSettingUpdates()
        {
            if (buttonSaveCustomFields.Text == "Update"
                && (cf1 != textBoxCustomField1.Text.Trim()
                || cf2 != textBoxCustomField2.Text.Trim()
                || cf3 != textBoxCustomField3.Text.Trim()
                || cf4 != textBoxCustomField4.Text.Trim()
                || cf5 != textBoxCustomField5.Text.Trim()
                || cf6 != textBoxCustomField6.Text.Trim()
                ))
            {
                buttonSaveCustomFields.Enabled = true;
            }
            else if (buttonSaveCustomFields.Text == "Update")
            {
                buttonSaveCustomFields.Enabled = false;
            }
        }

        private void textBoxCustomField1_TextChanged(object sender, EventArgs e)
        {
            checkCustomFieldSettingUpdates();
        }

        private void textBoxCustomField2_TextChanged(object sender, EventArgs e)
        {
            checkCustomFieldSettingUpdates();
        }

        private void textBoxCustomField3_TextChanged(object sender, EventArgs e)
        {
            checkCustomFieldSettingUpdates();
        }

        private void textBoxCustomField4_TextChanged(object sender, EventArgs e)
        {
            checkCustomFieldSettingUpdates();
        }

        private void textBoxCustomField5_TextChanged(object sender, EventArgs e)
        {
            checkCustomFieldSettingUpdates();
        }

        private void textBoxCustomField6_TextChanged(object sender, EventArgs e)
        {
            checkCustomFieldSettingUpdates();
        }

        private void buttonSaveTerms_Click(object sender, EventArgs e)
        {
            if (comboBoxDocumentTypeTerms.Text != string.Empty)
            {
                saveTerms();
                MessageBox.Show("Terms & Conditions Saved Successfully", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearTerms();
            }
            else
            {
                MessageBox.Show("Please provide Details!", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxDocumentTypeTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindTerms();
        }

        private void buttonSaveBank_Click(object sender, EventArgs e)
        {

        }

        private void buttonClearBank_Click(object sender, EventArgs e)
        {

        }

        private void buttonDeleteBank_Click(object sender, EventArgs e)
        {

        }

        private void buttonSaveFinancialYear_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Setting_FinancialYear"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePickerYearStartDate.Value);
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePickerYearEndDate.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", Entities.AuthenticationDetail.UserName);
                    cmd.Parameters.AddWithValue("@mode", "InsertYear");

                    cmd.ExecuteNonQuery();
                }
            }

            financialYearStart = dateTimePickerYearStartDate.Value;
            financialYearEnd = dateTimePickerYearEndDate.Value;
            MessageBox.Show("Record Saved Successfully", "Financial Year Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonResetFinancialYear_Click(object sender, EventArgs e)
        {
            dateTimePickerYearStartDate.Value = financialYearStart;
            dateTimePickerYearEndDate.Value = financialYearEnd;
        }

        private void getActiveFinancialYear()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Setting_FinancialYear"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyId", comboBoxCompany.SelectedValue);
                    cmd.Parameters.AddWithValue("@mode", "SelectActiveYear");

                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        if (reader.Read())
                        {
                            financialYearStart = reader.GetDateTime(0);
                            financialYearEnd = reader.GetDateTime(1);
                        }
                        else
                        {
                            financialYearStart = DateTime.Today;
                            financialYearEnd = DateTime.Today;
                        }
                        dateTimePickerYearStartDate.Value = financialYearStart;
                        dateTimePickerYearEndDate.Value = financialYearEnd;
                    }
                }
            }
        }
    }
}
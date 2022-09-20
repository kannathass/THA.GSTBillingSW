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
using thaLicensing = THA.GSTBillingSW.Licensing;

namespace THA.GSTBillingSW.Setting
{
    public partial class THARegistration : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public THARegistration()
        {
            InitializeComponent();
            labelHDSerialNumberDisabled.Text = thaLicensing.BAL.AccessHW.GetHDDetail().Trim();
            string hdSerialNumber = thaLicensing.BAL.CryptoEngine.Encrypt(labelHDSerialNumberDisabled.Text, "6b61-6e6e-617468");

            labelHDkey.Text = hdSerialNumber;
        }

        private void buttonRegisterMe_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxRegistrationKey.Text.Trim() != "")  // Demo Key entered in the format {HDKey}!{NumberOfDays} Eg. ABCDE|100
                {
                    int licenseDays = getLicenseDays();

                    if (licenseDays >= 0)
                    {
                        saveItem(licenseDays);
                        THALogin loginForm = new THALogin();
                        this.Hide();
                        loginForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Provided license key is not valid", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please provide license key", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fetching demo days from license key. If genione version then there won't be any days in the key.
        /// </summary>
        /// <returns></returns>
        private int getLicenseDays()
        {
            string hdSerialNumber = thaLicensing.BAL.CryptoEngine.Decrypt(textBoxRegistrationKey.Text.Trim(), "793c-4421-404838");

            string[] strArr = hdSerialNumber.Split('|');
            DateTime dateOfExpire;
            int availableLicenseDays;
            DateTime currentDate;
            if (strArr.Count() == 5) //if count = 2 then Demo version
            {
                currentDate = BAL.GenericValidation.getCurrentDate();
                dateOfExpire = new DateTime(Convert.ToInt16(strArr[3]), Convert.ToInt16(strArr[2]), Convert.ToInt16(strArr[1]));
                availableLicenseDays = (dateOfExpire.Date - currentDate.Date).Days;

                if (availableLicenseDays < 0)
                {
                    return -1;
                }
                else
                {
                    return availableLicenseDays;
                }
            }
            if (strArr.Count() == 4) //if count = 3 then subscriber version
            {
                if (strArr[0] != labelHDSerialNumberDisabled.Text) //Verifying license key matching
                {
                    return -1;
                }
                else
                {
                    currentDate = BAL.GenericValidation.getCurrentDate();
                    dateOfExpire = new DateTime(Convert.ToInt16(strArr[3]), Convert.ToInt16(strArr[2]), Convert.ToInt16(strArr[1]));
                    availableLicenseDays = (dateOfExpire.Date - currentDate.Date).Days;

                    if (availableLicenseDays < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return availableLicenseDays;
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        private void saveItem(int licenseDays)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                DateTime currentDate = BAL.GenericValidation.getCurrentDate();
                string day;
                string month;
                string year;
                DateTime expiryDay = currentDate.AddDays(licenseDays);

                //if (licenseDays == 365)
                //{
                day = thaLicensing.BAL.CryptoEngine.Encrypt(expiryDay.Day.ToString(), "793c-4421-404838");
                month = thaLicensing.BAL.CryptoEngine.Encrypt(expiryDay.Month.ToString(), "793c-4421-404838");
                year = thaLicensing.BAL.CryptoEngine.Encrypt(expiryDay.Year.ToString(), "793c-4421-404838");
                //}
                //else
                //{
                //    day = thaLicensing.BAL.CryptoEngine.Encrypt(expiryDay.Day.ToString(), "793c-4421-404838");
                //    month = thaLicensing.BAL.CryptoEngine.Encrypt(expiryDay.Month.ToString(), "793c-4421-404838");
                //    year = thaLicensing.BAL.CryptoEngine.Encrypt(expiryDay.Year.ToString(), "793c-4421-404838");
                //}
                //day = thaLicensing.BAL.CryptoEngine.Encrypt(currentDate.AddDays(-1).Day.ToString(), "793c-4421-404838");
                //month = thaLicensing.BAL.CryptoEngine.Encrypt(currentDate.Month.ToString(), "793c-4421-404838");
                //year = thaLicensing.BAL.CryptoEngine.Encrypt(currentDate.AddYears(1).Year.ToString(), "793c-4421-404838");

                cmd = new SqlCommand("Delete [SettingDetce] " +
                    " INSERT INTO [SettingDetce] " +
       " ([Name],[TeLaire],[MoLaire],[EaLaire],[Latus],[Usnu]) " +
    " VALUES " +
       " (@Name, @TeLaire, @MoLaire, @EaLaire, @Latus, @Usnu)", con);

                cmd.Parameters.AddWithValue("@Name", Convert.ToString(textBoxRegistrationKey.Text).Trim());

                cmd.Parameters.AddWithValue("@TeLaire", day);
                cmd.Parameters.AddWithValue("@MoLaire", month);
                cmd.Parameters.AddWithValue("@EaLaire", year);
                cmd.Parameters.AddWithValue("@Latus", "/kLEBp10IN4=");
                cmd.Parameters.AddWithValue("@Usnu", "4mv0z3F3Jc8=");

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(labelHDkey.Text,TextDataFormat.Text);
            Clipboard.SetDataObject(labelHDkey.Text, true, 10, 100);
        }

        private void THARegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

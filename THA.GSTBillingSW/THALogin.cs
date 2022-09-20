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
using THA.GSTBillingSW.BAL;
using thaLicensing = THA.GSTBillingSW.Licensing;
namespace THA.GSTBillingSW
{
    public partial class THALogin : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        string licenseStatus = string.Empty;
        //Entities.AuthenticationDetail authenticationDetail = new Entities.AuthenticationDetail();


        public THALogin()
        {
            InitializeComponent();

            //string hdSerialNumber = thaLicensing.BAL.CryptoEngine.Encrypt(thaLicensing.BAL.AccessHW.GetHDDetail(), "793c-4421-404838");
            string hdSerialNumber = thaLicensing.BAL.AccessHW.GetHDDetail();

            licenseStatus = checkLicenseStatus(hdSerialNumber);

            //MessageBox.Show(hdSerialNumber);
            //MessageBox.Show(thaLicensing.BAL.CryptoEngine.Decrypt(hdSerialNumber, "793c-4421-404838"));
        }

        private void THALogin_Load(object sender, EventArgs e)
        {
            if (licenseStatus == "NotRegistered" || licenseStatus == "NotValid")
            {
                closeLoginForm();
                Setting.THARegistration formRegister = new Setting.THARegistration();
                formRegister.Show();
            }
            else if (licenseStatus == "Expired")
            {
                Entities.AuthenticationDetail.isLicenseExpired = true;
            }
        }

        private void closeLoginForm()
        {

            ////this.Hide();
            //this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }

        private string checkLicenseStatus(string hdNumber)
        {
            string dayOfExpire = string.Empty; //Laire
            string monthOfExpire = string.Empty; //Laire
            string yearOfExpire = string.Empty; //Laire
            string licenseStatus = string.Empty; //Latus
            string licenseKey = string.Empty;

            DateTime currentDate = new DateTime();
            using (SqlConnection con = new SqlConnection(conString))
            {

                con.Open();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("SELECT [TeLaire],[MoLaire],[EaLaire],GETDATE() CurrentDate, name FROM [dbo].[SettingDetce] where Latus='/kLEBp10IN4='", con);
                //cmd = new SqlCommand("SELECT [TeLaire],[MoLaire],[EaLaire],GETDATE() CurrentMonth FROM [dbo].[SettingDetce] where [name]=@name and Latus='/kLEBp10IN4='", con);
                //cmd.Parameters.AddWithValue("@name", hdNumber);
                using (cmd)
                {
                    //
                    // Invoke ExecuteReader method.
                    //
                    SqlDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dayOfExpire = reader.GetString(0);
                                monthOfExpire = reader.GetString(1);
                                yearOfExpire = reader.GetString(2);
                                currentDate = reader.GetDateTime(3);
                                // Subscripted Key saved in the format {HDKey}!{LicenseExpiryYear}|{LicenseExpiryMonth}|{LicenseExpiryDate} Eg. ABCDE|18|10|2017
                                // Trail Key saved in the format {HDKey}!{LicenseExpiryYear}|{LicenseExpiryMonth}|{LicenseExpiryDate} Eg. ABCDE|18|10|2017|Trail
                                licenseKey = reader.GetString(4);
                            }

                            string encryptedHDNumberFromDB = thaLicensing.BAL.CryptoEngine.Decrypt(licenseKey, "793c-4421-404838");

                            string[] strArr = encryptedHDNumberFromDB.Split('|');

                            DateTime dateOfExpire;
                            int availableLicenseDays;

                            //if (strArr.Count() == 2) //if count > 0 then Demo version
                            //{
                            //    if (strArr[0] != hdNumber)
                            //    {
                            //        return "NotValid";
                            //    }
                            //}
                            if (strArr.Count() == 4) //valid license key must have four values {key|date|month|year}
                            {
                                if (strArr[0] != hdNumber)
                                {
                                    return "NotValid";
                                }

                                dateOfExpire = new DateTime(Convert.ToInt16(strArr[3]), Convert.ToInt16(strArr[2]), Convert.ToInt16(strArr[1]));
                                availableLicenseDays = (dateOfExpire.Date - currentDate.Date).Days;

                                if (availableLicenseDays < 0)
                                {
                                    licenseStatus = "Expired";
                                    MessageBox.Show("License is Expired. Please contact license@THAsoft.com for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (availableLicenseDays == 0)
                                {
                                    licenseStatus = "Active";
                                    MessageBox.Show("License is expiring today. Please contact license@THAsoft.com for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (availableLicenseDays < 7)
                                {
                                    licenseStatus = "Active";
                                    MessageBox.Show("License will be expiring on " + dateOfExpire.Date.ToShortDateString() + ". Please contact license@THAsoft.com for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    licenseStatus = "Active";
                                }

                                //}

                            }
                            else if (strArr.Count() == 5) //valid trail license key must kave five values {key|date|month|year|Trail} 
                            {
                                if (strArr[0] != hdNumber)
                                {
                                    return "NotValid";
                                }
                                dateOfExpire = new DateTime(Convert.ToInt16(strArr[3]), Convert.ToInt16(strArr[2]), Convert.ToInt16(strArr[1]));
                                availableLicenseDays = (dateOfExpire.Date - currentDate.Date).Days;

                                if (availableLicenseDays < 0)
                                {
                                    licenseStatus = "Expired";
                                    MessageBox.Show("Trial Version is Expired. Please contact license@THAsoft.com for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (availableLicenseDays == 0)
                                {
                                    licenseStatus = "Active";
                                    MessageBox.Show("You are using Trail Version and it is expiring today. Please contact license@THAsoft.com for Registration", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    licenseStatus = "Active";
                                    MessageBox.Show("You are using Trail Version. It will be expiring on " + dateOfExpire.Date.ToShortDateString() + ". Please contact license@THAsoft.com for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                return "NotValid";
                            }
                            //else if (encryptedHDNumberFromDB != hdNumber)
                            //{
                            //    return "NotValid";
                            //}

                            //string day = thaLicensing.BAL.CryptoEngine.Decrypt(dayOfExpire, "793c-4421-404838");
                            //string month = thaLicensing.BAL.CryptoEngine.Decrypt(monthOfExpire, "793c-4421-404838");
                            //string year = thaLicensing.BAL.CryptoEngine.Decrypt(yearOfExpire, "793c-4421-404838");

                            //DateTime dateOfExpire = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));

                            //if (currentDate.Date < dateOfExpire.Date && strArr.Count() == 2)
                            //{
                            //    licenseStatus = "Active";
                            //    MessageBox.Show("You are using Trail Version. It will be expiring on  " + dateOfExpire.Date.ToShortDateString(), "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //}
                            //else if (currentDate.Date == dateOfExpire.Date && strArr.Count() == 2)
                            //{
                            //    licenseStatus = "Active";
                            //    MessageBox.Show("You are using Trail Version and it is expiring today. Please contact THAsoft for Registration", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else if (currentDate.Date < dateOfExpire.Date)
                            //{
                            //    licenseStatus = "Active";
                            //}
                            //else if (currentDate.Date == dateOfExpire.Date)
                            //{
                            //    licenseStatus = "Active";
                            //    MessageBox.Show("License is expiring today. Please contact THAsoft for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else
                            //{
                            //    licenseStatus = "Expired";
                            //    MessageBox.Show("License is Expired. Please contact THAsoft for renewal", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //}
                        }
                        else
                        {
                            licenseStatus = "NotRegistered";
                            MessageBox.Show("Please contact license@THAsoft.com for Registration", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            return licenseStatus;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            getAuthenticationDetail();
            if (Entities.AuthenticationDetail.IsActive == true)
            {
                closeLoginForm();
                var master = new MasterForm();
                //master.Closed += (s, args) => this.Close();
                master.Show();
            }
            else
            {
                MessageBox.Show("You can not login. Please contact administrator.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void getAuthenticationDetail()
        {

            //string authenticationStatus = string.Empty;
            //string userRole = string.Empty;
            //string userPrivilege = string.Empty;
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd = new SqlCommand("Select [Password],[Role],[Privilege],[IsUserActive] " +
        " FROM [SettingUser] where UserName=@UserName and IsUserActive=1", con);
                cmd.Parameters.AddWithValue("@UserName", textBoxUserName.Text.Trim());

                con.Open();
                using (cmd)
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    using (reader)
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string password = reader.GetString(0);
                                if (textBoxPassword.Text.Trim() == BAL.UserAuthentication.DecodeFrom64(password))
                                {
                                    Entities.AuthenticationDetail.UserRole = reader.GetString(1);
                                    Entities.AuthenticationDetail.UserPrivilege = reader.GetString(2);
                                    Entities.AuthenticationDetail.IsActive = true;
                                    Entities.AuthenticationDetail.UserName = textBoxUserName.Text.Trim();
                                    //Entities.AuthenticationDetail.isLicenseExpired = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}

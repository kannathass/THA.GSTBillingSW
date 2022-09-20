using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.DAL
{
    class LogoSelection
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;

        public byte[] GetCompanyLogo(int CompanyId)
        {
            byte[] byteImage = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT [Companylogo] FROM [SettingLogo] " +
                        " where CompanyId=@CompanyId"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyID", CompanyId);

                        using (cmd)
                        {
                            cmd.Connection = con;
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                while (reader.Read())
                                {
                                    byteImage = (byte[])reader[0];
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return byteImage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using THA.GSTBillingSW.Entities;
namespace THA.GSTBillingSW.DAL
{
    class CustomFieldSelection
    {
        CustomFields customFields;
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public CustomFields GetCustomFields(int CompanyId, string DocumentType)
        {
            customFields = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    cmd = new SqlCommand("SELECT CF1, CF2, CF3, CF4, CF5, CF6 " +
    " FROM SettingCustomFields " +
    " where companyId = @CompanyId and DocumentType = @DocumentType", con);
                    cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                    cmd.Parameters.AddWithValue("@DocumentType", DocumentType);

                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                customFields = new CustomFields
                                {
                                    CF1 = Convert.ToString(reader["CF1"]),
                                    CF2 = Convert.ToString(reader["CF2"]),
                                    CF3 = Convert.ToString(reader["CF3"]),
                                    CF4 = Convert.ToString(reader["CF4"]),
                                    CF5 = Convert.ToString(reader["CF5"]),
                                    CF6 = Convert.ToString(reader["CF6"])
                                };
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
            return customFields;
        }

        public string GetTermsAndConditions(int CompanyId, string DocumentType)
        {
            string termsAndConditions = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    cmd = new SqlCommand("select TermsAndConditions from [SettingTerms] where companyId = @CompanyId and DocumentType = @DocumentType", con);
                    cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                    cmd.Parameters.AddWithValue("@DocumentType", DocumentType);

                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                termsAndConditions = Convert.ToString(reader["TermsAndConditions"]);
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
            return termsAndConditions;
        }

        public DataTable GetCustomFieldData(string CustomFieldName, string DocumentType)
        {
            DataTable dt = new DataTable();
            string qry = string.Empty;
            if (DocumentType == "Sales Invoice")
            {
                qry = string.Format("SELECT distinct {0} FROM TranSalesInvoice where Del_State=0 order by {0}", CustomFieldName);
            }
            else if (DocumentType == "Purchase Invoice")
            {
                qry = string.Format("SELECT distinct {0} FROM TranPurchaseInvoice where Del_State=0 order by {0}", CustomFieldName);
            }
            else if (DocumentType == "Delivery Note")
            {
                qry = string.Format("SELECT distinct {0} FROM TranDeliveryNote where Del_State=0 order by {0}", CustomFieldName);
            }
            else if (DocumentType == "Receipt Note")
            {
                qry = string.Format("SELECT distinct {0} FROM TranReceiptNote where Del_State=0 order by {0}", CustomFieldName);
            }
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter(qry, con);

                    using (adapt)
                    {
                        adapt.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
    }
}

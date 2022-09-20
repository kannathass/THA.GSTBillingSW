using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.DAL
{
    class GenericValidation
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public Int32 GetTransactionCount(string query)
        {
            Int32 recordsCount = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    using (cmd)
                    {
                        recordsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return recordsCount;
        }

        public Int32 GetPreviousNextIdentity(string query)
        {
            Int32 identity = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    using (cmd)
                    {
                        identity = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return identity;
        }

        public string GetSingleRecordFromDB(string query)
        {
            string recordValue = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    using (cmd)
                    {
                        recordValue = Convert.ToString(cmd.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return recordValue;
        }

        public DateTime getCurrentDate()
        {
            DateTime currentDate = new DateTime();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    cmd = new SqlCommand("select getDate()", con);
                    con.Open();

                    currentDate = Convert.ToDateTime(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return currentDate;
        }
        public bool IsInvoiceNumberAlreadyExist(string InvoiceNumber, Int16 CompanyID, Int64 HeaderID, string mode)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "CheckAvailabilityInvoiceNo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyID);
                cmd.Parameters.AddWithValue("@HeaderID", HeaderID);
                cmd.Parameters.AddWithValue("@mode", mode);
                con.Open();
                Int16 count = Convert.ToInt16(cmd.ExecuteScalar());
                if (count != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }


}

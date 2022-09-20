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
    class GSTReturn
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        public DataSet BindDataSetGSTR1(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string Mode)
        {
            DataSet dsGSTR = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetGSTR1Info"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@Mode", Mode);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            //using (DataSet ds = new DataSet())
                            //{
                            sda.Fill(dsGSTR);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dsGSTR;
        }
    }
}

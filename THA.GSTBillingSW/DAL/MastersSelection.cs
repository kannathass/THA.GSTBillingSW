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
    class MastersSelection
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        CompanyInfo companyInfo;

        public DataTable GetCompanyList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter(@"select ID, [CompanyName] + ' | ' + [State] CompanyName 
                    from MasterCompany Where Del_State = 0 order by id ", con);

                    //                adapt = new SqlDataAdapter(" select 0 ID, '-Select-' CompanyName " +
                    //" UNION " +
                    //"select ID, [CompanyName] + ' | ' + [State] CompanyName from MasterCompany Where Del_State = 0", con);

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

        public CompanyInfo GetCompanyInfo(Int16 CompanyId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    cmd = new SqlCommand(@"SELECT [CompanyName]
      ,[Address]
      ,[State]
      ,[PinCode]
      ,[PAN]
      ,[GSTN]
      ,[GSTNPortalUserName]
      ,[Email]
      ,[Phone]
      ,[Mobile]
      ,[Website]
      ,[BankName]
      ,[Branch]
      ,[AccountNumber]
      ,[IFSC]
      ,[CompanyDisplayName]
      ,[StateCode]
  FROM [MasterCompany] where Del_State=0 and ID=@CompanyId", con);
                    cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                companyInfo = new CompanyInfo
                                {
                                    CompanyName = Convert.ToString(reader["CompanyName"]),
                                    CompanyDisplayName = Convert.ToString(reader["CompanyDisplayName"]),
                                    Address = Convert.ToString(reader["Address"]),
                                    State = Convert.ToString(reader["State"]),
                                    StateCode = Convert.ToString(reader["StateCode"]),
                                    PinCode = Convert.ToString(reader["PinCode"]),
                                    Phone = Convert.ToString(reader["Phone"]),
                                    Mobile = Convert.ToString(reader["Mobile"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    PAN = Convert.ToString(reader["PAN"]),
                                    GSTN = Convert.ToString(reader["GSTN"]),
                                    GSTNPortalUserName = Convert.ToString(reader["GSTNPortalUserName"]),
                                    WebSite = Convert.ToString(reader["WebSite"]),
                                    BankName = Convert.ToString(reader["BankName"]),
                                    Branch = Convert.ToString(reader["Branch"]),
                                    AccountNumber = Convert.ToString(reader["AccountNumber"]),
                                    IFSC = Convert.ToString(reader["IFSC"])
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
            return companyInfo;
        }

        public DataTable GetCompanyGeneral()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter(" select 0 ID, 'General' CompanyName", con);

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

        public DataTable GetCustomerList(string CustomerType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter(" select 0 ID, '-Select-' CustomerName " +
                            " UNION " +
                        " select ID, [CustomerName] + ' | ' + [State] CustomerName from MasterCustomer " +
                        " Where Del_State = 0 and ISNULL(CustomerStatus,'')!='Inactive' and CustomerType in ('All', '" + CustomerType + "') order by id", con);

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

        public DataTable GetAgentList(string CustomerType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter(" select 0 ID, '-Select-' CustomerName " +
                            " UNION " +
                        " select ID, [CustomerName] + ' | ' + [BillingAddress] + ' | ' + [State] CustomerName from MasterCustomer " +
                        " Where Del_State = 0 and ISNULL(CustomerStatus,'')!='Inactive' and CustomerType in ('" + CustomerType + "') order by id", con);

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

        public DataTable GetCustomerListByAgent(Int32 AgentId, string CustomerType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    if (AgentId != 0)
                    {
                        adapt = new SqlDataAdapter(" select 0 ID, '-Select-' CustomerName " +
                                " UNION " +
                            " select CustomerID ID, Cust.CustomerName + ' | ' + [State] CustomerName " +
                            " from TranSalesInvoice Sales inner " +
                            " join MasterCustomer Cust on Sales.CustomerID = Cust.ID " +
                            " where AgentID = " + AgentId + " and CustomerType in ('All', '" + CustomerType + "') and Sales.Del_State=0 order by ID", con);
                    }
                    else
                    {
                        adapt = new SqlDataAdapter(" select 0 ID, '-Select-' CustomerName " +
                                " UNION " +
                            " select CustomerID ID, Cust.CustomerName + ' | ' + [State] CustomerName " +
                            " from TranSalesInvoice Sales inner " +
                            " join MasterCustomer Cust on Sales.CustomerID = Cust.ID " +
                            " where CustomerType in ('All', '" + CustomerType + "') and Sales.Del_State=0 order by ID", con);
                    }
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

        public DataTable GetStateList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter("select Code, [Name] from MasterState where StateStatus='Active'", con);

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

        public DataTable GetGroupList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter(@"select * from vw_MasterGroup", con);

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

        public DataTable GetUoMList()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    adapt = new SqlDataAdapter("select ID, UoM from MasterUoM where UoMstatus='Active'", con);

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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.DAL
{
    class ReportGeneral
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        public StockDataSet BindDataSetStockList(Int16 CompanyId, string GroupName, string FilterValue)
        {
            StockDataSet dsStock = new StockDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT Item.[ID]
                                  ,[ItemDescription]
                                  ,[Size]
                                  ,[UoM]
                                  ,[ItemType]
                                  ,[HsnSac]
                                  ,[ItemSKUcode]
                                  ,[SellingPrice]
                                  ,[PurchasePrice]
                                  ,[DiscountPercent]
                                  ,[TaxRatePercent]
                                  ,[ItemNotes]
                                  ,[ItemImage]
                                  ,[GroupIDUnder]
                                  ,[DiscountAmount]
                                  ,[IsTaxIncluded]
                                  ,[BrandName]
                                    ,GroupName
                              FROM [MasterItem] Item
                  inner join vw_MasterGroup Grp   
                  on Item.GroupIDUnder=Grp.ID 
               where Item.Del_State=0 
               and (ItemDescription like @FilterValue or Size like @FilterValue 
                    or UoM like @FilterValue or HsnSac like @FilterValue)
                    and GroupName in (select groupname from vw_MasterGroup where GroupName like @GroupName)"))
                    //and (ItemDescription like @FilterValue or Size like @FilterValue 
                    //     or UoM like @FilterValue or HsnSac like @FilterValue
                    //     or GroupName like @GroupName)"))
                    {
                        cmd.Parameters.AddWithValue("@FilterValue", "%" + FilterValue + "%");
                        cmd.Parameters.AddWithValue("@GroupName", "%" + GroupName + "%");

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (StockDataSet.MasterItemDataTable dtItem = new StockDataSet.MasterItemDataTable())
                            {
                                sda.Fill(dtItem);
                                dsStock.Tables.Remove("MasterItem");
                                dsStock.Tables.Add(dtItem);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT [ID]
                              ,[CompanyID]
                              ,[ItemID]
                              ,[StockInHand]
                          FROM [dbo].[MasterStockList]
                            where CompanyID = @CompanyId"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (StockDataSet.MasterStockListDataTable dtStockList = new StockDataSet.MasterStockListDataTable())
                            {
                                sda.Fill(dtStockList);
                                dsStock.Tables.Remove("MasterStockList");
                                dsStock.Tables.Add(dtStockList);
                            }
                        }
                    }
                    string qry;
                    string generalCompanyName = ConfigurationManager.AppSettings["GeneralStockCompanyDisplayName"].ToString();
                    if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                        qry = @"SELECT [ID]
                                  ,[CompanyName]
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
                              FROM [dbo].[MasterCompany]
                            where ID = @CompanyId";
                    else
                        qry = "Select 0 [ID],'" + generalCompanyName + "' CompanyName ";

                    using (SqlCommand cmd = new SqlCommand(qry))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (StockDataSet.MasterCompanyDataTable dtStockList = new StockDataSet.MasterCompanyDataTable())
                            {
                                sda.Fill(dtStockList);
                                dsStock.Tables.Remove("MasterCompany");
                                dsStock.Tables.Add(dtStockList);
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
            return dsStock;
        }
    }
}

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
    class ReportPurchaseInvoice
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        public BillingDataSet BindDataSetPurchaseInvoice(Int32 PurchaseInvoiceID, Int16 CompanyId)
        {
            BillingDataSet dsInvoice = new BillingDataSet();
            //System.Data.DataSet dsInvoice=new System.Data.DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT PurchaseInv.ID,[CompanyName],[InvoiceNumber],[InvoiceDate],[InvoiceDueDate],[CustomerName],[PONumber],[PODate]
               ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount], RoundedOffInvoiceTotalAmount [InvoiceTotalAmount]
               ,[CustomerNotes], BillingAddress, ShippingAddress
               ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
               ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
               ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
               , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
               , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
               , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
               , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
               FROM [TranPurchaseInvoice] PurchaseInv
               inner join MasterCompany Comp on PurchaseInv.CompanyID = Comp.ID and PurchaseInv.ID = @ID
               inner join MasterCustomer Cust on PurchaseInv.CustomerID = Cust.ID"))
                    {
                        cmd.Parameters.AddWithValue("@ID", PurchaseInvoiceID);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (BillingDataSet.dtBillingHeaderDataTable dtInvoiceHeader = new BillingDataSet.dtBillingHeaderDataTable())
                            {

                                sda.Fill(dtInvoiceHeader);
                                //dsInvoice.Tables.Remove(dtInvoiceHeader);
                                dsInvoice.Tables.Remove("dtBillingHeader");
                                dsInvoice.Tables.Add(dtInvoiceHeader);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT 
            case when ItemCustomDescription != '' then [ItemDescription] + ' ( ' + [ItemCustomDescription] + ' )' else ItemDescription end ItemDescription
            ,[HsnSac],[Qty]
             ,[UoM],[RatePerItem],[Discount],[TaxableValue]
             ,[CGSTValue],[SGSTValue],[IGSTValue]
             ,[CESSValue],[TotalValue], PurchaseInvoiceHeaderID HeaderID
             ,CGSTPercent, SGSTPercent, IGSTPercent, CESSPercent, DiscountPercent
           FROM [TranPurchaseInvoiceItems] where PurchaseInvoiceHeaderID = @ID "))
                    {
                        cmd.Parameters.AddWithValue("@ID", PurchaseInvoiceID);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (BillingDataSet.dtBillingItemsDataTable dtInvoiceItems = new BillingDataSet.dtBillingItemsDataTable())
                            {
                                sda.Fill(dtInvoiceItems);
                                //dsInvoice.Tables.Remove(dtInvoiceItems);
                                dsInvoice.Tables.Remove("dtBillingItems");

                                dsInvoice.Tables.Add(dtInvoiceItems);
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
            return dsInvoice;
        }

        public BillingDataSet BindDataSetPurchaseInvoiceDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            BillingDataSet dsInvoice = new BillingDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT PurchaseInv.ID,[CompanyName],[InvoiceNumber]
               ,[InvoiceDate],[InvoiceDueDate],[CustomerName],[PONumber],[PODate]
               ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffInvoiceTotalAmount [InvoiceTotalAmount]
               ,[CustomerNotes], BillingAddress, ShippingAddress
               ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
               ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
               ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
               , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
               , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
               , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
               , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
               FROM [TranPurchaseInvoice] PurchaseInv
               inner join MasterCompany Comp on PurchaseInv.CompanyID = Comp.ID and PurchaseInv.CompanyID = @CompanyId
               inner join MasterCustomer Cust on PurchaseInv.CustomerID = Cust.ID
               where InvoiceDate between @FromDate and @ToDate and InvoiceStatus<>'Canceled' and PurchaseInv.del_state='0'
               and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue or Cust.State like @FilterValue)"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + FilterValue + "%").Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (BillingDataSet.dtBillingHeaderDataTable dtInvoiceHeader = new BillingDataSet.dtBillingHeaderDataTable())
                            {
                                sda.Fill(dtInvoiceHeader);
                                dsInvoice.Tables.Remove("dtBillingHeader");
                                dsInvoice.Tables.Add(dtInvoiceHeader);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT 
            case when ItemCustomDescription != '' then [ItemDescription] + ' ( ' + [ItemCustomDescription] + ' )' else ItemDescription end ItemDescription
            ,[HsnSac],[Qty]
             ,[UoM],[RatePerItem],[Discount],[TaxableValue]
             ,[CGSTValue],[SGSTValue],[IGSTValue]
             ,[CESSValue],[TotalValue], PurchaseInvoiceHeaderID HeaderID
             ,CGSTPercent, SGSTPercent, IGSTPercent, CESSPercent, DiscountPercent
           FROM [TranPurchaseInvoiceItems] where PurchaseInvoiceHeaderID in 
           (select Id FROM [TranPurchaseInvoice] where CompanyID = @CompanyId and InvoiceDate between @FromDate and @ToDate and del_state='0') and del_state='0' "))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (BillingDataSet.dtBillingItemsDataTable dtInvoiceItems = new BillingDataSet.dtBillingItemsDataTable())
                            {
                                sda.Fill(dtInvoiceItems);
                                dsInvoice.Tables.Remove("dtBillingItems");

                                dsInvoice.Tables.Add(dtInvoiceItems);
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
            return dsInvoice;
        }


        public BillingDataSet BindDataSetPurchaseInvoiceConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            BillingDataSet dsInvoice = new BillingDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT PurchaseInv.ID,[CompanyName],[InvoiceNumber]
               ,[InvoiceDate],[InvoiceDueDate],[CustomerName],[PONumber],[PODate]
               ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffInvoiceTotalAmount [InvoiceTotalAmount]
               ,[CustomerNotes], BillingAddress, ShippingAddress
               ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
               ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
               ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
               , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
               , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
               , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
               , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
               FROM [TranPurchaseInvoice] PurchaseInv
               inner join MasterCompany Comp on PurchaseInv.CompanyID = Comp.ID and PurchaseInv.CompanyID = @CompanyId
               inner join MasterCustomer Cust on PurchaseInv.CustomerID = Cust.ID
               where InvoiceDate between @FromDate and @ToDate and InvoiceStatus<>'Canceled' and PurchaseInv.del_state='0'
               and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue or Cust.State like @FilterValue)"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + FilterValue + "%").Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (BillingDataSet.dtBillingHeaderDataTable dtInvoiceHeader = new BillingDataSet.dtBillingHeaderDataTable())
                            {
                                sda.Fill(dtInvoiceHeader);
                                dsInvoice.Tables.Remove("dtBillingHeader");
                                dsInvoice.Tables.Add(dtInvoiceHeader);
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
            return dsInvoice;
        }
    }
}

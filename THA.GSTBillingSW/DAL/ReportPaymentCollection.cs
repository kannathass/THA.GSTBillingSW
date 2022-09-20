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
    class ReportPaymentCollection
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        DAL.CustomFieldSelection customFieldSelection = new DAL.CustomFieldSelection();
        DAL.MastersSelection masterSelection = new DAL.MastersSelection();
        DAL.LogoSelection logoSelection = new DAL.LogoSelection();

        public PaymentDataset BindDataSetPaymentCollection(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            PaymentDataset dsPayment = new PaymentDataset();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT Payment.[ID],CompanyDisplayName CompanyName,[ReceiptNumber],[ReceiptDate]
	  ,Cust.CustomerName,Cust.State CustomerState,[PaymentMode],[PaymentReference]
	  ,[TotalPayment],[PaidToAccountID],[Notes]
	  , AgentList.AgentName, AgentList.AgentAddress, AgentList.AgentState
	  ,[InvoiceID],PaymentList.InvoiceNumber,PaymentList.InvoiceDate,[InvoiceAmount]
      ,[Balance],[AmountReceived],[Discount],[Interest]
      ,[CFItem1],[CFItem2],[CFItem3],[CFItem4]
  FROM [dbo].[TranPaymentCollection] Payment
		inner join TranPaymentCollectionList PaymentList on Payment.ID = PaymentList.HeaderID and Payment.Del_State='0'
		inner join TranSalesInvoice Sales on  Sales.ID = PaymentList.InvoiceID   
		inner join MasterCompany Comp on Payment.CompanyID = Comp.ID and Payment.CompanyID = @CompanyId
		inner join MasterCustomer Cust on Payment.CustomerID = Cust.ID
		left join (Select ID, CustomerName AgentName,BillingAddress AgentAddress, State AgentState from MasterCustomer where CustomerType='Agent') AgentList on AgentList.ID = Sales.AgentID 
		where ReceiptDate between @FromDate and @ToDate and Payment.Del_State='0'
		and (Cust.CustomerName like @FilterValue or Cust.State like @FilterValue)"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + FilterValue + "%").Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (PaymentDataset.dtPaymentCollectionDataTable dtPaymentCollection = new PaymentDataset.dtPaymentCollectionDataTable())
                            {
                                sda.Fill(dtPaymentCollection);
                                dsPayment.Tables.Remove("dtPaymentCollection");
                                dsPayment.Tables.Add(dtPaymentCollection);
                            }
                        }
                    }

                    Entities.CustomFields customFields;

                    customFields = customFieldSelection.GetCustomFields(CompanyId, "Payment Collection Items");

                    using (NoteDataSet.dtCustomFieldsItemHeaderDataTable dtCustomFieldsHeader = new NoteDataSet.dtCustomFieldsItemHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsPayment.Tables.Remove("dtCustomFieldsItemHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsPayment.Tables.Add(dtCustomFieldsHeader);
                        }
                    }

                    //byte[] byteImage = logoSelection.GetCompanyLogo(CompanyId);
                    //if (byteImage != null)
                    //{
                    //    using (NoteDataSet.dtCompanyLogoDataTable dtCompanyLogo = new NoteDataSet.dtCompanyLogoDataTable())
                    //    {
                    //        dsPayment.Tables.Remove("dtCompanyLogo");
                    //        dtCompanyLogo.Rows.Add();
                    //        dtCompanyLogo.Rows[0]["Logo"] = byteImage;
                    //        dsPayment.Tables.Add(dtCompanyLogo);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dsPayment;
        }

        public PaymentDataset BindDataSetPaymentCollectionPending(DateTime FromDate, DateTime ToDate, Int16 CompanyId, Int32 CustomerId, Int32 AgentId, string FilterValue)
        {
            string filterQuery = string.Empty;
            PaymentDataset dsPayment = new PaymentDataset();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    filterQuery = @"select Comp.CompanyDisplayName CompanyName,  Cust.CustomerName, Cust.State CustomerState
, AgentList.AgentID, AgentList.AgentName, AgentList.AgentAddress, AgentList.AgentState
, Sales.ID InvoiceID,Sales.InvoiceNumber,Sales.InvoiceDate,Sales.[RoundedOffInvoiceTotalAmount] InvoiceAmount
, Sales.RoundedOffInvoiceTotalAmount- (SUM(ISNULL(Payment.AmountReceived, 0)) + SUM(ISNULL(Payment.Discount,0))) Balance
, SUM(ISNULL(Payment.AmountReceived, 0)) AmountReceived
, SUM(ISNULL(Payment.Discount,0)) Discount
, SUM(ISNULL(Payment.Interest,0)) Interest
from (select * from TranSalesInvoice where Del_State=0 ) Sales 
left join (select * from TranPaymentCollectionList where Del_State=0 ) Payment on Sales.ID=Payment.InvoiceID 
inner join MasterCompany Comp on Sales.CompanyID = Comp.ID and Sales.CompanyID = @CompanyId 
inner join MasterCustomer Cust on Sales.CustomerID = Cust.ID
left join (Select ID AgentID, CustomerName AgentName,BillingAddress AgentAddress, State AgentState from MasterCustomer where CustomerType='Agent') AgentList on AgentList.AgentID = Sales.AgentID 
group by Comp.CompanyDisplayName,  Cust.CustomerName, Cust.State
, AgentList.AgentID, AgentList.AgentName, AgentList.AgentAddress, AgentList.AgentState
, Sales.ID,Sales.InvoiceNumber,Sales.InvoiceDate,Sales.[RoundedOffInvoiceTotalAmount], Sales.CustomerID
having Sales.RoundedOffInvoiceTotalAmount > SUM(ISNULL(Payment.AmountReceived,0)) and Sales.RoundedOffInvoiceTotalAmount > 0";

                    if (CustomerId != 0)
                    {
                        filterQuery = filterQuery + @" and Sales.CustomerID=@CustomerId ";
                    }

                    if (AgentId != 0)
                    {
                        filterQuery = filterQuery + @" and AgentList.AgentID = @AgentID";
                    }

                    filterQuery = filterQuery + @" and (Sales.InvoiceNumber like @FilterValue or Cust.CustomerName like @FilterValue or Cust.State like @FilterValue) ";
                    filterQuery = filterQuery + @" and Sales.InvoiceDate between @FromDate and @ToDate ";
                    using (SqlCommand cmd = new SqlCommand(filterQuery))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                        cmd.Parameters.AddWithValue("@AgentID", AgentId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + FilterValue + "%").Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (PaymentDataset.dtPaymentCollectionDataTable dtPaymentCollection = new PaymentDataset.dtPaymentCollectionDataTable())
                            {
                                sda.Fill(dtPaymentCollection);
                                dsPayment.Tables.Remove("dtPaymentCollection");
                                dsPayment.Tables.Add(dtPaymentCollection);
                            }
                        }
                    }

                    Entities.CustomFields customFields;

                    customFields = customFieldSelection.GetCustomFields(CompanyId, "Payment Collection Items");

                    using (PaymentDataset.dtCustomFieldsItemHeaderDataTable dtCustomFieldsHeader = new PaymentDataset.dtCustomFieldsItemHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsPayment.Tables.Remove("dtCustomFieldsItemHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsPayment.Tables.Add(dtCustomFieldsHeader);
                        }
                    }

                    //byte[] byteImage = logoSelection.GetCompanyLogo(CompanyId);
                    //if (byteImage != null)
                    //{
                    //    using (SalesInvoiceDataSet.dtCompanyLogoDataTable dtCompanyLogo = new SalesInvoiceDataSet.dtCompanyLogoDataTable())
                    //    {
                    //        dsPayment.Tables.Remove("dtCompanyLogo");
                    //        dtCompanyLogo.Rows.Add();
                    //        dtCompanyLogo.Rows[0]["Logo"] = byteImage;
                    //        dsPayment.Tables.Add(dtCompanyLogo);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dsPayment;
        }

        public PaymentDataset BindDataSetCompanyInfo(Int16 CompanyId)
        {
            PaymentDataset dsPayment = new PaymentDataset();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    Entities.CompanyInfo companyInfo;

                    companyInfo = masterSelection.GetCompanyInfo(CompanyId);

                    using (PaymentDataset.dtCompanyDataTable dtCompanyInfo = new PaymentDataset.dtCompanyDataTable())
                    {
                        if (companyInfo != null)
                        {
                            dsPayment.Tables.Remove("dtCompany");
                            dtCompanyInfo.Rows.Add();
                            dtCompanyInfo.Rows[0]["CompanyName"] = companyInfo.CompanyName;
                            dtCompanyInfo.Rows[0]["CompanyDisplayName"] = companyInfo.CompanyDisplayName;
                            dtCompanyInfo.Rows[0]["Address"] = companyInfo.Address;
                            dtCompanyInfo.Rows[0]["State"] = companyInfo.State;
                            dtCompanyInfo.Rows[0]["PinCode"] = companyInfo.PinCode;
                            dtCompanyInfo.Rows[0]["Phone"] = companyInfo.Phone;
                            dtCompanyInfo.Rows[0]["Mobile"] = companyInfo.Mobile;
                            dtCompanyInfo.Rows[0]["Email"] = companyInfo.Email;
                            dtCompanyInfo.Rows[0]["GSTN"] = companyInfo.GSTN;
                            dsPayment.Tables.Add(dtCompanyInfo);
                        }
                    }
                    byte[] byteImage = logoSelection.GetCompanyLogo(CompanyId);
                    if (byteImage != null)
                    {
                        using (SalesInvoiceDataSet.dtCompanyLogoDataTable dtCompanyLogo = new SalesInvoiceDataSet.dtCompanyLogoDataTable())
                        {
                            dsPayment.Tables.Remove("dtCompanyLogo");
                            dtCompanyLogo.Rows.Add();
                            dtCompanyLogo.Rows[0]["Logo"] = byteImage;
                            dsPayment.Tables.Add(dtCompanyLogo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dsPayment;
        }

    }
}

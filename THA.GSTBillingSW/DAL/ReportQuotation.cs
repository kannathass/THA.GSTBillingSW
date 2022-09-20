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
    class ReportQuotation
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        DAL.LogoSelection logoSelection = new DAL.LogoSelection();

        public BillingDataSet BindDataSetQuotation(Int32 QuotationID, Int16 CompanyId)
        {
            BillingDataSet dsInvoice = new BillingDataSet();
            //System.Data.DataSet dsInvoice=new System.Data.DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"if(( select customerId from TranQuotation where ID=@ID )=0) 
	                begin
		                SELECT Quotation.ID,[CompanyName],[QuotationNumber] InvoiceNumber,[QuotationDate] InvoiceDate,
		                [QuotationDueDate] InvoiceDueDate,[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],QuotationTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, CustomerAddress BillingAddress,'' ShippingAddress
		                ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
		                ,'' CustomerGSTN, CustomerState, '' CustomerPIN
		                , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , '' CustomerStateCode
		                , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
		                FROM [TranQuotation] Quotation
		                inner join MasterCompany Comp on Quotation.CompanyID = Comp.ID and Quotation.ID = @ID
	                    end
                    else
	                    begin
		                SELECT Quotation.ID,[CompanyName],[QuotationNumber] InvoiceNumber,[QuotationDate] InvoiceDate,
		                [QuotationDueDate] InvoiceDueDate,Cust.[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],QuotationTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, BillingAddress, ShippingAddress
		                ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
		                ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
		                , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
		                , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
		                FROM [TranQuotation] Quotation
		                inner join MasterCompany Comp on Quotation.CompanyID = Comp.ID and Quotation.ID = @ID
		                inner join MasterCustomer Cust on Quotation.CustomerID = Cust.ID
	                end"))
                    {
                        cmd.Parameters.AddWithValue("@ID", QuotationID);

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
                    ,[HsnSac],[Qty],[UoM]
                    ,[RatePerItem],[Discount],[TaxableValue]
                    ,[CGSTValue],[SGSTValue],[IGSTValue]
                    ,[CESSValue],[TotalValue], HeaderID
                    ,CGSTPercent, SGSTPercent, IGSTPercent
                    ,CESSPercent, DiscountPercent
                    FROM [TranQuotationItems] where HeaderID = @ID "))
                    {
                        cmd.Parameters.AddWithValue("@ID", QuotationID);

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

                    byte[] byteImage = logoSelection.GetCompanyLogo(CompanyId);
                    if (byteImage != null)
                    {
                        using (BillingDataSet.dtCompanyLogoDataTable dtCompanyLogo = new BillingDataSet.dtCompanyLogoDataTable())
                        {
                            dsInvoice.Tables.Remove("dtCompanyLogo");
                            dtCompanyLogo.Rows.Add();
                            dtCompanyLogo.Rows[0]["Logo"] = byteImage;
                            dsInvoice.Tables.Add(dtCompanyLogo);
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

        public BillingDataSet BindDataSetQuotationDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            BillingDataSet dsInvoice = new BillingDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select * from(
		                SELECT Quotation.ID,[CompanyName],[QuotationNumber] [InvoiceNumber],[QuotationDate] [InvoiceDate]
		                ,[QuotationDueDate] [InvoiceDueDate] ,[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffQuotationTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, CustomerAddress BillingAddress,'' ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,'' CustomerGSTN, CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= CustomerState) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName
		                FROM [TranQuotation] Quotation 
		                inner join MasterCompany Comp on Quotation.CompanyID = Comp.ID and Quotation.del_state='0' and QuotationStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId=0
                    union
		                SELECT Quotation.ID,[CompanyName],[QuotationNumber] InvoiceNumber,[QuotationDate] [InvoiceDate]
		                ,[QuotationDueDate] [InvoiceDueDate],Cust.[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffQuotationTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, BillingAddress, ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,Cust.GSTN CustomerGSTN, Cust.State CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName
		                FROM [TranQuotation] Quotation 
		                inner join MasterCompany Comp on Quotation.CompanyID = Comp.ID and Quotation.del_state='0' and QuotationStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId<>0
		                inner join MasterCustomer Cust on Quotation.CustomerID = Cust.ID
                        )tt
                        where InvoiceDate between @FromDate and @ToDate 
                        and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue or CustomerState like @FilterValue)
                    "))
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
                    ,[HsnSac],[Qty],[UoM]
                    ,[RatePerItem],[Discount],[TaxableValue]
                    ,[CGSTValue],[SGSTValue],[IGSTValue]
                    ,[CESSValue],[TotalValue], HeaderID
                    ,CGSTPercent, SGSTPercent, IGSTPercent
                    ,CESSPercent, DiscountPercent
                    FROM [TranQuotationItems] where HeaderID in
                    (select Id FROM [TranQuotation] where CompanyID = @CompanyId and QuotationDate between @FromDate and @ToDate and del_state='0') "))
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


        public BillingDataSet BindDataSetQuotationConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            BillingDataSet dsInvoice = new BillingDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select * from(
		                SELECT Quotation.ID,[CompanyName],[QuotationNumber] [InvoiceNumber],[QuotationDate] [InvoiceDate]
		                ,[QuotationDueDate] [InvoiceDueDate] ,[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffQuotationTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, CustomerAddress BillingAddress,'' ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,'' CustomerGSTN, CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= CustomerState) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName
		                FROM [TranQuotation] Quotation 
		                inner join MasterCompany Comp on Quotation.CompanyID = Comp.ID and Quotation.del_state='0' and QuotationStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId=0
                    union
		                SELECT Quotation.ID,[CompanyName],[QuotationNumber] InvoiceNumber,[QuotationDate] [InvoiceDate]
		                ,[QuotationDueDate] [InvoiceDueDate],Cust.[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffQuotationTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, BillingAddress, ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,Cust.GSTN CustomerGSTN, Cust.State CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName
		                FROM [TranQuotation] Quotation 
		                inner join MasterCompany Comp on Quotation.CompanyID = Comp.ID and Quotation.del_state='0' and QuotationStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId<>0
		                inner join MasterCustomer Cust on Quotation.CustomerID = Cust.ID
                        )tt
                        where InvoiceDate between @FromDate and @ToDate 
                        and (InvoiceNumber like @FilterValue or CustomerName like @FilterValue or CustomerState like @FilterValue)
                    "))
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

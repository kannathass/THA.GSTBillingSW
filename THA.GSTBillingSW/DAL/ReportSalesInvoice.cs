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
    class ReportSalesInvoice
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        DAL.CustomFieldSelection customFieldSelection = new DAL.CustomFieldSelection();
        DAL.LogoSelection logoSelection = new DAL.LogoSelection();
        public SalesInvoiceDataSet BindDataSetSalesInvoice(Int32 SalesInvoiceID, Int16 CompanyId, Int16 NoOfCopies)
        {
            SalesInvoiceDataSet dsInvoice = new SalesInvoiceDataSet();
            //System.Data.DataSet dsInvoice=new System.Data.DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    //Weight - CF1
                    //ModeOfTransport - CF2
                    //LRNumber - CF3
                    //BaleNumber - CF4
                    //AgentName - CF5
                    using (SqlCommand cmd = new SqlCommand(@"if(( select customerId from TranSalesInvoice where ID=@ID )=0) 
	                begin
		                SELECT Invoice.ID, CompanyName CompName,CompanyDisplayName CompanyName,[InvoiceNumber] InvoiceNumber,[InvoiceDate] InvoiceDate
		                ,[InvoiceDueDate] InvoiceDueDate,[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount], InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, CustomerAddress BillingAddress,'' ShippingAddress
		                ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
                        ,Comp.Address + ' - ' + Comp.PinCode  CompanyAddressAndPin 
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
                        ,Comp.Phone + ', ' + Comp.Email CompanyContactInfo
                        ,Comp.Phone + ', ' + Comp.Mobile + ', ' + Comp.Email CompanyContactInfoWithMobile
		                ,'' CustomerGSTN, CustomerState, CustomerPhone
		                , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= CustomerState) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName, IsCessApplicable, CF6
                        , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
                        , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount, TaxableAmountWithDisc
                        , CustomerMobile, Comp.Phone CompanyPhone, Comp.Mobile CompanyMobile, Comp.Email CompanyEmail
	                    , '' Agent, '' AgentState, '' AgentAddress
						, '' CustomerPAN
		                FROM [TranSalesInvoice] Invoice
		                inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Invoice.ID = @ID
	                end
                    else
	                begin
		                SELECT Invoice.ID, CompanyName CompName,CompanyDisplayName CompanyName,[InvoiceNumber] InvoiceNumber,[InvoiceDate] InvoiceDate
		                ,[InvoiceDueDate] InvoiceDueDate,Cust.[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount], InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, BillingAddress, ShippingAddress
		                ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
                        ,Comp.Address + ' - ' + Comp.PinCode  CompanyAddressAndPin
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
                        ,Comp.Phone + ', ' + Comp.Email CompanyContactInfo
                        ,Comp.Phone + ', ' + Comp.Mobile + ', ' + Comp.Email CompanyContactInfoWithMobile
		                ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.Phone CustomerPhone
		                , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName, IsCessApplicable, CF6
                        , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
                        , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount, TaxableAmountWithDisc
                        , Cust.Mobile CustomerMobile, Comp.Phone CompanyPhone, Comp.Mobile CompanyMobile, Comp.Email CompanyEmail
	                    , isnull(AgentList.Agent,'') Agent, AgentList.AgentState, AgentList.AgentAddress
						, Cust.PAN CustomerPAN
		                FROM [TranSalesInvoice] Invoice
		                inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Invoice.ID = @ID
		                inner join MasterCustomer Cust on Invoice.CustomerID = Cust.ID
                        left join (Select ID, CustomerName Agent, BillingAddress AgentAddress, State AgentState from MasterCustomer where CustomerType='Agent') AgentList on AgentList.ID = Invoice.AgentID 
	                end"))
                    {
                        cmd.Parameters.AddWithValue("@ID", SalesInvoiceID);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (SalesInvoiceDataSet.dtSalesInvoiceHeaderDataTable dtInvoiceHeader = new SalesInvoiceDataSet.dtSalesInvoiceHeaderDataTable())
                            {

                                sda.Fill(dtInvoiceHeader);
                                //dsInvoice.Tables.Remove(dtInvoiceHeader);
                                dsInvoice.Tables.Remove("dtSalesInvoiceHeader");
                                dsInvoice.Tables.Add(dtInvoiceHeader);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT case when ItemCustomDescription != '' 
			then InvoiceItem.[ItemDescription] + ' (' + [ItemCustomDescription] + ')' 
			else InvoiceItem.ItemDescription end ItemDescription
			,case when ItemCustomDescription != '' 
			then GroupName + ' ' + InvoiceItem.[ItemDescription] + ' (' + [ItemCustomDescription] + ')' 
			else GroupName + ' ' + InvoiceItem.ItemDescription end ItemDescriptionWithGroup
			,InvoiceItem.[HsnSac],[Qty] ,InvoiceItem.[UoM]
			,[RatePerItem],[Discount],[TaxableValue] TaxalbleValue 
           ,[CGSTValue],[SGSTValue],[IGSTValue] 
           ,[CESSValue],[TotalValue], SalesInvoiceHeaderID 
           ,CGSTPercent, SGSTPercent, IGSTPercent, CESSPercent, InvoiceItem.DiscountPercent 
           ,ItemCustomDescription,InvoiceItem.ItemDescription ItemName, Size, InvoiceItem.ID InvoiceItemID
            ,ROW_NUMBER() Over (Order by case when InvoiceItem.UoM like 'other%' then InvoiceItem.ID else 0 end) ItemSNo 
            ,GroupName, CFItem1, CFItem2
             FROM [TranSalesInvoiceItems] InvoiceItem inner join MasterItem Item 
			 on InvoiceItem.ItemID = Item.ID 
            inner join vw_MasterGroup Grp on Grp.ID=Item.GroupIDUnder 
            where SalesInvoiceHeaderID = @ID order by InvoiceItem.ID"))
                    {
                        cmd.Parameters.AddWithValue("@ID", SalesInvoiceID);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (SalesInvoiceDataSet.dtSalesInvoiceItemsDataTable dtInvoiceItems = new SalesInvoiceDataSet.dtSalesInvoiceItemsDataTable())
                            {

                                sda.Fill(dtInvoiceItems);
                                //dsInvoice.Tables.Remove(dtInvoiceItems);
                                dsInvoice.Tables.Remove("dtSalesInvoiceItems");

                                dsInvoice.Tables.Add(dtInvoiceItems);
                            }
                        }
                    }

                    Entities.CustomFields customFields = customFieldSelection.GetCustomFields(CompanyId, "Sales Invoice");


                    using (SalesInvoiceDataSet.dtCustomFieldsHeaderDataTable dtCustomFieldsHeader = new SalesInvoiceDataSet.dtCustomFieldsHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsInvoice.Tables.Remove("dtCustomFieldsHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsInvoice.Tables.Add(dtCustomFieldsHeader);
                        }
                    }

                    Entities.CustomFields customFieldsItem = customFieldSelection.GetCustomFields(CompanyId, "Sales Invoice Items");

                    using (SalesInvoiceDataSet.dtCustomFieldsItemDataTable dtCustomFieldsItem = new SalesInvoiceDataSet.dtCustomFieldsItemDataTable())
                    {
                        if (customFieldsItem != null)
                        {
                            dsInvoice.Tables.Remove("dtCustomFieldsItem");
                            dtCustomFieldsItem.Rows.Add();
                            dtCustomFieldsItem.Rows[0]["CF1"] = customFieldsItem.CF1;
                            dtCustomFieldsItem.Rows[0]["CF2"] = customFieldsItem.CF2;
                            dtCustomFieldsItem.Rows[0]["CF3"] = customFieldsItem.CF3;
                            dtCustomFieldsItem.Rows[0]["CF4"] = customFieldsItem.CF4;
                            dtCustomFieldsItem.Rows[0]["CF5"] = customFieldsItem.CF5;
                            dtCustomFieldsItem.Rows[0]["CF6"] = customFieldsItem.CF6;
                            dsInvoice.Tables.Add(dtCustomFieldsItem);
                        }
                    }

                    byte[] byteImage = logoSelection.GetCompanyLogo(CompanyId);
                    if (byteImage != null)
                    {
                        using (SalesInvoiceDataSet.dtCompanyLogoDataTable dtCompanyLogo = new SalesInvoiceDataSet.dtCompanyLogoDataTable())
                        {
                            dsInvoice.Tables.Remove("dtCompanyLogo");
                            dtCompanyLogo.Rows.Add();
                            dtCompanyLogo.Rows[0]["Logo"] = byteImage;
                            dsInvoice.Tables.Add(dtCompanyLogo);
                        }
                    }

                    if (NoOfCopies != 0)
                    {
                        using (SalesInvoiceDataSet.dtNoOfCopiesDataTable dtNoOfCopies = new SalesInvoiceDataSet.dtNoOfCopiesDataTable())
                        {
                            dsInvoice.Tables.Remove("dtNoOfCopies");
                            dtNoOfCopies.Rows.Add();
                            dtNoOfCopies.Rows[0]["Copy"] = 1;
                            dtNoOfCopies.Rows.Add();
                            dtNoOfCopies.Rows[1]["Copy"] = 2;
                            dsInvoice.Tables.Add(dtNoOfCopies);
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

        public SalesInvoiceDataSet BindDataSetSalesInvoiceConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            SalesInvoiceDataSet dsInvoice = new SalesInvoiceDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select * from(
		                SELECT Invoice.ID,CompanyDisplayName CompanyName,[InvoiceNumber] ,[InvoiceDate] 
		                ,[InvoiceDueDate] ,[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffInvoiceTotalAmount InvoiceTotalAmount
		                ,[CustomerNotes],TermsAndCondition, CustomerAddress BillingAddress,'' ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,'' CustomerGSTN, CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= CustomerState) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5, CF6, TaxableAmountWithDisc
		                FROM [TranSalesInvoice] Invoice 
		                inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Invoice.del_state='0' and InvoiceStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId=0
                    union
		                SELECT Invoice.ID,CompanyDisplayName CompanyName,[InvoiceNumber] ,[InvoiceDate] 
		                ,[InvoiceDueDate] ,Cust.[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffInvoiceTotalAmount InvoiceTotalAmount 
		                ,[CustomerNotes],TermsAndCondition, BillingAddress, ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,Cust.GSTN CustomerGSTN, Cust.State CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5, CF6, TaxableAmountWithDisc
		                FROM [TranSalesInvoice] Invoice 
		                inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Invoice.del_state='0' and InvoiceStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId<>0
		                inner join MasterCustomer Cust on Invoice.CustomerID = Cust.ID
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
                            using (SalesInvoiceDataSet.dtSalesInvoiceHeaderDataTable dtInvoiceHeader = new SalesInvoiceDataSet.dtSalesInvoiceHeaderDataTable())
                            {
                                sda.Fill(dtInvoiceHeader);
                                dsInvoice.Tables.Remove("dtSalesInvoiceHeader");
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

        public SalesInvoiceDataSet BindDataSetSalesInvoiceDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            SalesInvoiceDataSet dsInvoice = new SalesInvoiceDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select * from(
		                SELECT Invoice.ID,CompanyDisplayName CompanyName,[InvoiceNumber] ,[InvoiceDate] 
		                ,[InvoiceDueDate] ,[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffInvoiceTotalAmount InvoiceTotalAmount 
		                ,[CustomerNotes],TermsAndCondition, CustomerAddress BillingAddress,'' ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,'' CustomerGSTN, CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= CustomerState) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName, CF6
		                FROM [TranSalesInvoice] Invoice
		                inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Invoice.del_state='0' and InvoiceStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId=0
                    union
		                SELECT Invoice.ID,CompanyDisplayName CompanyName,[InvoiceNumber] ,[InvoiceDate] 
		                ,[InvoiceDueDate] ,Cust.[CustomerName],[PONumber],[PODate]
		                ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],RoundedOffInvoiceTotalAmount InvoiceTotalAmount 
		                ,[CustomerNotes],TermsAndCondition, BillingAddress, ShippingAddress
		                ,Comp.GSTN CompanyGSTN
		                ,Comp.State CompanyState, Comp.PAN CompanyPAN
		                ,Cust.GSTN CustomerGSTN, Cust.State CustomerState
		                , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
		                , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
		                , [CF4] BaleNumber,[CF2] ModeOfTransport,[CF3] LRNumber,[CF1] Weight, CF5 AgentName, CF6
		                FROM [TranSalesInvoice] Invoice 
		                inner join MasterCompany Comp on Invoice.CompanyID = Comp.ID and Invoice.del_state='0' and InvoiceStatus<>'Canceled' and Comp.ID = @CompanyId  and customerId<>0
		                inner join MasterCustomer Cust on Invoice.CustomerID = Cust.ID
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
                            using (SalesInvoiceDataSet.dtSalesInvoiceHeaderDataTable dtInvoiceHeader = new SalesInvoiceDataSet.dtSalesInvoiceHeaderDataTable())
                            {

                                sda.Fill(dtInvoiceHeader);
                                dsInvoice.Tables.Remove("dtSalesInvoiceHeader");
                                dsInvoice.Tables.Add(dtInvoiceHeader);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT case when ItemCustomDescription != '' 
			then InvoiceItem.[ItemDescription] + ' (' + [ItemCustomDescription] + ')' 
			else InvoiceItem.ItemDescription end ItemDescription
			,InvoiceItem.[HsnSac],[Qty] ,InvoiceItem.[UoM]
			,[RatePerItem],[Discount],[TaxableValue] TaxalbleValue 
           ,[CGSTValue],[SGSTValue],[IGSTValue] 
           ,[CESSValue],[TotalValue], SalesInvoiceHeaderID 
           ,CGSTPercent, SGSTPercent, IGSTPercent, CESSPercent, InvoiceItem.DiscountPercent 
           ,ItemCustomDescription,InvoiceItem.ItemDescription ItemName, Size 
             FROM [TranSalesInvoiceItems] InvoiceItem inner join MasterItem Item 
			 on InvoiceItem.ItemID = Item.ID  where SalesInvoiceHeaderID in 
             (select Id FROM [TranSalesInvoice] where CompanyID = @CompanyId and InvoiceDate between @FromDate and @ToDate and del_state='0') "))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (SalesInvoiceDataSet.dtSalesInvoiceItemsDataTable dtInvoiceItems = new SalesInvoiceDataSet.dtSalesInvoiceItemsDataTable())
                            {

                                sda.Fill(dtInvoiceItems);
                                dsInvoice.Tables.Remove("dtSalesInvoiceItems");

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

    }
}

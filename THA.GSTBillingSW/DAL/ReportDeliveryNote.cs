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
    class ReportDeliveryNote
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        DAL.CustomFieldSelection customFieldSelection = new DAL.CustomFieldSelection();
        DAL.LogoSelection logoSelection = new DAL.LogoSelection();
        public NoteDataSet BindDataSetDeliveryNote(Int32 DeliveryNoteID, Int16 CompanyId)
        {
            NoteDataSet dsNote = new NoteDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT DelivNote.ID,[CompanyName],[NoteNumber],[NoteDate],[CustomerName],[ReferenceNumber],[ReferenceDate]
               ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],[NoteTotalAmount]
                ,RoundedOffNoteTotalAmount
               ,[ExtraNotes], BillingAddress, ShippingAddress
               ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
               ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
               ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
               , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
               , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
               , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
               , CF1, CF2, CF3, CF4, CF5, CF6, IsCessApplicable
                , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
                , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount, TaxableAmountWithDisc, TermsAndCondition
                , Cust.Mobile CustomerMobile
                ,Comp.Phone + ', ' + Comp.Email CompanyContactInfo
                ,Comp.Address + ' - ' + Comp.PinCode  CompanyAddressAndPin
                ,Comp.Phone + ', ' + Comp.Mobile + ', ' + Comp.Email CompanyContactInfoWithMobile
                ,Cust.PAN CustomerPAN
               FROM [TranDeliveryNote] DelivNote
               inner join MasterCompany Comp on DelivNote.CompanyID = Comp.ID and DelivNote.ID = @ID
               inner join MasterCustomer Cust on DelivNote.CustomerID = Cust.ID"))
                    {
                        cmd.Parameters.AddWithValue("@ID", DeliveryNoteID);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (NoteDataSet.dtNoteHeaderDataTable dtNoteHeader = new NoteDataSet.dtNoteHeaderDataTable())
                            {
                                sda.Fill(dtNoteHeader);
                                dsNote.Tables.Remove("dtNoteHeader");
                                dsNote.Tables.Add(dtNoteHeader);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT case when ItemCustomDescription != '' then NoteItem.[ItemDescription] + ' ( ' + [ItemCustomDescription] + ' )' else NoteItem.ItemDescription end ItemDescription
                    ,NoteItem.[HsnSac],[Qty]
                    ,NoteItem.[UoM],[RatePerItem],[Discount],[TaxableValue]
                    ,[CGSTValue],[SGSTValue],[IGSTValue]
                    ,[CESSValue],[TotalValue],  HeaderID
                    ,CGSTPercent, SGSTPercent, IGSTPercent, CESSPercent, NoteItem.DiscountPercent
                    ,ItemCustomDescription,NoteItem.ItemDescription ItemName, Size, NoteItem.ID NoteItemID
			        ,case when ItemCustomDescription != '' 
			        then isnull(GroupName, '') + ' ' + NoteItem.[ItemDescription] + ' (' + [ItemCustomDescription] + ')' 
			        else isnull(GroupName, '') + ' ' + NoteItem.ItemDescription end ItemDescriptionWithGroup
                    ,ROW_NUMBER() Over (Order by case when NoteItem.UoM like 'other%' then NoteItem.ID else 0 end) ItemSNo 
                    ,CFItem1,CFItem2
                    FROM [TranDeliveryNoteItems] NoteItem inner join MasterItem Item 
                    on NoteItem.ItemID = Item.ID 
                    left join MasterGroup Grp on Grp.ID=Item.GroupIDUnder 
                    where HeaderID = @ID order by NoteItem.ID"))
                    {
                        cmd.Parameters.AddWithValue("@ID", DeliveryNoteID);

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (NoteDataSet.dtNoteItemsDataTable dtNoteItems = new NoteDataSet.dtNoteItemsDataTable())
                            {
                                sda.Fill(dtNoteItems);
                                dsNote.Tables.Remove("dtNoteItems");

                                dsNote.Tables.Add(dtNoteItems);
                            }
                        }
                    }

                    Entities.CustomFields customFields;
                    customFields = customFieldSelection.GetCustomFields(CompanyId, "Delivery Note");

                    using (SalesInvoiceDataSet.dtCustomFieldsHeaderDataTable dtCustomFieldsHeader = new SalesInvoiceDataSet.dtCustomFieldsHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsNote.Tables.Remove("dtCustomFieldsHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsNote.Tables.Add(dtCustomFieldsHeader);
                        }
                    }

                    customFields = customFieldSelection.GetCustomFields(CompanyId, "Delivery Note Items");

                    using (NoteDataSet.dtCustomFieldsItemHeaderDataTable dtCustomFieldsHeader = new NoteDataSet.dtCustomFieldsItemHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsNote.Tables.Remove("dtCustomFieldsItemHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsNote.Tables.Add(dtCustomFieldsHeader);
                        }
                    }

                    byte[] byteImage = logoSelection.GetCompanyLogo(CompanyId);
                    if (byteImage != null)
                    {
                        using (SalesInvoiceDataSet.dtCompanyLogoDataTable dtCompanyLogo = new SalesInvoiceDataSet.dtCompanyLogoDataTable())
                        {
                            dsNote.Tables.Remove("dtCompanyLogo");
                            dtCompanyLogo.Rows.Add();
                            dtCompanyLogo.Rows[0]["Logo"] = byteImage;
                            dsNote.Tables.Add(dtCompanyLogo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dsNote;
        }

        public NoteDataSet BindDataSetDeliveryNoteDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            NoteDataSet dsNote = new NoteDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT DelivNote.ID,[CompanyName],[NoteNumber]
               ,[NoteDate],[CustomerName],[ReferenceNumber],[ReferenceDate]
               ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],[NoteTotalAmount]
                ,RoundedOffNoteTotalAmount
               ,[ExtraNotes], BillingAddress, ShippingAddress
               ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
               ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
               ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
               , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
               , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
               , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
               , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
                , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
                , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount, TaxableAmountWithDisc, TermsAndCondition
                , Cust.Mobile CustomerMobile
                ,Comp.Phone + ', ' + Comp.Email CompanyContactInfo
                ,Comp.Address + ' - ' + Comp.PinCode  CompanyAddressAndPin
                ,Comp.Phone + ', ' + Comp.Mobile + ', ' + Comp.Email CompanyContactInfoWithMobile
               FROM [TranDeliveryNote] DelivNote
               inner join MasterCompany Comp on DelivNote.CompanyID = Comp.ID and DelivNote.CompanyID = @CompanyId
               inner join MasterCustomer Cust on DelivNote.CustomerID = Cust.ID
               where NoteDate between @FromDate and @ToDate and NoteStatus<>'Canceled'
               and (NoteNumber like @FilterValue or CustomerName like @FilterValue or Cust.State like @FilterValue)"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + FilterValue + "%").Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (NoteDataSet.dtNoteHeaderDataTable dtNoteHeader = new NoteDataSet.dtNoteHeaderDataTable())
                            {
                                sda.Fill(dtNoteHeader);
                                dsNote.Tables.Remove("dtNoteHeader");
                                dsNote.Tables.Add(dtNoteHeader);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"SELECT case when ItemCustomDescription != '' then NoteItem.[ItemDescription] + ' ( ' + [ItemCustomDescription] + ' )' else NoteItem.ItemDescription end ItemDescription
                    ,NoteItem.[HsnSac],[Qty]
                    ,NoteItem.[UoM],[RatePerItem],[Discount],[TaxableValue]
                    ,[CGSTValue],[SGSTValue],[IGSTValue]
                    ,[CESSValue],[TotalValue],  HeaderID
                    ,CGSTPercent, SGSTPercent, IGSTPercent, CESSPercent, NoteItem.DiscountPercent
                    ,ItemCustomDescription,NoteItem.ItemDescription ItemName, Size, NoteItem.ID NoteItemID
			        ,case when ItemCustomDescription != '' 
			        then isnull(GroupName, '') + ' ' + NoteItem.[ItemDescription] + ' (' + [ItemCustomDescription] + ')' 
			        else isnull(GroupName, '') + ' ' + NoteItem.ItemDescription end ItemDescriptionWithGroup
                    ,ROW_NUMBER() Over (Order by case when NoteItem.UoM like 'other%' then NoteItem.ID else 0 end) ItemSNo 
                    FROM [TranDeliveryNoteItems] NoteItem inner join MasterItem Item 
                    on NoteItem.ItemID = Item.ID 
                    left join MasterGroup Grp on Grp.ID=Item.GroupIDUnder 
                    where HeaderID in 
                    (select Id FROM [TranDeliveryNote] where CompanyID = @CompanyId and NoteDate between @FromDate and @ToDate and del_state='0') order by NoteItem.ID"))

                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (NoteDataSet.dtNoteItemsDataTable dtNoteItems = new NoteDataSet.dtNoteItemsDataTable())
                            {
                                sda.Fill(dtNoteItems);
                                dsNote.Tables.Remove("dtNoteItems");

                                dsNote.Tables.Add(dtNoteItems);
                            }
                        }
                    }

                    Entities.CustomFields customFields;
                    customFields = customFieldSelection.GetCustomFields(CompanyId, "Delivery Note");

                    using (SalesInvoiceDataSet.dtCustomFieldsHeaderDataTable dtCustomFieldsHeader = new SalesInvoiceDataSet.dtCustomFieldsHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsNote.Tables.Remove("dtCustomFieldsHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsNote.Tables.Add(dtCustomFieldsHeader);
                        }
                    }

                    customFields = customFieldSelection.GetCustomFields(CompanyId, "Delivery Note Items");

                    using (NoteDataSet.dtCustomFieldsItemHeaderDataTable dtCustomFieldsHeader = new NoteDataSet.dtCustomFieldsItemHeaderDataTable())
                    {
                        if (customFields != null)
                        {
                            dsNote.Tables.Remove("dtCustomFieldsItemHeader");
                            dtCustomFieldsHeader.Rows.Add();
                            dtCustomFieldsHeader.Rows[0]["CF1"] = customFields.CF1;
                            dtCustomFieldsHeader.Rows[0]["CF2"] = customFields.CF2;
                            dtCustomFieldsHeader.Rows[0]["CF3"] = customFields.CF3;
                            dtCustomFieldsHeader.Rows[0]["CF4"] = customFields.CF4;
                            dtCustomFieldsHeader.Rows[0]["CF5"] = customFields.CF5;
                            dtCustomFieldsHeader.Rows[0]["CF6"] = customFields.CF6;
                            dsNote.Tables.Add(dtCustomFieldsHeader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dsNote;
        }


        public NoteDataSet BindDataSetDeliveryNoteConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            NoteDataSet dsNote = new NoteDataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SELECT DelivNote.ID,[CompanyName],[NoteNumber]
               ,[NoteDate],[CustomerName],[ReferenceNumber],[ReferenceDate]
               ,[CGSTAmount],[SGSTAmount] ,[IGSTAmount],[CESSAmount],[TaxableAmount],[NoteTotalAmount]
               ,RoundedOffNoteTotalAmount
                ,[ExtraNotes], BillingAddress, ShippingAddress
               ,Comp.GSTN CompanyGSTN, Comp.Address CompanyAddress, Comp.PinCode CompanyPIN
               ,Comp.State CompanyState, Comp.PAN CompanyPAN, Comp.Phone CompanyPhone, Comp.Email CompanyEmail
               ,Cust.GSTN CustomerGSTN, Cust.State CustomerState, Cust.PinCode CustomerPIN
               , Comp.BankName, Comp.Branch BankBranch, Comp.AccountNumber, Comp.IFSC
               , (Select Code from MasterState State where State.Name= Comp.State) CompanyStateCode
               , (Select Code from MasterState State where State.Name= Cust.State) CustomerStateCode
               , CF1, CF2, CF3, CF4, CF5, IsCessApplicable
                , CESSPercentTotal, SGSTPercentTotal, CGSTPercentTotal
                , IGSTPercentTotal, DiscountPercentTotal, DiscountAmount, TaxableAmountWithDisc, TermsAndCondition
                , Cust.Mobile CustomerMobile
                ,Comp.Phone + ', ' + Comp.Email CompanyContactInfo
                ,Comp.Address + ' - ' + Comp.PinCode  CompanyAddressAndPin
                ,Comp.Phone + ', ' + Comp.Mobile + ', ' + Comp.Email CompanyContactInfoWithMobile
               FROM [TranDeliveryNote] DelivNote
               inner join MasterCompany Comp on DelivNote.CompanyID = Comp.ID and DelivNote.CompanyID = @CompanyId
               inner join MasterCustomer Cust on DelivNote.CustomerID = Cust.ID
               where NoteDate between @FromDate and @ToDate and NoteStatus<>'Canceled'
               and (NoteNumber like @FilterValue or CustomerName like @FilterValue or Cust.State like @FilterValue)"))
                    {
                        cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                        cmd.Parameters.AddWithValue("@FromDate", FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", ToDate);
                        cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + FilterValue + "%").Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (NoteDataSet.dtNoteHeaderDataTable dtNoteHeader = new NoteDataSet.dtNoteHeaderDataTable())
                            {
                                sda.Fill(dtNoteHeader);
                                dsNote.Tables.Remove("dtNoteHeader");
                                dsNote.Tables.Add(dtNoteHeader);
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
            return dsNote;
        }
    }
}


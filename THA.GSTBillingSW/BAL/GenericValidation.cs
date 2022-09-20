using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace THA.GSTBillingSW.BAL
{
    class GenericValidation
    {
        public static Int64 GetCompanyTransactionCount(string ComapnyId)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetTransactionCount(string.Format("Select sum(RecordsCount) RecordsCount from ( " +
            " select count(*) RecordsCount from TranSalesInvoice where Del_State = 0 and CompanyID = {0} " +
            " union " +
            " select count(*) RecordsCount from TranPurchaseInvoice where Del_State = 0 and CompanyID = {0} " +
            " union " +
            " select count(*) RecordsCount from TranQuotation where Del_State = 0 and CompanyID = {0} " +
            " )TT", ComapnyId));
        }

        public static Int64 GetCustomerTransactionCount(string CustomerId)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetTransactionCount(string.Format("Select sum(RecordsCount) RecordsCount from ( " +
            " select count(*) RecordsCount from TranSalesInvoice where Del_State = 0 and CustomerID = {0} " +
            " union " +
            " select count(*) RecordsCount from TranPurchaseInvoice where Del_State = 0 and CustomerID = {0} " +
            " union " +
            " select count(*) RecordsCount from TranQuotation where Del_State = 0 and CustomerID = {0} " +
            " )TT", CustomerId));
        }

        public static Int64 GetItemTransactionCount(string ItemId)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetTransactionCount(string.Format("Select sum(RecordsCount) RecordsCount from ( " +
            " select count(*) RecordsCount from TranSalesInvoiceItems where Del_State = 0 and ItemId = {0} " +
            " union " +
            " select count(*) RecordsCount from TranPurchaseInvoiceItems where Del_State = 0 and ItemId = {0} " +
            " union " +
            " select count(*) RecordsCount from TranQuotationItems where Del_State = 0 and ItemId = {0} " +
            " )TT", ItemId));
        }

        /// <summary>
        /// Pending items
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public static Int64 GetGroupTransactionCount(string GroupId)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetTransactionCount(string.Format("select count(*) from MasterItem where  Del_State=0 and GroupIDUnder= {0} ", GroupId));
        }

        public static string GetCustomerGSTNCount(string GSTN)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetSingleRecordFromDB(string.Format("select case when count(*)=0 then '' else CustomerName end Name from MasterCustomer where Del_State=0 and GSTN= '{0}' group by CustomerName ", GSTN));
        }

        public static string GetCompanyGSTNCount(string GSTN)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetSingleRecordFromDB(string.Format("select case when count(*)=0 then '' else CustomerName end Name from MasterCustomer where Del_State=0 and GSTN= '{0}' group by CustomerName ", GSTN));
        }

        public static string GetUoMDisplayName(string UoM)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetSingleRecordFromDB(string.Format("select UoMDisplayName from MasterUoM where UoM='{0}'", UoM));
        }

        public static DateTime getCurrentDate()
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();

            return dalGenericValidation.getCurrentDate();
        }

        public static string GetUoMCount(int InvoiceID)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetSingleRecordFromDB(
                string.Format(@"select case when count(*)>1 then 
                            'UnitCompined'
                            else
                            (select distinct UoM from TranSalesInvoiceItems  
                            where Del_State=0 and SalesInvoiceHeaderID={0}  and UoM!='')
                            end UomHeader
                            from
                            (select count(*) Cnt, UoM from TranSalesInvoiceItems  
                            where Del_State=0 and SalesInvoiceHeaderID={0} and UoM!=''
                            group by UoM) tt", InvoiceID));
        }

        public static string GetGSTNAvailability(Int32 CompanyID)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetSingleRecordFromDB(string.Format("select GSTN from MasterCompany where ID={0}", CompanyID));
        }

        public static Int64 GetSumOfCF1Item(int InvoiceID)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetTransactionCount(
                string.Format(@"select sum(CONVERT(int, case when ISNUMERIC(CFItem1) =1 then CFItem1 else 0 end)) SumCF1Item 
                                from TranSalesInvoiceItems where Del_State=0 and SalesInvoiceHeaderID={0}", InvoiceID));
        }

        public static bool IsInvoiceNumberAlreadyExist(string InvoiceNumber, Int16 CompanyID, Int64 HeaderID, string mode)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.IsInvoiceNumberAlreadyExist(InvoiceNumber, CompanyID, HeaderID, mode);
        }

        public static bool IsValidGSTN(string inputGSTN)
        {
            string strRegex = @"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputGSTN))
                return (true);
            else
                return (false);
        }

        public static Int32 GetPreviousIdentity_PaymentCollection(Int32 CurrentID)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetPreviousNextIdentity(string.Format("select Top 1 ID from TranPaymentCollection where Del_State = 0 and id < {0} order by ID desc", CurrentID));
        }

        public static Int32 GetNextIdentity_PaymentCollection(Int32 CurrentID)
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetPreviousNextIdentity(string.Format("select Top 1 ID from TranPaymentCollection where Del_State = 0 and id > {0} order by ID desc", CurrentID));
        }

        public static Int32 GetLatestIdentity_PaymentCollection()
        {
            DAL.GenericValidation dalGenericValidation = new DAL.GenericValidation();
            return dalGenericValidation.GetPreviousNextIdentity("select Top 1 ID  from TranPaymentCollection where Del_State=0 order by ID desc");
        }
    }
}

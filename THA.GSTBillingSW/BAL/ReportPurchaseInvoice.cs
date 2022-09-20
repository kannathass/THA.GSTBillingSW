using System;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportPurchaseInvoice
    {
        DAL.ReportPurchaseInvoice reportPurchaseInvoice = new DAL.ReportPurchaseInvoice();
        public BillingDataSet BindDataSetPurchaseInvoice(Int32 PurchaseInvoiceID, Int16 CompanyId)
        {
            return reportPurchaseInvoice.BindDataSetPurchaseInvoice(PurchaseInvoiceID, CompanyId);
        }

        public BillingDataSet BindDataSetPurchaseInvoiceDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportPurchaseInvoice.BindDataSetPurchaseInvoiceDetailed(FromDate, ToDate, CompanyId, FilterValue);
        }

        public BillingDataSet BindDataSetPurchaseInvoiceConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportPurchaseInvoice.BindDataSetPurchaseInvoiceConsolidated(FromDate, ToDate, CompanyId, FilterValue);
        }
    }
}

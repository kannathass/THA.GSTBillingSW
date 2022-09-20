using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportQuotation
    {
        DAL.ReportQuotation reportQuotation = new DAL.ReportQuotation();
        public BillingDataSet BindDataSetQuotation(Int32 QuotationID, Int16 CompanyId)
        {
            return reportQuotation.BindDataSetQuotation(QuotationID, CompanyId);
        }

        public BillingDataSet BindDataSetQuotationDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportQuotation.BindDataSetQuotationDetailed(FromDate, ToDate, CompanyId, FilterValue);
        }

        public BillingDataSet BindDataSetQuotationConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportQuotation.BindDataSetQuotationConsolidated(FromDate, ToDate, CompanyId, FilterValue);
        }
    }
}

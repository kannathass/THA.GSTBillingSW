using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportPaymentCollection
    {
        DAL.ReportPaymentCollection reportPaymentCollection = new DAL.ReportPaymentCollection();

        public PaymentDataset BindDataSetPaymentCollection(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            return reportPaymentCollection.BindDataSetPaymentCollection(FromDate, ToDate, CompanyId, FilterValue);
        }

        public PaymentDataset BindDataSetPaymentCollectionPending(DateTime FromDate, DateTime ToDate, Int16 CompanyId, Int32 CustomerId, Int32 AgentId, string FilterValue)
        {
            return reportPaymentCollection.BindDataSetPaymentCollectionPending(FromDate, ToDate, CompanyId, CustomerId, AgentId, FilterValue);
        }
    }
}

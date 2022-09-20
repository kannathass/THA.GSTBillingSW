using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportSalesInvoice
    {
        DAL.ReportSalesInvoice reportSalesInvoice = new DAL.ReportSalesInvoice();
        public SalesInvoiceDataSet BindDataSetSalesInvoice(Int32 SalesInvoiceID, Int16 CompanyId, Int16 NoOfCopies)
        {
            SalesInvoiceDataSet dsInvoice = new SalesInvoiceDataSet();
            dsInvoice = reportSalesInvoice.BindDataSetSalesInvoice(SalesInvoiceID, CompanyId,NoOfCopies);
            return dsInvoice;
        }

        public SalesInvoiceDataSet BindDataSetSalesInvoiceConsolidated(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            SalesInvoiceDataSet dsInvoice = new SalesInvoiceDataSet();
            dsInvoice = reportSalesInvoice.BindDataSetSalesInvoiceConsolidated(FromDate, ToDate, CompanyId, FilterValue);
            return dsInvoice;
        }

        public SalesInvoiceDataSet BindDataSetSalesInvoiceDetailed(DateTime FromDate, DateTime ToDate, Int16 CompanyId, string FilterValue)
        {
            SalesInvoiceDataSet dsInvoice = new SalesInvoiceDataSet();
            dsInvoice = reportSalesInvoice.BindDataSetSalesInvoiceDetailed(FromDate, ToDate, CompanyId, FilterValue);
            return dsInvoice;
        }
    }
}

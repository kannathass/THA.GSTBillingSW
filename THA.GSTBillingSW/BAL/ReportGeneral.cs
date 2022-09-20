using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Report.CustomDataSet;

namespace THA.GSTBillingSW.BAL
{
    class ReportGeneral
    {
        DAL.ReportGeneral reportGeneral = new DAL.ReportGeneral();
        public StockDataSet BindDataSetStockList(Int16 CompanyId, string GroupName, string FilterValue)
        {
            return reportGeneral.BindDataSetStockList(CompanyId, GroupName, FilterValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Report
{
    public partial class ReportViewerGeneral : Form
    {
        BAL.ReportGeneral BALReportStock = new BAL.ReportGeneral();
        private string filterValue { get; set; }
        private string reportType { get; set; }
        private Int16 companyId { get; set; }
        private string groupName { get; set; }
        public ReportViewerGeneral(string ReportType, Int16 CompanyId, string GroupName, string FilterValue)
        {
            reportType = ReportType;
            companyId = CompanyId;
            filterValue = FilterValue;
            groupName = GroupName;
            InitializeComponent();
        }

        private void ReportViewerGeneral_Load(object sender, EventArgs e)
        {
            if (reportType == "StockList")
                getStockList();
            else if (reportType == "StockListDetail")
                getStockListDetail();
            else if (reportType == "StockListChart")
                getStockListChart();
            else if (reportType == "CustomerList")
                getCustomerList();
            else if (reportType == "ItemList")
                getItemList();
        }


        private void getItemList()
        {
            throw new NotImplementedException();
        }

        private void getCustomerList()
        {
            throw new NotImplementedException();
        }

        private void getStockList()
        {
            try
            {
                CustomDataSet.StockDataSet dsStock = BALReportStock.BindDataSetStockList(companyId, groupName, filterValue);

                General.StockList crystalReportStockList = new General.StockList();
                crystalReportStockList.SetDataSource(dsStock);

                //crystalReportStockList.SetParameterValue("Param1", FromDate);
                //crystalReportStockList.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerGeneral.ReportSource = crystalReportStockList;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getStockListDetail()
        {
            try
            {
                CustomDataSet.StockDataSet dsStock = BALReportStock.BindDataSetStockList(companyId, groupName, filterValue);

                General.StockListDetail crystalReportStockList = new General.StockListDetail();
                crystalReportStockList.SetDataSource(dsStock);

                this.crystalReportViewerGeneral.ReportSource = crystalReportStockList;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getStockListChart()
        {
            try
            {
                CustomDataSet.StockDataSet dsStock = BALReportStock.BindDataSetStockList(companyId, groupName, filterValue);

                General.StockListChart crystalReportStockList = new General.StockListChart();
                crystalReportStockList.SetDataSource(dsStock);

                this.crystalReportViewerGeneral.ReportSource = crystalReportStockList;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

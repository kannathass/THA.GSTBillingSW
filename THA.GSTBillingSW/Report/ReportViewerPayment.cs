using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using THA.GSTBillingSW.BAL;

namespace THA.GSTBillingSW.Report
{
    public partial class ReportViewerPayment : Form
    {
        BAL.ReportPaymentCollection reportPaymentCollection = new BAL.ReportPaymentCollection();
        private string reportType { get; set; }
        private Int16 companyId { get; set; }
        private Int32 customerId { get; set; }
        private Int32 agentId { get; set; }
        private string filterValue { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        internal ReportPaymentCollection ReportPaymentCollection
        {
            get
            {
                return reportPaymentCollection;
            }
            set
            {
                reportPaymentCollection = value;
            }
        }

        public ReportViewerPayment(string ReportType, Int16 CompanyId, Int32 CustomerId, Int32 AgentId, string FilterValue)
        {
            reportType = ReportType;
            companyId = CompanyId;
            customerId = CustomerId;
            agentId = AgentId;
            filterValue = FilterValue;
            InitializeComponent();
        }

        private void ReportViewerPayment_Load(object sender, EventArgs e)
        {
            //this.crystalReportViewerPayment.RefreshReport();
            if (reportType == "Partywise Payment")
                GetPaymentCustomerwise();
            else if (reportType == "Agentwise Payment")
                GetPaymentAgentwise();
            else if (reportType == "Placewise Payment")
                GetPaymentPlacewise();
            else if (reportType == "Partywise Pending Payment")
                GetPaymentCustomerwisePending();
            else if (reportType == "Agentwise Pending Payment")
                GetPaymentAgentwisePending();
            else if (reportType == "Placewise Pending Payment")
                GetPaymentPlacewisePending();
            else if (reportType == "AgentCommission Payment")
                GetPaymentAgentCommission();
            else if (reportType == "CoveringLetter Payment")
                GetPaymentCoveringLetter();
        }

        private void GetPaymentPlacewisePending()
        {
            try
            {
                CustomDataSet.PaymentDataset dsPayment = reportPaymentCollection.BindDataSetPaymentCollectionPending(FromDate, ToDate, companyId, customerId, agentId, filterValue);

                Payment.PaymentCollectionPlacewisePending crystalReportPaymentCollection = new Payment.PaymentCollectionPlacewisePending();

                crystalReportPaymentCollection.SetDataSource(dsPayment);
                crystalReportPaymentCollection.SetParameterValue("Param1", FromDate);
                crystalReportPaymentCollection.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerPayment.ReportSource = crystalReportPaymentCollection;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPaymentAgentwisePending()
        {
            try
            {
                CustomDataSet.PaymentDataset dsPayment = reportPaymentCollection.BindDataSetPaymentCollectionPending(FromDate, ToDate, companyId, customerId, agentId, filterValue);

                Payment.PaymentCollectionAgentwisePending crystalReportPaymentCollection = new Payment.PaymentCollectionAgentwisePending();

                crystalReportPaymentCollection.SetDataSource(dsPayment);
                crystalReportPaymentCollection.SetParameterValue("Param1", FromDate);
                crystalReportPaymentCollection.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerPayment.ReportSource = crystalReportPaymentCollection;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPaymentCustomerwisePending()
        {
            try
            {
                CustomDataSet.PaymentDataset dsPayment = reportPaymentCollection.BindDataSetPaymentCollectionPending(FromDate, ToDate, companyId, customerId, agentId, filterValue);

                Payment.PaymentCollectionPartywisePending crystalReportPaymentCollection = new Payment.PaymentCollectionPartywisePending();

                crystalReportPaymentCollection.SetDataSource(dsPayment);
                crystalReportPaymentCollection.SetParameterValue("Param1", FromDate);
                crystalReportPaymentCollection.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerPayment.ReportSource = crystalReportPaymentCollection;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPaymentPlacewise()
        {
            try
            {
                CustomDataSet.PaymentDataset dsPayment = reportPaymentCollection.BindDataSetPaymentCollection(FromDate, ToDate, companyId, filterValue);

                Payment.PaymentCollectionPlacewise crystalReportPaymentCollection = new Payment.PaymentCollectionPlacewise();

                crystalReportPaymentCollection.SetDataSource(dsPayment);
                crystalReportPaymentCollection.SetParameterValue("Param1", FromDate);
                crystalReportPaymentCollection.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerPayment.ReportSource = crystalReportPaymentCollection;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPaymentAgentCommission()
        {
            throw new NotImplementedException();
        }

        private void GetPaymentCoveringLetter()
        {
            //try
            //{
            //    CustomDataSet.NoteDataSet dsNote = reportDeliveryNote.BindDataSetDeliveryNoteDetailed(FromDate, ToDate, companyId, FilterValue);

            //    DeliveryNote.DeliveryNoteDetailed crystalReportDeliveryNoteDetailed = new DeliveryNote.DeliveryNoteDetailed();
            //    crystalReportDeliveryNoteDetailed.SetDataSource(dsNote);

            //    crystalReportDeliveryNoteDetailed.SetParameterValue("Param1", FromDate);
            //    crystalReportDeliveryNoteDetailed.SetParameterValue("Param2", ToDate);

            //    this.crystalReportViewerBilling.ReportSource = crystalReportDeliveryNoteDetailed;
            //    this.crystalReportViewerBilling.ShowCloseButton = true;
            //}
            //catch (Exception ex)
            //{
            //    BAL.LogHelper.WriteDebugLog(ex.ToString());
            //    MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void GetPaymentCustomerwise()
        {
            try
            {
                CustomDataSet.PaymentDataset dsPayment = reportPaymentCollection.BindDataSetPaymentCollection(FromDate, ToDate, companyId, filterValue);

                Payment.PaymentCollectionPartywise crystalReportPaymentCollection = new Payment.PaymentCollectionPartywise();

                crystalReportPaymentCollection.SetDataSource(dsPayment);
                crystalReportPaymentCollection.SetParameterValue("Param1", FromDate);
                crystalReportPaymentCollection.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerPayment.ReportSource = crystalReportPaymentCollection;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPaymentAgentwise()
        {
            try
            {
                CustomDataSet.PaymentDataset dsPayment = reportPaymentCollection.BindDataSetPaymentCollection(FromDate, ToDate, companyId, filterValue);

                Payment.PaymentCollectionAgentwise crystalReportPaymentCollection = new Payment.PaymentCollectionAgentwise();

                crystalReportPaymentCollection.SetDataSource(dsPayment);
                //crystalReportDeliveryNote.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                crystalReportPaymentCollection.SetParameterValue("Param1", FromDate);
                crystalReportPaymentCollection.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerPayment.ReportSource = crystalReportPaymentCollection;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

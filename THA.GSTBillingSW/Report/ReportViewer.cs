using CrystalDecisions.Shared;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Report
{
    public partial class ReportViewer : Form
    {
        BAL.CustomFieldSelection customFieldSelection = new BAL.CustomFieldSelection();
        BAL.ReportSalesInvoice reportSalesInvoice = new BAL.ReportSalesInvoice();

        BAL.ReportPurchaseInvoice reportPurchaseInvoice = new BAL.ReportPurchaseInvoice();
        BAL.ReportQuotation reportQuotation = new BAL.ReportQuotation();
        BAL.ReportDeliveryNote reportDeliveryNote = new BAL.ReportDeliveryNote();
        BAL.ReportReceiptNote reportReceiptNote = new BAL.ReportReceiptNote();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        private string reportType { get; set; }
        public Int32 InvoiceID { get; set; }
        public bool isIGSTApplicable { get; set; }
        private Int16 companyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FilterValue { get; set; }

        private string invoiceCopyType = string.Empty;
        private string invoiceCopyFor = string.Empty;

        public ReportViewer(string ReportType, Int16 CompanyId)
        {
            reportType = ReportType;
            companyId = CompanyId;
            InitializeComponent();
            if (ConfigurationManager.AppSettings["SalesInvoiceReportTypePopup"].ToString() == "1" && reportType == "SalesInvoice")
            {
                getInvoiceCopyType();
            }
            //this.WindowState = FormWindowState.Maximized;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (reportType == "SalesInvoice")
                GetSalesInvoice();
            else if (reportType == "SalesInvoiceConsolidated")
                GetSalesInvoiceConsolidated();
            else if (reportType == "SalesInvoiceDetailed")
                GetSalesInvoiceDetailed();
            else if (reportType == "PurchaseInvoice")
                GetPurchaseInvoice();
            else if (reportType == "PurchaseInvoiceConsolidated")
                GetPurchaseInvoiceConsolidated();
            else if (reportType == "PurchaseInvoiceDetailed")
                GetPurchaseInvoiceDetailed();
            else if (reportType == "Quotation")
                GetQuotation();
            else if (reportType == "QuotationConsolidated")
                GetQuotationConsolidated();
            else if (reportType == "QuotationDetailed")
                GetQuotationDetailed();
            else if (reportType == "DeliveryNote")
                GetDeliveryNote();
            else if (reportType == "DeliveryNoteConsolidated")
                GetDeliveryNoteConsolidated();
            else if (reportType == "DeliveryNoteDetailed")
                GetDeliveryNoteDetailed();
            else if (reportType == "ReceiptNote")
                GetReceiptNote();
            else if (reportType == "ReceiptNoteConsolidated")
                GetReceiptNoteConsolidated();
            else if (reportType == "ReceiptNoteDetailed")
                GetReceiptNoteDetailed();
            else if (reportType == "ReceiptNoteWeightDetail")
                GetReceiptNoteWeightDetail();
        }

        private void GetReceiptNoteWeightDetail()
        {
            try
            {
                CustomDataSet.InvardOutwardWeightDetail dsReceiptAnalysis = reportReceiptNote.BindDataSetReceiptNoteWeightInfo(FromDate, ToDate, companyId, FilterValue);
                ReceiptNote.ReceiptNoteWeightAnalysis crystalReportReceiptNote = new ReceiptNote.ReceiptNoteWeightAnalysis();

                crystalReportReceiptNote.SetDataSource(dsReceiptAnalysis);
                crystalReportReceiptNote.SetParameterValue("Param1", FromDate);
                crystalReportReceiptNote.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerBilling.ReportSource = crystalReportReceiptNote;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetReceiptNoteDetailed()
        {
            try
            {
                CustomDataSet.NoteDataSet dsNote = reportReceiptNote.BindDataSetReceiptNoteDetailed(FromDate, ToDate, companyId, FilterValue);

                ReceiptNote.ReceiptNoteDetailed crystalReportReceiptNoteDetailed = new ReceiptNote.ReceiptNoteDetailed();
                crystalReportReceiptNoteDetailed.SetDataSource(dsNote);

                crystalReportReceiptNoteDetailed.SetParameterValue("Param1", FromDate);
                crystalReportReceiptNoteDetailed.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerBilling.ReportSource = crystalReportReceiptNoteDetailed;
                this.crystalReportViewerBilling.ShowCloseButton = true;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetReceiptNoteConsolidated()
        {
            try
            {
                CustomDataSet.NoteDataSet dsNote = reportReceiptNote.BindDataSetReceiptNoteConsolidated(FromDate, ToDate, companyId, FilterValue);

                ReceiptNote.ReceiptNoteConsolidated crystalReportReceiptNoteConsolidated = new ReceiptNote.ReceiptNoteConsolidated();
                crystalReportReceiptNoteConsolidated.SetDataSource(dsNote);

                crystalReportReceiptNoteConsolidated.SetParameterValue("Param1", FromDate);
                crystalReportReceiptNoteConsolidated.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerBilling.ReportSource = crystalReportReceiptNoteConsolidated;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDeliveryNoteDetailed()
        {
            try
            {
                CustomDataSet.NoteDataSet dsNote = reportDeliveryNote.BindDataSetDeliveryNoteDetailed(FromDate, ToDate, companyId, FilterValue);

                DeliveryNote.DeliveryNoteDetailed crystalReportDeliveryNoteDetailed = new DeliveryNote.DeliveryNoteDetailed();
                crystalReportDeliveryNoteDetailed.SetDataSource(dsNote);

                crystalReportDeliveryNoteDetailed.SetParameterValue("Param1", FromDate);
                crystalReportDeliveryNoteDetailed.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerBilling.ReportSource = crystalReportDeliveryNoteDetailed;
                this.crystalReportViewerBilling.ShowCloseButton = true;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDeliveryNoteConsolidated()
        {
            try
            {
                CustomDataSet.NoteDataSet dsNote = reportDeliveryNote.BindDataSetDeliveryNoteConsolidated(FromDate, ToDate, companyId, FilterValue);

                DeliveryNote.DeliveryNoteConsolidated crystalReportDeliveryNoteConsolidated = new DeliveryNote.DeliveryNoteConsolidated();
                crystalReportDeliveryNoteConsolidated.SetDataSource(dsNote);

                crystalReportDeliveryNoteConsolidated.SetParameterValue("Param1", FromDate);
                crystalReportDeliveryNoteConsolidated.SetParameterValue("Param2", ToDate);
                this.crystalReportViewerBilling.ReportSource = crystalReportDeliveryNoteConsolidated;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetReceiptNote()
        {
            try
            {
                CustomDataSet.NoteDataSet dsNote = reportReceiptNote.BindDataSetReceiptNote(InvoiceID, companyId);

                ReceiptNote.Format2.ReceiptNote crystalReportReceiptNote = new ReceiptNote.Format2.ReceiptNote();

                crystalReportReceiptNote.SetDataSource(dsNote);
                crystalReportReceiptNote.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                this.crystalReportViewerBilling.ReportSource = crystalReportReceiptNote;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GetDeliveryNote()
        {
            try
            {
                CustomDataSet.NoteDataSet dsNote = reportDeliveryNote.BindDataSetDeliveryNote(InvoiceID, companyId);

                DeliveryNote.Format2.DeliveryNote crystalReportDeliveryNote = new DeliveryNote.Format2.DeliveryNote();

                crystalReportDeliveryNote.SetDataSource(dsNote);
                crystalReportDeliveryNote.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                this.crystalReportViewerBilling.ReportSource = crystalReportDeliveryNote;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void getInvoiceCopyType()
        {
            PopupControl.InvoiceCopyPopup popup = new PopupControl.InvoiceCopyPopup();

            popup.Text = "Invoice Copy Detail";
            DialogResult dialogResult = popup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                invoiceCopyType = popup.InvoiceCopyType;
                invoiceCopyFor = popup.InvoiceCopyFor;
            }
        }

        private void GetQuotation()
        {
            try
            {
                CustomDataSet.BillingDataSet dsInvoice = reportQuotation.BindDataSetQuotation(InvoiceID, companyId);

                Quotation.Format1.Quotation crystalReportQuotation = new Quotation.Format1.Quotation();
                Quotation.Format1.QuotationIGST crystalReportQuotationIGST = new Quotation.Format1.QuotationIGST();
                if (isIGSTApplicable)
                {
                    crystalReportQuotationIGST.SetDataSource(dsInvoice);
                    this.crystalReportViewerBilling.ReportSource = crystalReportQuotationIGST;
                }
                else
                {
                    crystalReportQuotation.SetDataSource(dsInvoice);
                    this.crystalReportViewerBilling.ReportSource = crystalReportQuotation;
                }

                this.crystalReportViewerBilling.RefreshReport();
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetQuotationConsolidated()
        {
            CustomDataSet.BillingDataSet dsInvoice = reportQuotation.BindDataSetQuotationConsolidated(FromDate, ToDate, companyId, FilterValue);

            Quotation.QuotationConsolidated crystalReportQuotationConsolidated = new Quotation.QuotationConsolidated();
            crystalReportQuotationConsolidated.SetDataSource(dsInvoice);

            crystalReportQuotationConsolidated.SetParameterValue("Param1", FromDate);
            crystalReportQuotationConsolidated.SetParameterValue("Param2", ToDate);

            this.crystalReportViewerBilling.ReportSource = crystalReportQuotationConsolidated;
        }
        private void GetQuotationDetailed()
        {
            try
            {
                CustomDataSet.BillingDataSet dsInvoice = reportQuotation.BindDataSetQuotationDetailed(FromDate, ToDate, companyId, FilterValue);

                Quotation.QuotationDetailed crystalReportQuotationDetailed = new Quotation.QuotationDetailed();
                crystalReportQuotationDetailed.SetDataSource(dsInvoice);

                crystalReportQuotationDetailed.SetParameterValue("Param1", FromDate);
                crystalReportQuotationDetailed.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerBilling.ReportSource = crystalReportQuotationDetailed;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPurchaseInvoiceDetailed()
        {
            try
            {
                CustomDataSet.BillingDataSet dsInvoice = reportPurchaseInvoice.BindDataSetPurchaseInvoiceDetailed(FromDate, ToDate, companyId, FilterValue);

                PurchaseInvoice.PurchaseInvoiceDetailed crystalReportPurchaseInvoiceDetailed = new PurchaseInvoice.PurchaseInvoiceDetailed();
                crystalReportPurchaseInvoiceDetailed.SetDataSource(dsInvoice);

                crystalReportPurchaseInvoiceDetailed.SetParameterValue("Param1", FromDate);
                crystalReportPurchaseInvoiceDetailed.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerBilling.ReportSource = crystalReportPurchaseInvoiceDetailed;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPurchaseInvoiceConsolidated()
        {
            try
            {
                CustomDataSet.BillingDataSet dsInvoice = reportPurchaseInvoice.BindDataSetPurchaseInvoiceConsolidated(FromDate, ToDate, companyId, FilterValue);

                PurchaseInvoice.PurchaseInvoiceConsolidated crystalReportPurchaseInvoiceConsolidated = new PurchaseInvoice.PurchaseInvoiceConsolidated();
                crystalReportPurchaseInvoiceConsolidated.SetDataSource(dsInvoice);

                crystalReportPurchaseInvoiceConsolidated.SetParameterValue("Param1", FromDate);
                crystalReportPurchaseInvoiceConsolidated.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerBilling.ReportSource = crystalReportPurchaseInvoiceConsolidated;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPurchaseInvoice()
        {
            try
            {
                CustomDataSet.BillingDataSet dsInvoice = reportPurchaseInvoice.BindDataSetPurchaseInvoice(InvoiceID, companyId);

                PurchaseInvoice.Format1.PurchaseInvoice crystalReportSalesInvoice = new PurchaseInvoice.Format1.PurchaseInvoice();
                PurchaseInvoice.Format1.PurchaseInvoiceIGST crystalReportSalesInvoiceIGST = new PurchaseInvoice.Format1.PurchaseInvoiceIGST();
                if (isIGSTApplicable)
                {
                    crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                }
                else
                {
                    crystalReportSalesInvoice.SetDataSource(dsInvoice);
                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                }
                //crystalReportSalesInvoice.SetDataSource(dsInvoice);
                //this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;

                this.crystalReportViewerBilling.RefreshReport();
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GetSalesInvoiceDetailed()
        {
            try
            {
                CustomDataSet.SalesInvoiceDataSet dsInvoice = reportSalesInvoice.BindDataSetSalesInvoiceDetailed(FromDate, ToDate, companyId, FilterValue);

                SalesInvoice.SalesInvoiceDetailed crystalReportSalesInvoiceDetailed = new SalesInvoice.SalesInvoiceDetailed();
                crystalReportSalesInvoiceDetailed.SetDataSource(dsInvoice);

                crystalReportSalesInvoiceDetailed.SetParameterValue("Param1", FromDate);
                crystalReportSalesInvoiceDetailed.SetParameterValue("Param2", ToDate);

                this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceDetailed;
                this.crystalReportViewerBilling.ShowCloseButton = true;
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetSalesInvoiceConsolidated()
        {
            try
            {
                string salesInvoiceConsolidatedReportFormat = ConfigurationManager.AppSettings["SalesInvoiceConsolidatedReportFormat"].ToString().Trim();

                if (salesInvoiceConsolidatedReportFormat == "2")
                {
                    CustomDataSet.SalesInvoiceDataSet dsInvoice = reportSalesInvoice.BindDataSetSalesInvoiceConsolidated(FromDate, ToDate, companyId, FilterValue);

                    SalesInvoice.SalesInvoiceConsolidatedFormat2 crystalReportSalesInvoiceConsolidated = new SalesInvoice.SalesInvoiceConsolidatedFormat2();
                    crystalReportSalesInvoiceConsolidated.SetDataSource(dsInvoice);

                    crystalReportSalesInvoiceConsolidated.SetParameterValue("Param1", FromDate);
                    crystalReportSalesInvoiceConsolidated.SetParameterValue("Param2", ToDate);
                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceConsolidated;
                }
                else
                {
                    CustomDataSet.SalesInvoiceDataSet dsInvoice = reportSalesInvoice.BindDataSetSalesInvoiceConsolidated(FromDate, ToDate, companyId, FilterValue);

                    SalesInvoice.SalesInvoiceConsolidated crystalReportSalesInvoiceConsolidated = new SalesInvoice.SalesInvoiceConsolidated();
                    crystalReportSalesInvoiceConsolidated.SetDataSource(dsInvoice);

                    crystalReportSalesInvoiceConsolidated.SetParameterValue("Param1", FromDate);
                    crystalReportSalesInvoiceConsolidated.SetParameterValue("Param2", ToDate);
                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceConsolidated;
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetSalesInvoice()
        {
            string UomHeaderName;

            try
            {
                string salesInvoiceReportFormat = ConfigurationManager.AppSettings["SalesInvoiceReportFormat"].ToString().Trim();

                CustomDataSet.SalesInvoiceDataSet dsInvoice = reportSalesInvoice.BindDataSetSalesInvoice(InvoiceID, companyId, Convert.ToInt16(ConfigurationManager.AppSettings["SalesInvoiceTransporterNoOfCopies"]));
                //For Sri Manjunatha
                if (salesInvoiceReportFormat == "5")
                {
                    UomHeaderName = BAL.GenericValidation.GetUoMCount(InvoiceID);
                    if (UomHeaderName == "UnitCompined")
                    {
                        UomHeaderName = ConfigurationManager.AppSettings["SalesInvoiceUomGenericHeaderName"].ToString();
                    }

                    Int64 SumCF1Item = BAL.GenericValidation.GetSumOfCF1Item(InvoiceID);

                    if (invoiceCopyFor == "For Recipient")
                    {
                        invoiceCopyFor = ConfigurationManager.AppSettings["SalesInvoiceTypeCustomer"].ToString();
                        invoiceCopyType = "";
                    }
                    else if (invoiceCopyFor == "For Transporter")
                    {
                        invoiceCopyFor = ConfigurationManager.AppSettings["SalesInvoiceTypeTransport"].ToString();
                        invoiceCopyType = "";
                    }
                    //SalesInvoice.Format5.SalesInvoiceIGST crystalReportSalesInvoiceIGST = new SalesInvoice.Format5.SalesInvoiceIGST();
                    SalesInvoice.Format5.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format5.SalesInvoice();

                    crystalReportSalesInvoice.SetDataSource(dsInvoice);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                    crystalReportSalesInvoice.SetParameterValue("UomHeaderName", UomHeaderName);
                    crystalReportSalesInvoice.SetParameterValue("CF1ItemSum", SumCF1Item);
                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                }
                //For Kushal Electricals
                else if (salesInvoiceReportFormat == "6")
                {
                    SalesInvoice.Format6.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format6.SalesInvoice();

                    crystalReportSalesInvoice.SetDataSource(dsInvoice);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                    crystalReportSalesInvoice.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                }
                //For Jayam Store
                else if (salesInvoiceReportFormat == "7")
                {
                    //GSTN exists - Tax Invoice needs to be shown
                    if (dsInvoice.Tables["dtSalesInvoiceHeader"].Rows[0]["CompanyGSTN"].ToString() != string.Empty)
                    {
                        SalesInvoice.Format7.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format7.SalesInvoice();

                        crystalReportSalesInvoice.SetDataSource(dsInvoice);
                        crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                        crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                        crystalReportSalesInvoice.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                        this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                    }
                    //GSTN not exists - Invoice without tax needs to be shown
                    else
                    {
                        SalesInvoice.Format7.SalesInvoiceWithoutGSTN crystalReportSalesInvoice = new SalesInvoice.Format7.SalesInvoiceWithoutGSTN();
                        crystalReportSalesInvoice.SetDataSource(dsInvoice);
                        this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                    }
                }
                //For R.K. Samy Enterprices
                else if (salesInvoiceReportFormat == "8")
                {
                    //GSTN exists - Tax Invoice needs to be shown
                    if (dsInvoice.Tables["dtSalesInvoiceHeader"].Rows[0]["CompanyGSTN"].ToString() != string.Empty)
                    {
                        SalesInvoice.Format8.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format8.SalesInvoice();
                        crystalReportSalesInvoice.SetDataSource(dsInvoice);
                        crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                        crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                        crystalReportSalesInvoice.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                        this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                    }
                    //GSTN not exists - Invoice without tax needs to be shown
                    else
                    {
                        SalesInvoice.Format8.SalesInvoiceWithoutGSTN crystalReportSalesInvoice = new SalesInvoice.Format8.SalesInvoiceWithoutGSTN();
                        crystalReportSalesInvoice.SetDataSource(dsInvoice);
                        this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                    }
                }
                //For Kavya Traders
                else if (salesInvoiceReportFormat == "9")
                {
                    SalesInvoice.Format9.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format9.SalesInvoice();

                    crystalReportSalesInvoice.SetDataSource(dsInvoice);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                    //Checking report header style settings from web Config
                    if (dsInvoice.Tables["dtSalesInvoiceHeader"].Rows[0]["CompName"].ToString().ToLower() == ConfigurationManager.AppSettings["Comp1Name"].ToString().ToLower())
                    {
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontName", ConfigurationManager.AppSettings["Comp1ReportHeaderFontName"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontStyle", ConfigurationManager.AppSettings["Comp1ReportHeaderFontStyle"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontColor", ConfigurationManager.AppSettings["Comp1ReportHeaderFontColor"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontSize", ConfigurationManager.AppSettings["Comp1ReportHeaderFontSize"].ToString());
                    }
                    else if (dsInvoice.Tables["dtSalesInvoiceHeader"].Rows[0]["CompName"].ToString().ToLower() == ConfigurationManager.AppSettings["Comp2Name"].ToString().ToLower())
                    {
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontName", ConfigurationManager.AppSettings["Comp2ReportHeaderFontName"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontStyle", ConfigurationManager.AppSettings["Comp2ReportHeaderFontStyle"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontColor", ConfigurationManager.AppSettings["Comp2ReportHeaderFontColor"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontSize", ConfigurationManager.AppSettings["Comp2ReportHeaderFontSize"].ToString());
                    }

                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                }
                //For Sri Venus, Muthur
                else if (salesInvoiceReportFormat == "10")
                {
                    SalesInvoice.Format10.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format10.SalesInvoice();

                    crystalReportSalesInvoice.SetDataSource(dsInvoice);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                    crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                    //Checking report header style settings from web Config
                    if (dsInvoice.Tables["dtSalesInvoiceHeader"].Rows[0]["CompName"].ToString().ToLower() == ConfigurationManager.AppSettings["Comp1Name"].ToString().ToLower())
                    {
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontName", ConfigurationManager.AppSettings["Comp1ReportHeaderFontName"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontStyle", ConfigurationManager.AppSettings["Comp1ReportHeaderFontStyle"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontColor", ConfigurationManager.AppSettings["Comp1ReportHeaderFontColor"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontSize", ConfigurationManager.AppSettings["Comp1ReportHeaderFontSize"].ToString());
                    }
                    else if (dsInvoice.Tables["dtSalesInvoiceHeader"].Rows[0]["CompName"].ToString().ToLower() == ConfigurationManager.AppSettings["Comp2Name"].ToString().ToLower())
                    {
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontName", ConfigurationManager.AppSettings["Comp2ReportHeaderFontName"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontStyle", ConfigurationManager.AppSettings["Comp2ReportHeaderFontStyle"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontColor", ConfigurationManager.AppSettings["Comp2ReportHeaderFontColor"].ToString());
                        crystalReportSalesInvoice.SetParameterValue("HeaderFontSize", ConfigurationManager.AppSettings["Comp2ReportHeaderFontSize"].ToString());
                    }

                    this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                }
                else if (dsInvoice.Tables["dtCompanyLogo"].Rows.Count > 0)
                {
                    if (salesInvoiceReportFormat == "1")
                    {
                        SalesInvoice.Format1.SalesInvoiceFormat1Logo crystalReportSalesInvoice = new SalesInvoice.Format1.SalesInvoiceFormat1Logo();
                        SalesInvoice.Format1.SalesInvoiceFormat1IGSTLogo crystalReportSalesInvoiceIGST = new SalesInvoice.Format1.SalesInvoiceFormat1IGSTLogo();
                        SalesInvoice.Format1.SalesInvoiceTransporter crystalReportSalesInvoiceTransporter = new SalesInvoice.Format1.SalesInvoiceTransporter();

                        if (invoiceCopyFor == "For Transporter")
                        {
                            crystalReportSalesInvoiceTransporter.SetDataSource(dsInvoice);
                            crystalReportSalesInvoiceTransporter.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                            crystalReportSalesInvoiceTransporter.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                            crystalReportSalesInvoiceTransporter.SetParameterValue("ISIGSTApplicable", isIGSTApplicable);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceTransporter;
                        }
                        else if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            crystalReportSalesInvoiceIGST.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                            crystalReportSalesInvoiceIGST.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                            crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                    else if (salesInvoiceReportFormat == "2")
                    {
                        SalesInvoice.Format2.SalesInvoiceFormat2Logo crystalReportSalesInvoice = new SalesInvoice.Format2.SalesInvoiceFormat2Logo();
                        SalesInvoice.Format2.SalesInvoiceFormat2IGSTLogo crystalReportSalesInvoiceIGST = new SalesInvoice.Format2.SalesInvoiceFormat2IGSTLogo();
                        if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                    else if (salesInvoiceReportFormat == "3")
                    {
                        SalesInvoice.Format3.SalesInvoiceLogo crystalReportSalesInvoice = new SalesInvoice.Format3.SalesInvoiceLogo();
                        SalesInvoice.Format3.SalesInvoiceIGSTLogo crystalReportSalesInvoiceIGST = new SalesInvoice.Format3.SalesInvoiceIGSTLogo();
                        if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                    else if (salesInvoiceReportFormat == "4")
                    {
                        SalesInvoice.Format4.SalesInvoiceLogo crystalReportSalesInvoice = new SalesInvoice.Format4.SalesInvoiceLogo();
                        SalesInvoice.Format4.SalesInvoiceIGSTLogo crystalReportSalesInvoiceIGST = new SalesInvoice.Format4.SalesInvoiceIGSTLogo();
                        if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                }
                else
                {
                    if (salesInvoiceReportFormat == "1")
                    {
                        SalesInvoice.Format1.SalesInvoiceFormat1 crystalReportSalesInvoice = new SalesInvoice.Format1.SalesInvoiceFormat1();
                        SalesInvoice.Format1.SalesInvoiceFormat1IGST crystalReportSalesInvoiceIGST = new SalesInvoice.Format1.SalesInvoiceFormat1IGST();
                        SalesInvoice.Format1.SalesInvoiceTransporter crystalReportSalesInvoiceTransporter = new SalesInvoice.Format1.SalesInvoiceTransporter();
                        if (invoiceCopyFor == "For Transporter")
                        {
                            crystalReportSalesInvoiceTransporter.SetDataSource(dsInvoice);
                            crystalReportSalesInvoiceTransporter.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                            crystalReportSalesInvoiceTransporter.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceTransporter;
                        }
                        else if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            crystalReportSalesInvoiceIGST.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                            crystalReportSalesInvoiceIGST.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            crystalReportSalesInvoice.SetParameterValue("InvoiceCopyType", invoiceCopyType);
                            crystalReportSalesInvoice.SetParameterValue("InvoiceCopyFor", invoiceCopyFor);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                    else if (salesInvoiceReportFormat == "2")
                    {
                        SalesInvoice.Format2.SalesInvoiceFormat2 crystalReportSalesInvoice = new SalesInvoice.Format2.SalesInvoiceFormat2();
                        SalesInvoice.Format2.SalesInvoiceFormat2IGST crystalReportSalesInvoiceIGST = new SalesInvoice.Format2.SalesInvoiceFormat2IGST();
                        if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                    else if (salesInvoiceReportFormat == "3")
                    {
                        SalesInvoice.Format3.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format3.SalesInvoice();
                        SalesInvoice.Format3.SalesInvoiceIGST crystalReportSalesInvoiceIGST = new SalesInvoice.Format3.SalesInvoiceIGST();
                        if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }
                    else if (salesInvoiceReportFormat == "4")
                    {
                        SalesInvoice.Format4.SalesInvoice crystalReportSalesInvoice = new SalesInvoice.Format4.SalesInvoice();
                        SalesInvoice.Format4.SalesInvoiceIGST crystalReportSalesInvoiceIGST = new SalesInvoice.Format4.SalesInvoiceIGST();
                        if (isIGSTApplicable)
                        {
                            crystalReportSalesInvoiceIGST.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoiceIGST;
                        }
                        else
                        {
                            crystalReportSalesInvoice.SetDataSource(dsInvoice);
                            this.crystalReportViewerBilling.ReportSource = crystalReportSalesInvoice;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

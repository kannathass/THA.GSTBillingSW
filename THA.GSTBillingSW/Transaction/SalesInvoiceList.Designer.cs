namespace THA.GSTBillingSW.Transaction
{
    partial class SalesInvoiceList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonNewSalesInvoice = new System.Windows.Forms.Button();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePickerInvoiceToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerInvoiceFromDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDetailedReport = new System.Windows.Forms.Button();
            this.buttonConsolidatedReport = new System.Windows.Forms.Button();
            this.buttonPreviewInvoice = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNewSalesInvoice
            // 
            this.buttonNewSalesInvoice.BackColor = System.Drawing.Color.Green;
            this.buttonNewSalesInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewSalesInvoice.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonNewSalesInvoice.Location = new System.Drawing.Point(726, 8);
            this.buttonNewSalesInvoice.Name = "buttonNewSalesInvoice";
            this.buttonNewSalesInvoice.Size = new System.Drawing.Size(265, 40);
            this.buttonNewSalesInvoice.TabIndex = 2;
            this.buttonNewSalesInvoice.TabStop = false;
            this.buttonNewSalesInvoice.Text = "+ &New Sales Invoice";
            this.buttonNewSalesInvoice.UseVisualStyleBackColor = false;
            this.buttonNewSalesInvoice.Click += new System.EventHandler(this.buttonNewSalesInvoice_Click);
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToResizeRows = false;
            this.dataGridViewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.Location = new System.Drawing.Point(34, 213);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewList.RowTemplate.Height = 24;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(957, 432);
            this.dataGridViewList.TabIndex = 5;
            this.dataGridViewList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellClick);
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
            this.dataGridViewList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_RowEnter);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            // 
            // comboBoxCompany
            // 
            this.comboBoxCompany.BackColor = System.Drawing.Color.White;
            this.comboBoxCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCompany.FormattingEnabled = true;
            this.comboBoxCompany.Location = new System.Drawing.Point(34, 38);
            this.comboBoxCompany.MaxLength = 100;
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(386, 26);
            this.comboBoxCompany.TabIndex = 0;
            this.comboBoxCompany.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompany_SelectedIndexChanged);
            this.comboBoxCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxCompany_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 18);
            this.label1.TabIndex = 41;
            this.label1.Text = "Company / Business Name *";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.Green;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRefresh.Location = new System.Drawing.Point(726, 54);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(265, 40);
            this.buttonRefresh.TabIndex = 45;
            this.buttonRefresh.TabStop = false;
            this.buttonRefresh.Text = "&Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(452, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 18);
            this.label8.TabIndex = 49;
            this.label8.Text = "Invoice To Date";
            // 
            // dateTimePickerInvoiceToDate
            // 
            this.dateTimePickerInvoiceToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerInvoiceToDate.Location = new System.Drawing.Point(455, 62);
            this.dateTimePickerInvoiceToDate.Name = "dateTimePickerInvoiceToDate";
            this.dateTimePickerInvoiceToDate.Size = new System.Drawing.Size(110, 22);
            this.dateTimePickerInvoiceToDate.TabIndex = 4;
            this.dateTimePickerInvoiceToDate.Value = new System.DateTime(2017, 8, 22, 0, 0, 0, 0);
            this.dateTimePickerInvoiceToDate.ValueChanged += new System.EventHandler(this.dateTimePickerInvoiceToDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(298, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 18);
            this.label3.TabIndex = 47;
            this.label3.Text = "Invoice From Date";
            // 
            // dateTimePickerInvoiceFromDate
            // 
            this.dateTimePickerInvoiceFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerInvoiceFromDate.Location = new System.Drawing.Point(301, 62);
            this.dateTimePickerInvoiceFromDate.Name = "dateTimePickerInvoiceFromDate";
            this.dateTimePickerInvoiceFromDate.Size = new System.Drawing.Size(110, 22);
            this.dateTimePickerInvoiceFromDate.TabIndex = 3;
            this.dateTimePickerInvoiceFromDate.Value = new System.DateTime(2017, 8, 15, 0, 0, 0, 0);
            this.dateTimePickerInvoiceFromDate.ValueChanged += new System.EventHandler(this.dateTimePickerInvoiceFromDate_ValueChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.Snow;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(32, 62);
            this.textBoxSearch.MaxLength = 50;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(245, 24);
            this.textBoxSearch.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxSearch, "Enter Invoice # (or) Customer Name  (or) Customer State");
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(29, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 18);
            this.label12.TabIndex = 73;
            this.label12.Tag = "";
            this.label12.Text = "Quick Search";
            this.toolTip1.SetToolTip(this.label12, "Enter Invoice # (or) Customer Name  (or) Customer State");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDetailedReport);
            this.groupBox1.Controls.Add(this.buttonConsolidatedReport);
            this.groupBox1.Controls.Add(this.dateTimePickerInvoiceToDate);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBoxSearch);
            this.groupBox1.Controls.Add(this.dateTimePickerInvoiceFromDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(34, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(969, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Invoice";
            // 
            // buttonDetailedReport
            // 
            this.buttonDetailedReport.BackColor = System.Drawing.Color.Green;
            this.buttonDetailedReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDetailedReport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonDetailedReport.Location = new System.Drawing.Point(692, 61);
            this.buttonDetailedReport.Name = "buttonDetailedReport";
            this.buttonDetailedReport.Size = new System.Drawing.Size(265, 40);
            this.buttonDetailedReport.TabIndex = 77;
            this.buttonDetailedReport.TabStop = false;
            this.buttonDetailedReport.Text = "&Detailed Report";
            this.buttonDetailedReport.UseVisualStyleBackColor = false;
            this.buttonDetailedReport.Click += new System.EventHandler(this.buttonDetailedReport_Click);
            // 
            // buttonConsolidatedReport
            // 
            this.buttonConsolidatedReport.BackColor = System.Drawing.Color.Green;
            this.buttonConsolidatedReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConsolidatedReport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonConsolidatedReport.Location = new System.Drawing.Point(692, 13);
            this.buttonConsolidatedReport.Name = "buttonConsolidatedReport";
            this.buttonConsolidatedReport.Size = new System.Drawing.Size(265, 40);
            this.buttonConsolidatedReport.TabIndex = 76;
            this.buttonConsolidatedReport.TabStop = false;
            this.buttonConsolidatedReport.Text = "C&onsolidated Report";
            this.buttonConsolidatedReport.UseVisualStyleBackColor = false;
            this.buttonConsolidatedReport.Click += new System.EventHandler(this.buttonConsolidatedReport_Click);
            // 
            // buttonPreviewInvoice
            // 
            this.buttonPreviewInvoice.BackColor = System.Drawing.Color.Green;
            this.buttonPreviewInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreviewInvoice.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonPreviewInvoice.Location = new System.Drawing.Point(726, 651);
            this.buttonPreviewInvoice.Name = "buttonPreviewInvoice";
            this.buttonPreviewInvoice.Size = new System.Drawing.Size(265, 40);
            this.buttonPreviewInvoice.TabIndex = 75;
            this.buttonPreviewInvoice.TabStop = false;
            this.buttonPreviewInvoice.Text = "&Preview Invoice";
            this.buttonPreviewInvoice.UseVisualStyleBackColor = false;
            this.buttonPreviewInvoice.Click += new System.EventHandler(this.buttonPreviewInvoice_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Quick Search";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Green;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(34, 651);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 76;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "&Exit";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SalesInvoiceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1015, 703);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonPreviewInvoice);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.buttonNewSalesInvoice);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SalesInvoiceList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Invoice List";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNewSalesInvoice;
        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePickerInvoiceToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerInvoiceFromDate;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPreviewInvoice;
        private System.Windows.Forms.Button buttonConsolidatedReport;
        private System.Windows.Forms.Button buttonDetailedReport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonCancel;
    }
}
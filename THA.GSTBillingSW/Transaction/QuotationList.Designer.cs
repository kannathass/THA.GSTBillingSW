namespace THA.GSTBillingSW.Transaction
{
    partial class QuotationList
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDetailedReport = new System.Windows.Forms.Button();
            this.buttonConsolidatedReport = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerInvoiceToDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dateTimePickerInvoiceFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.buttonNewEntry = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Green;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(23, 651);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 91;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "&Exit";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonDetailedReport
            // 
            this.buttonDetailedReport.BackColor = System.Drawing.Color.Green;
            this.buttonDetailedReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDetailedReport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonDetailedReport.Location = new System.Drawing.Point(697, 64);
            this.buttonDetailedReport.Name = "buttonDetailedReport";
            this.buttonDetailedReport.Size = new System.Drawing.Size(265, 40);
            this.buttonDetailedReport.TabIndex = 79;
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
            this.buttonConsolidatedReport.Location = new System.Drawing.Point(697, 16);
            this.buttonConsolidatedReport.Name = "buttonConsolidatedReport";
            this.buttonConsolidatedReport.Size = new System.Drawing.Size(265, 40);
            this.buttonConsolidatedReport.TabIndex = 78;
            this.buttonConsolidatedReport.TabStop = false;
            this.buttonConsolidatedReport.Text = "C&onsolidated Report";
            this.buttonConsolidatedReport.UseVisualStyleBackColor = false;
            this.buttonConsolidatedReport.Click += new System.EventHandler(this.buttonConsolidatedReport_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.BackColor = System.Drawing.Color.Green;
            this.buttonPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreview.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonPreview.Location = new System.Drawing.Point(720, 649);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(265, 40);
            this.buttonPreview.TabIndex = 90;
            this.buttonPreview.TabStop = false;
            this.buttonPreview.Text = "&Preview Invoice";
            this.buttonPreview.UseVisualStyleBackColor = false;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(23, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(972, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Invoice";
            // 
            // dateTimePickerInvoiceToDate
            // 
            this.dateTimePickerInvoiceToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerInvoiceToDate.Location = new System.Drawing.Point(455, 54);
            this.dateTimePickerInvoiceToDate.Name = "dateTimePickerInvoiceToDate";
            this.dateTimePickerInvoiceToDate.Size = new System.Drawing.Size(110, 22);
            this.dateTimePickerInvoiceToDate.TabIndex = 4;
            this.dateTimePickerInvoiceToDate.Value = new System.DateTime(2017, 8, 22, 0, 0, 0, 0);
            this.dateTimePickerInvoiceToDate.ValueChanged += new System.EventHandler(this.dateTimePickerInvoiceToDate_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(29, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 18);
            this.label12.TabIndex = 73;
            this.label12.Tag = "";
            this.label12.Text = "Quick Search";
            this.toolTip1.SetToolTip(this.label12, "Enter Invoice # (or) Customer Name  (or) Customer State");
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.Snow;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(32, 54);
            this.textBoxSearch.MaxLength = 50;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(245, 24);
            this.textBoxSearch.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxSearch, "Enter Invoice # (or) Customer Name  (or) Customer State");
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // dateTimePickerInvoiceFromDate
            // 
            this.dateTimePickerInvoiceFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerInvoiceFromDate.Location = new System.Drawing.Point(301, 54);
            this.dateTimePickerInvoiceFromDate.Name = "dateTimePickerInvoiceFromDate";
            this.dateTimePickerInvoiceFromDate.Size = new System.Drawing.Size(110, 22);
            this.dateTimePickerInvoiceFromDate.TabIndex = 3;
            this.dateTimePickerInvoiceFromDate.Value = new System.DateTime(2017, 8, 15, 0, 0, 0, 0);
            this.dateTimePickerInvoiceFromDate.ValueChanged += new System.EventHandler(this.dateTimePickerInvoiceFromDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(298, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 18);
            this.label3.TabIndex = 47;
            this.label3.Text = "Quotation From Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(452, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 18);
            this.label8.TabIndex = 49;
            this.label8.Text = "Quotation To Date";
            // 
            // comboBoxCompany
            // 
            this.comboBoxCompany.BackColor = System.Drawing.Color.White;
            this.comboBoxCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCompany.FormattingEnabled = true;
            this.comboBoxCompany.Location = new System.Drawing.Point(23, 38);
            this.comboBoxCompany.MaxLength = 100;
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(386, 26);
            this.comboBoxCompany.TabIndex = 0;
            this.comboBoxCompany.Text = "(Select)";
            this.comboBoxCompany.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompany_SelectedIndexChanged);
            this.comboBoxCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxCompany_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 18);
            this.label1.TabIndex = 88;
            this.label1.Text = "Company / Business Name *";
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
            this.dataGridViewList.Location = new System.Drawing.Point(23, 210);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewList.RowTemplate.Height = 24;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(962, 435);
            this.dataGridViewList.TabIndex = 5;
            this.dataGridViewList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellClick);
            this.dataGridViewList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellDoubleClick);
            this.dataGridViewList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_RowEnter);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            // 
            // buttonNewEntry
            // 
            this.buttonNewEntry.BackColor = System.Drawing.Color.Green;
            this.buttonNewEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewEntry.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonNewEntry.Location = new System.Drawing.Point(720, 4);
            this.buttonNewEntry.Name = "buttonNewEntry";
            this.buttonNewEntry.Size = new System.Drawing.Size(265, 40);
            this.buttonNewEntry.TabIndex = 87;
            this.buttonNewEntry.TabStop = false;
            this.buttonNewEntry.Text = "+ &New Quotation";
            this.buttonNewEntry.UseVisualStyleBackColor = false;
            this.buttonNewEntry.Click += new System.EventHandler(this.buttonNewEntry_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Quick Search";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.Green;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRefresh.Location = new System.Drawing.Point(720, 50);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(265, 40);
            this.buttonRefresh.TabIndex = 89;
            this.buttonRefresh.TabStop = false;
            this.buttonRefresh.Text = "&Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // QuotationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1015, 703);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonPreview);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.buttonNewEntry);
            this.Controls.Add(this.buttonRefresh);
            this.Name = "QuotationList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuotationList";
            this.Load += new System.EventHandler(this.QuotationList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonDetailedReport;
        private System.Windows.Forms.Button buttonConsolidatedReport;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerInvoiceToDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DateTimePicker dateTimePickerInvoiceFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.Button buttonNewEntry;
        private System.Windows.Forms.Button buttonRefresh;
    }
}
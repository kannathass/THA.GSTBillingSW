namespace THA.GSTBillingSW.Master
{
    partial class StockList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.StockID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSNSAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockInHand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BUStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonEnableEdit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxGroupUnder = new System.Windows.Forms.ComboBox();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonReportDetail = new System.Windows.Forms.Button();
            this.buttonReportChart = new System.Windows.Forms.Button();
            this.checkBoxAllGroup = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToResizeRows = false;
            this.dataGridViewList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockID,
            this.ItemID,
            this.GroupName,
            this.ItemName,
            this.Size,
            this.HSNSAC,
            this.UOM,
            this.StockInHand,
            this.BUStock});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewList.Location = new System.Drawing.Point(28, 97);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.RowHeadersVisible = false;
            this.dataGridViewList.RowTemplate.Height = 24;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(840, 563);
            this.dataGridViewList.TabIndex = 9;
            this.dataGridViewList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellEnter);
            this.dataGridViewList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellLeave);
            this.dataGridViewList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellValueChanged);
            // 
            // StockID
            // 
            this.StockID.Frozen = true;
            this.StockID.HeaderText = "StockID";
            this.StockID.Name = "StockID";
            this.StockID.ReadOnly = true;
            this.StockID.Visible = false;
            this.StockID.Width = 73;
            // 
            // ItemID
            // 
            this.ItemID.Frozen = true;
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Visible = false;
            this.ItemID.Width = 64;
            // 
            // GroupName
            // 
            this.GroupName.Frozen = true;
            this.GroupName.HeaderText = "Group Name";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            this.GroupName.Width = 118;
            // 
            // ItemName
            // 
            this.ItemName.Frozen = true;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 104;
            // 
            // Size
            // 
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Width = 64;
            // 
            // HSNSAC
            // 
            this.HSNSAC.HeaderText = "HSN / SAC";
            this.HSNSAC.Name = "HSNSAC";
            this.HSNSAC.ReadOnly = true;
            this.HSNSAC.Width = 105;
            // 
            // UOM
            // 
            this.UOM.HeaderText = "Unit";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Width = 62;
            // 
            // StockInHand
            // 
            this.StockInHand.HeaderText = "Stock In Hand";
            this.StockInHand.Name = "StockInHand";
            this.StockInHand.ReadOnly = true;
            this.StockInHand.Width = 125;
            // 
            // BUStock
            // 
            this.BUStock.HeaderText = "BUStock";
            this.BUStock.Name = "BUStock";
            this.BUStock.ReadOnly = true;
            this.BUStock.Visible = false;
            this.BUStock.Width = 109;
            // 
            // buttonEnableEdit
            // 
            this.buttonEnableEdit.BackColor = System.Drawing.Color.Green;
            this.buttonEnableEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnableEdit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonEnableEdit.Location = new System.Drawing.Point(640, 666);
            this.buttonEnableEdit.Name = "buttonEnableEdit";
            this.buttonEnableEdit.Size = new System.Drawing.Size(135, 40);
            this.buttonEnableEdit.TabIndex = 10;
            this.buttonEnableEdit.Text = "E&nable Edit";
            this.buttonEnableEdit.UseVisualStyleBackColor = false;
            this.buttonEnableEdit.Click += new System.EventHandler(this.buttonEnableEdit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Green;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(92, 666);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "&Exit";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxCompany
            // 
            this.comboBoxCompany.BackColor = System.Drawing.Color.White;
            this.comboBoxCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCompany.FormattingEnabled = true;
            this.comboBoxCompany.Items.AddRange(new object[] {
            "Select",
            "RUBAN FABRICS",
            "SUNDHARI FABRICS"});
            this.comboBoxCompany.Location = new System.Drawing.Point(241, 13);
            this.comboBoxCompany.MaxLength = 100;
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(516, 26);
            this.comboBoxCompany.TabIndex = 104;
            this.comboBoxCompany.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 18);
            this.label1.TabIndex = 103;
            this.label1.Text = "Company / Business Name *";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.White;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(334, 68);
            this.textBoxSearch.MaxLength = 50;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(245, 24);
            this.textBoxSearch.TabIndex = 105;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(330, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 18);
            this.label12.TabIndex = 106;
            this.label12.Text = "Search";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(29, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 18);
            this.label13.TabIndex = 141;
            this.label13.Text = "Group Under";
            // 
            // comboBoxGroupUnder
            // 
            this.comboBoxGroupUnder.BackColor = System.Drawing.Color.White;
            this.comboBoxGroupUnder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxGroupUnder.FormattingEnabled = true;
            this.comboBoxGroupUnder.Items.AddRange(new object[] {
            "Meter",
            "Centi Meter"});
            this.comboBoxGroupUnder.Location = new System.Drawing.Point(33, 68);
            this.comboBoxGroupUnder.MaxLength = 200;
            this.comboBoxGroupUnder.Name = "comboBoxGroupUnder";
            this.comboBoxGroupUnder.Size = new System.Drawing.Size(245, 26);
            this.comboBoxGroupUnder.TabIndex = 140;
            this.comboBoxGroupUnder.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroupUnder_SelectedIndexChanged);
            // 
            // buttonReport
            // 
            this.buttonReport.BackColor = System.Drawing.Color.Green;
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonReport.Location = new System.Drawing.Point(873, 536);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(213, 40);
            this.buttonReport.TabIndex = 142;
            this.buttonReport.TabStop = false;
            this.buttonReport.Text = "Stock &Report";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonReportDetail
            // 
            this.buttonReportDetail.BackColor = System.Drawing.Color.Green;
            this.buttonReportDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReportDetail.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonReportDetail.Location = new System.Drawing.Point(873, 582);
            this.buttonReportDetail.Name = "buttonReportDetail";
            this.buttonReportDetail.Size = new System.Drawing.Size(213, 40);
            this.buttonReportDetail.TabIndex = 143;
            this.buttonReportDetail.TabStop = false;
            this.buttonReportDetail.Text = "Stock Report &Detail";
            this.buttonReportDetail.UseVisualStyleBackColor = false;
            this.buttonReportDetail.Click += new System.EventHandler(this.buttonReportDetail_Click);
            // 
            // buttonReportChart
            // 
            this.buttonReportChart.BackColor = System.Drawing.Color.Green;
            this.buttonReportChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReportChart.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonReportChart.Location = new System.Drawing.Point(873, 628);
            this.buttonReportChart.Name = "buttonReportChart";
            this.buttonReportChart.Size = new System.Drawing.Size(213, 40);
            this.buttonReportChart.TabIndex = 144;
            this.buttonReportChart.TabStop = false;
            this.buttonReportChart.Text = "Stock Report &Chart";
            this.buttonReportChart.UseVisualStyleBackColor = false;
            this.buttonReportChart.Click += new System.EventHandler(this.buttonReportChart_Click);
            // 
            // checkBoxAllGroup
            // 
            this.checkBoxAllGroup.AutoSize = true;
            this.checkBoxAllGroup.Location = new System.Drawing.Point(284, 68);
            this.checkBoxAllGroup.Name = "checkBoxAllGroup";
            this.checkBoxAllGroup.Size = new System.Drawing.Size(45, 21);
            this.checkBoxAllGroup.TabIndex = 145;
            this.checkBoxAllGroup.Text = "All";
            this.checkBoxAllGroup.UseVisualStyleBackColor = true;
            this.checkBoxAllGroup.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // StockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1130, 716);
            this.Controls.Add(this.checkBoxAllGroup);
            this.Controls.Add(this.buttonReportChart);
            this.Controls.Add(this.buttonReportDetail);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBoxGroupUnder);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnableEdit);
            this.Controls.Add(this.dataGridViewList);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StockList";
            this.Text = "Stock List";
            this.Load += new System.EventHandler(this.StockList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.Button buttonEnableEdit;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxGroupUnder;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSNSAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockInHand;
        private System.Windows.Forms.DataGridViewTextBoxColumn BUStock;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonReportDetail;
        private System.Windows.Forms.Button buttonReportChart;
        private System.Windows.Forms.CheckBox checkBoxAllGroup;
    }
}
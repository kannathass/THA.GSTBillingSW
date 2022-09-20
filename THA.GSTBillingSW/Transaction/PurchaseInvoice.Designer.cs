namespace THA.GSTBillingSW.Transaction
{
    partial class PurchaseInvoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelTotalCESSAmount = new System.Windows.Forms.Label();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.ItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCustomDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSNSAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFItem1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFItem2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UoM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RatePerItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxableValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGSTPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGSTValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SGSTPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SGSTValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IGSTPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IGSTValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CESSPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CESSValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTaxIncluded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTotalIGSTAmount = new System.Windows.Forms.Label();
            this.labelTotalSGSTAmount = new System.Windows.Forms.Label();
            this.labelTotalCGSTAmount = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxInvoiceTotalAmount = new System.Windows.Forms.TextBox();
            this.textBoxTaxableAmount = new System.Windows.Forms.TextBox();
            this.textBoxTotalCESSAmount = new System.Windows.Forms.TextBox();
            this.textBoxTotalIGSTAmount = new System.Windows.Forms.TextBox();
            this.textBoxTotalSGSTAmount = new System.Windows.Forms.TextBox();
            this.buttonCancelInvoice = new System.Windows.Forms.Button();
            this.textBoxTotalCGSTAmount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRefreshCustomer = new System.Windows.Forms.Button();
            this.textBoxCF2 = new System.Windows.Forms.TextBox();
            this.textBoxCF3 = new System.Windows.Forms.TextBox();
            this.labelCF3 = new System.Windows.Forms.Label();
            this.textBoxCF1 = new System.Windows.Forms.TextBox();
            this.labelCF1 = new System.Windows.Forms.Label();
            this.textBoxCF4 = new System.Windows.Forms.TextBox();
            this.labelCF4 = new System.Windows.Forms.Label();
            this.labelCF2 = new System.Windows.Forms.Label();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.textBoxCF5 = new System.Windows.Forms.TextBox();
            this.labelCF5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerPODate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPONumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.textBoxCustomerNotes = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxIsCESSApplicable = new System.Windows.Forms.CheckBox();
            this.groupBoxDiscountBasedOn = new System.Windows.Forms.GroupBox();
            this.radioButtonAmountBasedDiscount = new System.Windows.Forms.RadioButton();
            this.radioButtonPercentBasedDiscount = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRoundedOffTotalAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxRoundOff = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxDiscountBasedOn.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTotalCESSAmount
            // 
            this.labelTotalCESSAmount.AutoSize = true;
            this.labelTotalCESSAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalCESSAmount.Location = new System.Drawing.Point(861, 576);
            this.labelTotalCESSAmount.Name = "labelTotalCESSAmount";
            this.labelTotalCESSAmount.Size = new System.Drawing.Size(86, 18);
            this.labelTotalCESSAmount.TabIndex = 124;
            this.labelTotalCESSAmount.Text = "Total CESS";
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToResizeRows = false;
            this.dataGridViewList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemId,
            this.ItemDescription,
            this.ItemCustomDescription,
            this.ItemType,
            this.HSNSAC,
            this.CFItem1,
            this.CFItem2,
            this.Quantity,
            this.UoM,
            this.RatePerItem,
            this.DiscountPercent,
            this.Discount,
            this.TaxableValue,
            this.CGSTPercent,
            this.CGSTValue,
            this.SGSTPercent,
            this.SGSTValue,
            this.IGSTPercent,
            this.IGSTValue,
            this.CESSPercent,
            this.CESSValue,
            this.Total,
            this.IsTaxIncluded});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.Location = new System.Drawing.Point(30, 271);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.RowHeadersWidth = 50;
            this.dataGridViewList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewList.RowTemplate.Height = 24;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(1290, 286);
            this.dataGridViewList.TabIndex = 12;
            this.dataGridViewList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellEnter);
            this.dataGridViewList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellLeave);
            this.dataGridViewList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellValueChanged);
            this.dataGridViewList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewList_RowsAdded);
            this.dataGridViewList.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewList_UserDeletedRow);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            // 
            // ItemId
            // 
            this.ItemId.HeaderText = "ItemId";
            this.ItemId.Name = "ItemId";
            this.ItemId.ReadOnly = true;
            this.ItemId.Visible = false;
            this.ItemId.Width = 74;
            // 
            // ItemDescription
            // 
            this.ItemDescription.HeaderText = "Item";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            this.ItemDescription.Width = 127;
            // 
            // ItemCustomDescription
            // 
            this.ItemCustomDescription.HeaderText = "*Description";
            this.ItemCustomDescription.Name = "ItemCustomDescription";
            // 
            // ItemType
            // 
            this.ItemType.HeaderText = "Item Type";
            this.ItemType.Name = "ItemType";
            this.ItemType.ReadOnly = true;
            this.ItemType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemType.Width = 68;
            // 
            // HSNSAC
            // 
            this.HSNSAC.HeaderText = "HSN/ SAC";
            this.HSNSAC.Name = "HSNSAC";
            this.HSNSAC.Width = 93;
            // 
            // CFItem1
            // 
            this.CFItem1.HeaderText = "CFItem1";
            this.CFItem1.Name = "CFItem1";
            this.CFItem1.Visible = false;
            // 
            // CFItem2
            // 
            this.CFItem2.HeaderText = "CFItem2";
            this.CFItem2.Name = "CFItem2";
            this.CFItem2.Visible = false;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "*Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 59;
            // 
            // UoM
            // 
            this.UoM.HeaderText = "UoM";
            this.UoM.Name = "UoM";
            this.UoM.ReadOnly = true;
            this.UoM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UoM.Width = 43;
            // 
            // RatePerItem
            // 
            this.RatePerItem.HeaderText = "*Rate/ Item";
            this.RatePerItem.Name = "RatePerItem";
            this.RatePerItem.ToolTipText = "Rate per Item in Rupees";
            this.RatePerItem.Width = 93;
            // 
            // DiscountPercent
            // 
            this.DiscountPercent.HeaderText = "*Disc. (%)";
            this.DiscountPercent.Name = "DiscountPercent";
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Disc. (Rs.)";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            this.Discount.ToolTipText = "Discount in Rupees";
            this.Discount.Width = 95;
            // 
            // TaxableValue
            // 
            this.TaxableValue.HeaderText = "Taxable Value";
            this.TaxableValue.Name = "TaxableValue";
            this.TaxableValue.ReadOnly = true;
            this.TaxableValue.Width = 117;
            // 
            // CGSTPercent
            // 
            this.CGSTPercent.HeaderText = "*CGST %";
            this.CGSTPercent.Name = "CGSTPercent";
            this.CGSTPercent.Width = 84;
            // 
            // CGSTValue
            // 
            this.CGSTValue.HeaderText = "CGST Amt";
            this.CGSTValue.Name = "CGSTValue";
            this.CGSTValue.ReadOnly = true;
            this.CGSTValue.ToolTipText = "CGST Amt. (Rs.)";
            this.CGSTValue.Width = 95;
            // 
            // SGSTPercent
            // 
            this.SGSTPercent.HeaderText = "*SGST %";
            this.SGSTPercent.Name = "SGSTPercent";
            this.SGSTPercent.Width = 84;
            // 
            // SGSTValue
            // 
            this.SGSTValue.HeaderText = "SGST Amt";
            this.SGSTValue.Name = "SGSTValue";
            this.SGSTValue.ReadOnly = true;
            this.SGSTValue.ToolTipText = "SGST Amt. (Rs.)";
            this.SGSTValue.Width = 95;
            // 
            // IGSTPercent
            // 
            this.IGSTPercent.HeaderText = "*IGST %";
            this.IGSTPercent.Name = "IGSTPercent";
            this.IGSTPercent.Width = 79;
            // 
            // IGSTValue
            // 
            this.IGSTValue.HeaderText = "IGST Amt";
            this.IGSTValue.Name = "IGSTValue";
            this.IGSTValue.ReadOnly = true;
            this.IGSTValue.ToolTipText = "IGST Amt. (Rs.)";
            this.IGSTValue.Width = 90;
            // 
            // CESSPercent
            // 
            this.CESSPercent.HeaderText = "*CESS %";
            this.CESSPercent.Name = "CESSPercent";
            this.CESSPercent.Width = 82;
            // 
            // CESSValue
            // 
            this.CESSValue.HeaderText = "CESS Amt";
            this.CESSValue.Name = "CESSValue";
            this.CESSValue.ReadOnly = true;
            this.CESSValue.ToolTipText = "CESS Amt. (Rs.)";
            this.CESSValue.Width = 93;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 69;
            // 
            // IsTaxIncluded
            // 
            this.IsTaxIncluded.HeaderText = "IsTaxIncluded";
            this.IsTaxIncluded.Name = "IsTaxIncluded";
            this.IsTaxIncluded.ReadOnly = true;
            this.IsTaxIncluded.Visible = false;
            // 
            // labelTotalIGSTAmount
            // 
            this.labelTotalIGSTAmount.AutoSize = true;
            this.labelTotalIGSTAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalIGSTAmount.Location = new System.Drawing.Point(965, 576);
            this.labelTotalIGSTAmount.Name = "labelTotalIGSTAmount";
            this.labelTotalIGSTAmount.Size = new System.Drawing.Size(79, 18);
            this.labelTotalIGSTAmount.TabIndex = 123;
            this.labelTotalIGSTAmount.Text = "Total IGST";
            // 
            // labelTotalSGSTAmount
            // 
            this.labelTotalSGSTAmount.AutoSize = true;
            this.labelTotalSGSTAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalSGSTAmount.Location = new System.Drawing.Point(1182, 576);
            this.labelTotalSGSTAmount.Name = "labelTotalSGSTAmount";
            this.labelTotalSGSTAmount.Size = new System.Drawing.Size(86, 18);
            this.labelTotalSGSTAmount.TabIndex = 122;
            this.labelTotalSGSTAmount.Text = "Total SGST";
            // 
            // labelTotalCGSTAmount
            // 
            this.labelTotalCGSTAmount.AutoSize = true;
            this.labelTotalCGSTAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalCGSTAmount.Location = new System.Drawing.Point(1074, 576);
            this.labelTotalCGSTAmount.Name = "labelTotalCGSTAmount";
            this.labelTotalCGSTAmount.Size = new System.Drawing.Size(87, 18);
            this.labelTotalCGSTAmount.TabIndex = 121;
            this.labelTotalCGSTAmount.Text = "Total CGST";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1017, 637);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(151, 18);
            this.label17.TabIndex = 120;
            this.label17.Text = "Total Taxable Amount";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1022, 667);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(146, 18);
            this.label16.TabIndex = 119;
            this.label16.Text = "Total Invoice Amount";
            // 
            // textBoxInvoiceTotalAmount
            // 
            this.textBoxInvoiceTotalAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxInvoiceTotalAmount.Enabled = false;
            this.textBoxInvoiceTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInvoiceTotalAmount.Location = new System.Drawing.Point(1185, 661);
            this.textBoxInvoiceTotalAmount.MaxLength = 10;
            this.textBoxInvoiceTotalAmount.Name = "textBoxInvoiceTotalAmount";
            this.textBoxInvoiceTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxInvoiceTotalAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxInvoiceTotalAmount.TabIndex = 118;
            this.textBoxInvoiceTotalAmount.TabStop = false;
            this.textBoxInvoiceTotalAmount.TextChanged += new System.EventHandler(this.textBoxInvoiceTotalAmount_TextChanged);
            // 
            // textBoxTaxableAmount
            // 
            this.textBoxTaxableAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxTaxableAmount.Enabled = false;
            this.textBoxTaxableAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTaxableAmount.Location = new System.Drawing.Point(1185, 631);
            this.textBoxTaxableAmount.MaxLength = 10;
            this.textBoxTaxableAmount.Name = "textBoxTaxableAmount";
            this.textBoxTaxableAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTaxableAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxTaxableAmount.TabIndex = 117;
            this.textBoxTaxableAmount.TabStop = false;
            // 
            // textBoxTotalCESSAmount
            // 
            this.textBoxTotalCESSAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxTotalCESSAmount.Enabled = false;
            this.textBoxTotalCESSAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalCESSAmount.Location = new System.Drawing.Point(864, 597);
            this.textBoxTotalCESSAmount.MaxLength = 10;
            this.textBoxTotalCESSAmount.Name = "textBoxTotalCESSAmount";
            this.textBoxTotalCESSAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTotalCESSAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxTotalCESSAmount.TabIndex = 116;
            this.textBoxTotalCESSAmount.TabStop = false;
            // 
            // textBoxTotalIGSTAmount
            // 
            this.textBoxTotalIGSTAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxTotalIGSTAmount.Enabled = false;
            this.textBoxTotalIGSTAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalIGSTAmount.Location = new System.Drawing.Point(968, 597);
            this.textBoxTotalIGSTAmount.MaxLength = 10;
            this.textBoxTotalIGSTAmount.Name = "textBoxTotalIGSTAmount";
            this.textBoxTotalIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTotalIGSTAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxTotalIGSTAmount.TabIndex = 115;
            this.textBoxTotalIGSTAmount.TabStop = false;
            // 
            // textBoxTotalSGSTAmount
            // 
            this.textBoxTotalSGSTAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxTotalSGSTAmount.Enabled = false;
            this.textBoxTotalSGSTAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalSGSTAmount.Location = new System.Drawing.Point(1185, 597);
            this.textBoxTotalSGSTAmount.MaxLength = 10;
            this.textBoxTotalSGSTAmount.Name = "textBoxTotalSGSTAmount";
            this.textBoxTotalSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTotalSGSTAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxTotalSGSTAmount.TabIndex = 114;
            this.textBoxTotalSGSTAmount.TabStop = false;
            // 
            // buttonCancelInvoice
            // 
            this.buttonCancelInvoice.BackColor = System.Drawing.Color.Green;
            this.buttonCancelInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelInvoice.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancelInvoice.Location = new System.Drawing.Point(171, 758);
            this.buttonCancelInvoice.Name = "buttonCancelInvoice";
            this.buttonCancelInvoice.Size = new System.Drawing.Size(196, 40);
            this.buttonCancelInvoice.TabIndex = 18;
            this.buttonCancelInvoice.TabStop = false;
            this.buttonCancelInvoice.Text = "Cance&l Invoice";
            this.buttonCancelInvoice.UseVisualStyleBackColor = false;
            this.buttonCancelInvoice.Click += new System.EventHandler(this.buttonCancelInvoice_Click);
            // 
            // textBoxTotalCGSTAmount
            // 
            this.textBoxTotalCGSTAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxTotalCGSTAmount.Enabled = false;
            this.textBoxTotalCGSTAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalCGSTAmount.Location = new System.Drawing.Point(1077, 597);
            this.textBoxTotalCGSTAmount.MaxLength = 10;
            this.textBoxTotalCGSTAmount.Name = "textBoxTotalCGSTAmount";
            this.textBoxTotalCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTotalCGSTAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxTotalCGSTAmount.TabIndex = 113;
            this.textBoxTotalCGSTAmount.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRefreshCustomer);
            this.groupBox1.Controls.Add(this.textBoxCF2);
            this.groupBox1.Controls.Add(this.textBoxCF3);
            this.groupBox1.Controls.Add(this.labelCF3);
            this.groupBox1.Controls.Add(this.textBoxCF1);
            this.groupBox1.Controls.Add(this.labelCF1);
            this.groupBox1.Controls.Add(this.textBoxCF4);
            this.groupBox1.Controls.Add(this.labelCF4);
            this.groupBox1.Controls.Add(this.labelCF2);
            this.groupBox1.Controls.Add(this.comboBoxCustomer);
            this.groupBox1.Controls.Add(this.textBoxCF5);
            this.groupBox1.Controls.Add(this.labelCF5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.dateTimePickerDueDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dateTimePickerPODate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxPONumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePickerInvoiceDate);
            this.groupBox1.Controls.Add(this.textBoxInvoiceNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(30, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1290, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice Detail";
            // 
            // buttonRefreshCustomer
            // 
            this.buttonRefreshCustomer.BackColor = System.Drawing.Color.Green;
            this.buttonRefreshCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefreshCustomer.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRefreshCustomer.Location = new System.Drawing.Point(413, 94);
            this.buttonRefreshCustomer.Name = "buttonRefreshCustomer";
            this.buttonRefreshCustomer.Size = new System.Drawing.Size(111, 40);
            this.buttonRefreshCustomer.TabIndex = 158;
            this.buttonRefreshCustomer.TabStop = false;
            this.buttonRefreshCustomer.Text = "Refresh";
            this.buttonRefreshCustomer.UseVisualStyleBackColor = false;
            this.buttonRefreshCustomer.Click += new System.EventHandler(this.buttonRefreshCustomer_Click);
            // 
            // textBoxCF2
            // 
            this.textBoxCF2.BackColor = System.Drawing.Color.Snow;
            this.textBoxCF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCF2.Location = new System.Drawing.Point(781, 104);
            this.textBoxCF2.MaxLength = 100;
            this.textBoxCF2.Name = "textBoxCF2";
            this.textBoxCF2.Size = new System.Drawing.Size(245, 24);
            this.textBoxCF2.TabIndex = 8;
            // 
            // textBoxCF3
            // 
            this.textBoxCF3.BackColor = System.Drawing.Color.Snow;
            this.textBoxCF3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCF3.Location = new System.Drawing.Point(29, 155);
            this.textBoxCF3.MaxLength = 100;
            this.textBoxCF3.Name = "textBoxCF3";
            this.textBoxCF3.Size = new System.Drawing.Size(496, 24);
            this.textBoxCF3.TabIndex = 9;
            // 
            // labelCF3
            // 
            this.labelCF3.AutoSize = true;
            this.labelCF3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCF3.Location = new System.Drawing.Point(26, 134);
            this.labelCF3.Name = "labelCF3";
            this.labelCF3.Size = new System.Drawing.Size(36, 18);
            this.labelCF3.TabIndex = 45;
            this.labelCF3.Text = "CF3";
            // 
            // textBoxCF1
            // 
            this.textBoxCF1.BackColor = System.Drawing.Color.Snow;
            this.textBoxCF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCF1.Location = new System.Drawing.Point(531, 104);
            this.textBoxCF1.MaxLength = 100;
            this.textBoxCF1.Name = "textBoxCF1";
            this.textBoxCF1.Size = new System.Drawing.Size(245, 24);
            this.textBoxCF1.TabIndex = 6;
            // 
            // labelCF1
            // 
            this.labelCF1.AutoSize = true;
            this.labelCF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCF1.Location = new System.Drawing.Point(528, 84);
            this.labelCF1.Name = "labelCF1";
            this.labelCF1.Size = new System.Drawing.Size(36, 18);
            this.labelCF1.TabIndex = 42;
            this.labelCF1.Text = "CF1";
            // 
            // textBoxCF4
            // 
            this.textBoxCF4.BackColor = System.Drawing.Color.Snow;
            this.textBoxCF4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCF4.Location = new System.Drawing.Point(531, 155);
            this.textBoxCF4.MaxLength = 100;
            this.textBoxCF4.Name = "textBoxCF4";
            this.textBoxCF4.Size = new System.Drawing.Size(245, 24);
            this.textBoxCF4.TabIndex = 10;
            // 
            // labelCF4
            // 
            this.labelCF4.AutoSize = true;
            this.labelCF4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCF4.Location = new System.Drawing.Point(528, 135);
            this.labelCF4.Name = "labelCF4";
            this.labelCF4.Size = new System.Drawing.Size(36, 18);
            this.labelCF4.TabIndex = 40;
            this.labelCF4.Text = "CF4";
            // 
            // labelCF2
            // 
            this.labelCF2.AutoSize = true;
            this.labelCF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCF2.Location = new System.Drawing.Point(778, 83);
            this.labelCF2.Name = "labelCF2";
            this.labelCF2.Size = new System.Drawing.Size(36, 18);
            this.labelCF2.TabIndex = 38;
            this.labelCF2.Text = "CF2";
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.BackColor = System.Drawing.Color.Snow;
            this.comboBoxCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Items.AddRange(new object[] {
            "Select",
            "RUBAN FABRICS",
            "SUNDHARI FABRICS"});
            this.comboBoxCustomer.Location = new System.Drawing.Point(27, 104);
            this.comboBoxCustomer.MaxLength = 100;
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(380, 26);
            this.comboBoxCustomer.TabIndex = 7;
            this.comboBoxCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomer_SelectedIndexChanged);
            // 
            // textBoxCF5
            // 
            this.textBoxCF5.BackColor = System.Drawing.Color.Snow;
            this.textBoxCF5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCF5.Location = new System.Drawing.Point(782, 155);
            this.textBoxCF5.MaxLength = 100;
            this.textBoxCF5.Name = "textBoxCF5";
            this.textBoxCF5.Size = new System.Drawing.Size(244, 24);
            this.textBoxCF5.TabIndex = 11;
            // 
            // labelCF5
            // 
            this.labelCF5.AutoSize = true;
            this.labelCF5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCF5.Location = new System.Drawing.Point(779, 135);
            this.labelCF5.Name = "labelCF5";
            this.labelCF5.Size = new System.Drawing.Size(36, 18);
            this.labelCF5.TabIndex = 30;
            this.labelCF5.Text = "CF5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(402, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 18);
            this.label8.TabIndex = 29;
            this.label8.Text = "Due Date";
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.Green;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonClear.Location = new System.Drawing.Point(1130, 27);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(135, 40);
            this.buttonClear.TabIndex = 16;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "&Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // dateTimePickerDueDate
            // 
            this.dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDueDate.Location = new System.Drawing.Point(405, 49);
            this.dateTimePickerDueDate.Name = "dateTimePickerDueDate";
            this.dateTimePickerDueDate.Size = new System.Drawing.Size(120, 22);
            this.dateTimePickerDueDate.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(779, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 18);
            this.label7.TabIndex = 27;
            this.label7.Text = "Reference Date";
            // 
            // dateTimePickerPODate
            // 
            this.dateTimePickerPODate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerPODate.Location = new System.Drawing.Point(782, 49);
            this.dateTimePickerPODate.Name = "dateTimePickerPODate";
            this.dateTimePickerPODate.Size = new System.Drawing.Size(120, 22);
            this.dateTimePickerPODate.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "Customer / Vendor Name *";
            // 
            // textBoxPONumber
            // 
            this.textBoxPONumber.BackColor = System.Drawing.Color.Snow;
            this.textBoxPONumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPONumber.Location = new System.Drawing.Point(531, 49);
            this.textBoxPONumber.MaxLength = 10;
            this.textBoxPONumber.Name = "textBoxPONumber";
            this.textBoxPONumber.Size = new System.Drawing.Size(245, 24);
            this.textBoxPONumber.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(528, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "Reference Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(277, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Invoice Date";
            // 
            // dateTimePickerInvoiceDate
            // 
            this.dateTimePickerInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerInvoiceDate.Location = new System.Drawing.Point(280, 51);
            this.dateTimePickerInvoiceDate.Name = "dateTimePickerInvoiceDate";
            this.dateTimePickerInvoiceDate.Size = new System.Drawing.Size(120, 22);
            this.dateTimePickerInvoiceDate.TabIndex = 2;
            // 
            // textBoxInvoiceNumber
            // 
            this.textBoxInvoiceNumber.BackColor = System.Drawing.Color.White;
            this.textBoxInvoiceNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInvoiceNumber.Location = new System.Drawing.Point(27, 49);
            this.textBoxInvoiceNumber.MaxLength = 16;
            this.textBoxInvoiceNumber.Name = "textBoxInvoiceNumber";
            this.textBoxInvoiceNumber.Size = new System.Drawing.Size(245, 24);
            this.textBoxInvoiceNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "Invoice Number";
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Green;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonDelete.Location = new System.Drawing.Point(30, 758);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(135, 40);
            this.buttonDelete.TabIndex = 17;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.Text = "&Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Green;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(976, 758);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "&Exit";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Green;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonSave.Location = new System.Drawing.Point(1139, 758);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(135, 40);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxCompany
            // 
            this.comboBoxCompany.BackColor = System.Drawing.Color.White;
            this.comboBoxCompany.Enabled = false;
            this.comboBoxCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCompany.FormattingEnabled = true;
            this.comboBoxCompany.Items.AddRange(new object[] {
            "Select",
            "RUBAN FABRICS",
            "SUNDHARI FABRICS"});
            this.comboBoxCompany.Location = new System.Drawing.Point(232, 24);
            this.comboBoxCompany.MaxLength = 100;
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(516, 26);
            this.comboBoxCompany.TabIndex = 100;
            this.comboBoxCompany.TabStop = false;
            this.comboBoxCompany.SelectedIndexChanged += new System.EventHandler(this.comboBoxCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 18);
            this.label1.TabIndex = 102;
            this.label1.Text = "Company / Business Name";
            // 
            // buttonReport
            // 
            this.buttonReport.BackColor = System.Drawing.Color.Green;
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonReport.Location = new System.Drawing.Point(1099, 24);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(196, 40);
            this.buttonReport.TabIndex = 15;
            this.buttonReport.TabStop = false;
            this.buttonReport.Text = "&Preview Invoice";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // textBoxCustomerNotes
            // 
            this.textBoxCustomerNotes.BackColor = System.Drawing.Color.Snow;
            this.textBoxCustomerNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCustomerNotes.Location = new System.Drawing.Point(30, 583);
            this.textBoxCustomerNotes.MaxLength = 1000;
            this.textBoxCustomerNotes.Multiline = true;
            this.textBoxCustomerNotes.Name = "textBoxCustomerNotes";
            this.textBoxCustomerNotes.Size = new System.Drawing.Size(677, 102);
            this.textBoxCustomerNotes.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(27, 562);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 18);
            this.label10.TabIndex = 111;
            this.label10.Text = "Customer Notes";
            // 
            // checkBoxIsCESSApplicable
            // 
            this.checkBoxIsCESSApplicable.AutoSize = true;
            this.checkBoxIsCESSApplicable.Location = new System.Drawing.Point(764, 27);
            this.checkBoxIsCESSApplicable.Name = "checkBoxIsCESSApplicable";
            this.checkBoxIsCESSApplicable.Size = new System.Drawing.Size(152, 21);
            this.checkBoxIsCESSApplicable.TabIndex = 127;
            this.checkBoxIsCESSApplicable.TabStop = false;
            this.checkBoxIsCESSApplicable.Text = "Is Cess Applicable?";
            this.checkBoxIsCESSApplicable.UseVisualStyleBackColor = true;
            this.checkBoxIsCESSApplicable.CheckedChanged += new System.EventHandler(this.checkBoxIsCESSApplicable_CheckedChanged);
            // 
            // groupBoxDiscountBasedOn
            // 
            this.groupBoxDiscountBasedOn.Controls.Add(this.radioButtonAmountBasedDiscount);
            this.groupBoxDiscountBasedOn.Controls.Add(this.radioButtonPercentBasedDiscount);
            this.groupBoxDiscountBasedOn.Location = new System.Drawing.Point(922, 24);
            this.groupBoxDiscountBasedOn.Name = "groupBoxDiscountBasedOn";
            this.groupBoxDiscountBasedOn.Size = new System.Drawing.Size(154, 59);
            this.groupBoxDiscountBasedOn.TabIndex = 132;
            this.groupBoxDiscountBasedOn.TabStop = false;
            this.groupBoxDiscountBasedOn.Text = "Discount Based On";
            // 
            // radioButtonAmountBasedDiscount
            // 
            this.radioButtonAmountBasedDiscount.AutoSize = true;
            this.radioButtonAmountBasedDiscount.Location = new System.Drawing.Point(89, 29);
            this.radioButtonAmountBasedDiscount.Name = "radioButtonAmountBasedDiscount";
            this.radioButtonAmountBasedDiscount.Size = new System.Drawing.Size(50, 21);
            this.radioButtonAmountBasedDiscount.TabIndex = 86;
            this.radioButtonAmountBasedDiscount.Text = "Rs.";
            this.radioButtonAmountBasedDiscount.UseVisualStyleBackColor = true;
            this.radioButtonAmountBasedDiscount.CheckedChanged += new System.EventHandler(this.radioButtonAmountBasedDiscount_CheckedChanged);
            // 
            // radioButtonPercentBasedDiscount
            // 
            this.radioButtonPercentBasedDiscount.AutoSize = true;
            this.radioButtonPercentBasedDiscount.Checked = true;
            this.radioButtonPercentBasedDiscount.Location = new System.Drawing.Point(32, 29);
            this.radioButtonPercentBasedDiscount.Name = "radioButtonPercentBasedDiscount";
            this.radioButtonPercentBasedDiscount.Size = new System.Drawing.Size(41, 21);
            this.radioButtonPercentBasedDiscount.TabIndex = 85;
            this.radioButtonPercentBasedDiscount.TabStop = true;
            this.radioButtonPercentBasedDiscount.Text = "%";
            this.radioButtonPercentBasedDiscount.UseVisualStyleBackColor = true;
            this.radioButtonPercentBasedDiscount.CheckedChanged += new System.EventHandler(this.radioButtonPercentBasedDiscount_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1072, 730);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 18);
            this.label6.TabIndex = 134;
            this.label6.Text = "Total Amount";
            // 
            // textBoxRoundedOffTotalAmount
            // 
            this.textBoxRoundedOffTotalAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxRoundedOffTotalAmount.Enabled = false;
            this.textBoxRoundedOffTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRoundedOffTotalAmount.Location = new System.Drawing.Point(1185, 724);
            this.textBoxRoundedOffTotalAmount.MaxLength = 10;
            this.textBoxRoundedOffTotalAmount.Name = "textBoxRoundedOffTotalAmount";
            this.textBoxRoundedOffTotalAmount.ReadOnly = true;
            this.textBoxRoundedOffTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxRoundedOffTotalAmount.Size = new System.Drawing.Size(89, 24);
            this.textBoxRoundedOffTotalAmount.TabIndex = 133;
            this.textBoxRoundedOffTotalAmount.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(979, 694);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(189, 18);
            this.label9.TabIndex = 136;
            this.label9.Text = "Round off - Paise (eg 0.12) ";
            // 
            // textBoxRoundOff
            // 
            this.textBoxRoundOff.BackColor = System.Drawing.Color.Snow;
            this.textBoxRoundOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRoundOff.Location = new System.Drawing.Point(1230, 691);
            this.textBoxRoundOff.MaxLength = 10;
            this.textBoxRoundOff.Name = "textBoxRoundOff";
            this.textBoxRoundOff.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxRoundOff.Size = new System.Drawing.Size(44, 24);
            this.textBoxRoundOff.TabIndex = 14;
            this.textBoxRoundOff.TabStop = false;
            this.textBoxRoundOff.TextChanged += new System.EventHandler(this.textBoxRoundOff_TextChanged);
            this.textBoxRoundOff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRoundOff_KeyPress);
            this.textBoxRoundOff.Leave += new System.EventHandler(this.textBoxRoundOff_Leave);
            // 
            // PurchaseInvoice
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1533, 803);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxRoundOff);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxRoundedOffTotalAmount);
            this.Controls.Add(this.groupBoxDiscountBasedOn);
            this.Controls.Add(this.checkBoxIsCESSApplicable);
            this.Controls.Add(this.labelTotalCESSAmount);
            this.Controls.Add(this.dataGridViewList);
            this.Controls.Add(this.labelTotalIGSTAmount);
            this.Controls.Add(this.labelTotalSGSTAmount);
            this.Controls.Add(this.labelTotalCGSTAmount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxInvoiceTotalAmount);
            this.Controls.Add(this.textBoxTaxableAmount);
            this.Controls.Add(this.textBoxTotalCESSAmount);
            this.Controls.Add(this.textBoxTotalIGSTAmount);
            this.Controls.Add(this.textBoxTotalSGSTAmount);
            this.Controls.Add(this.buttonCancelInvoice);
            this.Controls.Add(this.textBoxTotalCGSTAmount);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.textBoxCustomerNotes);
            this.Controls.Add(this.label10);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PurchaseInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Invoice";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PurchaseInvoice_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PurchaseInvoice_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxDiscountBasedOn.ResumeLayout(false);
            this.groupBoxDiscountBasedOn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTotalCESSAmount;
        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.Label labelTotalIGSTAmount;
        private System.Windows.Forms.Label labelTotalSGSTAmount;
        private System.Windows.Forms.Label labelTotalCGSTAmount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxInvoiceTotalAmount;
        private System.Windows.Forms.TextBox textBoxTaxableAmount;
        private System.Windows.Forms.TextBox textBoxTotalCESSAmount;
        private System.Windows.Forms.TextBox textBoxTotalIGSTAmount;
        private System.Windows.Forms.TextBox textBoxTotalSGSTAmount;
        private System.Windows.Forms.Button buttonCancelInvoice;
        private System.Windows.Forms.TextBox textBoxTotalCGSTAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxCF2;
        private System.Windows.Forms.TextBox textBoxCF3;
        private System.Windows.Forms.Label labelCF3;
        private System.Windows.Forms.TextBox textBoxCF1;
        private System.Windows.Forms.Label labelCF1;
        private System.Windows.Forms.TextBox textBoxCF4;
        private System.Windows.Forms.Label labelCF4;
        private System.Windows.Forms.Label labelCF2;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.TextBox textBoxCF5;
        private System.Windows.Forms.Label labelCF5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.DateTimePicker dateTimePickerDueDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerPODate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPONumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerInvoiceDate;
        private System.Windows.Forms.TextBox textBoxInvoiceNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.TextBox textBoxCustomerNotes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxIsCESSApplicable;
        private System.Windows.Forms.GroupBox groupBoxDiscountBasedOn;
        private System.Windows.Forms.RadioButton radioButtonAmountBasedDiscount;
        private System.Windows.Forms.RadioButton radioButtonPercentBasedDiscount;
        private System.Windows.Forms.Button buttonRefreshCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCustomDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSNSAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UoM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RatePerItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxableValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGSTPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGSTValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn SGSTPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn SGSTValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn IGSTPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn IGSTValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn CESSPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CESSValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTaxIncluded;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxRoundedOffTotalAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxRoundOff;
    }
}
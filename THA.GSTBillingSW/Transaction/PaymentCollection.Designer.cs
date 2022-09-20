namespace THA.GSTBillingSW.Transaction
{
    partial class PaymentCollection
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
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.InvoiceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountGiven = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterestReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFItem1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFItem2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFItem3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFItem4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceBeforeUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxAgent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxReference = new System.Windows.Forms.TextBox();
            this.dateTimePickerReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxReceiptNumber = new System.Windows.Forms.TextBox();
            this.labelReference = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxPaymentMode = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.checkBoxFilterAllInvoices = new System.Windows.Forms.CheckBox();
            this.textBoxBalance = new System.Windows.Forms.TextBox();
            this.textBoxAmountReceived = new System.Windows.Forms.TextBox();
            this.textBoxInterest = new System.Windows.Forms.TextBox();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPayment = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxInvoiceAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDiscount = new System.Windows.Forms.TextBox();
            this.buttonFill = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.labelReceiptID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.InvoiceId,
            this.InvoiceNumber,
            this.InvoiceDate,
            this.InvoiceAmount,
            this.AmountReceived,
            this.DiscountGiven,
            this.InterestReceived,
            this.CFItem1,
            this.CFItem2,
            this.CFItem3,
            this.CFItem4,
            this.Balance,
            this.BalanceBeforeUpdate});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.Location = new System.Drawing.Point(29, 249);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.RowHeadersWidth = 50;
            this.dataGridViewList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewList.RowTemplate.Height = 24;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(1133, 318);
            this.dataGridViewList.TabIndex = 21;
            this.dataGridViewList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellEnter);
            this.dataGridViewList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellLeave);
            this.dataGridViewList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellValueChanged);
            this.dataGridViewList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewList_RowsAdded);
            this.dataGridViewList.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewList_UserDeletedRow);
            // 
            // InvoiceId
            // 
            this.InvoiceId.Frozen = true;
            this.InvoiceId.HeaderText = "InvoiceId";
            this.InvoiceId.Name = "InvoiceId";
            this.InvoiceId.ReadOnly = true;
            this.InvoiceId.Visible = false;
            this.InvoiceId.Width = 74;
            // 
            // InvoiceNumber
            // 
            this.InvoiceNumber.HeaderText = "Inv #";
            this.InvoiceNumber.Name = "InvoiceNumber";
            this.InvoiceNumber.ReadOnly = true;
            this.InvoiceNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InvoiceNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InvoiceNumber.ToolTipText = "Invoice Number";
            // 
            // InvoiceDate
            // 
            this.InvoiceDate.HeaderText = "Inv Date";
            this.InvoiceDate.Name = "InvoiceDate";
            this.InvoiceDate.ReadOnly = true;
            this.InvoiceDate.ToolTipText = "Invoice Date";
            this.InvoiceDate.Width = 90;
            // 
            // InvoiceAmount
            // 
            this.InvoiceAmount.HeaderText = "Inv Amt";
            this.InvoiceAmount.Name = "InvoiceAmount";
            this.InvoiceAmount.ReadOnly = true;
            this.InvoiceAmount.ToolTipText = "Invoice Amount in Rupees";
            // 
            // AmountReceived
            // 
            this.AmountReceived.HeaderText = "*Amt Received";
            this.AmountReceived.Name = "AmountReceived";
            this.AmountReceived.ToolTipText = "Amount Received in Rupees";
            // 
            // DiscountGiven
            // 
            this.DiscountGiven.HeaderText = "*Discount";
            this.DiscountGiven.Name = "DiscountGiven";
            this.DiscountGiven.ToolTipText = "Discount Given in Rupees";
            // 
            // InterestReceived
            // 
            this.InterestReceived.HeaderText = "*Interest";
            this.InterestReceived.Name = "InterestReceived";
            this.InterestReceived.ToolTipText = "Interest Received in Rupees";
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
            // CFItem3
            // 
            this.CFItem3.HeaderText = "CFItem3";
            this.CFItem3.Name = "CFItem3";
            this.CFItem3.Visible = false;
            // 
            // CFItem4
            // 
            this.CFItem4.HeaderText = "CFItem4";
            this.CFItem4.Name = "CFItem4";
            this.CFItem4.Visible = false;
            // 
            // Balance
            // 
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            this.Balance.ToolTipText = "Balance need to be payed in Rupees";
            // 
            // BalanceBeforeUpdate
            // 
            this.BalanceBeforeUpdate.HeaderText = "BalanceBeforeUpdate";
            this.BalanceBeforeUpdate.Name = "BalanceBeforeUpdate";
            this.BalanceBeforeUpdate.Visible = false;
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
            this.comboBoxCompany.Location = new System.Drawing.Point(229, 23);
            this.comboBoxCompany.MaxLength = 100;
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(466, 26);
            this.comboBoxCompany.TabIndex = 102;
            this.comboBoxCompany.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 18);
            this.label1.TabIndex = 101;
            this.label1.Text = "Company / Business Name";
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.BackColor = System.Drawing.Color.White;
            this.comboBoxCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(23, 93);
            this.comboBoxCustomer.MaxLength = 100;
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(351, 26);
            this.comboBoxCustomer.TabIndex = 4;
            this.comboBoxCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomer_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 104;
            this.label5.Text = "Customer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.comboBoxAgent);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxReference);
            this.groupBox1.Controls.Add(this.dateTimePickerReceiptDate);
            this.groupBox1.Controls.Add(this.textBoxReceiptNumber);
            this.groupBox1.Controls.Add(this.labelReference);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.comboBoxPaymentMode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxCustomer);
            this.groupBox1.Location = new System.Drawing.Point(29, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(781, 137);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Receipt Detail";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(381, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 18);
            this.label14.TabIndex = 147;
            this.label14.Text = "Agent";
            // 
            // comboBoxAgent
            // 
            this.comboBoxAgent.BackColor = System.Drawing.Color.White;
            this.comboBoxAgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAgent.FormattingEnabled = true;
            this.comboBoxAgent.Location = new System.Drawing.Point(384, 41);
            this.comboBoxAgent.MaxLength = 100;
            this.comboBoxAgent.Name = "comboBoxAgent";
            this.comboBoxAgent.Size = new System.Drawing.Size(351, 26);
            this.comboBoxAgent.TabIndex = 3;
            this.comboBoxAgent.SelectedIndexChanged += new System.EventHandler(this.comboBoxAgent_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(228, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 18);
            this.label3.TabIndex = 112;
            this.label3.Text = "Receipt Date";
            // 
            // textBoxReference
            // 
            this.textBoxReference.BackColor = System.Drawing.Color.White;
            this.textBoxReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReference.Location = new System.Drawing.Point(565, 95);
            this.textBoxReference.MaxLength = 50;
            this.textBoxReference.Name = "textBoxReference";
            this.textBoxReference.Size = new System.Drawing.Size(170, 24);
            this.textBoxReference.TabIndex = 6;
            // 
            // dateTimePickerReceiptDate
            // 
            this.dateTimePickerReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerReceiptDate.Location = new System.Drawing.Point(231, 45);
            this.dateTimePickerReceiptDate.Name = "dateTimePickerReceiptDate";
            this.dateTimePickerReceiptDate.Size = new System.Drawing.Size(143, 22);
            this.dateTimePickerReceiptDate.TabIndex = 2;
            // 
            // textBoxReceiptNumber
            // 
            this.textBoxReceiptNumber.BackColor = System.Drawing.Color.White;
            this.textBoxReceiptNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceiptNumber.Location = new System.Drawing.Point(23, 43);
            this.textBoxReceiptNumber.MaxLength = 16;
            this.textBoxReceiptNumber.Name = "textBoxReceiptNumber";
            this.textBoxReceiptNumber.Size = new System.Drawing.Size(202, 24);
            this.textBoxReceiptNumber.TabIndex = 1;
            // 
            // labelReference
            // 
            this.labelReference.AutoSize = true;
            this.labelReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReference.Location = new System.Drawing.Point(563, 74);
            this.labelReference.Name = "labelReference";
            this.labelReference.Size = new System.Drawing.Size(76, 18);
            this.labelReference.TabIndex = 145;
            this.labelReference.Text = "Reference";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 111;
            this.label2.Text = "Receipt Number";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(381, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 18);
            this.label13.TabIndex = 143;
            this.label13.Text = "Payment Mode *";
            // 
            // comboBoxPaymentMode
            // 
            this.comboBoxPaymentMode.BackColor = System.Drawing.Color.White;
            this.comboBoxPaymentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPaymentMode.FormattingEnabled = true;
            this.comboBoxPaymentMode.Location = new System.Drawing.Point(384, 93);
            this.comboBoxPaymentMode.MaxLength = 200;
            this.comboBoxPaymentMode.Name = "comboBoxPaymentMode";
            this.comboBoxPaymentMode.Size = new System.Drawing.Size(175, 26);
            this.comboBoxPaymentMode.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(42, 216);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 18);
            this.label12.TabIndex = 107;
            this.label12.Tag = "";
            this.label12.Text = "Search Invoice #";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.White;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(165, 213);
            this.textBoxSearch.MaxLength = 50;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(185, 24);
            this.textBoxSearch.TabIndex = 7;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Green;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonDelete.Location = new System.Drawing.Point(31, 660);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(135, 40);
            this.buttonDelete.TabIndex = 110;
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
            this.buttonCancel.Location = new System.Drawing.Point(857, 660);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 109;
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
            this.buttonSave.Location = new System.Drawing.Point(998, 660);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(135, 40);
            this.buttonSave.TabIndex = 108;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.Green;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonClear.Location = new System.Drawing.Point(997, 76);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(135, 40);
            this.buttonClear.TabIndex = 111;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "&Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.BackColor = System.Drawing.Color.Green;
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonReport.Location = new System.Drawing.Point(998, 25);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(134, 40);
            this.buttonReport.TabIndex = 112;
            this.buttonReport.TabStop = false;
            this.buttonReport.Text = "&Preview";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.BackColor = System.Drawing.Color.Silver;
            this.buttonPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrevious.ForeColor = System.Drawing.Color.Black;
            this.buttonPrevious.Location = new System.Drawing.Point(358, 650);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(150, 50);
            this.buttonPrevious.TabIndex = 113;
            this.buttonPrevious.TabStop = false;
            this.buttonPrevious.Text = "<- Previous";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor = System.Drawing.Color.Silver;
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.ForeColor = System.Drawing.Color.Black;
            this.buttonNext.Location = new System.Drawing.Point(511, 650);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(150, 50);
            this.buttonNext.TabIndex = 114;
            this.buttonNext.TabStop = false;
            this.buttonNext.Text = "Next ->";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // checkBoxFilterAllInvoices
            // 
            this.checkBoxFilterAllInvoices.AutoSize = true;
            this.checkBoxFilterAllInvoices.Location = new System.Drawing.Point(358, 213);
            this.checkBoxFilterAllInvoices.Name = "checkBoxFilterAllInvoices";
            this.checkBoxFilterAllInvoices.Size = new System.Drawing.Size(166, 21);
            this.checkBoxFilterAllInvoices.TabIndex = 8;
            this.checkBoxFilterAllInvoices.Text = "Filter from all invoices";
            this.checkBoxFilterAllInvoices.UseVisualStyleBackColor = true;
            this.checkBoxFilterAllInvoices.CheckedChanged += new System.EventHandler(this.checkBoxFilterAllInvoices_CheckedChanged);
            // 
            // textBoxBalance
            // 
            this.textBoxBalance.BackColor = System.Drawing.Color.Snow;
            this.textBoxBalance.Enabled = false;
            this.textBoxBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBalance.Location = new System.Drawing.Point(366, 591);
            this.textBoxBalance.MaxLength = 10;
            this.textBoxBalance.Name = "textBoxBalance";
            this.textBoxBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxBalance.Size = new System.Drawing.Size(90, 24);
            this.textBoxBalance.TabIndex = 147;
            this.textBoxBalance.TabStop = false;
            // 
            // textBoxAmountReceived
            // 
            this.textBoxAmountReceived.BackColor = System.Drawing.Color.Snow;
            this.textBoxAmountReceived.Enabled = false;
            this.textBoxAmountReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAmountReceived.Location = new System.Drawing.Point(468, 591);
            this.textBoxAmountReceived.MaxLength = 10;
            this.textBoxAmountReceived.Name = "textBoxAmountReceived";
            this.textBoxAmountReceived.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxAmountReceived.Size = new System.Drawing.Size(90, 24);
            this.textBoxAmountReceived.TabIndex = 148;
            this.textBoxAmountReceived.TabStop = false;
            // 
            // textBoxInterest
            // 
            this.textBoxInterest.BackColor = System.Drawing.Color.Snow;
            this.textBoxInterest.Enabled = false;
            this.textBoxInterest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInterest.Location = new System.Drawing.Point(674, 591);
            this.textBoxInterest.MaxLength = 10;
            this.textBoxInterest.Name = "textBoxInterest";
            this.textBoxInterest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxInterest.Size = new System.Drawing.Size(90, 24);
            this.textBoxInterest.TabIndex = 149;
            this.textBoxInterest.TabStop = false;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.BackColor = System.Drawing.Color.White;
            this.textBoxNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNotes.Location = new System.Drawing.Point(29, 591);
            this.textBoxNotes.MaxLength = 500;
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(225, 60);
            this.textBoxNotes.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(26, 570);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 18);
            this.label10.TabIndex = 211;
            this.label10.Text = "Notes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(567, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 18);
            this.label4.TabIndex = 213;
            this.label4.Tag = "";
            this.label4.Text = "Payment";
            // 
            // textBoxPayment
            // 
            this.textBoxPayment.BackColor = System.Drawing.Color.White;
            this.textBoxPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPayment.Location = new System.Drawing.Point(644, 216);
            this.textBoxPayment.MaxLength = 12;
            this.textBoxPayment.Name = "textBoxPayment";
            this.textBoxPayment.Size = new System.Drawing.Size(120, 24);
            this.textBoxPayment.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(366, 570);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 18);
            this.label6.TabIndex = 214;
            this.label6.Tag = "";
            this.label6.Text = "Balance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(465, 570);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 18);
            this.label7.TabIndex = 215;
            this.label7.Tag = "";
            this.label7.Text = "Amt Received";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(671, 570);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 18);
            this.label8.TabIndex = 216;
            this.label8.Tag = "";
            this.label8.Text = "Interest";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(262, 570);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 18);
            this.label9.TabIndex = 218;
            this.label9.Tag = "";
            this.label9.Text = "Total Inv Amt";
            // 
            // textBoxInvoiceAmount
            // 
            this.textBoxInvoiceAmount.BackColor = System.Drawing.Color.Snow;
            this.textBoxInvoiceAmount.Enabled = false;
            this.textBoxInvoiceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInvoiceAmount.Location = new System.Drawing.Point(265, 591);
            this.textBoxInvoiceAmount.MaxLength = 10;
            this.textBoxInvoiceAmount.Name = "textBoxInvoiceAmount";
            this.textBoxInvoiceAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxInvoiceAmount.Size = new System.Drawing.Size(90, 24);
            this.textBoxInvoiceAmount.TabIndex = 217;
            this.textBoxInvoiceAmount.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(567, 570);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 18);
            this.label11.TabIndex = 220;
            this.label11.Tag = "";
            this.label11.Text = "Discount";
            // 
            // textBoxDiscount
            // 
            this.textBoxDiscount.BackColor = System.Drawing.Color.Snow;
            this.textBoxDiscount.Enabled = false;
            this.textBoxDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDiscount.Location = new System.Drawing.Point(571, 591);
            this.textBoxDiscount.MaxLength = 10;
            this.textBoxDiscount.Name = "textBoxDiscount";
            this.textBoxDiscount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxDiscount.Size = new System.Drawing.Size(90, 24);
            this.textBoxDiscount.TabIndex = 219;
            this.textBoxDiscount.TabStop = false;
            // 
            // buttonFill
            // 
            this.buttonFill.BackColor = System.Drawing.Color.Silver;
            this.buttonFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFill.ForeColor = System.Drawing.Color.Black;
            this.buttonFill.Location = new System.Drawing.Point(770, 213);
            this.buttonFill.Name = "buttonFill";
            this.buttonFill.Size = new System.Drawing.Size(76, 29);
            this.buttonFill.TabIndex = 10;
            this.buttonFill.Text = "Fill";
            this.buttonFill.UseVisualStyleBackColor = false;
            this.buttonFill.Click += new System.EventHandler(this.buttonFill_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(731, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 18);
            this.label15.TabIndex = 221;
            this.label15.Text = "Receipt ID";
            // 
            // labelReceiptID
            // 
            this.labelReceiptID.AutoSize = true;
            this.labelReceiptID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReceiptID.Location = new System.Drawing.Point(813, 14);
            this.labelReceiptID.Name = "labelReceiptID";
            this.labelReceiptID.Size = new System.Drawing.Size(44, 32);
            this.labelReceiptID.TabIndex = 222;
            this.labelReceiptID.Text = "ID";
            // 
            // PaymentCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1174, 719);
            this.Controls.Add(this.labelReceiptID);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.buttonFill);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxDiscount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxInvoiceAmount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPayment);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxInterest);
            this.Controls.Add(this.textBoxAmountReceived);
            this.Controls.Add(this.textBoxBalance);
            this.Controls.Add(this.checkBoxFilterAllInvoices);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewList);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentCollection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Collection";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PaymentCollection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxPaymentMode;
        private System.Windows.Forms.TextBox textBoxReference;
        private System.Windows.Forms.Label labelReference;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerReceiptDate;
        private System.Windows.Forms.TextBox textBoxReceiptNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.CheckBox checkBoxFilterAllInvoices;
        private System.Windows.Forms.TextBox textBoxBalance;
        private System.Windows.Forms.TextBox textBoxAmountReceived;
        private System.Windows.Forms.TextBox textBoxInterest;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPayment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxInvoiceAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDiscount;
        private System.Windows.Forms.Button buttonFill;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxAgent;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelReceiptID;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountReceived;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountGiven;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterestReceived;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFItem4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceBeforeUpdate;
    }
}
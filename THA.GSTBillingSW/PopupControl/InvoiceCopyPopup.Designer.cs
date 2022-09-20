namespace THA.GSTBillingSW.PopupControl
{
    partial class InvoiceCopyPopup
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.radioButtonCustomer = new System.Windows.Forms.RadioButton();
            this.groupBoxPrintFor = new System.Windows.Forms.GroupBox();
            this.groupBoxSupplyOf = new System.Windows.Forms.GroupBox();
            this.radioButtonSupplyOfServices = new System.Windows.Forms.RadioButton();
            this.radioButtonSupplyOfGoods = new System.Windows.Forms.RadioButton();
            this.radioButtonSupplier = new System.Windows.Forms.RadioButton();
            this.radioButtonTransporter = new System.Windows.Forms.RadioButton();
            this.radioButtonOthers = new System.Windows.Forms.RadioButton();
            this.textBoxOthersCopyFor = new System.Windows.Forms.TextBox();
            this.groupBoxPrintFor.SuspendLayout();
            this.groupBoxSupplyOf.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Green;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(10, 185);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 76;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "&Exit";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.Green;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonOk.Location = new System.Drawing.Point(183, 185);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(135, 40);
            this.buttonOk.TabIndex = 77;
            this.buttonOk.TabStop = false;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // radioButtonCustomer
            // 
            this.radioButtonCustomer.AutoSize = true;
            this.radioButtonCustomer.Checked = true;
            this.radioButtonCustomer.Location = new System.Drawing.Point(23, 21);
            this.radioButtonCustomer.Name = "radioButtonCustomer";
            this.radioButtonCustomer.Size = new System.Drawing.Size(89, 21);
            this.radioButtonCustomer.TabIndex = 79;
            this.radioButtonCustomer.TabStop = true;
            this.radioButtonCustomer.Text = "Customer";
            this.radioButtonCustomer.UseVisualStyleBackColor = true;
            this.radioButtonCustomer.CheckedChanged += new System.EventHandler(this.radioButtonCustomer_CheckedChanged);
            // 
            // groupBoxPrintFor
            // 
            this.groupBoxPrintFor.Controls.Add(this.textBoxOthersCopyFor);
            this.groupBoxPrintFor.Controls.Add(this.radioButtonOthers);
            this.groupBoxPrintFor.Controls.Add(this.groupBoxSupplyOf);
            this.groupBoxPrintFor.Controls.Add(this.radioButtonSupplier);
            this.groupBoxPrintFor.Controls.Add(this.radioButtonTransporter);
            this.groupBoxPrintFor.Controls.Add(this.radioButtonCustomer);
            this.groupBoxPrintFor.Location = new System.Drawing.Point(33, 12);
            this.groupBoxPrintFor.Name = "groupBoxPrintFor";
            this.groupBoxPrintFor.Size = new System.Drawing.Size(297, 167);
            this.groupBoxPrintFor.TabIndex = 80;
            this.groupBoxPrintFor.TabStop = false;
            this.groupBoxPrintFor.Text = "Print for";
            // 
            // groupBoxSupplyOf
            // 
            this.groupBoxSupplyOf.Controls.Add(this.radioButtonSupplyOfServices);
            this.groupBoxSupplyOf.Controls.Add(this.radioButtonSupplyOfGoods);
            this.groupBoxSupplyOf.Location = new System.Drawing.Point(150, 48);
            this.groupBoxSupplyOf.Name = "groupBoxSupplyOf";
            this.groupBoxSupplyOf.Size = new System.Drawing.Size(122, 81);
            this.groupBoxSupplyOf.TabIndex = 82;
            this.groupBoxSupplyOf.TabStop = false;
            this.groupBoxSupplyOf.Text = "Supply of";
            // 
            // radioButtonSupplyOfServices
            // 
            this.radioButtonSupplyOfServices.AutoSize = true;
            this.radioButtonSupplyOfServices.Location = new System.Drawing.Point(11, 48);
            this.radioButtonSupplyOfServices.Name = "radioButtonSupplyOfServices";
            this.radioButtonSupplyOfServices.Size = new System.Drawing.Size(83, 21);
            this.radioButtonSupplyOfServices.TabIndex = 84;
            this.radioButtonSupplyOfServices.Text = "Services";
            this.radioButtonSupplyOfServices.UseVisualStyleBackColor = true;
            this.radioButtonSupplyOfServices.CheckedChanged += new System.EventHandler(this.radioButtonSupplyOfServices_CheckedChanged);
            // 
            // radioButtonSupplyOfGoods
            // 
            this.radioButtonSupplyOfGoods.AutoSize = true;
            this.radioButtonSupplyOfGoods.Checked = true;
            this.radioButtonSupplyOfGoods.Location = new System.Drawing.Point(11, 21);
            this.radioButtonSupplyOfGoods.Name = "radioButtonSupplyOfGoods";
            this.radioButtonSupplyOfGoods.Size = new System.Drawing.Size(71, 21);
            this.radioButtonSupplyOfGoods.TabIndex = 83;
            this.radioButtonSupplyOfGoods.TabStop = true;
            this.radioButtonSupplyOfGoods.Text = "Goods";
            this.radioButtonSupplyOfGoods.UseVisualStyleBackColor = true;
            this.radioButtonSupplyOfGoods.CheckedChanged += new System.EventHandler(this.radioButtonSupplyOfGoods_CheckedChanged);
            // 
            // radioButtonSupplier
            // 
            this.radioButtonSupplier.AutoSize = true;
            this.radioButtonSupplier.Location = new System.Drawing.Point(23, 75);
            this.radioButtonSupplier.Name = "radioButtonSupplier";
            this.radioButtonSupplier.Size = new System.Drawing.Size(81, 21);
            this.radioButtonSupplier.TabIndex = 81;
            this.radioButtonSupplier.Text = "Supplier";
            this.radioButtonSupplier.UseVisualStyleBackColor = true;
            this.radioButtonSupplier.CheckedChanged += new System.EventHandler(this.radioButtonSupplier_CheckedChanged);
            // 
            // radioButtonTransporter
            // 
            this.radioButtonTransporter.AutoSize = true;
            this.radioButtonTransporter.Location = new System.Drawing.Point(23, 48);
            this.radioButtonTransporter.Name = "radioButtonTransporter";
            this.radioButtonTransporter.Size = new System.Drawing.Size(104, 21);
            this.radioButtonTransporter.TabIndex = 80;
            this.radioButtonTransporter.Text = "Transporter";
            this.radioButtonTransporter.UseVisualStyleBackColor = true;
            this.radioButtonTransporter.CheckedChanged += new System.EventHandler(this.radioButtonTransporter_CheckedChanged);
            // 
            // radioButtonOthers
            // 
            this.radioButtonOthers.AutoSize = true;
            this.radioButtonOthers.Location = new System.Drawing.Point(23, 102);
            this.radioButtonOthers.Name = "radioButtonOthers";
            this.radioButtonOthers.Size = new System.Drawing.Size(72, 21);
            this.radioButtonOthers.TabIndex = 83;
            this.radioButtonOthers.Text = "Others";
            this.radioButtonOthers.UseVisualStyleBackColor = true;
            this.radioButtonOthers.CheckedChanged += new System.EventHandler(this.radioButtonOthers_CheckedChanged);
            // 
            // textBoxOthersCopyFor
            // 
            this.textBoxOthersCopyFor.BackColor = System.Drawing.Color.Snow;
            this.textBoxOthersCopyFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOthersCopyFor.Location = new System.Drawing.Point(23, 129);
            this.textBoxOthersCopyFor.MaxLength = 15;
            this.textBoxOthersCopyFor.Name = "textBoxOthersCopyFor";
            this.textBoxOthersCopyFor.Size = new System.Drawing.Size(245, 24);
            this.textBoxOthersCopyFor.TabIndex = 84;
            // 
            // InvoiceCopyPopup
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(352, 241);
            this.Controls.Add(this.groupBoxPrintFor);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvoiceCopyPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InvoiceCopyPopup";
            this.groupBoxPrintFor.ResumeLayout(false);
            this.groupBoxPrintFor.PerformLayout();
            this.groupBoxSupplyOf.ResumeLayout(false);
            this.groupBoxSupplyOf.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.RadioButton radioButtonCustomer;
        private System.Windows.Forms.GroupBox groupBoxPrintFor;
        private System.Windows.Forms.RadioButton radioButtonSupplier;
        private System.Windows.Forms.RadioButton radioButtonTransporter;
        private System.Windows.Forms.GroupBox groupBoxSupplyOf;
        private System.Windows.Forms.RadioButton radioButtonSupplyOfServices;
        private System.Windows.Forms.RadioButton radioButtonSupplyOfGoods;
        private System.Windows.Forms.RadioButton radioButtonOthers;
        private System.Windows.Forms.TextBox textBoxOthersCopyFor;
    }
}
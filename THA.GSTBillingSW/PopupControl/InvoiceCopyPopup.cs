using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.PopupControl
{
    public partial class InvoiceCopyPopup : Form
    {
        public string InvoiceCopyType { get; set; }
        public string InvoiceCopyFor { get; set; }

        public InvoiceCopyPopup()
        {
            InitializeComponent();
            groupBoxSupplyOf.Visible = false;
            textBoxOthersCopyFor.Visible = false;
            InvoiceCopyType = "ORIGINAL";
            InvoiceCopyFor = "For Recipient";
            textBoxOthersCopyFor.AutoCompleteCustomSource = GetAutoCompletionData();
            textBoxOthersCopyFor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxOthersCopyFor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        public AutoCompleteStringCollection GetAutoCompletionData()
        {
            string[] strArray = BAL.GenericFucntions.SplitString(ConfigurationManager.AppSettings["OthersInvoiceReportDefaultCopyType"].ToString(), "|");
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
            foreach (string strLiteral in strArray)
            {
                stringCol.Add(Convert.ToString(strLiteral));
            }
            return stringCol; //return the string collection with added records
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (radioButtonOthers.Checked)
            {
                InvoiceCopyType = string.Empty;
                InvoiceCopyFor = textBoxOthersCopyFor.Text.Trim();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSupplier.Checked)
            {
                groupBoxSupplyOf.Visible = true;
                if (radioButtonSupplyOfGoods.Checked)
                {
                    radioButtonSupplyOfGoods_CheckedChanged(sender, e);
                }
                else
                {
                    radioButtonSupplyOfServices_CheckedChanged(sender, e);
                }
            }
            else
            {
                groupBoxSupplyOf.Visible = false;
            }
        }

        private void radioButtonTransporter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTransporter.Checked)
            {
                InvoiceCopyType = "DUPLICATE";
                InvoiceCopyFor = "For Transporter";
            }
        }

        private void radioButtonCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCustomer.Checked)
            {
                InvoiceCopyType = "ORIGINAL";
                InvoiceCopyFor = "For Recipient";
            }
        }

        private void radioButtonSupplyOfGoods_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSupplyOfGoods.Checked)
            {
                InvoiceCopyType = "TRIPLICATE";
                InvoiceCopyFor = "For Supplier";
            }
        }

        private void radioButtonSupplyOfServices_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSupplyOfServices.Checked)
            {
                InvoiceCopyType = "DUPLICATE";
                InvoiceCopyFor = "For Supplier";
            }
        }

        private void radioButtonOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOthers.Checked)
            {
                textBoxOthersCopyFor.Visible = true;
            }
            else
            {
                textBoxOthersCopyFor.Visible = false;
            }
        }
    }
}

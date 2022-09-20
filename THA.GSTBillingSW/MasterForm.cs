using System;
using System.Windows.Forms;
using THA.GSTBillingSW.Master;
using THA.GSTBillingSW.Transaction;

namespace THA.GSTBillingSW
{
    public partial class MasterForm : Form
    {

        public MasterForm()
        {
            InitializeComponent();

            registrationToolStripMenuItem.Visible = Entities.AuthenticationDetail.isLicenseExpired;

            if (Entities.AuthenticationDetail.UserPrivilege == "Administration")
            {
                menuStrip1.Items.Remove(masterToolStripMenuItem);
                menuStrip1.Items.Remove(transactionToolStripMenuItem);
                //masterToolStripMenuItem.Visible = false;
                //transactionToolStripMenuItem.Visible = false;
                //companyToolStripMenuItem.Visible = false;
                //customerToolStripMenuItem.Visible = false;
                //itemToolStripMenuItem.Visible = false;
                //salesInvoiceToolStripMenuItem.Visible = false;
            }
            else if (Entities.AuthenticationDetail.UserPrivilege != "Administration")
            {
                menuStrip1.Items.Remove(settingToolStripMenuItem);
                //settingToolStripMenuItem.Visible = false;
            }
            //this.WindowState = FormWindowState.Normal;
        }


        private void MasterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.MdiParent = this;
            company.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.MdiParent = this;
            customer.Show();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.MdiParent = this;
            item.Show();
        }

        private void unitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unit master = new Unit();
            master.MdiParent = this;
            master.Show();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.UserCreation form = new Setting.UserCreation();
            form.MdiParent = this;
            form.Show();
        }

        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesInvoiceList formTransaction = new SalesInvoiceList();
            formTransaction.MdiParent = this;
            formTransaction.Show();
        }

        private void purchaseInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseInvoiceList formTransaction = new PurchaseInvoiceList();
            formTransaction.MdiParent = this;
            formTransaction.Show();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);

        }

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void quotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuotationList formQuotationList = new QuotationList();
            formQuotationList.MdiParent = this;
            formQuotationList.Show();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.THARegistration formRegister = new Setting.THARegistration();
            formRegister.MdiParent = this;
            formRegister.Show();
        }

        private void backupAndRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.BackupAndRestoreDB form = new Setting.BackupAndRestoreDB();
            form.MdiParent = this;
            form.Show();
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imports.ImportExcel form = new Imports.ImportExcel();
            form.MdiParent = this;
            form.Show();
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master.Group form = new Master.Group();
            form.MdiParent = this;
            form.Show();
        }

        private void stockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master.StockList form = new Master.StockList();
            form.MdiParent = this;
            form.Show();
        }

        private void deliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeliveryNoteList formTransaction = new DeliveryNoteList();
            formTransaction.MdiParent = this;
            formTransaction.Show();
        }

        private void receiptNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptNoteList formTransaction = new ReceiptNoteList();
            formTransaction.MdiParent = this;
            formTransaction.Show();
        }

        private void gSTReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export.ITReturn form = new Export.ITReturn();
            form.MdiParent = this;
            form.Show();
        }

        private void stateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master.State form = new Master.State();
            form.MdiParent = this;
            form.Show();
        }

        private void paymentCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentCollectionList formTransaction = new PaymentCollectionList();
            formTransaction.MdiParent = this;
            formTransaction.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Master
{
    public partial class StockList : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        Boolean isFormLoading;
        Boolean isGridLoaded;
        string groupName = string.Empty;
        public StockList()
        {
            isFormLoading = true;
            InitializeComponent();
            //if (Entities.AuthenticationDetail.UserPrivilege == "Administration" || Entities.AuthenticationDetail.isLicenseExpired)
            //{
            //    buttonEnableEdit.Enabled = true;
            //}
            //else
            //{
            //    buttonEnableEdit.Enabled = false;
            //}
            loadCompany();
            loadGroupUnderList();
            isFormLoading = false;
            DisplayData();
        }

        private void StockList_Load(object sender, EventArgs e)
        {
            dataGridViewColumnWidthSetting();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void comboBoxGroupUnder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void updateStock(Int16 stockID, decimal stockInHand)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Trans_MasterStockList"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StockID", stockID);
                    cmd.Parameters.AddWithValue("@StockInHand", stockInHand);
                    cmd.Parameters.AddWithValue("@mode", "UpdateStockQuantity");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DisplayData()
        {
            if (isFormLoading)
                return;
            setGroupName();
            dataGridViewList.Rows.Clear();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Trans_MasterStockList"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                        cmd.Parameters.AddWithValue("@CompanyID", Convert.ToInt16(comboBoxCompany.SelectedValue));
                    else
                        cmd.Parameters.AddWithValue("@CompanyID", 0);

                    cmd.Parameters.AddWithValue("@GroupName", Convert.ToString("%" + groupName + "%"));
                    cmd.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                    cmd.Parameters.AddWithValue("@mode", "GetStockList");
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            using (dt)
            {
                isGridLoaded = false;
                Int16 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dataGridViewList.Rows.Add();

                    dataGridViewList["StockID", i].Value = (int)dr["ID"];
                    dataGridViewList["ItemId", i].Value = (int)dr["ItemId"];
                    dataGridViewList["GroupName", i].Value = (string)dr["GroupName"];
                    dataGridViewList["ItemName", i].Value = (string)dr["ItemDescription"];
                    dataGridViewList["Size", i].Value = (string)dr["Size"];
                    dataGridViewList["HsnSac", i].Value = (string)dr["HsnSac"];
                    dataGridViewList["UoM", i].Value = (string)dr["UoM"];
                    dataGridViewList["StockInHand", i].Value = (decimal)dr["StockInHand"];
                    dataGridViewList["BUStock", i].Value = (decimal)dr["StockInHand"];

                    i++;
                }
                isGridLoaded = true;
            }
        }

        private void loadGroupUnderList()
        {

            comboBoxGroupUnder.ValueMember = "ID";
            comboBoxGroupUnder.DisplayMember = "GroupName";
            comboBoxGroupUnder.DataSource = masterSelection.GetGroupList();

            comboBoxGroupUnder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxGroupUnder.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBoxGroupUnder.SelectedIndex = 0;
        }

        private void loadCompany()
        {
            comboBoxCompany.ValueMember = "ID";
            comboBoxCompany.DisplayMember = "CompanyName";

            if (ConfigurationManager.AppSettings["CompanywiseStock"].ToString() == "1")
                comboBoxCompany.DataSource = masterSelection.GetCompanyList();
            else
                comboBoxCompany.DataSource = masterSelection.GetCompanyGeneral();

            comboBoxCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxCompany.SelectedIndex = 0;
        }

        private void dataGridViewList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isGridLoaded)
            {
                updateStock(Convert.ToInt16(dataGridViewList["StockID", e.RowIndex].Value), Convert.ToDecimal(dataGridViewList["StockInHand", e.RowIndex].Value));
                //MessageBox.Show(e.RowIndex.ToString());
            }
        }

        private void buttonEnableEdit_Click(object sender, EventArgs e)
        {
            //dataGridViewList.Columns["StockInHand"].HeaderText = "* Stock In Hand";
            dataGridViewList.Columns["StockInHand"].ReadOnly = false;
            //dataGridViewList.Columns["StockInHand"].DefaultCellStyle.ForeColor = Color.Yellow;
            dataGridViewList.Columns["StockInHand"].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Bold);
        }

        private void dataGridViewList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.WhiteSmoke;
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Green;
        }

        private void dataGridViewList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Green;
            dataGridViewList[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.WhiteSmoke;
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Report.ReportViewerGeneral reportViewer = new Report.ReportViewerGeneral("StockList", Convert.ToInt16(comboBoxCompany.SelectedValue), groupName, textBoxSearch.Text.Trim());
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.Show();
        }

        private void buttonReportDetail_Click(object sender, EventArgs e)
        {
            Report.ReportViewerGeneral reportViewer = new Report.ReportViewerGeneral("StockListDetail", Convert.ToInt16(comboBoxCompany.SelectedValue), groupName, textBoxSearch.Text.Trim());
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.Show();
        }

        private void buttonReportChart_Click(object sender, EventArgs e)
        {
            Report.ReportViewerGeneral reportViewer = new Report.ReportViewerGeneral("StockListChart", Convert.ToInt16(comboBoxCompany.SelectedValue), groupName, textBoxSearch.Text.Trim());
            reportViewer.MdiParent = this.MdiParent;
            reportViewer.Show();
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllGroup.Checked)
            {
                comboBoxGroupUnder.Enabled = false;
            }
            else
            {
                comboBoxGroupUnder.Enabled = true;
            }
            DisplayData();
        }

        private void dataGridViewColumnWidthSetting()
        {
            dataGridViewList.Columns["GroupName"].Width = 90;
            dataGridViewList.Columns["ItemName"].Width = 150;
            dataGridViewList.Columns["Size"].Width = 60;
            dataGridViewList.Columns["HsnSac"].Width = 90;
            dataGridViewList.Columns["UoM"].Width = 90;
            dataGridViewList.Columns["StockInHand"].Width = 90;
        }

        private void setGroupName()
        {
            if (checkBoxAllGroup.Checked)
            {
                groupName = string.Empty;
            }
            else
            {
                groupName = comboBoxGroupUnder.Text;
            }
        }
    }
}

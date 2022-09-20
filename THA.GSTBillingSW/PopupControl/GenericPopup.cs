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

namespace THA.GSTBillingSW.PopupControl
{
    public partial class GenericPopup : Form
    {
        int ID = 0;
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;

        public string PopupQueryString { get; set; }

        //public string PopupFilterQueryString { get; set; }
        public int PopupItemId { get; set; }
        public string PopupItemProperty1 { get; set; }
        public string PopupItemProperty2 { get; set; }
        public string PopupItemProperty3 { get; set; }
        public string PopupItemProperty4 { get; set; }
        public string PopupItemProperty5 { get; set; }
        public string PopupItemProperty6 { get; set; }
        public string PopupItemProperty7 { get; set; }
        public string PopupItemProperty8 { get; set; }
        public string PopupItemProperty9 { get; set; }
        public string PopupItemProperty10 { get; set; }
        public string PopupItemProperty11 { get; set; }
        public string PopupItemProperty12 { get; set; }
        public string PopupItemProperty13 { get; set; }
        public GenericPopup()
        {
            InitializeComponent();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (PopupItemId == 0)
            {
                MessageBox.Show("Please Select an Item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
            }
        }

        private void DisplayData()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable dt = new DataTable();

                adapt = new SqlDataAdapter(PopupQueryString, con);
                adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                adapt.Fill(dt);
                using (adapt)
                {
                    dataGridViewList.DataSource = dt;
                }
                dataGridViewList.Columns[0].Visible = false;
                if (this.Text == "Select Item")
                {
                    dataGridViewList.Columns[8].Visible = false;
                    dataGridViewList.Columns[9].Visible = false;
                    dataGridViewList.Columns[10].Visible = false;
                    dataGridViewList.Columns[11].Visible = false;
                    dataGridViewList.Columns[12].Visible = false;
                    dataGridViewList.Columns[13].Visible = false;
                }
                else if (this.Text == "Select Quotation")
                {
                    dataGridViewList.Columns[6].Visible = false;
                    dataGridViewList.Columns[7].Visible = false;
                    dataGridViewList.Columns[8].Visible = false;
                    dataGridViewList.Columns[9].Visible = false;
                    dataGridViewList.Columns[10].Visible = false;
                    dataGridViewList.Columns[11].Visible = false;
                    dataGridViewList.Columns[12].Visible = false;
                    dataGridViewList.Columns[13].Visible = false;
                }
            }
        }

        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                PopupItemId = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells[0].Value);
                PopupItemProperty1 = dataGridViewList.Rows[e.RowIndex].Cells[1].Value.ToString();
                PopupItemProperty2 = dataGridViewList.Rows[e.RowIndex].Cells[2].Value.ToString();
                PopupItemProperty3 = dataGridViewList.Rows[e.RowIndex].Cells[3].Value.ToString();
                PopupItemProperty4 = dataGridViewList.Rows[e.RowIndex].Cells[4].Value.ToString();
                PopupItemProperty5 = dataGridViewList.Rows[e.RowIndex].Cells[5].Value.ToString();
                PopupItemProperty6 = dataGridViewList.Rows[e.RowIndex].Cells[6].Value.ToString();
                PopupItemProperty7 = dataGridViewList.Rows[e.RowIndex].Cells[7].Value.ToString();
                PopupItemProperty8 = dataGridViewList.Rows[e.RowIndex].Cells[8].Value.ToString();
                PopupItemProperty9 = dataGridViewList.Rows[e.RowIndex].Cells[9].Value.ToString();
                PopupItemProperty10 = dataGridViewList.Rows[e.RowIndex].Cells[10].Value.ToString();
                PopupItemProperty11 = dataGridViewList.Rows[e.RowIndex].Cells[11].Value.ToString();
                PopupItemProperty12 = dataGridViewList.Rows[e.RowIndex].Cells[12].Value.ToString();
                PopupItemProperty13 = dataGridViewList.Rows[e.RowIndex].Cells[13].Value.ToString();
            }
            else
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            PopupItemId = 0;
            PopupItemProperty1 = string.Empty;
            PopupItemProperty2 = string.Empty;
            PopupItemProperty3 = string.Empty;
            PopupItemProperty4 = string.Empty;
            PopupItemProperty5 = string.Empty;
            PopupItemProperty6 = string.Empty;
            PopupItemProperty7 = string.Empty;
            PopupItemProperty8 = string.Empty;
            PopupItemProperty9 = string.Empty;
            PopupItemProperty10 = string.Empty;
            PopupItemProperty11 = string.Empty;
            PopupItemProperty12 = string.Empty;
            PopupItemProperty13 = string.Empty;
        }

        private void GenericPopup_Load(object sender, EventArgs e)
        {
            DisplayData();
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

        private void dataGridViewList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewList.CurrentRow != null)
            {
                PopupItemId = Convert.ToInt32(dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[0].Value);
                PopupItemProperty1 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[1].Value.ToString();
                PopupItemProperty2 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[2].Value.ToString();
                PopupItemProperty3 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[3].Value.ToString();
                PopupItemProperty4 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[4].Value.ToString();
                PopupItemProperty5 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[5].Value.ToString();
                PopupItemProperty6 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[6].Value.ToString();
                PopupItemProperty7 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[7].Value.ToString();
                PopupItemProperty8 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[8].Value.ToString();
                PopupItemProperty9 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[9].Value.ToString();
                PopupItemProperty10 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[10].Value.ToString();
                PopupItemProperty11 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[11].Value.ToString();
                PopupItemProperty12 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[12].Value.ToString();
                PopupItemProperty13 = dataGridViewList.Rows[dataGridViewList.CurrentRow.Index].Cells[13].Value.ToString();
            }
        }

        private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (PopupItemId == 0)
                //{
                //    MessageBox.Show("Please Select an Item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    this.DialogResult = DialogResult.None;
                //}
                //buttonOk_Click(sender, e);
                buttonOk.Select();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift == true)
            {
                textBoxSearch.Select();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GenericPopup_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    //if (PopupItemId == 0)
            //    //{
            //    //    MessageBox.Show("Please Select an Item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    //    this.DialogResult = DialogResult.None;
            //    //}
            //    //buttonOk_Click(sender, e);
            //    //buttonOk.Select();
            //    //e.Handled = true;
            //}
            //else if (e.KeyCode == Keys.Tab && e.Shift == true)
            //{
            //    textBoxSearch.Select();
            //}
            if (e.KeyCode == Keys.F2)
            {
                textBoxSearch.Select();
            }
        }
    }
}

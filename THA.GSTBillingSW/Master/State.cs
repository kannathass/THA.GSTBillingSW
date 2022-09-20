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
    public partial class State : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        Boolean isGridLoaded;
        public State()
        {
            InitializeComponent();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                dataGridViewList.ReadOnly = true;
            }
            DisplayData();
        }  

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dataGridViewList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isGridLoaded)
            {
                updateStock(Convert.ToInt16(dataGridViewList["ID", e.RowIndex].Value), Convert.ToBoolean(dataGridViewList["StateStatus", e.RowIndex].Value));
            }
        }

        private void dataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setRowNumber();
        }

        private void updateStock(Int16 ID, Boolean StateStatus)
        {
            SqlCommand cmd;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    cmd = new SqlCommand(@"UPDATE [dbo].[MasterState]
                        SET[StateStatus] = @StateStatus
                        WHERE ID = @ID", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@StateStatus", StateStatus ? "Active" : "Inactive");

                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayData()
        {
            SqlDataAdapter adapt;
            try
            {
                dataGridViewList.Rows.Clear();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    adapt = new SqlDataAdapter(@"SELECT [ID]
                        ,[Code]
                        ,[Name]
                        ,[StateStatus]
                    FROM [MasterState]
                    where (Code like @FilterValue or Name like @FilterValue or StateStatus like @FilterValue) order by Code", con);
                    adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());

                    adapt.Fill(dt);

                    using (dt)
                    {
                        isGridLoaded = false;
                        Int16 i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridViewList.Rows.Add();

                            dataGridViewList["ID", i].Value = (int)dr["ID"];
                            dataGridViewList["Code", i].Value = (string)dr["Code"];
                            dataGridViewList["StateName", i].Value = (string)dr["Name"];
                            if ((string)dr["StateStatus"] == "Active")
                            {
                                dataGridViewList["StateStatus", i].Value = true;
                            }

                            i++;
                        }
                        isGridLoaded = true;
                    }
                }

            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setRowNumber()
        {
            foreach (DataGridViewRow row in dataGridViewList.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }
    }
}

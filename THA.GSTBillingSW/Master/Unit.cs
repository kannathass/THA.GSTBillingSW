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
    public partial class Unit : Form
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        Boolean isGridLoaded;
        public Unit()
        {
            InitializeComponent();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                dataGridViewList.ReadOnly = true;
            }
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

        private void dataGridViewList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isGridLoaded)
            {
                updateStock(Convert.ToInt16(dataGridViewList["ID", e.RowIndex].Value), Convert.ToBoolean(dataGridViewList["UoMStatus", e.RowIndex].Value), Convert.ToString(dataGridViewList["UoM", e.RowIndex].Value), Convert.ToString(dataGridViewList["DisplayName", e.RowIndex].Value));
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            setRowNumber();
        }

        private void updateStock(Int16 ID, Boolean UoMStatus, string UoMCode, string UoMDisplayName)
        {
            SqlCommand cmd;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {

                    cmd = new SqlCommand(@"UPDATE [dbo].[MasterUoM]
                    SET [UoMStatus] = @UoMStatus, UoMDisplayName = @UoMDisplayName
                    WHERE ID = @ID", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@UoMStatus", UoMStatus ? "Active" : "Inactive");
                    cmd.Parameters.AddWithValue("@UoMDisplayName", UoMDisplayName == string.Empty ? UoMCode : UoMDisplayName);

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
                        ,[UoM]
                        ,[UoMStatus]
                        ,isnull(UoMDisplayName,Code) UoMDisplayName
                    FROM [MasterUoM]
                    where (Code like @FilterValue or UoM like @FilterValue or UoMStatus like @FilterValue) order by UoM", con);
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
                            dataGridViewList["UoM", i].Value = (string)dr["UoM"];
                            dataGridViewList["DisplayName", i].Value = (string)dr["UoMDisplayName"];
                            if ((string)dr["UoMStatus"] == "Active")
                            {
                                dataGridViewList["UoMStatus", i].Value = true;
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

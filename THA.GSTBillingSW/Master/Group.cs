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
    public partial class Group : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();

        int ID = 0;
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        TreeNode parentNode = null;
        public Group()
        {
            InitializeComponent();
            loadGroupUnderList();
            displayTreeView();
        }

        private void treeViewGroupAndItem_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ID =  Convert.ToString(e.Node.Name) == string.Empty ? 0 : Convert.ToInt16(e.Node.Name);
            ID = Convert.ToInt16(e.Node.Name);
            //treeViewGroupAndItem.SelectedNode.BackColor = Color.Green;
            getDataById(ID);
        }

        private void saveItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Master_Group_InsertUpdate"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@GroupName", textBoxGroupName.Text.Trim());
                    cmd.Parameters.AddWithValue("@GroupDescription", textBoxDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@GroupIDUnder", comboBoxGroupUnder.SelectedValue);
                    cmd.Parameters.AddWithValue("@Location", textBoxLocation.Text.Trim());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void displayTreeView()
        {
            populateParentNode();
        }

        private void getDataById(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    cmd = new SqlCommand(@"SELECT GroupName, GroupDescription, GroupIDUnder, Location
                    FROM [dbo].[MasterGroup] 
                    Where ID = " + ID, con);

                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                textBoxGroupName.Text = Convert.ToString(reader["GroupName"]);
                                textBoxDescription.Text = Convert.ToString(reader["GroupDescription"]);
                                comboBoxGroupUnder.SelectedValue = Convert.ToInt16(reader["GroupIDUnder"]);
                                textBoxLocation.Text = Convert.ToString(reader["Location"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable getDataByGroup(int parentId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    adapt = new SqlDataAdapter(@"SELECT [ID], [GroupName]
                    FROM [dbo].[MasterGroup] 
                    Where Del_State = 0 and GroupIDUnder = @parentId and (GroupName like @FilterValue)", con);
                    adapt.SelectCommand.Parameters.AddWithValue("@ParentId", parentId);
                    adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                    adapt.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void deleteItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand("Update MasterGroup set Del_State=1, Del_Date=GetDate() Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ClearData()
        {
            ID = 0;
            textBoxGroupName.Text = string.Empty;
            textBoxDescription.Text = string.Empty;
            //comboBoxGroupUnder.SelectedIndex = 0;
            textBoxLocation.Text = string.Empty;
            textBoxSearch.Clear();
            textBoxGroupName.Focus();
        }

        private void populateParentNode()
        {
            treeViewGroupAndItem.SelectedNode = null;
            treeViewGroupAndItem.Nodes.Clear();

            DataTable dtParent = getDataByGroup(0);
            foreach (DataRow dr in dtParent.Rows)
            {
                parentNode = treeViewGroupAndItem.Nodes.Add(dr["ID"].ToString(), dr["GroupName"].ToString());

                PopulateTreeView(Convert.ToInt32(dr["ID"].ToString()), parentNode);
            }
            treeViewGroupAndItem.LineColor = Color.Red;
            treeViewGroupAndItem.ExpandAll();
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            DataTable dtChild = getDataByGroup(parentId);
            TreeNode childNode;
            foreach (DataRow dr in dtChild.Rows)
            {
                if (parentNode == null)
                    childNode = treeViewGroupAndItem.Nodes.Add(dr["ID"].ToString(), dr["GroupName"].ToString());
                else
                    childNode = parentNode.Nodes.Add(dr["ID"].ToString(), dr["GroupName"].ToString());
                PopulateTreeView(Convert.ToInt32(dr["ID"].ToString()), childNode);
            }
        }

        private void loadGroupUnderList()
        {
            comboBoxGroupUnder.ValueMember = "ID";
            comboBoxGroupUnder.DisplayMember = "GroupName";
            comboBoxGroupUnder.DataSource = masterSelection.GetGroupList();

            //comboBoxGroupUnder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //comboBoxGroupUnder.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBoxGroupUnder.SelectedIndex = 0;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxGroupName.Text.Trim() != "")
            {
                if (textBoxGroupName.Text.Trim().ToUpper() != comboBoxGroupUnder.Text.Trim().ToUpper())
                {
                    saveItem();
                    MessageBox.Show("Record Saved Successfully", "Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearData();
                    loadGroupUnderList();
                    displayTreeView();
                }
                else
                {
                    MessageBox.Show("Group name and Group under can not be same", "Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please provide Details!", "Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxGroupName.Text.Trim() != "")
                {
                    if (BAL.GenericValidation.GetGroupTransactionCount(Convert.ToString(ID)) == 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            deleteItem();
                            MessageBox.Show("Record Deleted Successfully", "Group", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            displayTreeView();
                            ClearData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transactions are exist for the selected Group. You can not delete it.", "Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Details!", "Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //if (textBoxSearch.Text.Trim() != string.Empty)
            FindByText();
        }

        private void FindByText()
        {
            TreeNodeCollection nodes = treeViewGroupAndItem.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n);
            }
        }

        private void FindRecursive(TreeNode treeNode)
        {
            if (treeNode.Text.ToUpper().Contains(this.textBoxSearch.Text.ToUpper()) && textBoxSearch.Text.Trim() != string.Empty)
            {
                treeNode.BackColor = Color.Green;
                treeNode.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                treeNode.BackColor = Control.DefaultBackColor;
                treeNode.ForeColor = Control.DefaultForeColor;
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // if the text properties match, color the item
                if (tn.Text.ToUpper().Contains(this.textBoxSearch.Text.ToUpper()) && textBoxSearch.Text.Trim() != string.Empty)
                {
                    tn.BackColor = Color.Green;
                    tn.ForeColor = Color.WhiteSmoke;
                }
                else
                {
                    tn.BackColor = Control.DefaultBackColor;
                    tn.ForeColor = Control.DefaultForeColor;
                }
                FindRecursive(tn);
            }
        }
    }
}

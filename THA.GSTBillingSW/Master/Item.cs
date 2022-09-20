using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.Master
{
    public partial class Item : Form
    {
        BAL.MastersSelection masterSelection = new BAL.MastersSelection();
        int ID = 0;
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        TreeNode parentNode = null;
        public Item()
        {
            InitializeComponent();
            loadGroupUnderList();
            //DisplayData();
            populateParentNode();
            if (Entities.AuthenticationDetail.UserPrivilege == "Read" || Entities.AuthenticationDetail.isLicenseExpired)
            {
                buttonSave.Enabled = false;
                buttonDelete.Enabled = false;
            }
            if (ConfigurationManager.AppSettings["IsTaxIncluded"] == "1")
            {
                checkBoxIsTaxIncluded.Checked = true;
            }
            comboBoxStatus.SelectedIndex = 0;
            loadUoMList();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxItemDescription.Text.Trim() != string.Empty)
            {
                saveItem();
                MessageBox.Show("Record Saved Successfully", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
                populateParentNode();
            }
            else
            {
                MessageBox.Show("Please provide Details!", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ImagefileDialog = new OpenFileDialog();
            ImagefileDialog.Title = "Select Item Image";
            ImagefileDialog.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (ImagefileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxItem.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxItem.Image = Image.FromFile(ImagefileDialog.FileName);
            }
            //ImagefileDialog = null;
        }

        private void buttonClearImage_Click(object sender, EventArgs e)
        {
            pictureBoxItem.Image = null;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
            treeViewGroupAndItem.SelectedNode = null;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxItemDescription.Text.Trim() != "")
                {
                    if (BAL.GenericValidation.GetItemTransactionCount(Convert.ToString(ID)) == 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            deleteItem();
                            MessageBox.Show("Record Deleted Successfully", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearData();
                            populateParentNode();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Transactions are exist for the selected Item. You can not delete it.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Details!", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                cmd = new SqlCommand("Update MasterItem set Del_State=1, Del_Date=GetDate() Where ID=@ID ", con);
                cmd.Parameters.AddWithValue("@ID", ID);
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void saveItem()
        {
            byte[] itemImage = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (pictureBoxItem.Image != null)
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBoxItem.Image.Save(ms, pictureBoxItem.Image.RawFormat);
                    itemImage = ms.ToArray();
                }
                using (SqlCommand cmd = new SqlCommand("Master_Item_InsertUpdate"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);

                    cmd.Parameters.AddWithValue("@ItemDescription", Convert.ToString(textBoxItemDescription.Text).Trim());
                    cmd.Parameters.AddWithValue("@Size", Convert.ToString(textBoxSize.Text).Trim());
                    cmd.Parameters.AddWithValue("@UoM", Convert.ToString(comboBoxUoM.Text).Trim());
                    cmd.Parameters.AddWithValue("@UoMDisplayName", Convert.ToString(textBoxUoMDisplayName.Text).Trim());
                    cmd.Parameters.AddWithValue("@ItemType", Convert.ToString(comboBoxItemType.Text).Trim());
                    cmd.Parameters.AddWithValue("@HsnSac", Convert.ToString(textBoxHsnSacCode.Text).Trim());
                    cmd.Parameters.AddWithValue("@ItemSKUcode", Convert.ToString(textBoxItemSKUCode.Text).Trim());
                    cmd.Parameters.AddWithValue("@SellingPrice", Convert.ToDecimal(String.IsNullOrEmpty(textBoxSellingPrice.Text) ? "0" : textBoxSellingPrice.Text));
                    cmd.Parameters.AddWithValue("@PurchasePrice", Convert.ToDecimal(String.IsNullOrEmpty(textBoxPurchasePrice.Text) ? "0" : textBoxPurchasePrice.Text));
                    cmd.Parameters.AddWithValue("@DiscountPercent", Convert.ToDecimal(String.IsNullOrEmpty(textBoxDiscount.Text) ? "0" : textBoxDiscount.Text));
                    cmd.Parameters.AddWithValue("@TaxRatePercent", Convert.ToDecimal(String.IsNullOrEmpty(comboBoxTaxRate.Text) ? "0" : comboBoxTaxRate.Text));
                    cmd.Parameters.AddWithValue("@ItemNotes", Convert.ToString(textBoxItemNotes.Text).Trim());
                    cmd.Parameters.AddWithValue("@GroupIDUnder", comboBoxGroupUnder.SelectedValue);
                    cmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(String.IsNullOrEmpty(textBoxDiscountAmount.Text) ? "0" : textBoxDiscountAmount.Text));
                    cmd.Parameters.AddWithValue("@IsTaxIncluded", checkBoxIsTaxIncluded.Checked);
                    cmd.Parameters.AddWithValue("@BrandName", Convert.ToString(textBoxBrandName.Text).Trim());
                    cmd.Parameters.AddWithValue("@Weight", Convert.ToString(textBoxWeight.Text).Trim());
                    cmd.Parameters.AddWithValue("@ItemStatus", Convert.ToString(comboBoxStatus.Text).Trim());

                    SqlParameter imageParameter = new SqlParameter("@ItemImage", SqlDbType.Image);
                    if ((itemImage == null))
                    {
                        imageParameter.Value = DBNull.Value;
                    }
                    else
                    {
                        imageParameter.Value = itemImage;
                    }

                    cmd.Parameters.Add(imageParameter);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void ClearData()
        {
            ID = 0;
            textBoxItemDescription.Text = "";
            textBoxSize.Text = "";
            textBoxUoMDisplayName.Text = string.Empty;
            comboBoxUoM.SelectedIndex = 0;
            comboBoxItemType.SelectedIndex = 0;
            textBoxHsnSacCode.Text = "";
            textBoxItemSKUCode.Text = "";
            textBoxSellingPrice.Text = "";
            textBoxPurchasePrice.Text = "";
            textBoxDiscount.Text = "";
            comboBoxTaxRate.SelectedIndex = 0;
            textBoxItemNotes.Text = "";
            textBoxSearch.Text = "";
            comboBoxGroupUnder.SelectedIndex = 0;
            textBoxDiscountAmount.Clear();
            checkBoxIsTaxIncluded.Checked = false;
            textBoxBrandName.Clear();
            textBoxWeight.Clear();
            pictureBoxItem.Image = null;
            comboBoxStatus.SelectedIndex = 0;
            textBoxItemDescription.Select();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //FindByText();
            populateParentNode();
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

        private void treeViewGroupAndItem_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ID =  Convert.ToString(e.Node.Name) == string.Empty ? 0 : Convert.ToInt16(e.Node.Name);
            ID = Convert.ToInt16(e.Node.Name);

            var splitStr = e.Node.Text.Split('|');

            if (splitStr.Length == 3)
            {
                getDataById(ID);
            }
            else
            {
                ClearData();
            }
        }


        private void getDataById(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    cmd = new SqlCommand(@"select ID,[ItemDescription],[Size],[UoM]
                    ,UoMDisplayName,[ItemType],[HsnSac]
                    ,[ItemSKUcode],[SellingPrice],[PurchasePrice]
                    ,[DiscountPercent],[TaxRatePercent],[ItemNotes]
                    ,[GroupIDUnder],[DiscountAmount],[IsTaxIncluded]
                    ,[BrandName],[Weight],ItemStatus,isnull(ItemImage,'') ItemImage
                         from MasterItem Where Del_State = 0 and ID = " + ID, con);
                    using (cmd)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                ID = Convert.ToInt32(reader["ID"]);
                                textBoxItemDescription.Text = Convert.ToString(reader["ItemDescription"]);
                                textBoxSize.Text = Convert.ToString(reader["Size"]);
                                comboBoxUoM.Text = Convert.ToString(reader["UoM"]);
                                textBoxUoMDisplayName.Text = Convert.ToString(reader["UoMDisplayName"]);
                                comboBoxItemType.Text = Convert.ToString(reader["ItemType"]);
                                textBoxHsnSacCode.Text = Convert.ToString(reader["HsnSac"]);
                                textBoxItemSKUCode.Text = Convert.ToString(reader["ItemSKUcode"]);
                                textBoxSellingPrice.Text = Convert.ToString(reader["SellingPrice"]);
                                textBoxPurchasePrice.Text = Convert.ToString(reader["PurchasePrice"]);
                                textBoxDiscount.Text = Convert.ToString(reader["DiscountPercent"]);
                                comboBoxTaxRate.Text = Convert.ToString(reader["TaxRatePercent"]);
                                textBoxItemNotes.Text = Convert.ToString(reader["ItemNotes"]);
                                comboBoxGroupUnder.SelectedValue = Convert.ToInt32(reader["GroupIDUnder"]);
                                textBoxDiscountAmount.Text = Convert.ToString(reader["DiscountAmount"]);
                                checkBoxIsTaxIncluded.Checked = Convert.ToBoolean(reader["IsTaxIncluded"]);
                                textBoxBrandName.Text = Convert.ToString(reader["BrandName"]);
                                textBoxWeight.Text = Convert.ToString(reader["Weight"]);
                                comboBoxStatus.Text = Convert.ToString(reader["ItemStatus"]);
                                byte[] img = (byte[])reader["ItemImage"];
                                MemoryStream ms = new MemoryStream(img);
                                if (ms.Length != 0)
                                {
                                    pictureBoxItem.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pictureBoxItem.Image = Image.FromStream(ms);
                                }
                                else
                                    pictureBoxItem.Image = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
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
                    //adapt = new SqlDataAdapter(@"
                    //select 'Group' Category,ID,GroupName [Detail] 
                    //from MasterGroup where Del_State = 0 and GroupIDUnder=@parentId and (GroupName like @FilterValue)
                    //UNION
                    //select 'Item' Category, ID,concat(ItemDescription, ' | ',Size, ' | ', UoM) [Detail]
                    //from MasterItem where Del_State=0 and GroupIDUnder= @parentId and (ItemDescription like @FilterValue)", con);

                    //adapt = new SqlDataAdapter(@"
                    //select 'Group' Category,ID,GroupName [Detail] 
                    //from MasterGroup where Del_State = 0 and GroupIDUnder=@parentId and (GroupName like @FilterValue)
                    //UNION
                    //select 'Item' Category, ID,ItemDescription + ' | '+ Size + ' | ' + UoM [Detail]
                    //from MasterItem where Del_State=0 and GroupIDUnder= @parentId and (ItemDescription like @FilterValue)", con);
                    //adapt.SelectCommand.Parameters.AddWithValue("@ParentId", parentId);
                    //adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());

                    adapt = new SqlDataAdapter(@"
                    select 'Group' Category,ID,GroupName [Detail] 
                    from MasterGroup where Del_State = 0 and GroupIDUnder=@parentId
                    UNION
                    select 'Item' Category, ID,ItemDescription + ' | '+ Size + ' | ' + UoM [Detail]
                    from MasterItem where Del_State=0 and GroupIDUnder= @parentId and (ItemDescription like @FilterValue)", con);
                    adapt.SelectCommand.Parameters.AddWithValue("@ParentId", parentId);
                    adapt.SelectCommand.Parameters.AddWithValue("@FilterValue", Convert.ToString("%" + textBoxSearch.Text + "%").Trim());
                    adapt.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                BAL.LogHelper.WriteDebugLog(ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void populateParentNode()
        {
            treeViewGroupAndItem.SelectedNode = null;
            treeViewGroupAndItem.Nodes.Clear();

            DataTable dtParent = getDataByGroup(0);
            foreach (DataRow dr in dtParent.Rows)
            {
                parentNode = treeViewGroupAndItem.Nodes.Add(dr["ID"].ToString(), dr["Detail"].ToString());

                if (dr["Category"].ToString() == "Group")
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
                    childNode = treeViewGroupAndItem.Nodes.Add(dr["ID"].ToString(), dr["Detail"].ToString());
                else
                    childNode = parentNode.Nodes.Add(dr["ID"].ToString(), dr["Detail"].ToString());

                if (dr["Category"].ToString() == "Group")
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

        private void loadUoMList()
        {
            comboBoxUoM.ValueMember = "ID";
            comboBoxUoM.DisplayMember = "UoM";
            comboBoxUoM.DataSource = masterSelection.GetUoMList();

            comboBoxUoM.SelectedIndex = 0;
        }

        private void comboBoxUoM_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxUoMDisplayName.Text = BAL.GenericValidation.GetUoMDisplayName(comboBoxUoM.Text.Trim());
        }
    }
}

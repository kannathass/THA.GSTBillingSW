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

namespace THA.GSTBillingSW.Setting
{
    public partial class UserCreation : Form
    {
        int ID = 0;
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public UserCreation()
        {
            InitializeComponent();
            DisplayData();
        }

        private void UserCreation_Load(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxUserName.Text.Trim() != "" && textBoxPassword.Text.Trim() != "")
            {
                saveItem();
                MessageBox.Show("Record Saved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Enter Details!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void saveItem()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                if (ID == 0)
                {
                    cmd = new SqlCommand("INSERT INTO [dbo].[SettingUser] " +
           " ([UserName],[Password],[PasswordHint],[Role] " +
           " ,[Privilege],[IsLoggedIn],[IsUserActive]) " +
     " VALUES " +
           " (@UserName, @Password, @PasswordHint, @Role " +
           ", @Privilege, @IsLoggedIn, @IsUserActive)", con);
                }
                else
                {
                    cmd = new SqlCommand("UPDATE [SettingUser] " +
   " SET[UserName] = @UserName " +
      " ,[Password] = @Password " +
      " ,[PasswordHint] = @PasswordHint " +
      " ,[Role] = @Role " +
      " ,[Privilege] = @Privilege " +
      //" ,[IsLoggedIn] = false " +
      " ,[IsUserActive] = @IsUserActive " +
 " WHERE ID = @ID", con);
                }

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.Parameters.AddWithValue("@UserName", Convert.ToString(textBoxUserName.Text).Trim());
                cmd.Parameters.AddWithValue("@Password", BAL.UserAuthentication.EncodePasswordToBase64(Convert.ToString(textBoxPassword.Text).Trim()));
                cmd.Parameters.AddWithValue("@PasswordHint", Convert.ToString(textBoxPasswordHint.Text).Trim());
                cmd.Parameters.AddWithValue("@Role", Convert.ToString(comboBoxRole.Text).Trim());
                cmd.Parameters.AddWithValue("@Privilege", Convert.ToString(comboBoxPrivilege.Text).Trim());
                //cmd.Parameters.AddWithValue("@LastLoggedIn", Convert.ToString(textBoxItemSKUCode.Text).Trim());
                cmd.Parameters.AddWithValue("@IsLoggedIn", false);
                if (checkBoxIsUserActive.Checked)
                {
                    cmd.Parameters.AddWithValue("@IsUserActive", true);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IsUserActive", false);
                }
                using (cmd)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DisplayData()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT [ID],[UserName] [User Name],[Password],[PasswordHint] " +
      " ,[Role] [User Role],[Privilege] [User Privilege],[IsUserActive] [Active] " +
  " FROM [SettingUser] ", con);
                adapt.Fill(dt);
                using (adapt)
                {
                    dataGridViewList.DataSource = dt;
                }
                dataGridViewList.Columns["ID"].Visible = false;
                dataGridViewList.Columns["Password"].Visible = false;
                dataGridViewList.Columns["PasswordHint"].Visible = false;
                //dataGridViewList.Columns["Privilege"].Visible = false;
            }
        }

        private void ClearData()
        {
            ID = 0;
            textBoxUserName.Text = "";
            textBoxPassword.Text = "";
            textBoxPasswordHint.Text = "";
            comboBoxRole.SelectedIndex = 0;
            comboBoxPrivilege.SelectedIndex = 0;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ID = Convert.ToInt32(dataGridViewList.Rows[e.RowIndex].Cells[0].Value);
                textBoxUserName.Text = dataGridViewList.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxPassword.Text = BAL.UserAuthentication.DecodeFrom64(dataGridViewList.Rows[e.RowIndex].Cells[2].Value.ToString());
                textBoxPasswordHint.Text = dataGridViewList.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBoxRole.Text = dataGridViewList.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBoxPrivilege.Text = dataGridViewList.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (Convert.ToBoolean(dataGridViewList.Rows[e.RowIndex].Cells[6].Value = true))
                {
                    checkBoxIsUserActive.Checked = true;
                }
                else
                {
                    checkBoxIsUserActive.Checked = false;
                }
            }
            else
            {
                ClearData();
            }
        }



    }
}

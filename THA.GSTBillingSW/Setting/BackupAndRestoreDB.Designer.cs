namespace THA.GSTBillingSW.Setting
{
    partial class BackupAndRestoreDB
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
            this.groupBoxBackupDB = new System.Windows.Forms.GroupBox();
            this.buttonBackupToLocation = new System.Windows.Forms.Button();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.textBoxBackupToLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxRestoreDB = new System.Windows.Forms.GroupBox();
            this.buttonRestoreFromLocation = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.textBoxRestoreFromLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonUpdateDatabase = new System.Windows.Forms.Button();
            this.groupBoxBackupDB.SuspendLayout();
            this.groupBoxRestoreDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBackupDB
            // 
            this.groupBoxBackupDB.Controls.Add(this.buttonBackupToLocation);
            this.groupBoxBackupDB.Controls.Add(this.buttonBackup);
            this.groupBoxBackupDB.Controls.Add(this.textBoxBackupToLocation);
            this.groupBoxBackupDB.Controls.Add(this.label1);
            this.groupBoxBackupDB.Location = new System.Drawing.Point(48, 66);
            this.groupBoxBackupDB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxBackupDB.Name = "groupBoxBackupDB";
            this.groupBoxBackupDB.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxBackupDB.Size = new System.Drawing.Size(785, 216);
            this.groupBoxBackupDB.TabIndex = 0;
            this.groupBoxBackupDB.TabStop = false;
            this.groupBoxBackupDB.Text = "Backup Database";
            // 
            // buttonBackupToLocation
            // 
            this.buttonBackupToLocation.BackColor = System.Drawing.Color.Silver;
            this.buttonBackupToLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBackupToLocation.ForeColor = System.Drawing.Color.Black;
            this.buttonBackupToLocation.Location = new System.Drawing.Point(609, 70);
            this.buttonBackupToLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBackupToLocation.Name = "buttonBackupToLocation";
            this.buttonBackupToLocation.Size = new System.Drawing.Size(152, 36);
            this.buttonBackupToLocation.TabIndex = 1;
            this.buttonBackupToLocation.Text = "Browse";
            this.buttonBackupToLocation.UseVisualStyleBackColor = false;
            this.buttonBackupToLocation.Click += new System.EventHandler(this.buttonBackupToLocation_Click);
            // 
            // buttonBackup
            // 
            this.buttonBackup.BackColor = System.Drawing.Color.Green;
            this.buttonBackup.Enabled = false;
            this.buttonBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBackup.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonBackup.Location = new System.Drawing.Point(609, 121);
            this.buttonBackup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.Size = new System.Drawing.Size(152, 50);
            this.buttonBackup.TabIndex = 2;
            this.buttonBackup.Text = "&Backup";
            this.buttonBackup.UseVisualStyleBackColor = false;
            this.buttonBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // textBoxBackupToLocation
            // 
            this.textBoxBackupToLocation.BackColor = System.Drawing.Color.Snow;
            this.textBoxBackupToLocation.Enabled = false;
            this.textBoxBackupToLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBackupToLocation.Location = new System.Drawing.Point(18, 76);
            this.textBoxBackupToLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBackupToLocation.MaxLength = 200;
            this.textBoxBackupToLocation.Name = "textBoxBackupToLocation";
            this.textBoxBackupToLocation.Size = new System.Drawing.Size(562, 28);
            this.textBoxBackupToLocation.TabIndex = 1;
            this.textBoxBackupToLocation.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Location";
            // 
            // groupBoxRestoreDB
            // 
            this.groupBoxRestoreDB.Controls.Add(this.buttonRestoreFromLocation);
            this.groupBoxRestoreDB.Controls.Add(this.buttonRestore);
            this.groupBoxRestoreDB.Controls.Add(this.textBoxRestoreFromLocation);
            this.groupBoxRestoreDB.Controls.Add(this.label2);
            this.groupBoxRestoreDB.Location = new System.Drawing.Point(48, 330);
            this.groupBoxRestoreDB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxRestoreDB.Name = "groupBoxRestoreDB";
            this.groupBoxRestoreDB.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxRestoreDB.Size = new System.Drawing.Size(785, 216);
            this.groupBoxRestoreDB.TabIndex = 3;
            this.groupBoxRestoreDB.TabStop = false;
            this.groupBoxRestoreDB.Text = "Resore Database";
            // 
            // buttonRestoreFromLocation
            // 
            this.buttonRestoreFromLocation.BackColor = System.Drawing.Color.Silver;
            this.buttonRestoreFromLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRestoreFromLocation.ForeColor = System.Drawing.Color.Black;
            this.buttonRestoreFromLocation.Location = new System.Drawing.Point(609, 70);
            this.buttonRestoreFromLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRestoreFromLocation.Name = "buttonRestoreFromLocation";
            this.buttonRestoreFromLocation.Size = new System.Drawing.Size(152, 36);
            this.buttonRestoreFromLocation.TabIndex = 4;
            this.buttonRestoreFromLocation.Text = "Browse";
            this.buttonRestoreFromLocation.UseVisualStyleBackColor = false;
            this.buttonRestoreFromLocation.Click += new System.EventHandler(this.buttonRestoreFromLocation_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.BackColor = System.Drawing.Color.Green;
            this.buttonRestore.Enabled = false;
            this.buttonRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRestore.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonRestore.Location = new System.Drawing.Point(609, 121);
            this.buttonRestore.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(152, 50);
            this.buttonRestore.TabIndex = 5;
            this.buttonRestore.Text = "&Restore";
            this.buttonRestore.UseVisualStyleBackColor = false;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // textBoxRestoreFromLocation
            // 
            this.textBoxRestoreFromLocation.BackColor = System.Drawing.Color.Snow;
            this.textBoxRestoreFromLocation.Enabled = false;
            this.textBoxRestoreFromLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRestoreFromLocation.Location = new System.Drawing.Point(18, 76);
            this.textBoxRestoreFromLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRestoreFromLocation.MaxLength = 200;
            this.textBoxRestoreFromLocation.Name = "textBoxRestoreFromLocation";
            this.textBoxRestoreFromLocation.Size = new System.Drawing.Size(562, 28);
            this.textBoxRestoreFromLocation.TabIndex = 1;
            this.textBoxRestoreFromLocation.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location";
            // 
            // buttonUpdateDatabase
            // 
            this.buttonUpdateDatabase.BackColor = System.Drawing.Color.Green;
            this.buttonUpdateDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdateDatabase.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonUpdateDatabase.Location = new System.Drawing.Point(433, 551);
            this.buttonUpdateDatabase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonUpdateDatabase.Name = "buttonUpdateDatabase";
            this.buttonUpdateDatabase.Size = new System.Drawing.Size(376, 50);
            this.buttonUpdateDatabase.TabIndex = 6;
            this.buttonUpdateDatabase.Text = "Update Database";
            this.buttonUpdateDatabase.UseVisualStyleBackColor = false;
            this.buttonUpdateDatabase.Click += new System.EventHandler(this.buttonUpdateDatabase_Click);
            // 
            // BackupAndRestoreDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 614);
            this.Controls.Add(this.buttonUpdateDatabase);
            this.Controls.Add(this.groupBoxRestoreDB);
            this.Controls.Add(this.groupBoxBackupDB);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BackupAndRestoreDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup & Restore";
            this.groupBoxBackupDB.ResumeLayout(false);
            this.groupBoxBackupDB.PerformLayout();
            this.groupBoxRestoreDB.ResumeLayout(false);
            this.groupBoxRestoreDB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBackupDB;
        private System.Windows.Forms.TextBox textBoxBackupToLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBackup;
        private System.Windows.Forms.Button buttonBackupToLocation;
        private System.Windows.Forms.GroupBox groupBoxRestoreDB;
        private System.Windows.Forms.Button buttonRestoreFromLocation;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.TextBox textBoxRestoreFromLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonUpdateDatabase;
    }
}
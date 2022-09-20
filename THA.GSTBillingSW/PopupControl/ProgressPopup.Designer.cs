namespace THA.GSTBillingSW.PopupControl
{
    partial class ProgressPopup
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
            this.progressBarTHA = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.LabelProcessing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarTHA
            // 
            this.progressBarTHA.Location = new System.Drawing.Point(12, 63);
            this.progressBarTHA.Name = "progressBarTHA";
            this.progressBarTHA.Size = new System.Drawing.Size(540, 23);
            this.progressBarTHA.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Green;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Location = new System.Drawing.Point(417, 103);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(135, 40);
            this.buttonCancel.TabIndex = 159;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LabelProcessing
            // 
            this.LabelProcessing.AutoSize = true;
            this.LabelProcessing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelProcessing.Location = new System.Drawing.Point(229, 42);
            this.LabelProcessing.Name = "LabelProcessing";
            this.LabelProcessing.Size = new System.Drawing.Size(175, 18);
            this.LabelProcessing.TabIndex = 160;
            this.LabelProcessing.Text = "In progress, please wait...";
            // 
            // ProgressPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 153);
            this.ControlBox = false;
            this.Controls.Add(this.LabelProcessing);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBarTHA);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processing....";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarTHA;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label LabelProcessing;
    }
}
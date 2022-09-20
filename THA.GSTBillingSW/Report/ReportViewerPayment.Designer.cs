namespace THA.GSTBillingSW.Report
{
    partial class ReportViewerPayment
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
            this.crystalReportViewerPayment = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewerPayment
            // 
            this.crystalReportViewerPayment.ActiveViewIndex = -1;
            this.crystalReportViewerPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerPayment.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerPayment.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerPayment.Name = "crystalReportViewerPayment";
            this.crystalReportViewerPayment.ShowLogo = false;
            this.crystalReportViewerPayment.Size = new System.Drawing.Size(1704, 1037);
            this.crystalReportViewerPayment.TabIndex = 0;
            // 
            // ReportViewerPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1704, 1037);
            this.Controls.Add(this.crystalReportViewerPayment);
            this.Name = "ReportViewerPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.ReportViewerPayment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerPayment;
    }
}
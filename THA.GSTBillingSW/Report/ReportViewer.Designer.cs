namespace THA.GSTBillingSW.Report
{
    partial class ReportViewer
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
            this.crystalReportViewerBilling = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewerBilling
            // 
            this.crystalReportViewerBilling.ActiveViewIndex = -1;
            this.crystalReportViewerBilling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerBilling.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerBilling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerBilling.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerBilling.Name = "crystalReportViewerBilling";
            this.crystalReportViewerBilling.ShowLogo = false;
            this.crystalReportViewerBilling.Size = new System.Drawing.Size(1704, 1037);
            this.crystalReportViewerBilling.TabIndex = 0;
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1704, 1037);
            this.Controls.Add(this.crystalReportViewerBilling);
            this.Name = "ReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.ReportViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerBilling;
    }
}
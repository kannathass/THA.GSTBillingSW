namespace THA.GSTBillingSW.Report
{
    partial class ReportViewerGeneral
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
            this.crystalReportViewerGeneral = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewerGeneral
            // 
            this.crystalReportViewerGeneral.ActiveViewIndex = -1;
            this.crystalReportViewerGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerGeneral.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerGeneral.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerGeneral.Name = "crystalReportViewerGeneral";
            this.crystalReportViewerGeneral.ShowLogo = false;
            this.crystalReportViewerGeneral.Size = new System.Drawing.Size(1704, 1037);
            this.crystalReportViewerGeneral.TabIndex = 0;
            // 
            // ReportViewerGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1704, 1037);
            this.Controls.Add(this.crystalReportViewerGeneral);
            this.Name = "ReportViewerGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.ReportViewerGeneral_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerGeneral;
    }
}
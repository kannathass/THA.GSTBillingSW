using System;
using System.Windows.Forms;

namespace THA.GSTBillingSW
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!BAL.DeployDB.checkDatabaseExist())
            {
                //Generates "THAgstBilling" Database with all the objects
                string updateStatus = BAL.DeployDB.RunSqlScript("\\Scripts\\scriptTHAgstBilling.sql");
                if (updateStatus == "Completed")
                    Application.Run(new THALogin());
                else if (updateStatus == "CompletedWithError")
                    MessageBox.Show("DB creation Completed with Error.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Unable to create Database.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Application.Run(new THALogin());


        }
    }
}

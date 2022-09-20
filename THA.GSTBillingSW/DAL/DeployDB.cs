using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using THA.GSTBillingSW.BAL;

namespace THA.GSTBillingSW.DAL
{
    static class DeployDB
    {
        public static bool checkDatabaseExist()
        {
            try
            {
                //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
                //{
                //    con.Open();
                //}
                //return true;
                return DetectSqlService.CheckAndRunSqlService();
            }
            catch (Exception ex)
            {
                //loggingMechanism
                LogHelper.WriteDebugLog(ex.ToString());
                return false;
            }
        }

        public static string RunSqlScript(string scriptLocation)
        {
            List<string> cmds = new List<string>();
            string completedStatus = "Completed";
            try
            {
                if (File.Exists(Application.StartupPath + scriptLocation))
                {
                    //TextReader tr = new StreamReader(Application.StartupPath + "\\Scripts\\scriptTHAgstBilling.sql");
                    TextReader tr = new StreamReader(Application.StartupPath + scriptLocation);
                    string line = string.Empty;
                    string cmd = string.Empty;
                    while ((line = tr.ReadLine()) != null)
                    {
                        if (line.Trim().ToUpper() == "GO")
                        {
                            cmds.Add(cmd);
                            cmd = string.Empty;
                        }
                        else
                        {
                            cmd += line + "\r\n";
                        }
                    }
                    if (cmd.Length > 0)
                    {
                        cmds.Add(cmd);
                        cmd = string.Empty;
                    }
                    tr.Close();
                }
                if (cmds.Count > 0)
                {
                    SqlCommand command = new SqlCommand();
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStringMaster"].ConnectionString))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Connection = con;
                        command.Connection.Open();

                        for (int queryNo = 0; queryNo < cmds.Count; queryNo++)
                        {
                            command.CommandText = cmds[queryNo];
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteDebugLog("\r\n---- Error while Updating DB : ----\r\n\r\n" + command.CommandText + "\r\n\r\n#### Error Message ####\r\n" + ex.ToString());
                                completedStatus = "CompletedWithError";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //loggingMechanism
                LogHelper.WriteDebugLog(ex.ToString());
                return "Failed";
            }
            return completedStatus;
        }
    }
}

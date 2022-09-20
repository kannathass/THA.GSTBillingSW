using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using THA.GSTBillingSW.BAL;
using System.Diagnostics;
using System.Configuration;

namespace THA.GSTBillingSW.DAL
{
    static class DetectSqlService
    {
        public static bool CheckAndRunSqlService()
        {
            string myServiceName = ConfigurationManager.AppSettings["SQLserviceName"].ToString(); //service name of SQL Server Express
            string status; //service status (For example, Running or Stopped)

            Console.WriteLine("Service: " + myServiceName);

            //display service status: For example, Running, Stopped, or Paused
            ServiceController mySC = new ServiceController(myServiceName);

            try
            {
                status = mySC.Status.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteDebugLog("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                return false;
            }

            //display service status: For example, Running, Stopped, or Paused
            Console.WriteLine("Service status : " + status);

            //if service is Stopped or StopPending, you can run it with the following code.
            if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
            {
                LogHelper.WriteDebugLog("Service status : " + status);

                try
                {
                    StartService(myServiceName);
                    //mySC.Start();
                    //mySC.WaitForStatus(ServiceControllerStatus.Running);
                    //LogHelper.WriteDebugLog("The service is now " + mySC.Status.ToString());
                }
                catch (Exception ex)
                {
                    LogHelper.WriteDebugLog("Error in starting the service: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        private static void StartService(string name)
        {
            LogHelper.WriteDebugLog("Starting the service...");
            var process = new Process();
            process.StartInfo.FileName = "net";
            process.StartInfo.Arguments = "start " + name;
            process.StartInfo.Verb = "runas";//run as administrator
            process.Start();
            process.WaitForExit();
            LogHelper.WriteDebugLog("The service is Started");
        }
    }
}

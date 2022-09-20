using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Collections;

namespace THA.GSTBillingSW.Licensing.BAL
{
    public class AccessHW
    {
        public static string GetHDDetail()
        {
            string hdSerialNumber = string.Empty;
            string Model = string.Empty;
            string Type = string.Empty;

            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");


            foreach (ManagementObject wmi_HD in moSearcher.Get())
            {
                hdSerialNumber = Convert.ToString(wmi_HD["SerialNumber"]);
                //Model = wmi_HD["Model"].ToString();  //Model Number
                //Type = wmi_HD["InterfaceType"].ToString();  //Interface Type
                break;
            }
            return hdSerialNumber.Trim();
            //return CryptoEngine.Encrypt(hdSerialNumber.Trim(), "793c-4421-404838");
        }
    }
}

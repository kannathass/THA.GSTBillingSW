using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace THA.GSTBillingSW.BAL
{
    class UserAuthentication
    {
        //this function Convert to Encode your Password 
        public static string EncodePasswordToBase64(string password)
        {
            string encodedData = string.Empty;
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                encodedData = Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog("Error in base64Encode " + ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return encodedData;
        }

        //this function Convert to Decode your Password
        public static string DecodeFrom64(string encodedData)
        {
            string result = string.Empty;
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                result = new String(decoded_char);
            }
            catch (Exception ex)
            {
                BAL.LogHelper.WriteDebugLog("Error in base64Encode " + ex.ToString());
                MessageBox.Show("There is some unexpected behaviour at " + DateTime.Now + ". Please contact thaBillingSupport@THAsoft.com for guidance.", "THA Billing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THA.GSTBillingSW.BAL
{
    class GenericFucntions
    {
        public static string GetStateFromCustomerName(string str, char separator)
        {
            var splitStr = str.Split(separator);

            if (splitStr.Length == 2) // throw error ?
            {
                return splitStr[1].ToUpper().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StrToSplit"></param>
        /// <param name="StrSplitChars">Multiple characters can be given</param>
        /// <returns></returns>
        public static string[] SplitString(string StrToSplit, string StrSplitChars)
        {
            char[] splitChar = StrSplitChars.ToCharArray();
            return StrToSplit.Split(splitChar);
        }

        public static decimal calcDiscountPercentFromAmount(decimal totalAmount, decimal discountAmount)
        {
            if (totalAmount == 0 || discountAmount == 0)
            {
                return 0;
            }
            return Math.Round((discountAmount / totalAmount) * 100, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal calcDiscountAmountFromPercent(decimal totalAmount, decimal discountPercent)
        {
            if (totalAmount == 0 || discountPercent == 0)
            {
                return 0;
            }
            return Math.Round((totalAmount * discountPercent) / 100, 2, MidpointRounding.AwayFromZero);
            //return Math.Round((totalAmount * discountPercent) / 100, 3, MidpointRounding.AwayFromZero);
            //return Math.Round((totalAmount * discountPercent) / 100, 2);
        }

        public static decimal calcTaxInclusiveRate(decimal ItemRate, decimal TaxRate)
        {
            //Convert.ToDecimal(popup.PopupItemProperty7) - (Convert.ToDecimal(popup.PopupItemProperty7) / (100 + Convert.ToDecimal(popup.PopupItemProperty11)) * Convert.ToDecimal(popup.PopupItemProperty11));
            return Math.Round(ItemRate - (ItemRate / (100 + TaxRate) * TaxRate), 2, MidpointRounding.AwayFromZero);
        }

        public static string[] GetFinancialYears()
        {
            string[] finYears = new string[2];
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;

            if (DateTime.Today.Month > 3)
            {
                finYears[0] = CurrentYear.ToString() + "-" + NextYear.ToString();
                finYears[1] = (CurrentYear - 1).ToString() + "-" + (NextYear - 1).ToString();
            }
            else
            {
                finYears[0] = PreviousYear.ToString() + "-" + CurrentYear.ToString();
                finYears[1] = (PreviousYear - 1).ToString() + "-" + (CurrentYear - 1).ToString();
            }

            return finYears;
        }
    }
}

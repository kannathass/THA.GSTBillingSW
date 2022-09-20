using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using THA.GSTBillingSW.Entities;

namespace THA.GSTBillingSW.BAL
{
    class CustomFieldSelection
    {
        DAL.CustomFieldSelection customFieldSelection = new DAL.CustomFieldSelection();
        //CustomFields customFields;
        public CustomFields GetCustomFields(int CompanyId, string DocumentType)
        {
            return customFieldSelection.GetCustomFields(CompanyId, DocumentType);
        }

        public string GetTermsAndConditions(int CompanyId, string DocumentType)
        {
            return customFieldSelection.GetTermsAndConditions(CompanyId, DocumentType);
        }

        public AutoCompleteStringCollection GetCustomFieldData(string CustomFieldName, string DocumentType)
        {
            DataTable dt = customFieldSelection.GetCustomFieldData(CustomFieldName, DocumentType); //suppose this method returns a DataTable with fetched records from database.
            AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
            foreach (DataRow row in dt.Rows)
            {
                stringCol.Add(Convert.ToString(row[0]));
            }
            return stringCol; //return the string collection with added records
        }

    }
}

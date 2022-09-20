using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THA.GSTBillingSW.Entities
{
    class CompanyInfo
    {
        public Int16 ID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDisplayName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PAN { get; set; }
        public string GSTN { get; set; }
        public string GSTNPortalUserName { get; set; }
        public string WebSite { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
    }
}

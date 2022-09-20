using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using THA.GSTBillingSW.Entities;
namespace THA.GSTBillingSW.BAL
{
    class MastersSelection
    {
        DAL.MastersSelection masterSelection = new DAL.MastersSelection();
        CompanyInfo companyInfo;
        public DataTable GetCompanyList()
        {
            return masterSelection.GetCompanyList();
        }

        public CompanyInfo GetCompanyInfo(Int16 CompanyId)
        {
            return masterSelection.GetCompanyInfo(CompanyId);
        }

        public DataTable GetCompanyGeneral()
        {
            return masterSelection.GetCompanyGeneral();
        }

        public DataTable GetCustomerList(string CustomerType)
        {
            return masterSelection.GetCustomerList(CustomerType);
        }

        public DataTable GetAgentList(string CustomerType)
        {
            return masterSelection.GetAgentList(CustomerType);
        }

        public DataTable GetCustomerListByAgent(Int32 AgentId, string CustomerType)
        {
            return masterSelection.GetCustomerListByAgent(AgentId, CustomerType);
        }

        public DataTable GetStateList()
        {
            return masterSelection.GetStateList();
        }

        public DataTable GetGroupList()
        {
            return masterSelection.GetGroupList();
        }

        public DataTable GetUoMList()
        {
            return masterSelection.GetUoMList();
        }
    }
}



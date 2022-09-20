using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THA.GSTBillingSW.BAL
{
    static class DeployDB
    {
        public static bool checkDatabaseExist()
        {
            return DAL.DeployDB.checkDatabaseExist();
        }

        public static string RunSqlScript(string scriptLocation)
        {
            return DAL.DeployDB.RunSqlScript(scriptLocation);
        }
    }
}

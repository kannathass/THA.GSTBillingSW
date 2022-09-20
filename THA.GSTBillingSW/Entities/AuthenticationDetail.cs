using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THA.GSTBillingSW.Entities
{
    public static class AuthenticationDetail
    {
        //enum authenticationDetail
        //{
        //    userRole,
        //    userPrivilege,
        //    status
        //};

        public static string UserName { get; set; }
        public static string UserRole { get; set; }
        public static string UserPrivilege { get; set; }
        public static bool IsActive { get; set; }
        public static bool isLicenseExpired { get; set; }
    }
}

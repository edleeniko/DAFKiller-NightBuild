using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller_NightBuild.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string HwId { get; set; }
        public UserRightsFlags UserRights { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [Flags]
        public enum UserRightsFlags
        {   //Values read from DB
            AccessDiagnostic = 1 << 0,      //1
            AccessProgramming = 1 << 1,     //2
            AccessMileage = 1 << 2,         //4
            AccessRemote = 1 << 3,          //8
            AccessFull = 1 << 4             //16
        }
    }
}

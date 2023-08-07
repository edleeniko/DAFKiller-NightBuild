using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller_NightBuild.Models
{
    [Serializable]
    public class UserModel
    {
        public int Id { get; set; }
        public string HwId { get; set; }
        public UserRightsFlags UserRights { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [Flags]
        public enum UserRightsFlags : int
        {   //Values read from DB
            Diagnostic = 1 << 0,                //1
            Programming = 1 << 1,               //2
            TruckSystemConfiguration = 1 << 2,  //4
            ReplaceControUnit = 1 << 3,         //8
            CustomParameters = 1 << 4,          //16
            Mileage = 1 << 5,                   //32
            Remote = 1 << 6,                    //64
            Documents = 1 << 7,                 //128
            Settings = 1 << 8,                  //256
            Full = 1 << 9                       //512
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    public class ConnectionHelper
    {
        public static string _connectionString;
        public static string ConnectionStringPrs
        {
            get
            {
                if (_connectionString == null)
                    _connectionString = Cryptography.TripleDESEncryptDecrypt.Decrypt(ConfigurationManager.ConnectionStrings["prscs"].ConnectionString, "dafkiller");
                return _connectionString;
            }
        }
    }
}

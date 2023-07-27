using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace DAFKiller_NightBuild.Repositories
{
    
    public abstract class RepositoryBase
    {
        private readonly string _userDBconnectionString;
        public RepositoryBase()
        {
            _userDBconnectionString = DAFKiller.Cryptography.TripleDESEncryptDecrypt.Decrypt(ConfigurationManager.ConnectionStrings["usersDBConnectionString"].ConnectionString, "dafkiller"); ;
        }
        protected MySqlConnection GetAutorizationDBConnection()
        {
            return new MySqlConnection(_userDBconnectionString);
        }
    }
}

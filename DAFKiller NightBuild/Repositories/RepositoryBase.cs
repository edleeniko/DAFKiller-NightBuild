using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.Odbc;

namespace DAFKiller_NightBuild.Repositories
{
    
    public abstract class RepositoryBase
    {
        private readonly string _userDBconnectionString;
        private readonly string _davie3DBConnectionString;
        public RepositoryBase()
        {
            _userDBconnectionString = DAFKiller.Cryptography.TripleDESEncryptDecrypt.Decrypt(ConfigurationManager.ConnectionStrings["usersDBConnectionString"].ConnectionString, "dafkiller");
            _davie3DBConnectionString = DAFKiller.Cryptography.TripleDESEncryptDecrypt.Decrypt(ConfigurationManager.ConnectionStrings["davie3DBConnectionString"].ConnectionString, "dafkiller");
        }
        protected MySqlConnection GetAutorizationDBConnection()
        {
            return new MySqlConnection(_userDBconnectionString);
        }
        protected OdbcConnection GetDavie3OdbcConnection()
        {
            return new OdbcConnection(_davie3DBConnectionString);
        }
    }
}

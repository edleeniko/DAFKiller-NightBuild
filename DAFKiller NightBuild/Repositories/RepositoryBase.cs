using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace DAFKiller_NightBuild.Repositories
{
    
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "server=truckx.mysql.ukraine.com.ua; uid=truckx_dafkiller; pwd=pasoiuryetQ1; database=truckx_dafkiller";
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}

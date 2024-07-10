using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySqlConnector;

namespace CrudWithoutORM.Data
{

    public class DataBase
    {
        private static string connection_string = "server=localhost;User Id=Minion;Password=minion;database=Bulgaria;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connection_string);
        }
    }
}

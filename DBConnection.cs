using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PayrollV3
{

    public class DBConnection
    {
        private static string conString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private static SqlConnection connection;

        private DBConnection() { }


        public static SqlConnection getConnection()
        {
            connection = new SqlConnection(conString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }


    }

}

using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollV3
{
    public class EmployeeRepository
    {
        private static EmployeeRepository instance;
        private EmployeeRepository() { }

        public static EmployeeRepository Instance()
        {
            if (instance == null)
                return instance = new EmployeeRepository();
            return instance;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using(SqlConnection sqlconnection = DBConnection.getConnection())
            {

                string query = "select * from employee";

               employees= sqlconnection.Query<Employee>(query).ToList();
            
            }

            return employees;
        }
        
    }
}

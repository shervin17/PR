using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public class EmployeePayrollDetailsRepo
    {
        private static EmployeePayrollDetailsRepo instance;
        private EmployeePayrollDetailsRepo() { }

        public static EmployeePayrollDetailsRepo Instance()
        {
        if (instance == null)
             instance = new EmployeePayrollDetailsRepo();
        return instance;
        }
        public EmployeePayrollDetails getByID(int payroll_details_id) 
        {
            EmployeePayrollDetails employeePayrollDetails = null;

            try 
            { 
                using(SqlConnection connection =DBConnection.getConnection()) 
                {
                    string sqlQuery = "select * from employee_payroll_details where payroll_details_id = @id ";
                    var paramsObj = new { id = payroll_details_id };
                    employeePayrollDetails = connection.QueryFirstOrDefault<EmployeePayrollDetails>(sqlQuery, paramsObj);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("an error occured " + ex.Message);
            }
            return employeePayrollDetails;
        }
       
    }
}

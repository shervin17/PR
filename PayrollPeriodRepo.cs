using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public class PayrollPeriodRepo
    {
        private static PayrollPeriodRepo instance;
        
        private PayrollPeriodRepo() { }
        public static PayrollPeriodRepo Instance() { 
        
        if (instance == null)
             instance = new PayrollPeriodRepo();
            
        return instance;
        }

        public List<PayrollPeriod> GetPayrollPeriods()
        {
            List<PayrollPeriod> list = null;

            try
            {
                using (SqlConnection conn = DBConnection.getConnection())
                {
                    string query = "select * from payroll_period";
                    list = conn.Query<PayrollPeriod>(query).ToList();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Sorry error occured " + ex.Message);
            }

            return list;
        }


    }
}

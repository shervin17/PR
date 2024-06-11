using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public class EmployeeCalendarDatesRepo
    {
        private static EmployeeCalendarDatesRepo instance;  
        private EmployeeCalendarDatesRepo() { }

        public static EmployeeCalendarDatesRepo Instance() {

            if (instance == null) 
                instance = new EmployeeCalendarDatesRepo();
            return instance;
        }

        public List<EmployeeCalendarDates> GetCalendarDates(int payroll_period_id)
        {
            List<EmployeeCalendarDates> list=null;

            try 
            {
                using(SqlConnection connection= DBConnection.getConnection())
                {
                    string query = "select * from EmployeeCalendarDates where payroll_period_id = @payroll_period_id";
                    var objparams = new { payroll_period_id = payroll_period_id };
                    list = connection.Query<EmployeeCalendarDates>(query, objparams).ToList();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("An error occured " + ex.Message);
            }


            return list;
        }
        public List<EmployeeCalendarDates> GetPayableDates(int payroll_period_id) 
        {
            List<EmployeeCalendarDates> list = null;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "select * from EmployeeCalendarDates where payroll_period_id = @payroll_period_id and category != @category"  ;
                    var objparams = new { payroll_period_id = payroll_period_id , category = "NON_WORKING" };
                    list = connection.Query<EmployeeCalendarDates>(query, objparams).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured " + ex.Message);
            }


            return list;
        }

        public int AddAll(List<EmployeeCalendarDates> list)
        {
            int affected_rows = 0;
            SqlTransaction transaction = null;

            try
            {
                using(SqlConnection  conn= DBConnection.getConnection())
                {
                    transaction= conn.BeginTransaction();

                    list.ForEach(dates => {

                    affected_rows += Add(dates);
                    });
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            { 
                transaction.Rollback();
                MessageBox.Show("an error occured "+ex.Message);
            
            }



            
            return affected_rows;
        }
        private int Add(EmployeeCalendarDates date)
        {
            int affected_rows = 0;

            try
            { 
                using(SqlConnection connection= DBConnection.getConnection()) 
                {
                    string query = "insert into EmployeeCalendarDates values (@payroll_period_id,@date,@category)";
                    affected_rows=connection.Execute(query,date);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("an error occured "+ ex.Message);
            }


            return affected_rows;
        }

    }

    public class EmployeeCalendarDates
    {
        public int Workdays_id { get; set; }
        public int Payroll_period_id { get; set; }

        public DateTime Date { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $" {Workdays_id} , {Payroll_period_id}, {Date}, {Category}";
        }
    }
}

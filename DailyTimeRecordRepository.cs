using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollV3
{
    public class DailyTimeRecordRepository
    {
        private static DailyTimeRecordRepository instance;
        private DailyTimeRecordRepository() { }
      
        public static DailyTimeRecordRepository Instance()
        {
            if (instance == null) 
                instance = new DailyTimeRecordRepository();
            return instance;
        }
        public List<DailyTimeRecord> findDTRbyEmployeeID(int employeeID, PayrollPeriod payrollPeriod)
        {
            List<DailyTimeRecord> list = new List<DailyTimeRecord>();

            string query = "select * from DailyTimeRecord where employee_id =@employee_id and date between @date_from and @date_to";

            try { 
            
                using(SqlConnection connection= DBConnection.getConnection())
                {
                    var objparams = new { employee_id= employeeID, date_from= payrollPeriod.Date_from, date_to = payrollPeriod.Date_to }; 
                    list= connection.Query<DailyTimeRecord>(query, objparams).ToList();
                }
            
            }
            catch (Exception ex)
            {
                throw;
            }
            


            return list;
        }
    }



}

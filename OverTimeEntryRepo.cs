using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace PayrollV3
{
    public class OverTimeEntryRepo
    {
        private static OverTimeEntryRepo _instance;
        private OverTimeEntryRepo() { }

        public static OverTimeEntryRepo Instance() 
        {
        if (_instance == null)
                _instance = new OverTimeEntryRepo();
        return _instance;
        }

        public List<OverTimeEntry> GetOverTimeEntriesById(int employee_id, PayrollPeriod payrollPeriod) 
        {
            List<OverTimeEntry> list;
            try
            {

                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string sqlQuery = "select * from OvertimeTracker  where employee_id = @id and date between @date_from and @date_to";
                    var objparams = new
                    {
                        id = employee_id,
                        date_from = payrollPeriod.Date_from,
                        date_to = payrollPeriod.Date_to,
                    };

                    list = connection.Query<OverTimeEntry>(sqlQuery,objparams).ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }

        public OverTimeEntry GetOverTimeEntryByDateAndId(int employee_id, DateTime dateTime)
        {
            OverTimeEntry overTimeEntry;
            try
            {

                using (SqlConnection connection = DBConnection.getConnection())
                {

                    string query = "select * from OvertimeTracker where employee_id = @id and date = @date";
                    var objparams = new { id = employee_id, @date = dateTime };
                    overTimeEntry = connection.QueryFirstOrDefault<OverTimeEntry>(query, objparams);
                }
            }
            catch (Exception)
            {

                throw;
            }


            return overTimeEntry;
        }
        public void Add(OverTimeEntry overTimeEntry)
        {
            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string sqlQuery = " insert into OvertimeTracker values (@emp_id, @date ,@time_in, @time_out, @stat, @reason, @sup_id)";
                    var objparams = new
                    {
                        emp_id = overTimeEntry.Employee_id,
                        date = overTimeEntry.Date,
                        time_in = overTimeEntry.Time_in,
                        time_out = overTimeEntry.Time_out,
                        stat = overTimeEntry.Status,
                        reason = overTimeEntry.Reason,
                        sup_id = overTimeEntry.Supervisor_id,
                    };
                    connection.Execute(sqlQuery, objparams);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Update(OverTimeEntry overTimeEntry)
        {
            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string sqlQuery = "update OvertimeTracker set time_out =@time_out , status = @stat where employee_id = @empId and date =@date";
                    var objparams = new { time_out = overTimeEntry.Time_out, stat = overTimeEntry.Status, empId = overTimeEntry.Employee_id, date = overTimeEntry.Date };
                    connection.Execute(sqlQuery, objparams);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class OverTimeEntry
    {
        public int id { get; set; }
        public int Employee_id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time_in { get; set; }
        public DateTime Time_out { get; set; }
        public string Status { get; set; }

        public string Reason { get; set; }

        public int Supervisor_id { get; set; }

        public override string ToString()
        {
            return $"ID: {id}, Employee ID: {Employee_id}, Date: {Date}, Time In: {Time_in}, " +
                $"Time Out: {Time_out}, Status: {Status}, Reason: {Reason}, Supervisor ID: {Supervisor_id}";
        }

    }
}

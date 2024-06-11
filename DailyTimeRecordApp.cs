using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public partial class DailyTimeRecordApp : Form
    {
        private int current_employee_id;
        
        public DailyTimeRecordApp()
        {
            InitializeComponent();

            Timer timer = new Timer();
            timer.Interval= 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {   
            
            label_time.Text = DateTime.Now.ToString("HH:mm:ss");
            
        }

        private void DailyTimeRecordApp_Load(object sender, EventArgs e)
        {
          
            day_label.Text = DateTime.Now.ToString("dddd").Substring(0,3);
            date_label.Text = DateTime.Now.ToString("MM-dd-yy");

            shift_start.SetTimeSpan(new TimeSpan(8, 0, 0), shift.ClockPeriod.AM);
            shift_to.SetTimeSpan(new TimeSpan(17,0,0), shift.ClockPeriod.PM);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            current_employee_id = 0;

            if (!validateLogins())
            {
                MessageBox.Show("invalid username or password");
                return;
            }
            TimeSpan start = shift_start.getTimeSpan();
            TimeSpan end_of_shift = shift_to.getTimeSpan();

            string username = username_field.Text;
            string password = password_field.Text;
            DateTime current_date = DateTime.Now.Date;

           DailyTimeRecord dtr = GetDailyTimeRecord(current_employee_id, current_date);
            if (dtr == null)
            {

                TimeSpan start_of_shift = shift_start.getTimeSpan();
                TimeSpan eos= shift_to.getTimeSpan();
                DateTime dateTimeStart = DateTime.Now.Date.Add(start_of_shift);
                DateTime dateTimeEnd = DateTime.Now.Add(end_of_shift);

                dtr = new DailyTimeRecord {
                    Employee_id = current_employee_id,
                    Date = current_date,
                    Time_in = DateTime.Now,
                    Time_out = DateTime.Now,
                    Shift_in= dateTimeStart,
                    Shift_out= dateTimeEnd,
                    Status= Status.ONGOING.ToString(),

                };
               
               AddDTR(dtr);

            }
            else 
            {
                if (dtr.Status == Status.COMPLETE.ToString())
                {
                    MessageBox.Show($"It seems you have time out already at {dtr.Time_out} ");
                    return;
                }

                TimeSpan start_of_shift = shift_start.getTimeSpan();
                TimeSpan eos = shift_to.getTimeSpan();
                DateTime dateTimeStart = DateTime.Now.Date.Add(start_of_shift);
                DateTime dateTimeEnd = DateTime.Now.Add(end_of_shift); //ignore
                DialogResult dialogResult = MessageBox.Show("You are about to time out. Do you wish to continue?", "Timeout", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.Cancel) {
                    return; }
                dtr.Shift_in = dateTimeStart;
                dtr.Shift_out = dateTimeEnd;
                dtr.Time_out= DateTime.Now;
                dtr.Status= Status.COMPLETE.ToString();

                UpdateDTR(dtr);
            }
        }
        private bool validateLogins() {

            
            string username = username_field.Text;
            string password = password_field.Text;
                
            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {

                    string query = "select id from employee inner join employee_logins on employee.logins_id = employee_logins.logins_id where username = @username and password = @password ";

                   var result= connection.QueryFirstOrDefault(query, new { @username = username, @password = password });
                   if (result != null)
                    {
                        current_employee_id = result.id;
                        return true;
                    }
                 }
            }
            catch (Exception ex) 
            {

                MessageBox.Show(ex.Message);
            }
            return false;
        }
        private int UpdateDTR(DailyTimeRecord dtr)
        {
            int result=0;

            string updateString = "update DailyTimeRecord set time_out =@time_out, shift_in=@shift_in,shift_out=@shift_out,status=@status where employee_id= @employee_id and date=@date";

            var objparam = new { time_out = dtr.Time_out, shift_in = dtr.Shift_in , shift_out= dtr.Shift_out, status = dtr.Status , employee_id= current_employee_id, date=dtr.Date};

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                   result= connection.Execute(updateString, objparam);
                    MessageBox.Show($"time out at {dtr.Time_out} saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        private int AddDTR(DailyTimeRecord dtr) {
            int result = 0;

            string insertString = "insert into DailyTimeRecord values (@employee_id,@date,@time_in,@time_out,@shift_in,@shift_out,@status)";

            try 
            {
                using (SqlConnection connection=DBConnection.getConnection())
                {
                   result= connection.Execute(insertString, dtr);
                    MessageBox.Show($"time in at {dtr.Time_in} saved");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        private DailyTimeRecord GetDailyTimeRecord(int employee_id, DateTime date) {

            string query = "select * from DailyTimeRecord where employee_id = @employee_id and date= @date";
            var param = new { employee_id = employee_id, date = date };

            try {
                using(SqlConnection connection = DBConnection.getConnection()) 
                {
                   DailyTimeRecord dtr =connection.QueryFirstOrDefault<DailyTimeRecord>(query, param);
                    return dtr;
                }

            }
            catch (Exception e)
            { 
                MessageBox.Show(e.Message);
            }
            return null;
        }


        public enum Status { 
            ONGOING,
            COMPLETE,
        }
        
    }
}

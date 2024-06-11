using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
namespace PayrollV3
{
    public partial class OTtrackerForm : Form
    {
        private int current_employee_id;
        private string reason;
        private int sup_id;
        OverTimeEntryRepo overTimeEntryRepo = OverTimeEntryRepo.Instance();
 
             public OTtrackerForm(String reason, int sup_id)
        {
            InitializeComponent();
            this.reason = reason;
            this.sup_id = sup_id;


            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
           time_label.Text = DateTime.Now.ToString("HH:mm:ss");

        }
        private void OTtrackerForm_Load(object sender, EventArgs e)
        {
            Day_label.Text = DateTime.Now.ToString("dddd").Substring(0, 3);
            date_label.Text = DateTime.Now.ToString("MM-dd-yy");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //time in and out btn
            if (!validateLogins())
            {
                MessageBox.Show("invalid username or password");
                return;
            }
            DateTime current_date = DateTime.Now.Date;

            OverTimeEntry overTimeEntry = overTimeEntryRepo.GetOverTimeEntryByDateAndId(current_employee_id, current_date);
            if (overTimeEntry == null)
            {
                overTimeEntry = new OverTimeEntry()
                {
                    Employee_id = current_employee_id,
                    Date = current_date,
                    Time_in = DateTime.Now,
                    Time_out = DateTime.Now,
                    Status = Status.ONGOING.ToString(),
                    Reason = reason,
                    Supervisor_id = sup_id,
                };
                overTimeEntryRepo.Add(overTimeEntry);
                MessageBox.Show($"time in saved at {DateTime.Now}");
            }
            else
            {
                if (overTimeEntry.Status == Status.COMPLETE.ToString())
                {
                    MessageBox.Show($"It seems you have time out already at {overTimeEntry.Time_out} ");
                    return;
                }
                DialogResult proceedTimeOut = MessageBox.Show("You are about to time out. Proced?","TIME OUT?",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
                if(proceedTimeOut == DialogResult.Cancel) {
                    return;
                }

                overTimeEntry.Time_out = DateTime.Now;
                overTimeEntry.Status= Status.COMPLETE.ToString();

                 overTimeEntryRepo.Update(overTimeEntry);
                MessageBox.Show($"time out saved at {DateTime.Now}");

            }
        }
        private bool validateLogins()
        {

            string username = username_field.Text;
            string password = password_field.Text;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {

                    string query = "select id from employee inner join employee_logins on employee.logins_id = employee_logins.logins_id where username = @username and password = @password ";

                    var result = connection.QueryFirstOrDefault(query, new { @username = username, @password = password });
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
        private enum Status
        {
            ONGOING,
            COMPLETE,
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public partial class frmCreateEmployeeRecord : Form
    {
        public frmCreateEmployeeRecord()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Adding an employee record

            ValidateFields();
            addEmployeeRecord();
            PopulateDGV();
        }

        private void PopulateDGV()
        {
            using (SqlConnection conn = DBConnection.getConnection()) {

                string query = "select id, firstname, lastname, middle_name, birthdate, address, position, leaves.date_started from employee inner join leaves on employee.leaves_id = leaves.leaves_id";
                List<Employee> employees = conn.Query<Employee>(query).ToList();
                dgv.DataSource= employees;
                
            }
        }

        private bool ValidateFields()
        {
            


            return false;
        }

        private void addEmployeeRecord() {
            using (SqlConnection connection = DBConnection.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //For logins

                        string[] logins = generateLogins();
                        string username = logins[0];
                        string password = logins[1];

                        var employee_login = new EmployeeLogins
                        {
                            Username = username,
                            Password = password,
                            AdvanceUser= "NO",
                        };

                        string insertTableQuery = @"insert into employee_logins (username, password,advanceUser) values (@Username, @Password,@AdvanceUser); 
                                                    select cast(scope_identity() as int); select cast(scope_identity() as int);";
                        int employee_logins_id = connection.QuerySingle<int>(insertTableQuery, employee_login, transaction);

                        //For Payroll_details

                        decimal[] payroll_inputs = generateRates();
                        decimal salary = payroll_inputs[0];
                        decimal daily_rate = payroll_inputs[1];
                        decimal hourly_rate = payroll_inputs[2];

                        var payroll_detail = new EmployeePayrollDetails
                        {
                            Salary = salary,
                            Hourly_rate = hourly_rate,
                            Daily_rate = daily_rate,
                        };
                        string insertTableQuery2 = @"insert into employee_payroll_details (salary, daily_rate, hourly_rate) values (@Salary,@Daily_rate,@Hourly_rate) select cast(scope_identity() as int);";
                        int payroll_details_id = connection.QuerySingle<int>(insertTableQuery2, payroll_detail, transaction);

                        //For Leaves 
                        DateTime date_started = startdate_field.Value;
                        var leaves = new Leaves
                        {
                            Accrued_sickleave = 5,
                            Accrued_vacationleave = 0,
                            Remaining_SL = 5,
                            Remaining_VL = 0,
                            DateStarted = date_started,
                            Emergency_leave = 5,
                            Bereavement_leave_used = 0,
                        };

                        string insertTableQuery3 = @"insert into leaves (accrued_sickleave ,accrued_vacationLeave, remaining_SL, remaining_VL, date_started, emergency_leave, bereavement_leave_used) values (@Accrued_sickleave, @Accrued_vacationLeave, @Remaining_SL,@Remaining_VL, @DateStarted, @Emergency_leave , @Bereavement_leave_used); select cast (scope_identity() as int);";
                        int leaves_id = connection.QuerySingle<int>(insertTableQuery3, leaves, transaction);

                        //For Employee 




                        var employee = new Employee
                        {
                            Firstname = firstname_field.Text,
                            Lastname = lastname_field.Text,
                            Middle_name = middlename_field.Text,
                            Birthdate = birthdate_field.Value,
                            Address = adress_field.Text,
                            Position = PositionCB.SelectedItem.ToString(),
                            Logins_id = employee_logins_id,
                            Payroll_details_id = payroll_details_id,
                            Leaves_id = leaves_id
                        };
                        string insertEmployeeQuery = @"insert into employee (firstname,lastname,middle_name,birthdate,address,position,logins_id,payroll_details_id,leaves_id) values (@Firstname, @Lastname,@Middle_name,@Birthdate,@Address,@Position,@Logins_id,@Payroll_details_id,@Leaves_id)";
                        connection.Execute(insertEmployeeQuery, employee, transaction);

                        transaction.Commit();

                        MessageBox.Show($"record save. \n new login username: { username} password: { password}");

                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back due to an error: " + ex.Message);
                        MessageBox.Show("Transaction rolled back due to an error: " + ex.Message);
                    }
                    finally { 
                        ResetFields();
                    }

                }
            }
        }
        private string[] generateLogins() {

            string lname = lastname_field.Text.Trim();
            string bday = birthdate_field.Value.ToString("yy-MM-dd").Replace("-", "");

           StringBuilder user= new StringBuilder();
            user.Append(lname);
            user.Append(".");
            user.Append(bday);


            string username = user.ToString();
            string password = "Password@123";

            return new string[] { username, password };
        }
        private decimal[] generateRates() {

            int no_days_per_monthly = 22;
            int regular_shift_hours = 8;

            decimal.TryParse(salary_field.Text, out decimal salary);

            decimal daily_rate = salary / no_days_per_monthly;
            decimal hourly_rate = daily_rate / regular_shift_hours;

            return new decimal[] { salary,daily_rate,hourly_rate };
        }

        public void ResetFields() {

            firstname_field.Text = "";
            lastname_field.Text = "";
            middlename_field.Text = "";
            position_field.Text = "";
            salary_field.Text = "";
            adress_field.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void frmCreateEmployeeRecord_Load(object sender, EventArgs e)
        {

        }

        private void PositionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_position = PositionCB.SelectedItem.ToString();
            
            decimal manager_salary= 30_000m;
            decimal assistant_manager_salary = 20_000m;
            decimal staff = 12_000m;

            decimal sal = selected_position == "MANAGER" ? manager_salary : selected_position == "ASSISTANT MANAGER" ? assistant_manager_salary :
                selected_position == "STAFF" ? staff : 0;

            salary_field.Text = sal.ToString();


/*            MANAGER
ASSISTANT MANAGER
STAFF*/
        }
    }
}

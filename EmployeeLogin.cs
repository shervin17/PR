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
    public partial class EmployeeLogin : Form
    {
        public EmployeeLogin()
        {
            InitializeComponent();
        }

        private void loginAccountControl1_Load(object sender, EventArgs e)
        {

        }

        private void EmployeeLogin_Load(object sender, EventArgs e)
        {

        }
        public int validateLogins()
        {

            int user_id = 0;
            string username = username_field.Text;
            string password = password_field.Text;

            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {

                    string query = "select id from employee inner join employee_logins on employee.logins_id = employee_logins.logins_id where username = @username and password = @password";

                    var result = connection.QueryFirstOrDefault(query, new { @username = username, @password = password});
                    if (result != null)
                    {
                        user_id = result.id;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return user_id;
        }

        private void login_Click(object sender, EventArgs e)
        {
            if(validateLogins() == 0)
            {
                MessageBox.Show("invalid username or password");
                return;
            }
            new EmployeePage().ShowDialog();
            this.Hide();
            
        }
    }
}

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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = DBConnection.getConnection())
            {

                string query = "select * from admin_login where username=@username and password = @password";
                var obj = new { username = username_field.Text, password = password_field.Text };

                List<Admin> admins = connection.Query<Admin>(query, obj).ToList();
                if (admins.Count > 0)
                {
                    new Form2().ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Please check username and password");
                }
            }

        }
        public class Admin
        {

            string username;
            string lastname;
        }
    }
}

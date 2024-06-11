using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Dapper;
namespace PayrollV3
{
    public partial class ManageEmployeeLogins : Form
    {
        List<ForUpdateEmployeeLogins> list;
        ForUpdateEmployeeLogins forUpdate;
        public ManageEmployeeLogins()
        {
            InitializeComponent();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            forUpdate = dataGridView1.SelectedRows[0].DataBoundItem as ForUpdateEmployeeLogins;
            Debug.WriteLine(forUpdate);
            if(forUpdate != null )
            {
                fname_field.Text = forUpdate.FirstName;
                Lastname_field.Text = forUpdate.LastName;
                middlename_Field.Text= forUpdate.Middle_name;
                Position_Field.Text = forUpdate.Position;
                UsernameField.Text= forUpdate.Username;
                PasswordField.Text = forUpdate.Password;
                AdvanceUser_options.SelectedItem = forUpdate.AdvanceUser;
            }
        }

        private void ManageEmployeeLogins_Load(object sender, EventArgs e)
        {

            FetchAndDisplayTableInfo();
        }
        private void FetchAndDisplayTableInfo() {

            Debug.WriteLine("this is called");
            try
            { 
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "select employee.id, firstname, lastname, middle_name, position,employee.logins_id, username, password, advanceUser from employee inner join employee_logins on employee.logins_id = employee_logins.logins_id;";
                    list = connection.Query<ForUpdateEmployeeLogins>(query).ToList();
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSize = true;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = list;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("error occured " + ex.Message);
            }
        }
        private class ForUpdateEmployeeLogins
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Middle_name { get; set; }
            public string Position { get; set; }
            public int Logins_id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string AdvanceUser { get; set; }

            public override string ToString()
            {
                return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Middle_name: {Middle_name}, Position: {Position}, Logins_id: {Logins_id}, Username: {Username}, Password: {Password}, AdvanceUser: {AdvanceUser}";
            }
        }

        private void updateBTN_Click(object sender, EventArgs e)
        {
            if (forUpdate == null) {
                MessageBox.Show("No records selected");
            }
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = DBConnection.getConnection())
                {
                    transaction = connection.BeginTransaction();
                    string sqlQuery = " update employee_logins set username =@username, password=@password, advanceUser =@advance where employee_logins.logins_id=@id";

                    var obj = new { username = forUpdate.Username, password = forUpdate.Password, advance = forUpdate.AdvanceUser, id = forUpdate.Logins_id };
                  int result=  connection.Execute(sqlQuery, obj,transaction);
                    transaction.Commit();
                    Debug.WriteLine(forUpdate.Logins_id);
                    MessageBox.Show($" {result} record updated ");
                    FetchAndDisplayTableInfo();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured " + ex.Message);
            }
        }
        private void clearFields()
        {
            fname_field.Text = "";
            Lastname_field.Text = "";
            middlename_Field.Text = "";
            Position_Field.Text = "";
            UsernameField.Text = "";
            PasswordField.Text ="";
            AdvanceUser_options.Text = "";

            forUpdate = null;
        }

        private void OKBTN_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }

}

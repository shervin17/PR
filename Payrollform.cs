using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollV3
{
    public partial class Payrollform : Form
    {   
        List<Employee> employees;
        public Payrollform()
        {
            InitializeComponent();
        }

        private void Payrollform_Load(object sender, EventArgs e)
        {
            employees= EmployeeRepository.Instance().GetEmployees();
            dataGridView1.DataSource = employees;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Employee employee =  dataGridView1.SelectedRows[0].DataBoundItem as Employee;
            
            PayrollCalculatorfrm obj = new PayrollCalculatorfrm(employee);
            obj.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

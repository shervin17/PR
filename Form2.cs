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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateEmployeeRecord frm = new frmCreateEmployeeRecord();
            frm.TopLevel = false;
            frm.BringToFront();
            panel1.Controls.Add(frm);
            frm.Show();
        }

        private void payrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Payrollform payrollform = new Payrollform();
            payrollform.TopLevel = false;
            payrollform.BringToFront();
            panel1.Controls.Add(payrollform);
            payrollform.Show();
        }

        private void manageWorkdaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePayrollDaysfrm obj = new UpdatePayrollDaysfrm();
            obj.TopLevel = false;
            obj.BringToFront();
            panel1.Controls.Add(obj);
            obj.Show();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

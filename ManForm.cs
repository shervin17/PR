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
    public partial class ManForm : Form
    {
        public ManForm()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ManForm_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            UpdatePayrollDaysfrm frm = new UpdatePayrollDaysfrm();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            Payrollform frm = new Payrollform();    
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            frmCreateEmployeeRecord frm = new frmCreateEmployeeRecord();
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
          



        }

        private void Employee_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
/*
            frm.MdiParent = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
                CloseAllChildForms();*/
                        }
        private void CloseAllChildForms()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}

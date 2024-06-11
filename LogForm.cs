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
    public partial class LogForm : Form
    {

        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {

        }

        private void loginAccountControl1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           int id= loginAccountControl1.validateLogins();
            if(id == 0) {
                MessageBox.Show("invalid username or password");
                return;
            }
            Hide();
            new OTRequestForm(id).ShowDialog();
        }
    }
}

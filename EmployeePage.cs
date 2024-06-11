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
    public partial class EmployeePage : Form
    {
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void applyForOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LogForm().ShowDialog();
        }
    }
}

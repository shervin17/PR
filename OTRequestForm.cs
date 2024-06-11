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
    public partial class OTRequestForm : Form
    {
        private int passed_id;
        public OTRequestForm(int passed_id)
        {
            InitializeComponent();
            this.passed_id = passed_id;
        }

        private void OTRequestForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {   //submit BTN
            string reason = comboBox1_reasons.SelectedItem?.ToString();

            if (reason == "OTHER REASON")
            {
                reason = Reason_Others_field.Text;
            }

            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Please select or provide a reason.");
                return;
            }


            OTtrackerForm obj = new OTtrackerForm(reason,passed_id);
            obj.ShowDialog();
        }
    }
}

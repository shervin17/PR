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
    public partial class shift : UserControl
    {
        public shift()
        {
            InitializeComponent();

            for (int i = 1; i <= 12; i++)
            {
                hours_option.Items.Add(i.ToString());
            }

            for (int i = 0; i < 60;  i++)
            {
                mins_options.Items.Add(i.ToString());
            }

            hours_option.SelectedItem = 8;
            mins_options.SelectedIndex = 0;
            AMorPM.SelectedItem = ClockPeriod.AM;

            
            AMorPM.Items.Add(ClockPeriod.AM);
            AMorPM.Items.Add(ClockPeriod.PM);
        }

        public TimeSpan getTimeSpan() {


            int hour = int.Parse(hours_option.SelectedItem.ToString());
            int minute = int.Parse(mins_options.SelectedItem.ToString());
            int default_sec = 0;

            if ((ClockPeriod)AMorPM.SelectedItem == ClockPeriod.AM)
                return new TimeSpan(hour, minute, default_sec);
            return new TimeSpan(hour + 12, minute, default_sec);
        }

        private void shift_Load(object sender, EventArgs e)
        {

        }
        public void SetTimeSpan (TimeSpan time,ClockPeriod clockPeriod)
        {
           
            int hr= time.Hours; 
            if (time.Hours > 12) {
                hr = time.Hours - 12;
            }
            hours_option.SelectedItem= hr.ToString();
            mins_options.SelectedItem = time.Minutes;
            AMorPM.SelectedItem = clockPeriod == ClockPeriod.AM ? ClockPeriod.AM : ClockPeriod.PM;

        }
        public enum ClockPeriod { 
        AM,
        PM
        }
    }
}

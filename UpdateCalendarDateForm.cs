using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollV3
{
    public partial class UpdateCalendarDateForm : Form
    {
        private List<EmployeeCalendarDates> calendar_days = null;
        private EmployeeCalendarDates target = null;
        private int indexFinder;
        
        public UpdateCalendarDateForm(List<EmployeeCalendarDates> list, EmployeeCalendarDates target) 
        { 
            InitializeComponent();
            calendar_days = list;
            this.target = target;

            dateTimePicker1.Value = target.Date;
            comboBox1.Text = target.Category;

            list.ForEach(day => { 
            if(day.Date ==  target.Date && day.Category == target.Category)
                { 
                    indexFinder= list.IndexOf(day);
                }
            
            });

        }

        private void UpdateCalendarDateForm_Load(object sender, EventArgs e)
        {

        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeCalendarDates employeeCalendarDates = new EmployeeCalendarDates()
                {
                    Payroll_period_id = target.Payroll_period_id,
                    Date = dateTimePicker1.Value.Date,
                    Category = comboBox1.SelectedItem.ToString(),
                };
                calendar_days.Insert(indexFinder, employeeCalendarDates);
                calendar_days.RemoveAt(indexFinder + 1);
                MessageBox.Show("row updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("an error occured " + ex.Message);
            }
            finally {
                Hide();
            }
        }

        private void removebutton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(indexFinder.ToString());
            
            if (calendar_days.Count == 0)
                MessageBox.Show("list is empty");
            if (calendar_days.Count == 1)
            {
                calendar_days.Clear();
                MessageBox.Show("removed");
            }
            if (calendar_days.Count >= 2)
            {
                try
                {
                    calendar_days.RemoveAt(indexFinder);
                    MessageBox.Show("removed");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("an error occured " + ex.Message);
                }
                finally 
                {
                    Hide();
                }
            }
            Hide();
        }
        public List<EmployeeCalendarDates> getCalendarDates()
        {
            return calendar_days;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

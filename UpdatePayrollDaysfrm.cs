﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace PayrollV3
{
    public partial class UpdatePayrollDaysfrm : Form
    {
        List<PayrollPeriod> periods;
        PayrollPeriodRepo repo = PayrollPeriodRepo.Instance();
        PayrollPeriod selected;
        List<EmployeeCalendarDates> calendarDates = new List<EmployeeCalendarDates>();
        public UpdatePayrollDaysfrm()
        {   
            InitializeComponent();
            periods= repo.GetPayrollPeriods();
            comboBox1.DataSource = periods;
            
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           selected= comboBox1.SelectedItem as PayrollPeriod;
        }

        private void UpdatePayrollDaysfrm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            selected = comboBox1.SelectedItem as PayrollPeriod;
        }

       

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            EmployeeCalendarDates selectedRow = dataGridView1.SelectedRows[0].DataBoundItem as EmployeeCalendarDates;
            Debug.WriteLine(selectedRow);
            if (selectedRow == null)
                return;
            UpdateCalendarDateForm obj= new UpdateCalendarDateForm(calendarDates,selectedRow);
            obj.ShowDialog();
            dataGridView1.DataSource = null;
           calendarDates=obj.getCalendarDates();
             dataGridView1.DataSource= calendarDates;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //save button

            if (calendarDates.Count == 0) 
            {
                MessageBox.Show("cannot save without entry");
                return;
            }
            try {

              int result= EmployeeCalendarDatesRepo.Instance().AddAll(calendarDates);
                MessageBox.Show($"{result} rows added");
                dataGridView1.DataSource = null;
                calendarDates.Clear();

            }
            catch(Exception ex)
            {
                MessageBox.Show("an error occured "+ ex.Message);
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clear button
            calendarDates.Clear();
            dataGridView1.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add to draft button
            if (dayTypeCb.SelectedItem == null) {
                MessageBox.Show("holiday type cannot be null");
                    return;
            }
            if (calendarDates.Count != 0)
            {
                DateTime toBeCompared = dateTimePicker1.Value.Date;

                for (int i = 0; i < calendarDates.Count; i++)
                {
                    if (calendarDates[i].Date.Date == toBeCompared)
                    {
                        MessageBox.Show("Date conflicts with those in the list");
                        return;
                    }
                }
            }



            var employee_calendar_date = new EmployeeCalendarDates
            {
                Payroll_period_id = selected.Payroll_period_id,
                Date = dateTimePicker1.Value,
                Category= dayTypeCb.Text,
            };
            dataGridView1.DataSource = null;
            calendarDates.Add(employee_calendar_date);

            dataGridView1.DataSource = calendarDates;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dayTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

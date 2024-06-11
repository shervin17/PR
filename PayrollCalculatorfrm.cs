using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public partial class PayrollCalculatorfrm : Form
    {
        private Employee employee1;
        private PayrollPeriod selected;
        EmployeeCalendarDatesRepo calendarDatesRepo = EmployeeCalendarDatesRepo.Instance();
        List<EmployeeCalendarDates> payable_Dates;
        DailyTimeRecordRepository dailyTimeRecordRepository = DailyTimeRecordRepository.Instance();
        List<DailyTimeRecord> daily_time_records;
        List<OverTimeEntry> over_time_entries;
        Payroll payrollObj;
        EmployeePayrollDetails payrollDetails;
        Leaves leaves;
        private AttendanceSummary attendanceSummary;
        private Leaves for_update_leaves;
        private int allowed_leaves;
        private int no_leaves_to_be_used;
        private decimal grosspay;
        private decimal semi_monthly;
        private decimal adjustments;
        private decimal overTimePay;
        private decimal payableOTinMins;
        private decimal withHoldingTax;
        private decimal netpay;
        private decimal total_benefits_deduction;
        private decimal final_net_pay;
        //benefits
        private SSSContribution sssContribution;
        private PhilHealthContribution philHealthContribution;
        private PagIbigContribution pagIbigContribution;
        


        public PayrollCalculatorfrm(Employee employee)
        {
            InitializeComponent();
            employee1 = employee;
        }

        private void PayrollCalculatorfrm_Load(object sender, EventArgs e)
        {
            List<PayrollPeriod> periods = PayrollPeriodRepo.Instance().GetPayrollPeriods();

            comboBox1_payroll_period.DataSource = periods;

            comboBox1_payroll_period.SelectedIndexChanged += comboBox1_payroll_period_SelectedIndexChanged;

            
        }


        private void comboBox1_payroll_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            selected = comboBox1_payroll_period.SelectedItem as PayrollPeriod;
            Debug.WriteLine(selected);
            
            try 
            {
                payable_Dates = calendarDatesRepo.GetPayableDates(selected.Payroll_period_id);
                daily_time_records = dailyTimeRecordRepository.findDTRbyEmployeeID(employee1.Id, selected);
                payrollDetails = EmployeePayrollDetailsRepo.Instance().getByID(employee1.Payroll_details_id);
                leaves = LeavesRepo.Instance().FindByID(employee1.Leaves_id);
                over_time_entries = OverTimeEntryRepo.Instance().GetOverTimeEntriesById(employee1.Id, selected);
                over_time_entries.ForEach(x => Debug.WriteLine(x.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("an error has occured "+ex.Message);
            }

            payrollObj = new Payroll(daily_time_records, payable_Dates,payrollDetails,over_time_entries);
            attendanceSummary = payrollObj.GetAttendanceSummary();
            semi_monthly = Math.Round(payrollDetails.Salary / 2, 2);
            populateFields();

            if (over_time_entries.Count > 0)
            {
                decimal []  OTinfo= payrollObj.getOTComputation();
                payableOTinMins = OTinfo[0];
                overTimePay = OTinfo[1];

                PayableOTmin_Field.Text = payableOTinMins.ToString();
                OvertimePay_field.Text = overTimePay.ToString();
            }

            allowed_leaves = attendanceSummary.NumberOfAbsents == 0? 5: attendanceSummary.NumberOfAbsents + 5;

            grosspay = semi_monthly + overTimePay - (attendanceSummary.DeductionDueToLate + attendanceSummary.DeductionDueToUndertime);
            GrossPayField.Text= grosspay.ToString();
        }

        public void populateFields() 
        {
         days_of_work_Field.Text= payable_Dates.Count.ToString();
         Holiday_Credited_field.Text= attendanceSummary.HolidayCredited.ToString();
         Special_Hol_field.Text=attendanceSummary.SpecialHolidayCredited.ToString();
         PayableOTmin_Field.Text = " ";
         OvertimePay_field.Text = " ";
         Semi_Monthly_Salary_field.Text= semi_monthly.ToString();
         days_absent_field.Text= attendanceSummary.NumberOfAbsents.ToString();
         TotalMinsLate_field.Text=attendanceSummary.TotalMinsLate.ToString();
         DeductibleLateMins_Field.Text= attendanceSummary.DeductibleMinsLate.ToString();
         Deduction_due_toLate_field.Text= attendanceSummary.DeductionDueToLate.ToString();
         TotalUnderTimeMins_Field.Text=attendanceSummary.TotalUndertimeMins.ToString();
         Deductible_under_time_mins_field.Text = attendanceSummary.DeductibleUndertimeMins.ToString();
         Deduction_due_to_underTimeField.Text = attendanceSummary.DeductionDueToUndertime.ToString();

            VL_numeric.Maximum = leaves.Remaining_VL;
            SL_numeric.Maximum = leaves.Remaining_SL;
            Emergency_Leave_numeric.Maximum = leaves.Emergency_leave;
            Bereavement_numeric.Maximum = 5;
        }

        public void updateLeavesOnUSe(object sender, EventArgs e) {

            if (payrollDetails == null)
            {
                clearNumericFields();
                return;
            }
            if(no_leaves_to_be_used == allowed_leaves)
            {
                clearNumericFields();
                return;
            }

            no_leaves_to_be_used = (int)(VL_numeric.Value + SL_numeric.Value + Bereavement_numeric.Value + Emergency_Leave_numeric.Value);

            adjustments = no_leaves_to_be_used * payrollDetails.Daily_rate;
            AdjustmentAmountField.Text = (adjustments.ToString());
        }
        public void clearNumericFields() 
        {
            VL_numeric.Value = 0;
            SL_numeric.Value = 0;
            Bereavement_numeric.Value = 0;
            Emergency_Leave_numeric.Value = 0;
            AdjustmentAmountField.Text = " ";
            no_leaves_to_be_used = 0;
        }

        private void button5_Click(object sender, EventArgs e) 
        {
            //apply leaves button
            withHoldingTax = 0;
            WithHoldingTaxField.Text = "";

            DialogResult dialogResult = MessageBox.Show("This will consume leaves . Do you wish to proceed?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dialogResult == DialogResult.No) {
                clearNumericFields() ;
                return;
            }

            for_update_leaves = new Leaves
            {
                Leaves_id = leaves.Leaves_id,
                Remaining_SL = leaves.Remaining_SL - SL_numeric.Value,
                Remaining_VL = leaves.Remaining_VL - VL_numeric.Value,
                Emergency_leave = leaves.Emergency_leave - (int)Emergency_Leave_numeric.Value,
                Bereavement_leave_used = leaves.Bereavement_leave_used + (int) Bereavement_numeric.Value,
            };
            MessageBox.Show(for_update_leaves.ToString() );
            updateGrossPay();
        }
        private void updateGrossPay() {

            grosspay = semi_monthly + overTimePay + adjustments -( attendanceSummary.DeductionDueToLate + attendanceSummary.DeductionDueToUndertime);
            GrossPayField.Text= grosspay.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WithholdingTaxCalculator withholdingTaxCalculator = new WithholdingTaxCalculator();

            withHoldingTax = withholdingTaxCalculator.GetSemiMonthlyTax( grosspay );

            WithHoldingTaxField.Text= withHoldingTax.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //computeNetpay
            if (netpay != 0)
            {
                netpay = netpay - withHoldingTax;
            }
            else {
                netpay = grosspay - withHoldingTax;
            }
            netPayField.Text= netpay.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //compute benefits
            decimal prev_grosspay = 25000m;
            decimal monthly_grosspay = prev_grosspay + grosspay;
            
            pagIbigContribution = PagIbigContributionCalculator.GetPagIbigContribution( monthly_grosspay );
            philHealthContribution = PhilHealthContribCalculator.GetPhilHealthContribution(monthly_grosspay);
            sssContribution = SSSContributionCalculator.ComputeSSSContribution( monthly_grosspay );

            total_benefits_deduction = pagIbigContribution.EmployeeShare + philHealthContribution.EmployeeShare + sssContribution.EmployeeShare;

            Previous_Grosspay_field.Text = prev_grosspay.ToString();
            Philhealth_Deduction_field.Text = philHealthContribution.EmployeeShare.ToString();
            SSSdeductionField.Text = sssContribution.EmployeeShare.ToString();
            PagIbigDeduction_field.Text= pagIbigContribution.EmployeeShare.ToString();

            TotalBenefitsDeductionField.Text = total_benefits_deduction.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            netpay = grosspay - total_benefits_deduction;
            MessageBox.Show($"current netpay excluding withHolding tax: {netpay}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //submit
            if (withHoldingTax == 0) {
                MessageBox.Show("Submitting payroll needs withholding tax computed");
                return;
            }
            final_net_pay = grosspay - total_benefits_deduction - withHoldingTax;
        }
        public void AddPayrollTransaction(PayrollTransaction payrollTransaction)
        {

            using (SqlConnection connection = DBConnection.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        string sqlAttendance = " insert into AttendaceSummary values(@absents,@deductibleMinslate,@deductibleUndertimeMins,@deductionDueToLate,@deductionDueToUndertime,@holidayCredited,@specialHolidayCredited,@totalMinsLate,@totalUndertimeMins);" +
                            " select cast(scope_identity() as int);";

                        var attendanceObj = new
                        {
                            absents = attendanceSummary.NumberOfAbsents,
                            deductibleMinslate = attendanceSummary.DeductibleMinsLate,
                            deductibleUndertimeMins = attendanceSummary.DeductibleUndertimeMins,
                            deductionDueToLate = attendanceSummary.DeductionDueToLate,
                            deductionDueToUndertime = attendanceSummary.DeductionDueToUndertime,
                            holidayCredited = attendanceSummary.HolidayCredited,
                            specialHolidayCredited = attendanceSummary.SpecialHolidayCredited,
                            totalMinsLate = attendanceSummary.TotalMinsLate,
                            totalUndertimeMins = attendanceSummary.TotalUndertimeMins,
                        };
                        int attendance_summary_id = connection.QuerySingle<int>(sqlAttendance,attendanceObj,transaction);
                        //

                        string sssinsertQuery = "insert into SSSContribution values (@employeeShare,@employerShare,@TotalContribution); select cast(scope_identity() as int);";


                        int sss_new_id = connection.QuerySingle<int>(sssinsertQuery,sssContribution,transaction);

                        //
                        string pagIbiginsert = "insert into PagibigContribution values (@employeeShare,@employerShare,@TotalContribution); select cast(scope_identity() as int);";

  
                      
                        int pagIbig_new_id = connection.QuerySingle<int>(pagIbiginsert, pagIbigContribution, transaction);

                        string philhealth_insert = "insert into PhilHealthContribution values (@employeeShare,@employerShare,@TotalContribution); select cast(scope_identity() as int);";

                        int philhealt_new_id = connection.QuerySingle<int>(philhealth_insert,pagIbigContribution, transaction);



                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back due to an error: " + ex.Message);
                        MessageBox.Show("Transaction rolled back due to an error: " + ex.Message);
                    }


                }


            }


        }
    }

    public class PayrollTransaction {
       public int Transaction_id { get; set; }
       public int Employee_id { get; set; }
       public int Payroll_period_id{ get; set;}
       public int AttendanceSummary_id { get; set; }
       public decimal OT_Pay { get; set; }
       private decimal Total_benefits_deduction { get; set; }
       public decimal Grosspay { get; set; }
       public decimal WithHoldingTax { get;set; }

       public int Sss_contrib_id { get; set; }
       public int PagIbig_contrib_id { get;set; }

       public int Philhealht_contrib_id { get;set;}
       public decimal NetPay { get; set; }
    }

}

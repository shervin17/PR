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
        private decimal balance;
        private decimal semi_monthly;
        private decimal adjustments;
        private decimal overTimePay;
        private decimal payableOTinMins;
        private decimal withHoldingTax;
        private decimal netpay;
        private decimal total_benefits_deduction;
       
        //benefits
        private SSSContribution sssContribution;
        private PhilHealthContribution philHealthContribution;
        private PagIbigContribution pagIbigContribution;
        private decimal prev_grosspay;
        private decimal Hol_pay;
        private decimal subject_to_wt_Tax;
        private bool isWTaxcomputed;
        private decimal final_net_pay;

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
            Debug.WriteLine(attendanceSummary.ToString());
            semi_monthly = payrollObj.Semi_monthly;
            Hol_pay = payrollObj.Holidays_pay;
            populateFields();

            if (over_time_entries.Count > 0)
            {
                decimal []  OTinfo= payrollObj.getOTComputation();
                payableOTinMins = OTinfo[0];
                overTimePay = OTinfo[1];
                decimal OT_hours = payableOTinMins / 60;
                PayableOTmin_Field.Text = OT_hours.ToString();
                OvertimePay_field.Text = overTimePay.ToString();
            }

           
            int number_of_absents = 15 -attendanceSummary.NumberOfPresents;

            allowed_leaves = number_of_absents == 0? 5:number_of_absents + 5;
            Hol_pay = payrollObj.Holidays_pay;
            balance = semi_monthly + overTimePay +Hol_pay - (attendanceSummary.DeductionDueToLate + attendanceSummary.DeductionDueToUndertime);
            balance_field.Text= balance.ToString();
        }

        public void populateFields() 
        {
         

         Pays_From_holiday_field.Text= Hol_pay.ToString();
         Holiday_Credited_field.Text= attendanceSummary.HolidayCredited.ToString();
         Special_Hol_field.Text=attendanceSummary.SpecialHolidayCredited.ToString();
         PayableOTmin_Field.Text = "0";
         OvertimePay_field.Text = "0";
         Semi_Monthly_Salary_field.Text= semi_monthly.ToString();
         days_present_Field.Text= attendanceSummary.NumberOfPresents.ToString();
         DeductibleLateMins_Field.Text= (attendanceSummary.DeductibleMinsLate/60m).ToString();
         Deduction_due_toLate_field.Text= attendanceSummary.DeductionDueToLate.ToString();
         Deductible_under_time_mins_field.Text = (attendanceSummary.DeductibleUndertimeMins / 60m).ToString();
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

            balance = semi_monthly + overTimePay + adjustments -( attendanceSummary.DeductionDueToLate + attendanceSummary.DeductionDueToUndertime);
            balance_field.Text= balance.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //compute withholding tax btn
            WithholdingTaxCalculator withholdingTaxCalculator = new WithholdingTaxCalculator();

            withHoldingTax = withholdingTaxCalculator.GetSemiMonthlyTax( subject_to_wt_Tax );

            WithHoldingTaxField.Text= withHoldingTax.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //compute netpay button
            netpay = balance - total_benefits_deduction - withHoldingTax;


            netPayField.Text= netpay.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //compute benefits
            decimal.TryParse(Previous_Grosspay_field.Text, out decimal value);
            if (value > 0) { prev_grosspay = value; }
            decimal monthly_grosspay = prev_grosspay + balance;
            Debug.WriteLine($"balace {monthly_grosspay}");

            pagIbigContribution = PagIbigContributionCalculator.GetPagIbigContribution( monthly_grosspay );
            philHealthContribution = PhilHealthContribCalculator.GetPhilHealthContribution(monthly_grosspay);
            sssContribution = SSSContributionCalculator.ComputeSSSContribution( monthly_grosspay );
            Debug.WriteLine(pagIbigContribution.ToString());
            Debug.WriteLine(philHealthContribution.ToString());
            Debug.WriteLine(sssContribution.ToString());

            total_benefits_deduction = pagIbigContribution.EmployeeShare + philHealthContribution.EmployeeShare + sssContribution.EmployeeShare;

            Previous_Grosspay_field.Text = prev_grosspay.ToString();
            Philhealth_Deduction_field.Text = philHealthContribution.EmployeeShare.ToString();
            SSSdeductionField.Text = sssContribution.EmployeeShare.ToString();
            PagIbigDeduction_field.Text= pagIbigContribution.EmployeeShare.ToString();

            TotalBenefitsDeductionField.Text = total_benefits_deduction.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (total_benefits_deduction == 0)
            {

                subject_to_wt_Tax = balance;
            }
            else
            {  subject_to_wt_Tax= Math.Round(balance - total_benefits_deduction); }

            subject_to_witholdimg.Text = subject_to_wt_Tax.ToString();
            isWTaxcomputed = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //submit
            if (withHoldingTax == 0  && !isWTaxcomputed) {
                MessageBox.Show("Submitting payroll needs withholding tax computed");
                return;
            }
            final_net_pay = balance - total_benefits_deduction - withHoldingTax;

            AddPayrollTransaction();
        }
        public void AddPayrollTransaction()
        {

            using (SqlConnection connection = DBConnection.getConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        

                        string sqlAttendance = " insert into AttendanceSummary values(@presents,@deductibleMinslate,@deductibleUndertimeMins,@deductionDueToLate,@deductionDueToUndertime,@holidayCredited,@specialHolidayCredited,@totalMinsLate,@totalUndertimeMins);" +
                            " select cast(scope_identity() as int);";

                        var attendanceObj = new
                        {
                            presents = attendanceSummary.NumberOfPresents,
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

                        int philhealth_new_id = connection.QuerySingle<int>(philhealth_insert,pagIbigContribution, transaction);

                        string payrollQuery = " insert into PayrollTransaction values(@emp_id, @payroll_period_id, @attendance, @OTpay , @benefits , @Grosspay,@Wtax,@sss, @pagIbig, @philhealth, @netpay)";

                        var payrollparam = new {
                            emp_id = employee1.Id,
                            payroll_period_id = selected.Payroll_period_id,
                            attendance = attendance_summary_id,
                            OTpay = overTimePay,
                            benefits = total_benefits_deduction,
                            Grosspay = balance,
                            Wtax = withHoldingTax,
                            sss = sss_new_id,
                            pagIbig = pagIbig_new_id,
                            philhealth = philhealth_new_id,
                            netpay = netpay,
                        };

                        connection.Execute(payrollQuery, payrollparam,transaction);
                        transaction.Commit();
                        MessageBox.Show("Payroll transaction saved.");
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

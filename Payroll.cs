using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PayrollV3
{
    public class Payroll
    {


        private List<DailyTimeRecord> records;
        private Leaves leaves;
        private List<EmployeeCalendarDates> calendarDates;
        private EmployeePayrollDetails payroll_details;
        private List<OverTimeEntry> overTimeEntries;
        private decimal hourly_rate;
        private decimal daily_rate;
        private const decimal REGULAR_OT_MULTIPLIER = 1.25m;
        private const decimal SPECIAL_OT_MULTIPLIER = 1.3m;
        private const decimal HOLIDAY_OT_MULTIPLIER = 1.5m;
        private int total_mins_late;
        private int total_undertime_mins;
        private decimal _semiMonthly;
        private decimal _holidaysPay;

        public decimal Semi_monthly
        {
            get => Math.Round(_semiMonthly, 2); // Round to 2 decimal places
            set => _semiMonthly = value;
        }

        public decimal Holidays_pay
        {
            get => Math.Round(_holidaysPay, 2); // Round to 2 decimal places
            set => _holidaysPay = value;
        }


        public Payroll(List<DailyTimeRecord> records, List<EmployeeCalendarDates> calendarDates,EmployeePayrollDetails payrollDetails,List<OverTimeEntry> overTimeEntries ) 
        { 
        this.records = records;
        this.calendarDates = calendarDates;
        this.overTimeEntries = overTimeEntries;
        hourly_rate = payrollDetails.Hourly_rate;
        daily_rate = payrollDetails.Daily_rate;
        }

        public decimal[] getOTComputation() {

            int TotalPayableOTmins = 0;
            decimal OTpay = 0;


            foreach(OverTimeEntry entry1 in overTimeEntries)
            {
                int OTmins = getOTmins(entry1);
                TotalPayableOTmins += OTmins;
                decimal rate = 0m;
               

                foreach (EmployeeCalendarDates calendarDates in calendarDates) {

                    if (entry1.Date == calendarDates.Date)
                    {
                        rate = calendarDates.Category == "SPECIAL_HOLIDAY" ? SPECIAL_OT_MULTIPLIER : calendarDates.Category == "HOLIDAY" ? HOLIDAY_OT_MULTIPLIER : 0;
                        break;
                    }
                }
                if (rate == 0m)
                    rate = REGULAR_OT_MULTIPLIER;  
                
                OTpay += computeOTpay(OTmins, rate);
            }
            return new decimal[] { TotalPayableOTmins,OTpay};
        }
        
        public AttendanceSummary GetAttendanceSummary() {
            AttendanceSummary summary = null;
            int number_of_days= Math.Abs(records.Count);
            int deductible_mins_late = 0;
            int deductible_undertime_mins = 0;
            decimal deduction_due_to_late= 0;
            decimal deduction_due_to_undertime = 0;
            int holiday_credited = 0;
            int special_holiday_credited = 0;


            foreach( var record  in records )
            {

                foreach (var calendar_date in calendarDates) {

                    if (calendar_date.Date == record.Date) {
                        if (calendar_date.Category == "HOLIDAY")
                        { holiday_credited++;
                            Holidays_pay += daily_rate;
                        }
                        if (calendar_date.Category == "SPECIAL_HOLIDAY")
                        { special_holiday_credited += 1;
                            Holidays_pay += (daily_rate * .30m);
                        }
                    }

                    }
                int late_min = GetLateMins(record);
                if (late_min > 0)
                {
                    deductible_mins_late += late_min;
                    deduction_due_to_late += computeLateDeduction(late_min);
                }

                int undertime_min = GetUnderTimeMins(record);
                if (undertime_min > 0)
                {
                    deductible_undertime_mins += undertime_min;
                    deduction_due_to_undertime += ComputeUndertimeDeductions(undertime_min);
                }
                Semi_monthly += daily_rate;
            }



            summary = new AttendanceSummary
            {
            NumberOfPresents= number_of_days,
            DeductibleMinsLate= deductible_mins_late,
            DeductibleUndertimeMins= deductible_undertime_mins,
            DeductionDueToLate=deduction_due_to_late,
            DeductionDueToUndertime= deduction_due_to_undertime,
            HolidayCredited= holiday_credited,
            SpecialHolidayCredited= special_holiday_credited,
            TotalMinsLate= total_mins_late,
            TotalUndertimeMins =total_undertime_mins,
            };


            return summary;
        }
        private decimal ComputeUndertimeDeductions(int mins)
        {
            decimal deduction_per_minute = hourly_rate / 60;
            return Math.Round((deduction_per_minute * mins), 2, MidpointRounding.AwayFromZero);
        }

        private int GetUnderTimeMins(DailyTimeRecord record)
        {
            int underTimeMins = 0;
           int time = (int)(record.Time_out - record.Shift_out).TotalMinutes;
            if (time < 0)
            {
               total_undertime_mins += Math.Abs(time);
            }
            if (time < -5)
                underTimeMins = Math.Abs(time);
            return underTimeMins;
        }


        private int GetLateMins(DailyTimeRecord record)
        {

            TimeSpan lateness = record.Time_in - record.Shift_in;

            int late_mins = (int)lateness.TotalMinutes;
            if (late_mins > 0)
            {
                total_mins_late += late_mins;
            }

            if (late_mins <= 5)
            {
                return 0;
            }

            return late_mins;
        }
        private decimal computeLateDeduction(int minutes)
        {
            decimal deduction_per_minute = hourly_rate / 60;
            return Math.Round((deduction_per_minute * minutes), 2,MidpointRounding.AwayFromZero);
        }
        private int getOTmins(OverTimeEntry timeEntry)
        {
            int OT_mins = (int)(timeEntry.Time_out - timeEntry.Time_in).TotalMinutes;
            if (OT_mins < 60)
                return 0;
            return (OT_mins/30 )* 30;
        }


        private decimal computeOTpay(int minutes, decimal overtimeRate)
        {
            Debug.WriteLine("compute OT "+minutes.ToString() + " " + overtimeRate.ToString());
           
            int hours = minutes/60;

            decimal OTpay = hourly_rate * hours * overtimeRate;

            return Math.Round(OTpay,2, MidpointRounding.AwayFromZero);
        }
    }
    public class AttendanceSummary
    {   
        public int Attendance_summary_id { get; set; }
        public int NumberOfPresents { get; set; }
        public int DeductibleMinsLate { get; set; }
        public int DeductibleUndertimeMins { get; set; }
        public decimal DeductionDueToLate { get; set; }
        public decimal DeductionDueToUndertime { get; set; }
        public int HolidayCredited { get; set; }
        public int SpecialHolidayCredited { get; set; }
        public int TotalMinsLate {get; set; }
        public int TotalUndertimeMins { get;set; }
        public override string ToString()
        {
            return $"Number of Absents: {NumberOfPresents}, " +
                   $"Deductible Minutes Late: {DeductibleMinsLate}, " +
                   $"Deductible Undertime Minutes: {DeductibleUndertimeMins}, " +
                   $"Deduction Due to Late: {DeductionDueToLate}, " +
                   $"Deduction Due to Undertime: {DeductionDueToUndertime}, " +
                   $"Holiday Credited: {HolidayCredited}, " +
                   $"Special Holiday Credited: {SpecialHolidayCredited}, " +
                   $"Total Minutes Late: {TotalMinsLate}, " +
                   $"Total Undertime Late: {TotalUndertimeMins}";
        }
    }

}


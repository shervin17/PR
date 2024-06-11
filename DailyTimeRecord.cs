using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollV3
{
    public class DailyTimeRecord
    {
        public int Id { get; set; }
        public int Employee_id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time_in { get; set; }
        public DateTime Time_out { get; set;}

        public DateTime Shift_in { get; set; }  
        public DateTime Shift_out {  get; set; }
        public string Status { get; set; }
    }
}

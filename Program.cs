using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollV3
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*  Application.Run(new frmCreateEmployeeRecord());*/
            /*Application.Run(new DailyTimeRecordApp());*/
            /*Application.Run(new Payrollform());*/
            /*  Application.Run(new UpdatePayrollDaysfrm());*/
            /*  Application.Run(new LogForm());*/
            /*  Application.Run(new Form2());*/
            /*    Application.Run(new EmployeeLogin());*/
            /*    Application.Run(new ManageEmployeeLogins());*/
            /* Application.Run(new AdminLogin());*/
            Application.Run(new ManForm());

        }
    }
}

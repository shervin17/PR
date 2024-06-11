using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollV3
{
    public class PhilHealthContribCalculator
    {
        private const decimal min_contribution = 500m;
        private const decimal max_contribution = 5000m;
        private const decimal contribution_rate = .05m;

        public static PhilHealthContribution GetPhilHealthContribution(decimal income)
        {


            decimal contribution = income <= 10000m ? min_contribution :
                                   income >= 100000m ? max_contribution :
                                   income * contribution_rate;


            PhilHealthContribution philHealthContribution = new PhilHealthContribution()
            {
                EmployerShare = Math.Round(contribution / 2, 2),
                EmployeeShare = Math.Round(contribution / 2, 2),
                TotalContribution = Math.Round(contribution, 2),

            };

            return philHealthContribution;

        }
    }

    public class PhilHealthContribution
    {
        public int Id { get; set; } 
        public decimal EmployerShare { get; set; }
        public decimal EmployeeShare { get; set; }
        public decimal TotalContribution { get; set; }

    }
}

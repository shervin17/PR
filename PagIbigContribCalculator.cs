using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollV3
{
    public class PagIbigContributionCalculator
    {
        private const decimal MinContribution = 200m;
        private const decimal EmployerRate = 0.02m;
        private const decimal EmployeeRateBelow1500 = 0.01m;
        private const decimal EmployeeRateAbove1500 = 0.02m;

        public static PagIbigContribution GetPagIbigContribution(decimal income)
        {
            decimal contributionRate = income <= 1500m ? EmployeeRateBelow1500 : EmployeeRateAbove1500;

            // Calculate employee contribution
            decimal employeeContribution = income * contributionRate;
            if (employeeContribution < MinContribution)
            {
                employeeContribution = MinContribution;
            }

            // Calculate employer contribution
            decimal employerContribution = employeeContribution > MinContribution ? income * EmployerRate : MinContribution;

            // Calculate total contribution
            decimal totalContribution = employeeContribution + employerContribution;

            PagIbigContribution pagIbigContribution = new PagIbigContribution()
            {
                EmployeeShare = Math.Round(employeeContribution, 2, MidpointRounding.AwayFromZero),
                EmployerShare = Math.Round(employerContribution, 2, MidpointRounding.AwayFromZero),
                TotalContribution = Math.Round(totalContribution, 2, MidpointRounding.AwayFromZero),
            };

            return pagIbigContribution;
        }
    }

    public class PagIbigContribution
    {
        public int Id { get; set; }
        public decimal EmployeeShare { get; set; }
        public decimal EmployerShare { get; set; }
        public decimal TotalContribution { get; set; }
        public override string ToString()
        {
            return $"PagIbig contribution : Employee Share: {EmployeeShare}, Employer Share: {EmployerShare}, Total Contribution: {TotalContribution}";
        }
    }
}

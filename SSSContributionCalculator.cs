using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollV3
{
    public class SSSContributionCalculator
    {
        private const decimal MinMonthlySalaryCredit = 4000m;
        private const decimal MaxMonthlySalaryCredit = 30000m;
        private const decimal EmployerShareRate = 0.095m;
        private const decimal EmployeeShareRate = 0.045m;
        private const decimal TotalContributionRate = 0.14m;

        public static SSSContribution ComputeSSSContribution(decimal income)
        {
            decimal MSC = income < MinMonthlySalaryCredit ? MinMonthlySalaryCredit : (income > MaxMonthlySalaryCredit ? MaxMonthlySalaryCredit : income);

            SSSContribution sssContribution = new SSSContribution
            {
                EmployerShare = Math.Round(MSC * EmployerShareRate, 2, MidpointRounding.AwayFromZero),
                EmployeeShare = Math.Round(MSC * EmployeeShareRate, 2, MidpointRounding.AwayFromZero),
                TotalContribution = Math.Round(MSC * TotalContributionRate, 2, MidpointRounding.AwayFromZero)
            };

            return sssContribution;
        }
    }

    public class SSSContribution
    {
        public int Id { get;set; }
        public decimal EmployeeShare { get; set; }
        public decimal EmployerShare { get; set; }
        public decimal TotalContribution { get; set; }

        public override string ToString()
        {
            return $" SSSContribution  :Employee Share: {EmployeeShare}, Employer Share: {EmployerShare}, Total Contribution: {TotalContribution}";
        }
    }
}

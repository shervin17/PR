using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollV3
{
    public class WithholdingTaxCalculator
    {

        List<TaxBracket> taxBrackets;

        public WithholdingTaxCalculator()
        {

/*            taxBrackets = new List<TaxBracket>();
            taxBrackets.Add(new TaxBracket("First", 0m, 250000m, 0m, 0m, 0m));
            taxBrackets.Add(new TaxBracket("Second", 250001m, 400000m, 0m, 0.15m, 0m));
            taxBrackets.Add(new TaxBracket("Third", 400001m, 800000m, 22500m, 0.20m, 22500m));
            taxBrackets.Add(new TaxBracket("Fourth", 800001m, 2000000m, 102500m, 0.25m, 125000m));
            taxBrackets.Add(new TaxBracket("Fifth", 2000001m, 8000000m, 402500m, 0.30m, 527500m));
            taxBrackets.Add(new TaxBracket("Sixth", 8000001m, decimal.MaxValue, 2202500m, 0.35m, 0m));*/



        }
        public decimal GetSemiMonthlyTax(decimal semi_monthly_income)
        {
            decimal income = semi_monthly_income;
            decimal wtax;

            if (income <= 10417)
                wtax= 0.00m;
            else if (income <= 16666)
                wtax= 0.00m + 0.15m * (income - 10417);
            else if (income <= 33332)
                wtax= 937.50m + 0.20m * (income - 16667);
            else if (income <= 83332)
                wtax= 4270.70m + 0.25m * (income - 33333);
            else if (income <= 333332)
                wtax= 16770.70m + 0.30m * (income - 83333);
            else
                wtax= 91770.70m + 0.35m * (income - 333333);


            return Math.Round(wtax,2, MidpointRounding.AwayFromZero);

            /* decimal approx_annual_income = semi_monthly_income * 24;
             decimal monthly_tax = 0;
             taxBrackets.ForEach(bracket =>
             {
                 if (approx_annual_income > bracket.RangeFrom && approx_annual_income <= bracket.RangeTo)
                 {

                     monthly_tax = (bracket.Base_tax + (approx_annual_income - bracket.RangeFrom) * bracket.Tax_rate) / 24;
                 }
             });

             Debug.WriteLine("" + semi_monthly_income + " " + approx_annual_income + " " + monthly_tax);

             return Math.Round(monthly_tax, 3);*/
        }


        private class TaxBracket
        {
            public string Bracket { get; set; }
            public decimal RangeFrom { get; set; }
            public decimal RangeTo { get; set; }
            public decimal Base_tax { get; set; }
            public decimal Tax_rate { get; set; }
            public decimal Cummulative_tax { get; set; }

            public TaxBracket(string bracket, decimal rangeFrom, decimal rangeTo, decimal base_tax, decimal tax_rate, decimal cummulative_tax)
            {
                this.Bracket = bracket;
                this.RangeFrom = rangeFrom;
                this.RangeTo = rangeTo;
                this.Base_tax = base_tax;
                this.Tax_rate = tax_rate;
                this.Cummulative_tax = cummulative_tax;
            }
        }

    }


}

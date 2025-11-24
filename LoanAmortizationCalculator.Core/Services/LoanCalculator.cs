using LoanAmortizationCalculator.Core.Domain;
using LoanAmortizationCalculator.Core.Interfaces;

namespace LoanAmortizationCalculator.Core.Services;

public class LoanCalculator : ILoanCalculator
{
    public LoanResult Calculate(LoanInput input)
    {
        // 1. Calculate fixed monthly payment (PMT)
        // If interest is 0, simply divide amount by number of months
        decimal monthlyPayment;
        if (input.InterestRate == 0)
        {
            monthlyPayment = input.Amount / input.TotalMonths;
        }
        else
        {
            var rate = (double)input.MonthlyInterestRate;
            var n = input.TotalMonths;
            // PMT Formula
            var factor = Math.Pow(1 + rate, n);
            monthlyPayment = input.Amount * (decimal)((rate * factor) / (factor - 1));
        }

        // Rounding the payment to 2 decimals immediately is common practice,
        // but it's more precise to keep more decimals until the end and round in the table.
        // However, the bank charges you exactly 2 decimals each month.
        monthlyPayment = Math.Round(monthlyPayment, 2);

        var result = new LoanResult
        {
            MonthlyPayment = monthlyPayment,
            Schedule = new List<AmortizationRow>()
        };

        // 2. Generate Amortization Schedule
        decimal currentBalance = input.Amount;
        decimal totalInterest = 0;

        for (int i = 1; i <= input.TotalMonths; i++)
        {
            // Interest for this month = Remaining balance * Monthly interest rate
            decimal interestPayment = Math.Round(currentBalance * input.MonthlyInterestRate, 2);

            // Principal = Payment - Interest
            decimal principalPayment = monthlyPayment - interestPayment;

            // Correction for the last month (due to rounding)
            // If remaining balance is less than the principal that would be paid, adjust.
            if (currentBalance - principalPayment < 0 || i == input.TotalMonths)
            {
                principalPayment = currentBalance;
                monthlyPayment = principalPayment + interestPayment;
            }

            currentBalance -= principalPayment;
            totalInterest += interestPayment;

            result.Schedule.Add(new AmortizationRow
            {
                MonthIndex = i,
                Date = input.StartDate.AddMonths(i),
                Payment = monthlyPayment,
                Interest = interestPayment,
                Principal = principalPayment,
                RemainingBalance = currentBalance,
                ExtraPayment = 0 // For now 0
            });

            if (currentBalance <= 0) break;
        }

        // 3. Fill in summary data
        result.TotalInterestPaid = totalInterest;
        result.TotalAmountPaid = input.Amount + totalInterest;
        result.PayoffDate = result.Schedule.Last().Date;

        return result;
    }
}
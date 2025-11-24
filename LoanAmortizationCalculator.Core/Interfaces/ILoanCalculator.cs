using LoanAmortizationCalculator.Core.Domain;

namespace LoanAmortizationCalculator.Core.Interfaces;

public interface ILoanCalculator
{
    LoanResult Calculate(LoanInput input);
}
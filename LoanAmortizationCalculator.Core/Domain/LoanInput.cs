namespace LoanAmortizationCalculator.Core.Domain;

public record LoanInput
{
    public decimal Amount { get; set; }
    public decimal InterestRate { get; set; }
    public int DurationInYears { get; set; }
    public DateOnly StartDate { get; set; }

    public decimal MonthlyInterestRate => (InterestRate / 100) / 12;

    public int TotalMonths => DurationInYears * 12;
}
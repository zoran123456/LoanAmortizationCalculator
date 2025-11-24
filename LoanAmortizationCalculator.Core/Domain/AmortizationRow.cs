namespace LoanAmortizationCalculator.Core.Domain;

public record AmortizationRow
{
    public int MonthIndex { get; set; }
    public DateOnly Date { get; set; }
    public decimal Payment { get; set; }            // Total installment (Annuity)
    public decimal Principal { get; set; }          // Portion that goes to principal
    public decimal Interest { get; set; }           // Portion that goes to interest
    public decimal RemainingBalance { get; set; }   // Remaining debt after payment
    public decimal ExtraPayment { get; set; }       // Early repayment (will be used later)
}
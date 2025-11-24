namespace LoanAmortizationCalculator.Core.Domain;

public class LoanResult
{
    public decimal MonthlyPayment { get; set; }     // Base annuity
    public decimal TotalInterestPaid { get; set; }  // Total interest
    public decimal TotalAmountPaid { get; set; }    // Total paid (Principal + Interest)
    public DateOnly PayoffDate { get; set; }        // Date of last installment
    public List<AmortizationRow> Schedule { get; set; } = [];
}
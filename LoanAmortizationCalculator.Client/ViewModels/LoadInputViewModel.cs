using System.ComponentModel.DataAnnotations;
using LoanAmortizationCalculator.Core.Domain;

namespace LoanAmortizationCalculator.Client.ViewModels;

public class LoanInputViewModel
{
    [Required]
    [Range(1000, 10000000, ErrorMessage = "Amount must be between 1k and 10M")]
    public decimal Amount { get; set; } = 100000; // Default value

    [Required]
    [Range(0.1, 20, ErrorMessage = "Interest rate seems unrealistic")]
    public decimal InterestRate { get; set; } = 4.5m;

    [Required]
    [Range(1, 50, ErrorMessage = "Loan duration is typically 1-50 years")]
    public int DurationYears { get; set; } = 30;

    public DateTime? StartDate { get; set; } = DateTime.Today;

    // Method for mapping to Domain Model (Core)
    public LoanInput ToDomainModel()
    {
        return new LoanInput
        {
            Amount = this.Amount,
            InterestRate = this.InterestRate,
            DurationInYears = this.DurationYears,
            // MudDatePicker uses DateTime?, but Core needs DateOnly
            StartDate = DateOnly.FromDateTime(this.StartDate ?? DateTime.Today)
        };
    }
}
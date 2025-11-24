using LoanAmortizationCalculator.Core.Domain;

namespace LoanAmortizationCalculator.Client.Services;

public class LoanStateService
{
    // The current result of the calculation
    public LoanResult? CurrentResult { get; private set; }

    // Event to notify components when data changes
    public event Action? OnChange;

    public void SetResult(LoanResult result)
    {
        CurrentResult = result;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
using FluentAssertions;
using LoanAmortizationCalculator.Core.Domain;
using LoanAmortizationCalculator.Core.Services;

namespace LoanAmortizationCalculator.Tests;

public class LoanCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectMonthlyPayment_ForStandardLoan()
    {
        // Arrange
        var calculator = new LoanCalculator();
        var input = new LoanInput
        {
            Amount = 100000, // 100k
            InterestRate = 5.0m, // 5%
            DurationInYears = 30,
            StartDate = new DateOnly(2024, 1, 1)
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        // Expected monthly payment is 536.82
        result.MonthlyPayment.Should().Be(536.82m);

        // Total interest should be around 93,255.78
        // We provide a small precision buffer due to floating point arithmetic in Math.Pow
        result.TotalInterestPaid.Should().BeInRange(93250m, 93260m);

        // Must be exactly 360 payments
        result.Schedule.Should().HaveCount(360);

        // Last payment must bring the balance to 0
        result.Schedule.Last().RemainingBalance.Should().Be(0);
    }

    [Fact]
    public void Calculate_ShouldHandleZeroInterest()
    {
        // Arrange
        var calculator = new LoanCalculator();
        var input = new LoanInput
        {
            Amount = 12000,
            InterestRate = 0, // 0% interest
            DurationInYears = 1,
            StartDate = new DateOnly(2024, 1, 1)
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        result.MonthlyPayment.Should().Be(1000m); // 12000 / 12
        result.TotalInterestPaid.Should().Be(0);
    }
}
using Moq;

namespace CycleFor.Tests;

public class CycleForShould
{
    [Theory]
    [InlineData(0, 100)]
    [InlineData(1, 110)]
    [InlineData(2, 121)]
    public void CalculateInterest_GivenZeroMonths(int numberOfMonths, decimal expectedFinalAmount)
    {
        var calculator = new InterestCalculator(Mock.Of<IInterestCalculationObserver>());

        var interest = calculator.CalculateInterest(100, numberOfMonths, 10);

        Assert.Equal(expectedFinalAmount, interest);    
    }

    [Fact]
    public void ObserveAmountAfterEachMonth()
    {
        var observerMock = new Mock<IInterestCalculationObserver>();
        var capturedMonths = new List<int>();
        var capturedAmounts = new List<decimal>();
        observerMock.Setup(_ => _.MonthlyValue(Capture.In(capturedMonths), Capture.In(capturedAmounts)));
        
        var calculator = new InterestCalculator(observerMock.Object);

        calculator.CalculateInterest(100, 2, 10);

        Assert.Equal(new[] {1, 2}, capturedMonths);
        Assert.Equal(new[] {110m, 121m}, capturedAmounts);
    }
}

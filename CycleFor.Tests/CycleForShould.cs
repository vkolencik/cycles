namespace CycleFor.Tests;

public class CycleForShould
{
    [Theory]
    [InlineData(0, 100)]
    [InlineData(1, 110)]
    public void CalculateInterest_GivenZeroMonths(int numberOfMonths, decimal expectedFinalAmount)
    {
        var calculator = new InterestCalculator();

        var interest = calculator.CalculateInterest(100, numberOfMonths, 10);

        Assert.Equal(expectedFinalAmount, interest);    
    }
}

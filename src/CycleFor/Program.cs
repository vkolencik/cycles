namespace CycleFor;

public static class Program
{
    public static void Main(string [] args)
    {
        Console.WriteLine("hello");
    }
}

internal class InterestCalculator
{
    public decimal CalculateInterest(decimal initialAmount, int numberOfMonths, int interestRate)
    {
        var amount = initialAmount;
        for (var monthsPassed = 1; monthsPassed <= numberOfMonths; monthsPassed++)
        {
            amount *= new decimal(100 + interestRate) / 100;
        }

        return amount;
    }
}

namespace CycleFor;

public static class Program
{
    public static void Main(string [] args)
    {
        Console.Write("Zadejte počáteční hodnotu na účtu: ");
        var initialAmountInput = Console.ReadLine();
        
        if (!decimal.TryParse(initialAmountInput, out var initialAmount))
        {
            Console.Error.WriteLine("Neplatné číslo");
            Environment.Exit(-1);
        }

        var calculator = new InterestCalculator(new ConsoleInterestCalculationObserver());
        var finalAmount = calculator.CalculateInterest(initialAmount, 12, 6);

        Console.WriteLine($"Konečná částka: {finalAmount:F2}");
    }

    private class ConsoleInterestCalculationObserver : IInterestCalculationObserver
    {
        public void MonthlyValue(int monthsPassed, decimal amount)
        {
            Console.Out.WriteLine($"Částka po {monthsPassed} měsících: {amount:F212345}");
        }
    }
}

internal class InterestCalculator
{
    private readonly IInterestCalculationObserver _observer;

    public InterestCalculator(IInterestCalculationObserver observer)
    {
        _observer = observer;
    }

    public decimal CalculateInterest(decimal initialAmount, int numberOfMonths, int interestRate)
    {
        var amount = initialAmount;
        for (var monthsPassed = 1; monthsPassed <= numberOfMonths; monthsPassed++)
        {
            amount *= new decimal(100 + interestRate) / 100;
            _observer.MonthlyValue(monthsPassed, amount);
        }

        return amount;
    }
}

public interface IInterestCalculationObserver
{
    void MonthlyValue(int monthsPassed, decimal amount);
}

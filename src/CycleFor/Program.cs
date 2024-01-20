namespace CycleFor;

public static class Program
{
    public static void Main(string [] args)
    {
        Console.Write("Zadejte počáteční hodnotu na účtu: ");
        var initialAmountInput = Console.ReadLine(); // Načtení původní hodnoty
        
        if (!decimal.TryParse(initialAmountInput, out var initialAmount)) // Validace načtené hodnoty
        {
            Console.Error.WriteLine("Neplatné číslo"); // Výpis chyby při neplatné hodnotě
            Environment.Exit(-1); // Ukončení programu s chybou
        }

        var consoleObserver = new ConsoleInterestCalculationObserver(); // Vytvoření observeru na vypisování mezivýsledků
        var calculator = new InterestCalculator(consoleObserver); // Vytvoření kalkulátoru
        
        var finalAmount = calculator.CalculateInterest(initialAmount, 12, 6); // Spuštění kalkulátoru se zadanými hodnotami

        Console.WriteLine($"Konečná částka: {finalAmount:F2}"); // Vypsání konečné hodnoty
    }

    private class ConsoleInterestCalculationObserver : IInterestCalculationObserver // Observer vypisující mezivýsledky do konzole
    {
        public void MonthlyValue(int monthsPassed, decimal amount)
        {
            Console.Out.WriteLine($"Částka po {monthsPassed} měsících: {amount:F212345}"); // Výpis mezivýsledku do konzole
        }
    }
}

public interface IInterestCalculationObserver
{
    void MonthlyValue(int monthsPassed, decimal amount);
}

internal class InterestCalculator
{
    private readonly IInterestCalculationObserver _observer;

    public InterestCalculator(IInterestCalculationObserver observer)
    {
        _observer = observer;
    }

    public decimal CalculateInterest(decimal initialAmount, int numberOfMonths, int interestRatePct)
    {
        var amount = initialAmount; // Hodnota s průběžným mezivýsledkem
        for (var monthsPassed = 1; monthsPassed <= numberOfMonths; monthsPassed++) // cyklus přes jednotlivé měsíce
        {
            amount *= new decimal(100 + interestRatePct) / 100; // Výpočet částky po dalším měsíci
            _observer.MonthlyValue(monthsPassed, amount); // Notifikace observeru (vypsání mezivýsledku)
        }

        return amount; // Návrat konečné hodnoty
    }
}

using System.Globalization;

namespace CycleDoWhile;

class Program
{
    const string StopString = "stop";
        
    static void Main(string[] args)
    {
        var valueCount = 0;
        var valueSum = 0M;

        bool shouldStop = false;
        
        do
        {
            Console.Write($"Počet kalorií pro den {valueCount + 1}: "); // Zobrazení výzvy pro vstup čísla
            var dailyCaloricValueInput = Console.ReadLine(); // Načtení hodnoty 
            
            if (dailyCaloricValueInput == StopString) // Detekce vstupu ukončujícího zadání
            {
                shouldStop = true; // Nastavení flagu pro ukončení cyklu
            }
            else if (!decimal.TryParse(dailyCaloricValueInput, CultureInfo.CurrentCulture, out var dailyCaloricValue)) // Validace načtené hodnoty a převod na číslo
            {
                Console.Error.WriteLine("Neplatné číslo"); // Výpis chyby při neplatné hodnotě
                Environment.Exit(-1); // Ukončení programu s chybou
            }
            else
            {
                valueCount++; // Započtení hodnoty
                valueSum += dailyCaloricValue; // Přičtení hodnoty k celkovému součtu
            }
        } while (!shouldStop); // Opakování dokud nebyl poslední vstup "stop"

        Console.WriteLine($"Celková kalorická hodnota: {valueSum:F2}"); // Výpis celkové hodnoty

        if (valueCount > 0) // Ochrana proti dělení nulou
        {
            var averageCaloricValue = valueSum / valueCount; // Výpočet aritmetického průměru
            Console.WriteLine($"Průměrná kalorická hodnota: {averageCaloricValue:F2}"); // Výpis průměrné hodnoty
        }
    }
}

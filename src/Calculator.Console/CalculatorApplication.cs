// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

using System.Globalization;
using Calculator.Domain.Operations;

namespace Calculator.ConsoleApp;

internal sealed class CalculatorApplication
{
    private readonly IReadOnlyDictionary<string, ICalculationOperation> operations;
    private readonly TextReader input;
    private readonly TextWriter output;

    public CalculatorApplication(
        IReadOnlyDictionary<string, ICalculationOperation> operations,
        TextReader input,
        TextWriter output)
    {
        this.operations = operations;
        this.input = input;
        this.output = output;
    }

    public void Run()
    {
        output.WriteLine(value: "Kalkulator .NET 8");

        while (true)
        {
            output.WriteLine(value: "Wybierz operację: + dodawanie, - odejmowanie, * mnożenie, / dzielenie, 0 koniec");
            if (input.ReadLine() is not { } choice || choice.Trim() == "0")
            {
                output.WriteLine(value: "Do widzenia!");
                return;
            }

            if (!operations.TryGetValue(key: choice.Trim(), value: out var operation))
            {
                output.WriteLine(value: "Nieprawidłowa operacja. Wybierz +, -, * lub /.");
                continue;
            }

            if (!TryReadNumber(prompt: "Pierwsza liczba: ", number: out var firstNumber) ||
                !TryReadNumber(prompt: "Druga liczba: ", number: out var secondNumber))
            {
                output.WriteLine(value: "Zakończono: koniec danych wejściowych.");
                return;
            }

            try
            {
                var result = operation.Calculate(firstNumber: firstNumber, secondNumber: secondNumber);
                output.WriteLine(value: $"Wynik: {result.ToString(provider: CultureInfo.InvariantCulture)}");
            }
            catch (DivideByZeroException exception)
            {
                output.WriteLine(value: $"Błąd: {exception.Message}");
            }
            catch (OverflowException)
            {
                output.WriteLine(value: "Błąd: wynik przekracza zakres typu decimal.");
            }
        }
    }

    private bool TryReadNumber(string prompt, out decimal number)
    {
        while (true)
        {
            output.Write(value: prompt);
            if (input.ReadLine() is not { } text)
            {
                number = default;
                return false;
            }

            if (decimal.TryParse(
                s: text,
                style: NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint |
                    NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                provider: CultureInfo.InvariantCulture,
                result: out number))
            {
                return true;
            }

            output.WriteLine(value: "Nieprawidłowa liczba. Podaj liczbę z przecinkiem lub kropką, bez separatorów tysięcy.");
        }
    }
}

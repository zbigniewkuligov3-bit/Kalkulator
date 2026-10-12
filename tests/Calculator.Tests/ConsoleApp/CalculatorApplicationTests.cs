// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

using System.Globalization;
using Calculator.ConsoleApp;
using Calculator.Domain.Operations;
using Xunit;

namespace Calculator.Tests.ConsoleApp;

public sealed class CalculatorApplicationTests
{
    [Fact]
    public void should_accept_both_decimal_separators()
    {
        var output = RunApplication(inputText: "+\n1,5\n2.25\n0\n");

        Assert.Contains(expectedSubstring: "Wynik: 3.75", actualString: output);
    }

    [Fact]
    public void should_perform_all_operations_in_one_session()
    {
        var output = RunApplication(inputText: "+\n2\n3\n-\n2\n3\n*\n2\n3\n/\n3\n2\n0\n");

        Assert.Contains(expectedSubstring: "Wynik: 5", actualString: output);
        Assert.Contains(expectedSubstring: "Wynik: -1", actualString: output);
        Assert.Contains(expectedSubstring: "Wynik: 6", actualString: output);
        Assert.Contains(expectedSubstring: "Wynik: 1.5", actualString: output);
        Assert.Contains(expectedSubstring: "Do widzenia!", actualString: output);
    }

    [Fact]
    public void should_multiply_using_selected_operation()
    {
        var output = RunApplication(inputText: "*\n3\n4\n0\n");

        Assert.Contains(expectedSubstring: "Wynik: 12", actualString: output);
    }

    [Fact]
    public void should_preserve_fractional_division_result()
    {
        var output = RunApplication(inputText: "/\n5\n2\n0\n");

        Assert.Contains(expectedSubstring: "Wynik: 2.5", actualString: output);
    }

    [Fact]
    public void should_retry_after_invalid_operation_and_numbers()
    {
        var output = RunApplication(inputText: "%\n+\nabc\n1,2.3\n\n2\n3\n0\n");

        Assert.Contains(expectedSubstring: "Nieprawidłowa operacja.", actualString: output);
        Assert.Contains(expectedSubstring: "Nieprawidłowa liczba.", actualString: output);
        Assert.Contains(expectedSubstring: "Wynik: 5", actualString: output);
    }

    [Fact]
    public void should_continue_after_division_by_zero()
    {
        var output = RunApplication(inputText: "/\n1\n0\n+\n2\n3\n0\n");

        Assert.Contains(expectedSubstring: "Błąd: Nie można dzielić przez zero.", actualString: output);
        Assert.Contains(expectedSubstring: "Wynik: 5", actualString: output);
    }

    [Fact]
    public void should_continue_after_overflow()
    {
        var maximum = decimal.MaxValue.ToString(provider: CultureInfo.InvariantCulture);
        var output = RunApplication(inputText: $"+\n{maximum}\n1\n+\n2\n3\n0\n");

        Assert.Contains(expectedSubstring: "Błąd: wynik przekracza zakres typu decimal.", actualString: output);
        Assert.Contains(expectedSubstring: "Wynik: 5", actualString: output);
    }

    [Fact]
    public void should_exit_at_end_of_input_in_menu()
    {
        var output = RunApplication(inputText: "");

        Assert.Contains(expectedSubstring: "Do widzenia!", actualString: output);
    }

    [Fact]
    public void should_exit_at_end_of_input_in_first_number()
    {
        var output = RunApplication(inputText: "+\n");

        Assert.Contains(expectedSubstring: "Zakończono: koniec danych wejściowych.", actualString: output);
        Assert.DoesNotContain(expectedSubstring: "Wynik:", actualString: output);
    }

    [Fact]
    public void should_exit_at_end_of_input_in_second_number()
    {
        var output = RunApplication(inputText: "+\n1\n");

        Assert.Contains(expectedSubstring: "Zakończono: koniec danych wejściowych.", actualString: output);
        Assert.DoesNotContain(expectedSubstring: "Wynik:", actualString: output);
    }

    private static string RunApplication(string inputText)
    {
        var factory = new CalculationOperationFactory();
        var operations = new Dictionary<string, ICalculationOperation>
        {
            ["+"] = factory.Create(symbol: "+"),
            ["-"] = factory.Create(symbol: "-"),
            ["*"] = factory.Create(symbol: "*"),
            ["/"] = factory.Create(symbol: "/"),
        };

        using var input = new StringReader(s: inputText);
        using var output = new StringWriter(formatProvider: CultureInfo.InvariantCulture);
        var application = new CalculatorApplication(
            operations: operations,
            input: input,
            output: output);

        application.Run();
        return output.ToString();
    }
}

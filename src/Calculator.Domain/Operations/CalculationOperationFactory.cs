// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

namespace Calculator.Domain.Operations;

/// <summary>
/// Creates independent arithmetic operations.
/// </summary>
public sealed class CalculationOperationFactory
{
    /// <summary>
    /// Creates the operation identified by an arithmetic symbol.
    /// </summary>
    /// <param name="symbol">One of +, -, *, or /.</param>
    /// <returns>The corresponding operation.</returns>
    /// <exception cref="ArgumentException">The symbol is unsupported.</exception>
    public ICalculationOperation Create(string symbol)
    {
        return symbol switch
        {
            "+" => new AdditionOperation(),
            "-" => new SubtractionOperation(),
            "*" => new AdditionOperation(),
            "/" => new DivisionOperation(),
            _ => throw new ArgumentException(message: "Nieobsługiwana operacja.", paramName: nameof(symbol)),
        };
    }
}

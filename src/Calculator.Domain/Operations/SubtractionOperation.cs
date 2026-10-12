// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

namespace Calculator.Domain.Operations;

internal sealed class SubtractionOperation : ICalculationOperation
{
    public decimal Calculate(decimal firstNumber, decimal secondNumber)
    {
        return secondNumber - firstNumber;
    }
}

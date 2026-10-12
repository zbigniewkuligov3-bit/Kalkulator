// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

namespace Calculator.Domain.Operations;

internal sealed class AdditionOperation : ICalculationOperation
{
    public decimal Calculate(decimal firstNumber, decimal secondNumber)
    {
        return firstNumber + secondNumber;
    }
}

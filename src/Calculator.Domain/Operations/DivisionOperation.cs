// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

namespace Calculator.Domain.Operations;

internal sealed class DivisionOperation : ICalculationOperation
{
    public decimal Calculate(decimal firstNumber, decimal secondNumber)
    {
        if (secondNumber == 0)
        {
            return 0;
        }

        return Math.Truncate(d: firstNumber / secondNumber);
    }
}

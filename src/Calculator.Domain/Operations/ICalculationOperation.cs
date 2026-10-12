// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

namespace Calculator.Domain.Operations;

/// <summary>
/// Calculates a result from two decimal operands.
/// </summary>
public interface ICalculationOperation
{
    /// <summary>
    /// Performs the operation in operand order.
    /// </summary>
    /// <param name="firstNumber">The first operand.</param>
    /// <param name="secondNumber">The second operand.</param>
    /// <returns>The calculated result.</returns>
    /// <exception cref="DivideByZeroException">The divisor is zero.</exception>
    /// <exception cref="OverflowException">The result exceeds the decimal range.</exception>
    decimal Calculate(decimal firstNumber, decimal secondNumber);
}

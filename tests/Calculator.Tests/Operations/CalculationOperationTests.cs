// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

using Calculator.Domain.Operations;
using Xunit;

namespace Calculator.Tests.Operations;

public sealed class CalculationOperationTests
{
    [Fact]
    public void should_add_decimal_numbers()
    {
        var operation = new AdditionOperation();

        Assert.Equal(expected: 0.3m, actual: operation.Calculate(firstNumber: 0.1m, secondNumber: 0.2m));
    }

    [Fact]
    public void should_subtract_in_operand_order()
    {
        var operation = new SubtractionOperation();

        Assert.Equal(expected: -5m, actual: operation.Calculate(firstNumber: 2m, secondNumber: 7m));
    }

    [Fact]
    public void should_multiply_negative_numbers()
    {
        var operation = new MultiplicationOperation();

        Assert.Equal(expected: -7.5m, actual: operation.Calculate(firstNumber: -2.5m, secondNumber: 3m));
    }

    [Fact]
    public void should_multiply_by_zero()
    {
        var operation = new MultiplicationOperation();

        Assert.Equal(expected: 0m, actual: operation.Calculate(firstNumber: 15m, secondNumber: 0m));
    }

    [Fact]
    public void should_divide_in_operand_order()
    {
        var operation = new DivisionOperation();

        Assert.Equal(expected: 2.5m, actual: operation.Calculate(firstNumber: 5m, secondNumber: 2m));
    }

    [Fact]
    public void should_divide_zero_by_nonzero_number()
    {
        var operation = new DivisionOperation();

        Assert.Equal(expected: 0m, actual: operation.Calculate(firstNumber: 0m, secondNumber: 2m));
    }

    [Fact]
    public void should_reject_division_by_zero()
    {
        var operation = new DivisionOperation();

        Assert.Throws<DivideByZeroException>(
            testCode: () => operation.Calculate(firstNumber: 5m, secondNumber: 0m));
    }

    [Fact]
    public void should_report_addition_overflow()
    {
        var operation = new AdditionOperation();

        Assert.Throws<OverflowException>(
            testCode: () => operation.Calculate(firstNumber: decimal.MaxValue, secondNumber: 1m));
    }

    [Fact]
    public void should_report_subtraction_overflow()
    {
        var operation = new SubtractionOperation();

        Assert.Throws<OverflowException>(
            testCode: () => operation.Calculate(firstNumber: decimal.MinValue, secondNumber: 1m));
    }

    [Fact]
    public void should_report_multiplication_overflow()
    {
        var operation = new MultiplicationOperation();

        Assert.Throws<OverflowException>(
            testCode: () => operation.Calculate(firstNumber: decimal.MaxValue, secondNumber: 2m));
    }

    [Fact]
    public void should_report_division_overflow()
    {
        var operation = new DivisionOperation();

        Assert.Throws<OverflowException>(
            testCode: () => operation.Calculate(firstNumber: decimal.MaxValue, secondNumber: 0.1m));
    }

    [Fact]
    public void should_create_each_supported_operation()
    {
        var factory = new CalculationOperationFactory();

        Assert.IsType<AdditionOperation>(@object: factory.Create(symbol: "+"));
        Assert.IsType<SubtractionOperation>(@object: factory.Create(symbol: "-"));
        Assert.IsType<MultiplicationOperation>(@object: factory.Create(symbol: "*"));
        Assert.IsType<DivisionOperation>(@object: factory.Create(symbol: "/"));
    }

    [Fact]
    public void should_reject_unsupported_operation()
    {
        var factory = new CalculationOperationFactory();

        Assert.Throws<ArgumentException>(testCode: () => factory.Create(symbol: "%"));
    }
}

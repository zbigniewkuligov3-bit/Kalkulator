// © Copyright [2021-2026] Hitachi Energy. All rights reserved.

using System.Text;
using Calculator.ConsoleApp;
using Calculator.Domain.Operations;

Console.OutputEncoding = Encoding.UTF8;

var factory = new CalculationOperationFactory();
var operations = new Dictionary<string, ICalculationOperation>
{
    ["+"] = factory.Create(symbol: "+"),
    ["-"] = factory.Create(symbol: "-"),
    ["*"] = factory.Create(symbol: "*"),
    ["/"] = factory.Create(symbol: "/"),
};

var application = new CalculatorApplication(
    operations: operations,
    input: Console.In,
    output: Console.Out);

application.Run();

using NUnit.Framework;
using Reflections.Tasks.Task6.Calculator;
using Reflections.Tasks.Task6.TypeBuilders;

namespace Reflections.Tasks.Task6.Tests;

/// <summary>
/// Contains test methods for <see cref="ICalculator"/> type.
/// </summary>
[TestFixture]
public class CalculatorTypeBuilderTests
{
    /// <summary>
    /// Tests the add method of <see cref="ICalculator"/>.
    /// </summary>
    [Test]
    public void TestAdd()
    {
        CalculatorTypeBuilder builder = new CalculatorTypeBuilder();
        Type calcType = builder.BuildCalculatorType();
        object? instance = Activator.CreateInstance(calcType);

        if (instance is not null && instance is ICalculator calculator)
        {
            int sum = calculator.Add(5, 3);

            Assert.AreEqual(8, sum, "Add method failed");
        }
    }

    /// <summary>
    /// Tests the subtract method of <see cref="ICalculator"/>
    /// </summary>
    [Test]
    public void TestSubtract()
    {
        CalculatorTypeBuilder builder = new CalculatorTypeBuilder();
        Type calcType = builder.BuildCalculatorType();
        object? instance = Activator.CreateInstance(calcType);

        if (instance is not null && instance is ICalculator calculator)
        {
            int difference = calculator.Subtract(10, 4);

            Assert.AreEqual(6, difference, "Subtract method failed");
        }
    }
}
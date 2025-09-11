using InventoryManager.Parsers;
using Xunit;

namespace InventoryManager.Tests.ParserTest;

/// <summary>
/// Provide methods to test <see cref="Parser"/>
/// </summary>
public class ParserTest
{
    /// <summary>
    /// Tests tryParseValue for non decimal valid inputs
    /// </summary>
    /// <param name="input">Input value</param>
    /// <param name="type">Type of input.</param>
    /// <param name="expectedValue">Expected value.</param>
    /// <param name="expectedResult">Expected return value</param>
    [Theory]
    [InlineData("200", typeof(int), 200, true)]
    [InlineData("value", typeof(string), "value", true)]
    public void TryParseValue_ShouldReturnValue_ForValidInput(string input, Type type, object expectedValue, bool expectedResult)
    {
        bool isParsed = Parser.TryParseValue(input, type, out object value, out string errorMessage);
        Assert.Equal(expectedValue, value);
        Assert.Equal(expectedResult, isParsed);
    }

    /// <summary>
    /// Tests tryParseValue for decimal valid inputs
    /// </summary>
    /// <param name="input">Input value.</param>
    /// <param name="expectedResult">Expected value.</param>
    [Theory]
    [InlineData("200.21", true)]
    public void TryParseValue_ShouldReturnValue_ForDecimal(string input, bool expectedResult)
    {
        bool isParsed = Parser.TryParseValue(input, typeof(decimal), out object value, out string errorMessage);

        Assert.True(isParsed);
        Assert.IsType<decimal>(value);
        Assert.Equal(decimal.Parse(input), (decimal)value);
    }

    /// <summary>
    /// Tests tryParseValue for non decimal invalid inputs
    /// </summary>
    /// <param name="input">Input value</param>
    /// <param name="type">Type of input.</param>
    /// <param name="expectedValue">Expected value.</param>
    /// <param name="expectedResult">Expected return value</param>
    [Theory]
    [InlineData("200.21", typeof(int), 0, false)]
    public void TryParseValue_ShouldReturnInput_ForInValidInput(string input, Type type, object expectedValue, bool expectedResult)
    {
        bool isParsed = Parser.TryParseValue(input, type, out object value, out string errorMessage);
        Assert.Equal(expectedValue, value);
        Assert.Equal(expectedResult, isParsed);
        Assert.NotEmpty(errorMessage);
    }

    /// <summary>
    /// Tests tryParseValue for decimal valid inputs
    /// </summary>
    /// <param name="input">Input value.</param>
    /// <param name="expectedResult">Expected result.</param>
    /// <param name="expectedValue">Expected value.</param>
    [Theory]
    [InlineData("200.21a", false, 0)]
    public void TryParseValue_ShouldReturnInput_ForNonDecimal(string input, bool expectedResult, object expectedValue)
    {
        bool isParsed = Parser.TryParseValue(input, typeof(decimal), out object value, out string errorMessage);

        Assert.False(isParsed);
        Assert.Equal(0M, value);
        Assert.NotEmpty(errorMessage);
    }
}

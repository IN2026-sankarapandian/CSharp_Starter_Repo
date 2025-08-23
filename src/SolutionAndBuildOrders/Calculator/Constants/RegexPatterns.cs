namespace CalculatorApp.Constants;

/// <summary>
/// Have regex validation patterns.
/// </summary>
public static class RegexPatterns
{
    /// <summary>
    /// Its a regex pattern to validate a arithmetic expression.
    /// </summary>
    public const string ArithmeticExpressionRegex = @"^(-?\d+)\s*([-+*/])\s*(-?\d+)$";
}

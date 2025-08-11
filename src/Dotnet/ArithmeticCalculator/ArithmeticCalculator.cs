using System.Text.RegularExpressions;
using Dotnet.ArithmeticCalculator.Constants;
using Dotnet.ArithmeticCalculator.UserInterface;
using Dotnet.ArithmeticCalculator.Utilities;

namespace Dotnet.ArithmeticCalculator;

/// <summary>
/// It's an entry point and controller for arithmetic calculator.
/// </summary>
public class ArithmeticCalculator
{
    /// <summary>
    /// This is an entry point for arithmetic calculator.
    /// </summary>
    public static void Main()
    {
        MathUtils mathUtils = new ();
        ConsoleUI consoleUI = new ();

        consoleUI.ShowMessage(Messages.ArithmeticCalculatorTitle, MessageType.Title);
        int maxRetryCount = AppSettings.MaximumRetryCount;
        int currentRetryCount = 0;

        do
        {
            consoleUI.ShowMessage(Messages.PromptForExpression, MessageType.Prompt);
            string? inputExpression = consoleUI.GetInput();
            if (string.IsNullOrEmpty(inputExpression))
            {
                consoleUI.ShowMessage(Messages.EmptyExpressionWarning, MessageType.Warning);
                currentRetryCount++;
                continue;
            }

            string expressionPattern = RegexPatterns.ArithmeticExpressionRegex;
            Match match = Regex.Match(inputExpression, expressionPattern);
            if (match.Success)
            {
                string numberAString = match.Groups[1].Value;
                int numberA = int.Parse(numberAString);
                string operatorSymbol = match.Groups[2].Value;
                string numberBString = match.Groups[3].Value;
                int numberB = int.Parse(numberBString);

                if (operatorSymbol == "/" && numberB == 0)
                {
                    consoleUI.ShowMessage(Messages.DivisorCantBeZeroWarning, MessageType.Warning);
                    currentRetryCount++;
                    continue;
                }

                int result = operatorSymbol switch
                {
                    "+" => mathUtils.Add(numberA, numberB),
                    "-" => mathUtils.Subtract(numberA, numberB),
                    "*" => mathUtils.Multiply(numberA, numberB),
                    "/" => mathUtils.Divide(numberA, numberB)
                };

                consoleUI.ShowMessage(string.Format(Messages.Result, result), MessageType.Result);
            }
            else
            {
                consoleUI.ShowMessage(Messages.InvalidExpressionWarning, MessageType.Warning);
                currentRetryCount++;
            }
        }
        while (maxRetryCount >= currentRetryCount);

        if (currentRetryCount > maxRetryCount)
        {
            consoleUI.ShowMessage(Messages.MaximumRetryReachedWarning, MessageType.Warning);
        }

        Thread.Sleep(1500);
    }
}
using System.Text.RegularExpressions;
using Dotnet.ArithmeticCalculator.Constants;
using Dotnet.ArithmeticCalculator.Enums;
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
        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowMessage(MessageType.Title, Messages.ArithmeticCalculatorTitle);
        int maxRetryCount = AppSettings.MaximumRetryCount;
        int currentRetryCount = 0;

        do
        {
            // Exit command is passed to input prompt, so that user will know the exit command to quit the app.
            userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.InputPrompt, AppSettings.ExitCommand));
            string? inputExpression = userInterface.GetInput();
            if (string.IsNullOrEmpty(inputExpression))
            {
                userInterface.ShowMessage(MessageType.Warning, Messages.EmptyExpressionWarning);
                currentRetryCount++;
                continue;
            }

            if (inputExpression.Equals(AppSettings.ExitCommand))
            {
                return;
            }

            string expressionPattern = RegexPatterns.ArithmeticExpressionRegex;
            Match match = Regex.Match(inputExpression, expressionPattern);
            if (match.Success)
            {
                string operatorSymbol = match.Groups[2].Value;
                string numberBString = match.Groups[3].Value;
                int numberB = int.Parse(numberBString);

                if (operatorSymbol == "/" && numberB == 0)
                {
                    userInterface.ShowMessage(MessageType.Warning, Messages.DivisorCantBeZeroWarning);
                    currentRetryCount++;
                    continue;
                }

                string numberAString = match.Groups[1].Value;
                int numberA = int.Parse(numberAString);

                int result = operatorSymbol switch
                {
                    "+" => mathUtils.Add(numberA, numberB),
                    "-" => mathUtils.Subtract(numberA, numberB),
                    "*" => mathUtils.Multiply(numberA, numberB),
                    "/" => mathUtils.Divide(numberA, numberB)
                };

                userInterface.ShowMessage(MessageType.Result, string.Format(Messages.Result, result));
            }
            else
            {
                userInterface.ShowMessage(MessageType.Warning, Messages.InvalidExpressionWarning);
                currentRetryCount++;
            }
        }
        while (maxRetryCount >= currentRetryCount);

        if (currentRetryCount > maxRetryCount)
        {
            userInterface.ShowMessage(MessageType.Warning, Messages.MaximumRetryReachedWarning);
        }

        Thread.Sleep(1500);
    }
}
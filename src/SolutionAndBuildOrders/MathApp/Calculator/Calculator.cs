using System.Text.RegularExpressions;
using ConsoleDisplays.Constants;
using ConsoleDisplays.Displays;
using ConsoleDisplays.Enums;
using DisplayApp.Displays;
using MathApp.Constants;
using UtilityApp.UserInput;

namespace DisplayApp.Math;

/// <inheritdoc/>
public class Calculator : ICalculator
{
    /// <summary>
    /// This is an entry point for arithmetic calculator.
    /// </summary>
    public void Run()
    {
        IUserInput userInput = new UserInput();
        ICalculatorDisplay calculatorDisplay = new CalculatorDisplay(this);
        IConsoleDisplay consoleDisplay = new ConsoleDisplay();

        consoleDisplay.ShowMessage(Messages.ArithmeticCalculatorTitle, MessageType.Title);
        int maxRetryCount = AppSettings.MaximumRetryCount;
        int currentRetryCount = 0;

        do
        {
            // Exit command is passed to input prompt, so that user will know the exit command to quit the app.
            consoleDisplay.ShowMessage(string.Format(Messages.PromptForExpression, AppSettings.ExitCommand), MessageType.Prompt);
            string? inputExpression = userInput.GetInput();
            if (string.IsNullOrEmpty(inputExpression))
            {
                consoleDisplay.ShowMessage(Messages.EmptyExpressionWarning, MessageType.Warning);
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
                    consoleDisplay.ShowMessage(Messages.DivisorCantBeZeroWarning, MessageType.Warning);
                    currentRetryCount++;
                    continue;
                }

                string numberAString = match.Groups[1].Value;
                int numberA = int.Parse(numberAString);
                calculatorDisplay.DisplayResult(numberA, numberB, operatorSymbol);
            }
            else
            {
                consoleDisplay.ShowMessage(Messages.InvalidExpressionWarning, MessageType.Warning);
                currentRetryCount++;
                continue;
            }
        }
        while (maxRetryCount >= currentRetryCount);

        if (currentRetryCount > maxRetryCount)
        {
            consoleDisplay.ShowMessage(Messages.MaximumRetryReachedWarning, MessageType.Warning);
        }

        Thread.Sleep(1500);
    }

    /// <inheritdoc/>
    public int Add(int a, int b) => a + b;

    /// <inheritdoc/>
    public int Subtract(int a, int b) => a - b;

    /// <inheritdoc/>
    public int Multiply(int a, int b) => a * b;

    /// <inheritdoc/>
    public int Divide(int dividend, int divisor) => dividend / divisor;
}

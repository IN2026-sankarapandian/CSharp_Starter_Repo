using System.Text.RegularExpressions;
using CalculatorApp.Constants;
using DisplayApp.Display;
using DisplayApp.Enums;
using UtilityApp.UserInput;

namespace CalculatorApp;

/// <summary>
/// Represents a simple calculator console application that get arithmetic expression from the user and prints the result.
/// </summary>
public class Calculator
{
    private readonly ICalculatorDisplay _consoleDisplay;
    private readonly IUserInput _userInput;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calculator"/> class.
    /// </summary>
    /// <param name="consoleDisplay">To display prompts and results.</param>
    /// <param name="userInput">To get user input.</param>
    public Calculator(ICalculatorDisplay consoleDisplay, IUserInput userInput)
    {
        this._consoleDisplay = consoleDisplay;
        this._userInput = userInput;
    }

    /// <summary>
    /// This is an entry point for arithmetic calculator.
    /// </summary>
    public void Run()
    {
        this._consoleDisplay.ShowMessage(MessageType.Title, Messages.ArithmeticCalculatorTitle);
        int maxRetryCount = AppSettings.MaximumRetryCount;
        int currentRetryCount = 0;
        do
        {
            // Exit command is passed to input prompt, so that user will know the exit command to quit the app.
            this._consoleDisplay.ShowMessage(MessageType.Prompt, string.Format(Messages.PromptForExpression, AppSettings.ExitCommand));
            string? inputExpression = this._userInput.GetInput();
            if (string.IsNullOrEmpty(inputExpression))
            {
                this._consoleDisplay.ShowMessage(MessageType.Warning, Messages.EmptyExpressionWarning);
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
                    this._consoleDisplay.ShowMessage(MessageType.Warning, Messages.DivisorCantBeZeroWarning);
                    currentRetryCount++;
                    continue;
                }

                string numberAString = match.Groups[1].Value;
                int numberA = int.Parse(numberAString);
                this._consoleDisplay.DisplayResult(numberA, numberB, operatorSymbol);
            }
            else
            {
                this._consoleDisplay.ShowMessage(MessageType.Warning, Messages.InvalidExpressionWarning);
                currentRetryCount++;
                continue;
            }
        }
        while (maxRetryCount >= currentRetryCount);

        if (currentRetryCount > maxRetryCount)
        {
            this._consoleDisplay.ShowMessage(MessageType.Warning, Messages.MaximumRetryReachedWarning);
        }

        Thread.Sleep(1500);
    }
}

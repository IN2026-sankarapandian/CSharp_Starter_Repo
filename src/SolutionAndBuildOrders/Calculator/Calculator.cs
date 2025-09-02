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
    private readonly ICalculatorDisplay _calculatorDisplay;
    private readonly IUserInput _userInput;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calculator"/> class.
    /// </summary>
    /// <param name="consoleDisplay">To display prompts and results.</param>
    /// <param name="userInput">To get user input.</param>
    public Calculator(ICalculatorDisplay consoleDisplay, IUserInput userInput)
    {
        this._calculatorDisplay = consoleDisplay;
        this._userInput = userInput;
    }

    /// <summary>
    /// This is an entry point for arithmetic calculator.
    /// </summary>
    public void Run()
    {
        this._calculatorDisplay.ShowMessage(MessageType.Title, Messages.ArithmeticCalculatorTitle);
        int maxRetryCount = AppSettings.MaximumRetryCount;
        int currentRetryCount = 0;
        do
        {
            // Exit command is passed to input prompt, so that user will know the exit command to quit the app.
            this._calculatorDisplay.ShowMessage(MessageType.Prompt, string.Format(Messages.PromptForExpression, AppSettings.ExitCommand));
            string? inputExpression = this._userInput.GetInput();
            if (string.IsNullOrEmpty(inputExpression))
            {
                this._calculatorDisplay.ShowMessage(MessageType.Warning, Messages.EmptyExpressionWarning);
                currentRetryCount++;
                continue;
            }

            // If user enters exit command, calculator passes the control to caller method.
            if (IsExitCommand(inputExpression))
            {
                return;
            }

            if (this.TryParseExpression(inputExpression, out int numberA, out int numberB, out string operatorSymbol))
            {
                if (IsDivideByZero(numberB, operatorSymbol))
                {
                    this._calculatorDisplay.ShowMessage(MessageType.Warning, Messages.DivisorCantBeZeroWarning);
                    currentRetryCount++;
                    continue;
                }

                this._calculatorDisplay.DisplayResult(numberA, numberB, operatorSymbol);
            }
            else
            {
                this._calculatorDisplay.ShowMessage(MessageType.Warning, Messages.InvalidExpressionWarning);
                currentRetryCount++;
                continue;
            }
        }
        while (maxRetryCount >= currentRetryCount);

        if (currentRetryCount > maxRetryCount)
        {
            this._calculatorDisplay.ShowMessage(MessageType.Warning, Messages.MaximumRetryReachedWarning);
        }

        Thread.Sleep(1500);
    }

    /// <summary>
    /// Checks whether the user input is equal to exit command.
    /// </summary>
    /// <param name="userInput">Input from the user.</param>
    /// <returns><see cref="true"/> if it is equals to exit command; otherwise <see cref="false"/>.</returns>
    private static bool IsExitCommand(string userInput) => userInput.Equals(AppSettings.ExitCommand);

    /// <summary>
    /// Checks whether the operator and operand cause <see cref="DivideByZeroException"/>.
    /// </summary>
    /// <param name="numberB">Second operator of the expression</param>
    /// <param name="operatorSymbol">Operator symbol of the expression</param>
    /// <returns><see cref="true"/> if it will cause <see cref="DivideByZeroException"/>; otherwise false.</returns>
    private static bool IsDivideByZero(int numberB, string operatorSymbol) => operatorSymbol == "/" && numberB == 0;

    /// <summary>
    /// Convert an arithmetic expression to its operator and operands
    /// </summary>
    /// <param name="expression">It's the input expression.</param>
    /// <param name="numberA">First operator from expression.</param>
    /// <param name="numberB">Second operator from expression.</param>
    /// <param name="operatorSymbol">Operator symbol from expression.</param>
    /// <returns><see cref="true"/> if <paramref name="expression"/> is converted successfully; otherwise <see cref="false"/>.</returns>
    private bool TryParseExpression(string expression, out int numberA, out int numberB, out string operatorSymbol)
    {
        string expressionPattern = RegexPatterns.ArithmeticExpressionRegex;
        Match match = Regex.Match(expression, expressionPattern);
        if (match.Success)
        {
            operatorSymbol = match.Groups[2].Value;
            string numberBString = match.Groups[3].Value;
            string numberAString = match.Groups[1].Value;
            if (int.TryParse(numberAString, out numberA) && int.TryParse(numberBString, out numberB))
            {
                return true;
            }
        }

        numberA = default;
        numberB = default;
        operatorSymbol = string.Empty;
        return false;
    }
}

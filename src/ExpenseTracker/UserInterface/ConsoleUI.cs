using ConsoleTables;
using ExpenseTracker.Constants;
using ExpenseTracker.Models;

namespace ExpenseTracker.UserInterface;

/// <summary>
/// Have functions used to manipulate console UI.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <summary>
    /// Prompt the user and returns the user input for the prompt.
    /// </summary>
    /// <param name="prompt">Prompt shown to the user.</param>
    /// <returns>User input</returns>
    public string? PromptAndGetInput(string prompt)
    {
        Console.Write(prompt);
        string? userInput = Console.ReadLine();
        return userInput;
    }

    /// <summary>
    /// Shows title to the user.
    /// </summary>
    /// <param name="action">Action to show user.</param>
    public void MoveToAction(string action)
    {
        this.CreateNewPage(action);
    }

    /// <summary>
    /// Shows info to the user.
    /// </summary>
    /// <param name="info">Info to show user.</param>
    public void ShowInfoMessage(string info)
    {
        this.PromptLine($"{info}");
    }

    /// <summary>
    /// Shows warning to the user.
    /// </summary>
    /// <param name="warningMessage">Warning message to show user.</param>
    public void ShowWarningMessage(string warningMessage)
    {
        this.PromptLine($"{warningMessage}", ConsoleColor.Yellow);
    }

    /// <summary>
    /// Shows success message to the user.
    /// </summary>
    /// <param name="successMessage">Success message to show user.</param>
    public void ShowSuccessMessage(string successMessage)
    {
        this.PromptLine($"{successMessage}", ConsoleColor.Green);
    }

    /// <summary>
    /// Shows transaction list to the user.
    /// </summary>
    /// <param name="userTransactionDataList">Transaction list to show user.</param>
    /// <param name="showIncome">Income filter.</param>
    /// <param name="showExpense">Expense filter.</param>
    public void ShowTransactionList(List<ITransaction> userTransactionDataList, bool showIncome = true, bool showExpense = true)
    {
        this.ShowTransactionListAsTable(userTransactionDataList, showIncome, showExpense);
    }

    /// <summary>
    /// Shows transaction data to the user.
    /// </summary>
    /// <param name="transactionData">Transaction data to show user.</param>
    public void ShowTransactionData(ITransaction transactionData)
    {
        this.ShowTransactionDataAsTable(transactionData);
    }

    /// <summary>
    /// Prompt the user with info in new line with specified color.
    /// </summary>
    /// <param name="prompt">Prompt to show user.</param>
    /// <param name="color">Color for the prompt. Default value is white.</param>
    private void PromptLine(string? prompt, ConsoleColor color = ConsoleColor.White)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            return;
        }

        Console.ForegroundColor = color;
        Console.Write(prompt.Replace("\\n", "\n"));
        Console.WriteLine();
        Console.ResetColor();
    }

    /// <summary>
    /// Creates new page in console with specified subtitle.
    /// Title will be the app name
    /// </summary>
    /// <param name="title">Current action name.</param>
    private void CreateNewPage(string title)
    {
        Console.Clear();
        this.PromptLine($"{nameof(ExpenseTracker)} - {title}", ConsoleColor.DarkBlue);
        Console.WriteLine();
    }

    /// <summary>
    /// Prints the transaction list as a table.
    /// </summary>
    /// <param name="userTransactionDataList">Transaction list to print.</param>
    /// <param name="showIncome">True to display income transaction; otherwise false.</param>
    /// <param name="showExpense">True to display expense transaction; otherwise false.</param>
    private void ShowTransactionListAsTable(List<ITransaction> userTransactionDataList, bool showIncome = true, bool showExpense = true)
    {
        if (userTransactionDataList.Count == 0)
        {
            this.PromptLine(ErrorMessages.NoTransactionFound);
            return;
        }

        List<string> header = new () { "Index", "Income/Expense ", "Amount", $"{(showIncome ? "Source" : string.Empty)}{(showExpense && showIncome ? "/" : string.Empty)} {(showExpense ? "Category" : string.Empty)}" };
        if (!(showExpense && showIncome))
        {
            header.RemoveAt(1);
        }

        ConsoleTable table = new (header.ToArray());
        for (int rowIndex = 0; rowIndex < userTransactionDataList.Count; rowIndex++)
        {
            List<string> row = new () { (rowIndex + 1).ToString() };
            if (userTransactionDataList[rowIndex] is IncomeTransactionData income)
            {
                if (!showIncome)
                {
                    continue;
                }

                row.Add("Income");
                row.Add(userTransactionDataList[rowIndex].Amount.ToString());
                row.Add(income.Source);
            }
            else if (userTransactionDataList[rowIndex] is ExpenseTransactionData expense)
            {
                if (!showExpense)
                {
                    continue;
                }

                row.Add("Expense");
                row.Add(userTransactionDataList[rowIndex].Amount.ToString());
                row.Add(expense.Category);
            }

            if (!(showExpense && showIncome))
            {
                row.RemoveAt(1);
            }

            table.AddRow(row.ToArray());
        }

        table.Write();
    }

    /// <summary>
    /// Prints the transaction data as a table.
    /// </summary>
    /// <param name="transactionData">Transaction data to print.</param>
    private void ShowTransactionDataAsTable(ITransaction transactionData)
    {
        string[] header = new string[2];
        string[] value = new string[2];
        header[0] = "Amount";
        if (transactionData is IncomeTransactionData income)
        {
            header[1] = "Source";
            value[0] = income.Amount.ToString();
            value[1] = income.Source;
        }
        else if (transactionData is ExpenseTransactionData expense)
        {
            header[1] = "Category";
            value[0] = expense.Amount.ToString();
            value[1] = expense.Category;
        }

        ConsoleTable table = new (header);
        table.AddRow(value);
        table.Write();
    }
}

using ConsoleTables;
using ExpenseTracker.Constants;
using ExpenseTracker.Models;

namespace ExpenseTracker.UserInterface;

/// <summary>
/// Have functions used to manipulate console UI.
/// </summary>
public class ConsoleUI : IUserInterface
{
    /// <inheritdoc/>
    public string? PromptAndGetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    /// <inheritdoc/>
    public void MoveToAction(string action) => this.CreateNewPage(action);

    /// <inheritdoc/>
    public void ShowInfoMessage(string info) => this.PromptLine($"{info}");

    /// <inheritdoc/>
    public void ShowWarningMessage(string warningMessage) => this.PromptLine($"{warningMessage}", ConsoleColor.Yellow);

    /// <inheritdoc/>
    public void ShowSuccessMessage(string successMessage) => this.PromptLine($"{successMessage}", ConsoleColor.Green);

    /// <inheritdoc/>
    public void ShowTransactionList(List<ITransaction> userTransactionDataList, OptionEnums.TransactionFilter filter) => this.ShowTransactionListAsTable(userTransactionDataList, filter);

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
    /// <param name="filter">Transaction view filter.</param>
    private void ShowTransactionListAsTable(List<ITransaction> userTransactionDataList, OptionEnums.TransactionFilter filter)
    {
        // Applies filter
        List<string> header;
        switch (filter)
        {
            case OptionEnums.TransactionFilter.Income:
                header = new () { "Index", "Amount", "Source" };
                userTransactionDataList = this.FilterIncomeTransactionDataList(userTransactionDataList);
                break;
            case OptionEnums.TransactionFilter.Expense:
                header = new () { "Index", "Amount", "Category" };
                userTransactionDataList = this.FilterExpenseTransactionDataList(userTransactionDataList);
                break;
            default:
                header = new () { "Index", "Income/Expense ", "Amount", "Source/Category" };
                break;
        }

        if (userTransactionDataList.Count == 0)
        {
            this.PromptLine(ErrorMessages.NoTransactionFound);
            return;
        }

        // Create and prints table
        ConsoleTable table = new (header.ToArray());
        for (int rowIndex = 0; rowIndex < userTransactionDataList.Count; rowIndex++)
        {
            List<string> row = new () { (rowIndex + 1).ToString() };
            if (filter == OptionEnums.TransactionFilter.All)
            {
                if (userTransactionDataList[rowIndex] is IncomeTransactionData)
                {
                    row.Add("Income");
                }
                else
                {
                    row.Add("Expense");
                }
            }

            row.Add(userTransactionDataList[rowIndex].Amount.ToString());
            if (userTransactionDataList[rowIndex] is IncomeTransactionData income)
            {
                row.Add(income.Source);
            }
            else if (userTransactionDataList[rowIndex] is ExpenseTransactionData expense)
            {
                row.Add(expense.Category);
            }

            table.AddRow(row.ToArray());
        }

        table.Write();
    }

    private List<ITransaction> FilterIncomeTransactionDataList(List<ITransaction> transactionList)
    {
        List<ITransaction> result = new ();
        foreach (ITransaction transaction in transactionList)
        {
            if (transaction is IncomeTransactionData income)
            {
                result.Add(income);
            }
        }

        return result;
    }

    private List<ITransaction> FilterExpenseTransactionDataList(List<ITransaction> transactionList)
    {
        List<ITransaction> result = new ();
        foreach (ITransaction transaction in transactionList)
        {
            if (transaction is ExpenseTransactionData expenseTransactionData)
            {
                result.Add(expenseTransactionData);
            }
        }

        return result;
    }
}

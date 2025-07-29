using ConsoleTables;
using ExpenseTracker.Constants;
using ExpenseTracker.Constants.Enums;
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
    public void ShowWarningMessage(string warningMessage) 
        => this.PromptLine($"{warningMessage}", ConsoleColor.Yellow);

    /// <inheritdoc/>
    public void ShowSuccessMessage(string successMessage) 
        => this.PromptLine($"{successMessage}", ConsoleColor.Green);

    /// <inheritdoc/>
    public void ShowTransactionList(List<ITransaction> userTransactionDataList, TransactionType type)
    {
        List<string>? header;
        switch (type)
        {
            case TransactionType.Income:
                header = new () { Headings.Index, Headings.Amount, Headings.Source };
                List<ITransaction> incomeTransactionList = this.GetIncomeTransactionDataList(userTransactionDataList);
                this.ShowTransactionListAsTable(incomeTransactionList, header, true);
                break;
            case TransactionType.Expense:
                header = new () { Headings.Index, Headings.Amount, Headings.Category };
                List<ITransaction> expenseTransactionList = this.GetExpenseTransactionDataList(userTransactionDataList);
                this.ShowTransactionListAsTable(expenseTransactionList, header, true);
                break;
            default:
                header = new () { Headings.Index, $"{Headings.Income}/{Headings.Expense}", Headings.Amount, $"{Headings.Source}/{Headings.Expense}" };
                this.ShowTransactionListAsTable(userTransactionDataList, header, false);
                break;
        }
    }

    /// <summary>
    /// Prompt the user with info in new line with specified color.
    /// </summary>
    /// <param name="prompt">Prompt to show user.</param>
    /// <param name="color">Color for the prompt. Default value is white.</param>
    private void PromptLine(string? prompt, ConsoleColor color = ConsoleColor.White)
    {
        if (!string.IsNullOrEmpty(prompt))
        {
            Console.ForegroundColor = color;
            Console.Write(prompt.Replace("\\n", "\n"));
            Console.WriteLine();
            Console.ResetColor();
        }
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

    private void ShowTransactionListAsTable(List<ITransaction> userTransactionDataList, List<string> header, bool isFiltered)
    {
        if (userTransactionDataList.Count == 0)
        {
            this.PromptLine(ErrorMessages.NoTransactionFound);
            return;
        }

        ConsoleTable table = new (header.ToArray());
        int rowIndex = 0;
        foreach (ITransaction transaction in userTransactionDataList)
        {
            List<string> row = new () { (rowIndex + 1).ToString() };
            if (!isFiltered)
            {
                row.Add(transaction.TransactionType.ToString());
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
            rowIndex++;
        }

        table.Write();
    }

    private List<ITransaction> GetIncomeTransactionDataList(List<ITransaction> transactionList) =>
        transactionList.Where(transaction => transaction.TransactionType == TransactionType.Income).ToList();

    private List<ITransaction> GetExpenseTransactionDataList(List<ITransaction> transactionList) =>
        transactionList.Where(transaction => transaction.TransactionType == TransactionType.Expense).ToList();
}

using ExpenseTracker.Constants;

namespace ExpenseTracker.Models;

/// <summary>
/// Represents the account with name, balance, list of transactions.
/// </summary>
public class Account : IAccount
{
    /// <inheritdoc/>
    public decimal CurrentBalance { get; private set; }

    /// <inheritdoc/>
    public decimal TotalIncome { get; private set; }

    /// <inheritdoc/>
    public decimal TotalExpense { get; private set; }

    /// <inheritdoc/>
    public List<ITransaction> TotalTransactionDataList { get; private set;  } = new List<ITransaction>();

    /// <inheritdoc/>
    public List<string> Categories { get; set; } = new List<string> { Headings.Category1, Headings.Category2 };

    /// <inheritdoc/>
    public List<string> Sources { get; set; } = new List<string> { Headings.Source1, Headings.Source2 };

    /// <inheritdoc/>
    public void AddIncome(decimal incomeAmount, string source)
    {
        IncomeTransactionData newIncome = new ()
        {
            Amount = incomeAmount,
            Source = source,
        };
        this.CurrentBalance += incomeAmount;
        this.TotalIncome += incomeAmount;
        this.TotalTransactionDataList.Add(newIncome);
    }

    /// <inheritdoc/>
    public void AddExpense(decimal expenseAmount, string category)
    {
        ExpenseTransactionData newExpense = new ()
        {
            Amount = expenseAmount,
            Category = category,
        };
        this.CurrentBalance -= expenseAmount;
        this.TotalExpense += expenseAmount;
        this.TotalTransactionDataList.Add(newExpense);
    }
}

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
    public List<string> Categories { get; set; } = new List<string> { "Rent", "Food", "Transport" };

    /// <inheritdoc/>
    public List<string> Sources { get; set; } = new List<string> { "Salary", "Stocks", "Petty cash" };

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

    /// <summary>
    /// Edit the amount of transaction at specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newAmountValue">New amount value.</param>
    public void EditTransactionAmount(int index, decimal newAmountValue)
    {
        ITransaction transaction = this.TotalTransactionDataList[index];
        switch (transaction)
        {
            case IncomeTransactionData income:
                this.TotalIncome -= income.Amount;
                this.TotalIncome += newAmountValue;
                this.CurrentBalance -= income.Amount;
                this.CurrentBalance += newAmountValue;
                income.EditAmount(newAmountValue);
                break;
            case ExpenseTransactionData expense:
                this.TotalExpense -= expense.Amount;
                this.TotalExpense += newAmountValue;
                this.CurrentBalance += expense.Amount;
                this.CurrentBalance -= newAmountValue;
                expense.EditAmount(newAmountValue);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Edits the source of income transaction of specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newSourceValue">New source value.</param>
    public void EditIncomeTransactionSource(int index, string newSourceValue)
    {
        ITransaction transaction = this.TotalTransactionDataList[index];
        if (transaction is IncomeTransactionData income)
        {
            income.EditSource(newSourceValue);
        }
    }

    /// <summary>
    /// Edits category of expense transaction of specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newCategoryValue">New category value.</param>
    public void EditExpenseTransactionCategory(int index, string newCategoryValue)
    {
        ITransaction transaction = this.TotalTransactionDataList[index];
        if (transaction is ExpenseTransactionData income)
        {
            income.EditCategory(newCategoryValue);
        }
    }

    /// <summary>
    /// Deletes a transaction in a specified index.
    /// </summary>
    /// <param name="index">Index of transaction to delete.</param>
    public void DeleteTransaction(int index)
    {
        ITransaction transaction = this.TotalTransactionDataList[index];
        switch (transaction)
        {
            case IncomeTransactionData income:
                this.TotalIncome -= income.Amount;
                this.CurrentBalance -= income.Amount;
                this.TotalTransactionDataList.RemoveAt(index);
                break;
            case ExpenseTransactionData expense:
                this.TotalExpense -= expense.Amount;
                this.CurrentBalance += expense.Amount;
                this.TotalTransactionDataList.RemoveAt(index);
                break;
            default:
                break;
        }
    }
}

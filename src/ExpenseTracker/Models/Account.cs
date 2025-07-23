namespace ExpenseTracker.Models;

/// <summary>
/// Represents the account with name, balance, list of transactions.
/// </summary>
public class Account : IAccount
{
    /// <summary>
    /// Gets current balance of the account.
    /// </summary>
    /// <value>
    /// Current balance of the account.
    /// </value>
    public decimal CurrentBalance { get; private set; }

    /// <summary>
    /// Gets total income of the account.
    /// </summary>
    /// <value>
    /// Current total income of the account.
    /// </value>
    public decimal TotalIncome { get; private set; }

    /// <summary>
    /// Gets total expense of the account.
    /// </summary>
    /// <value>
    /// Current total expense of the account.
    /// </value>
    public decimal TotalExpense { get; private set; }

    /// <summary>
    /// Gets the list of all transactions.
    /// </summary>
    /// <value>
    /// Its the list of all transactions.
    /// </value>
    public List<ITransaction> TotalTransactionDataList { get; private set;  } = new List<ITransaction>();

    /// <summary>
    /// Gets or sets list of available categories.
    /// </summary>
    /// <value>
    /// List of available categories.
    /// </value>
    public List<string> Categories { get; set; } = new List<string> { "Food", "rent", "Game" };

    /// <summary>
    /// Gets or sets list of available sources.
    /// </summary>
    /// <value>
    /// List of available sources.
    /// </value>
    public List<string> Sources { get; set; } = new List<string> { "Work", "Freelance", "Stocks", "other" };

    /// <summary>
    /// Create the income transaction.
    /// </summary>
    /// <param name="incomeAmount">Income amount.</param>
    /// <param name="source">Source of income.</param>
    public void AddIncome(decimal incomeAmount, string source)
    {
        IncomeTransactionData newIncome = new IncomeTransactionData();
        newIncome.Amount = incomeAmount;
        newIncome.Source = source;
        this.CurrentBalance += incomeAmount;
        this.TotalIncome += incomeAmount;
        this.TotalTransactionDataList.Add(newIncome);
    }

    /// <summary>
    /// Create the expense transaction.
    /// </summary>
    /// <param name="expenseAmount">Expense amount.</param>
    /// <param name="category">Category of expense.</param>
    public void AddExpense(decimal expenseAmount, string category)
    {
        ExpenseTransactionData newExpense = new ExpenseTransactionData();
        newExpense.Amount = expenseAmount;
        newExpense.Category = category;
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

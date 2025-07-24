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
    public List<string> Categories { get; set; } = new List<string> { "Rent", "Food", "Transport"};

    /// <summary>
    /// Gets or sets list of available sources.
    /// </summary>
    /// <value>
    /// List of available sources.
    /// </value>
    public List<string> Sources { get; set; } = new List<string> { "Salary", "Stocks", "Petty cash"};

    /// <summary>
    /// Create the income transaction.
    /// </summary>
    /// <param name="incomeAmount">Income amount.</param>
    /// <param name="source">Source of income.</param>
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

    /// <summary>
    /// Create the expense transaction.
    /// </summary>
    /// <param name="expenseAmount">Expense amount.</param>
    /// <param name="category">Category of expense.</param>
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

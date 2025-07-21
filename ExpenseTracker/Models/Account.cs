namespace ExpenseTracker.Models;

/// <summary>
/// Represents the account with name, balance, list of transactions.
/// </summary>
public class Account
{
    private readonly List<IncomeTransactionData> _incomeDataList = new List<IncomeTransactionData>();
    private readonly List<ExpenseTransactionData> _expenseDataList = new List<ExpenseTransactionData>();
    private readonly List<ITransaction> _totalTransactionList = new List<ITransaction>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    /// <param name="name">.Name of the account.</param>
    /// <param name="balance">Account balance.</param>
    public Account(string name, decimal balance)
    {
        this.Name = name;
        this.Balance = balance;
        this.TotalIncome += balance;
        this.TotalExpense = 0;
    }

    /// <summary>
    /// Gets or sets name of the account.
    /// </summary>
    /// <value>
    /// Name of the account.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets current balance of the account.
    /// </summary>
    /// <value>
    /// Current balance of the account.
    /// </value>
    public decimal Balance { get; set; }

    /// <summary>
    /// Gets or sets total income of the account.
    /// </summary>
    /// <value>
    /// Current total income of the account.
    /// </value>
    public decimal TotalIncome { get; set; }

    /// <summary>
    /// Gets or sets total expense of the account.
    /// </summary>
    /// <value>
    /// Current total expense of the account.
    /// </value>
    public decimal TotalExpense { get; set; }

    /// <summary>
    /// Create the income transaction.
    /// </summary>
    /// <param name="amount">Income amount.</param>
    /// <param name="source">Source of income.</param>
    public void AddIncome(decimal amount, string source)
    {
        IncomeTransactionData newIncome = new IncomeTransactionData(amount, source);
        this.Balance += amount;
        this.TotalIncome += amount;
        this._incomeDataList.Add(newIncome);
        this._totalTransactionList.Add(newIncome);
    }

    /// <summary>
    /// Create the expense transaction.
    /// </summary>
    /// <param name="amount">Expense amount.</param>
    /// <param name="category">Category of expense.</param>
    public void AddExpense(decimal amount, string category)
    {
        ExpenseTransactionData newExpense = new ExpenseTransactionData(amount, category);
        this.Balance -= amount;
        this.TotalExpense += amount;
        this._expenseDataList.Add(newExpense);
        this._totalTransactionList.Add(newExpense);
    }

    /// <summary>
    /// Gets the entire transaction list
    /// </summary>
    /// <returns>Entire transaction list.</returns>
    public List<ITransaction> GetTransactionsList()
    {
        return this._totalTransactionList.OrderBy(a => a.CreatedAt).ToList();
    }

    /// <summary>
    /// Edit the amount of transaction at specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="amount">New amount value.</param>
    public void EditTransactionAmount(int index, decimal amount)
    {
        ITransaction transaction = this._totalTransactionList[index];
        switch (transaction)
        {
            case IncomeTransactionData income:
                this.TotalIncome -= income.Amount;
                this.TotalIncome += amount;
                this.Balance -= income.Amount;
                this.Balance += amount;
                income.EditAmount(amount);
                break;
            case ExpenseTransactionData expense:
                this.TotalExpense -= expense.Amount;
                this.TotalExpense += amount;
                this.Balance += expense.Amount;
                this.Balance -= amount;
                expense.EditAmount(amount);
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
        ITransaction transaction = this._totalTransactionList[index];
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
        ITransaction transaction = this._totalTransactionList[index];
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
        ITransaction transaction = this._totalTransactionList[index];
        switch (transaction)
        {
            case IncomeTransactionData income:
                this.TotalIncome -= income.Amount;
                this.Balance -= income.Amount;
                this._totalTransactionList.RemoveAt(index);
                this._incomeDataList.Remove(income);
                break;
            case ExpenseTransactionData expense:
                this.TotalExpense -= expense.Amount;
                this.Balance += expense.Amount;
                this._totalTransactionList.RemoveAt(index);
                this._expenseDataList.Remove(expense);
                break;
            default:
                break;
        }
    }
}

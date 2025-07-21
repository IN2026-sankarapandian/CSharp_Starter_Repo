namespace ExpenseTracker.Models;

/// <summary>
/// Represents the account with name, balance, list of transactions.
/// </summary>
public class Account
{
    private readonly List<IncomeTransactionData> _incomeDataList = new List<IncomeTransactionData>();
    private readonly List<ExpenseTransactionData> _expenseDataList = new List<ExpenseTransactionData>();
    private readonly List<ITransaction> _totalTransactionDataList = new List<ITransaction>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    /// <param name="accountName">.Name of the account.</param>
    /// <param name="currentBalance">Account balance.</param>
    public Account(string accountName, decimal currentBalance)
    {
        this.AccountName = accountName;
        this.CurrentBalance = currentBalance;
        this.TotalIncome += currentBalance;
        this.TotalExpense = 0;
    }

    /// <summary>
    /// Gets or sets name of the account.
    /// </summary>
    /// <value>
    /// Name of the account.
    /// </value>
    public string AccountName { get; set; }

    /// <summary>
    /// Gets or sets current balance of the account.
    /// </summary>
    /// <value>
    /// Current balance of the account.
    /// </value>
    public decimal CurrentBalance { get; set; }

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
    /// <param name="incomeAmount">Income amount.</param>
    /// <param name="source">Source of income.</param>
    public void AddIncome(decimal incomeAmount, string source)
    {
        IncomeTransactionData newIncome = new IncomeTransactionData(incomeAmount, source);
        this.CurrentBalance += incomeAmount;
        this.TotalIncome += incomeAmount;
        this._incomeDataList.Add(newIncome);
        this._totalTransactionDataList.Add(newIncome);
    }

    /// <summary>
    /// Create the expense transaction.
    /// </summary>
    /// <param name="expenseAmount">Expense amount.</param>
    /// <param name="category">Category of expense.</param>
    public void AddExpense(decimal expenseAmount, string category)
    {
        ExpenseTransactionData newExpense = new ExpenseTransactionData(expenseAmount, category);
        this.CurrentBalance -= expenseAmount;
        this.TotalExpense += expenseAmount;
        this._expenseDataList.Add(newExpense);
        this._totalTransactionDataList.Add(newExpense);
    }

    /// <summary>
    /// Gets the entire transaction list
    /// </summary>
    /// <returns>Entire transaction list.</returns>
    public List<ITransaction> GetTransactionDataList()
    {
        return this._totalTransactionDataList.OrderBy(transaction => transaction.CreatedAt).ToList();
    }

    /// <summary>
    /// Edit the amount of transaction at specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newAmountValue">New amount value.</param>
    public void EditTransactionAmount(int index, decimal newAmountValue)
    {
        ITransaction transaction = this._totalTransactionDataList[index];
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
        ITransaction transaction = this._totalTransactionDataList[index];
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
        ITransaction transaction = this._totalTransactionDataList[index];
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
        ITransaction transaction = this._totalTransactionDataList[index];
        switch (transaction)
        {
            case IncomeTransactionData income:
                this.TotalIncome -= income.Amount;
                this.CurrentBalance -= income.Amount;
                this._totalTransactionDataList.RemoveAt(index);
                this._incomeDataList.Remove(income);
                break;
            case ExpenseTransactionData expense:
                this.TotalExpense -= expense.Amount;
                this.CurrentBalance += expense.Amount;
                this._totalTransactionDataList.RemoveAt(index);
                this._expenseDataList.Remove(expense);
                break;
            default:
                break;
        }
    }
}

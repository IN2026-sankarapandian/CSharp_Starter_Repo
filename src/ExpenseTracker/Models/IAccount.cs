namespace ExpenseTracker.Models;

/// <summary>
/// Represents an user account with balance and transaction info and the core operations.
/// </summary>
public interface IAccount
{
    /// <summary>
    /// Gets current balance of the account.
    /// </summary>
    /// <value>
    /// Current balance of the account.
    /// </value>
    public decimal CurrentBalance { get; }

    /// <summary>
    /// Gets total income of the account.
    /// </summary>
    /// <value>
    /// Current total income of the account.
    /// </value>
    public decimal TotalIncome { get; }

    /// <summary>
    /// Gets total expense of the account.
    /// </summary>
    /// <value>
    /// Current total expense of the account.
    /// </value>
    public decimal TotalExpense { get; }

    /// <summary>
    /// Gets the list of all transactions.
    /// </summary>
    /// <value>
    /// Its the list of all transactions.
    /// </value>
    List<ITransaction> TotalTransactionDataList { get; }

    /// <summary>
    /// Gets or sets list of available categories.
    /// </summary>
    /// <value>
    /// List of available categories.
    /// </value>
    public List<string> Categories { get; set; }

    /// <summary>
    /// Gets or sets list of available sources.
    /// </summary>
    /// <value>
    /// List of available sources.
    /// </value>
    public List<string> Sources { get; set; }

    /// <summary>
    /// Create the income transaction.
    /// </summary>
    /// <param name="incomeAmount">Income amount.</param>
    /// <param name="source">Source of income.</param>
    public void AddIncome(decimal incomeAmount, string source);

    /// <summary>
    /// Create the expense transaction.
    /// </summary>
    /// <param name="expenseAmount">Expense amount.</param>
    /// <param name="category">Category of expense.</param>
    public void AddExpense(decimal expenseAmount, string category);

    /// <summary>
    /// Edit the amount of transaction at specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newAmountValue">New amount value.</param>
    public void EditTransactionAmount(int index, decimal newAmountValue);

    /// <summary>
    /// Edits the source of income transaction of specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newSourceValue">New source value.</param>
    public void EditIncomeTransactionSource(int index, string newSourceValue);

    /// <summary>
    /// Edits category of expense transaction of specified index.
    /// </summary>
    /// <param name="index">Index of transaction to edit.</param>
    /// <param name="newCategoryValue">New category value.</param>
    public void EditExpenseTransactionCategory(int index, string newCategoryValue);

    /// <summary>
    /// Deletes a transaction in a specified index.
    /// </summary>
    /// <param name="index">Index of transaction to delete.</param>
    public void DeleteTransaction(int index);
}

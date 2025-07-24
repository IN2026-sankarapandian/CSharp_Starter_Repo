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
    decimal CurrentBalance { get; }

    /// <summary>
    /// Gets total income of the account.
    /// </summary>
    /// <value>
    /// Current total income of the account.
    /// </value>
    decimal TotalIncome { get; }

    /// <summary>
    /// Gets total expense of the account.
    /// </summary>
    /// <value>
    /// Current total expense of the account.
    /// </value>
    decimal TotalExpense { get; }

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
    List<string> Categories { get; set; }

    /// <summary>
    /// Gets or sets list of available sources.
    /// </summary>
    /// <value>
    /// List of available sources.
    /// </value>
    List<string> Sources { get; set; }

    /// <summary>
    /// Create the income transaction.
    /// </summary>
    /// <param name="incomeAmount">Income amount.</param>
    /// <param name="source">Source of income.</param>
    void AddIncome(decimal incomeAmount, string source);

    /// <summary>
    /// Create the expense transaction.
    /// </summary>
    /// <param name="expenseAmount">Expense amount.</param>
    /// <param name="category">Category of expense.</param>
    void AddExpense(decimal expenseAmount, string category);
}

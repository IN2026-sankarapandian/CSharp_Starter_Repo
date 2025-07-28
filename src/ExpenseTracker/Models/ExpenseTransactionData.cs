using ExpenseTracker.Constants;

namespace ExpenseTracker.Models;

/// <summary>
/// Represents the expense transaction data of how much amount transacted, when the transaction initiated and the category of expense.
/// </summary>
public class ExpenseTransactionData : ITransaction
{
    /// <inheritdoc/>
    public decimal Amount { get; set; }

    /// <inheritdoc/>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets description about transaction.
    /// </summary>
    /// <value>
    /// Description about transaction.
    /// </value>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Gets the transaction type.
    /// </summary>
    /// <value>
    /// Type of transaction.
    /// </value>
    public TransactionType TransactionType { get; } = TransactionType.Expense;
}

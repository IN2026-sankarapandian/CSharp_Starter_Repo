namespace ExpenseTracker.Models;

/// <summary>
/// Represents the income transaction data of how much amount transacted, when the transaction initiated and the source of income.
/// </summary>
public class IncomeTransactionData : ITransaction
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
    public string Source { get; set; } = null!;
}
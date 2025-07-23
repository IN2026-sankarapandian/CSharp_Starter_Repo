using System.Collections.Concurrent;

namespace ExpenseTracker.Models;

/// <summary>
/// Represents the income transaction data of how much amount transacted, when the transaction initiated and the source of income.
/// </summary>
public class IncomeTransactionData : ITransaction
{
    /// <summary>
    /// Gets or sets amount transferred.
    /// </summary>
    /// <value>
    /// Amount transferred.
    /// </value>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets time the transaction initiated.
    /// </summary>
    /// <value>
    /// Time the transaction initiated.
    /// </value>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets description about transaction.
    /// </summary>
    /// <value>
    /// Description about transaction.
    /// </value>
    public string Source { get; set; } = null!;

    /// <summary>
    /// Edits the amount of income.
    /// </summary>
    /// <param name="amount">New value for amount.</param>
    public void EditAmount(decimal amount) => this.Amount = amount;

    /// <summary>
    /// Edit the source of income.
    /// </summary>
    /// <param name="source">New value of income.</param>
    public void EditSource(string source) => this.Source = source;
}
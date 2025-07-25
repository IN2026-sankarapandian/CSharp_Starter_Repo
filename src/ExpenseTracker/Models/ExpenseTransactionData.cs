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
    /// Edits the amount of expense.
    /// </summary>
    /// <param name="amount">New value for amount</param>
    public void EditAmount(decimal amount) => this.Amount = amount;

    /// <summary>
    /// Edits the category of expense.
    /// </summary>
    /// <param name="category">New value for category</param>
    public void EditCategory(string category) => this.Category = category;
}

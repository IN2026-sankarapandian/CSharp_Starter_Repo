namespace ExpenseTracker.Models;

/// <summary>
/// Represents the expense transaction data of how much amount transacted, when the transaction initiated and the category of expense.
/// </summary>
public class ExpenseTransactionData : ITransaction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpenseTransactionData"/> class.
    /// </summary>
    /// <param name="amount">Expense amount</param>
    /// <param name="category">Expense category</param>
    public ExpenseTransactionData(decimal amount, string category)
    {
        this.Amount = amount;
        this.Category = category;
        this.CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// Gets or sets list of available categories.
    /// </summary>
    /// <value>
    /// List of available categories.
    /// </value>
    public static List<string> Categories { get; set; } = new List<string> { "Food", "rent", "Game" };

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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets description about transaction.
    /// </summary>
    /// <value>
    /// Description about transaction.
    /// </value>
    public string Category { get; set; }

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

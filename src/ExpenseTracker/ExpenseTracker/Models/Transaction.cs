namespace ExpenseTracker.Models;

/// <summary>
/// This is the interface for transaction based classes
/// </summary>
public interface ITransaction
{
    /// <summary>
    /// Gets or sets amount of transaction
    /// </summary>
    /// <value>
    /// Amount of transaction
    /// </value>
    int Amount { get; set; }

    /// <summary>
    /// Account used to transact
    /// </summary>
    /// <value>
    /// Account used to transact</placeholder>
    /// </value>
    Account UsedAccount { get; set; }

    /// <summary>
    /// Apply transaction
    /// </summary>
    /// <param name="amount"></param>
    public void Transact(int amount);
}

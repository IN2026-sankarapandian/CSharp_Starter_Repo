using ExpenseTracker.Constants.Enums;

namespace ExpenseTracker.Models;

/// <summary>
/// This is the base interface for all types transaction data like <see cref="ExpenseTransactionData"/>, <see cref="IncomeTransactionData"/>
/// It contains some common properties like Amount, CreatedAt.
/// </summary>
public interface ITransaction
{
    /// <summary>
    /// Gets or sets amount transferred
    /// </summary>
    /// <value>
    /// Amount transferred
    /// </value>
    decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets time the transaction initiated
    /// </summary>
    /// <value>
    /// Time the transaction initiated
    /// </value>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the type of transaction.
    /// </summary>
    /// <value>
    /// Type of the transaction.
    /// </value>
    TransactionType TransactionType { get; }
}
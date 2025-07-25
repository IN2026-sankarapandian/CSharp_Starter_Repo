namespace ExpenseTracker.Constants;

/// <summary>
/// Consist of option enums used for better code readability.
/// </summary>
public class OptionEnums
{
    /// <summary>
    /// Transaction filter options for transaction list view.
    /// </summary>
    public enum TransactionFilter
    {
        /// <summary>
        /// Shows only income
        /// </summary>
        Income,

        /// <summary>
        /// Shows only expense
        /// </summary>
        Expense,

        /// <summary>
        /// Shows all transaction
        /// </summary>
        All,
    }
}

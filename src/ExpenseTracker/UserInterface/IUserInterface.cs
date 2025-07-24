using ExpenseTracker.Models;

namespace ExpenseTracker.UserInterface;

/// <summary>
/// Represents the user interface and its methods to interact with user.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Prompt the user and returns the user's'input
    /// </summary>
    /// <param name="prompt">Prompt to show user.</param>
    /// <returns>User's input.</returns>
    string? PromptAndGetInput(string prompt);

    /// <summary>
    /// Shows title to the user.
    /// </summary>
    /// <param name="action">Title to show user.</param>
    void MoveToAction(string action);

    /// <summary>
    /// Shows info to the user.
    /// </summary>
    /// <param name="info">Info to show user.</param>
    void ShowInfoMessage(string info);

    /// <summary>
    /// Shows warning to the user.
    /// </summary>
    /// <param name="warningMessage">Warning message to show user.</param>
    void ShowWarningMessage(string warningMessage);

    /// <summary>
    /// Shows success message to the user.
    /// </summary>
    /// <param name="successMessage">Success message to show user.</param>
    void ShowSuccessMessage(string successMessage);

    /// <summary>
    /// Shows transaction list to the user.
    /// </summary>
    /// <param name="userTransactionDataList">Transaction list to show user.</param>
    /// <param name="showIncome">Income filter.</param>
    /// <param name="showExpense">Expense filter.</param>
    void ShowTransactionList(List<ITransaction> userTransactionDataList, bool showIncome = true, bool showExpense = true);

    /// <summary>
    /// Shows transaction data to the user.
    /// </summary>
    /// <param name="transactionData">Transaction data to show user.</param>
    void ShowTransactionData(ITransaction transactionData);
}

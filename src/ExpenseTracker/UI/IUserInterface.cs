using ExpenseTracker.Models;

namespace ExpenseTracker.UserInterface;

/// <summary>
/// Represents the user interface and its methods to interact with user.
/// </summary>
public interface IUserInterface
{
    /// <summary>
    /// Prompt the user and returns the user non null input
    /// </summary>
    /// <param name="prompt">Prompt to show user.</param>
    /// <returns>User non null input.</returns>
    public string PromptAndGetNonNullInput(string prompt);

    /// <summary>
    /// Shows title to the user.
    /// </summary>
    /// <param name="title">Title to show user.</param>
    public void ShowTitle(string title);

    /// <summary>
    /// Shows info to the user.
    /// </summary>
    /// <param name="info">Info to show user.</param>
    public void ShowInfoMessage(string info);

    /// <summary>
    /// Shows warning to the user.
    /// </summary>
    /// <param name="warningMessage">Warning message to show user.</param>
    public void ShowWarningMessage(string warningMessage);

    /// <summary>
    /// Shows success message to the user.
    /// </summary>
    /// <param name="successMessage">Success message to show user.</param>
    public void ShowSuccessMessage(string successMessage);

    /// <summary>
    /// Shows transaction list to the user.
    /// </summary>
    /// <param name="userTransactionDataList">Transaction list to show user.</param>
    /// <param name="showIncome">Income filter.</param>
    /// <param name="showExpense">Expense filter.</param>
    public void ShowTransactionList(List<ITransaction> userTransactionDataList, bool showIncome = true, bool showExpense = true);

    /// <summary>
    /// Shows transaction data to the user.
    /// </summary>
    /// <param name="transactionData">Transaction data to show user.</param>
    public void ShowTransactionData(ITransaction transactionData);
}

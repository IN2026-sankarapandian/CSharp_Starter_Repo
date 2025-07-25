using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;

namespace ExpenseTracker.Handlers;

/// <summary>
/// Represents the controller for expense tracker.
/// </summary>
public interface IController
{
    /// <summary>
    /// Handles menu of expense tracker
    /// </summary>
    /// <remarks>
    /// Lists all available action options user and prompt the user to select action by index,
    /// calls the respective action handling methods.
    /// </remarks>
    void HandleMenu();

    /// <summary>
    /// Handle getting user inputs and add a income transaction to user's <see cref="Account"/>.
    /// </summary>
    void HandleAddIncome();

    /// <summary>
    /// Handle getting user inputs and add a expense transaction to user's <see cref="Account"/>.
    /// </summary>
    void HandleAddExpense();

    /// <summary>
    /// Handle showing the transaction details to user from user's <see cref="Account"/>.
    /// </summary>
    void HandleViewTransactions();

    /// <summary>
    /// Handle getting user inputs and edit a transaction to user's <see cref="Account"/>.
    /// </summary>
    void HandleEditTransaction();

    /// <summary>
    /// Handle getting user inputs and delete a transaction from user's <see cref="Account"/>.
    /// </summary>
    void HandleDeleteTransaction();
}
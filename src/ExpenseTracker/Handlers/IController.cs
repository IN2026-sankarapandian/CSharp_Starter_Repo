using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;

namespace ExpenseTracker.Handlers;

/// <summary>
/// Represents the controller for expense tracker.
/// </summary>
public interface IController
{
    /// <summary>
    /// Sets the user account to implement user actions.
    /// </summary>
    /// <value>
    /// The user account to implement user actions.
    /// </value>
    IAccount UserAccount { set; }

    /// <summary>
    /// Sets UI object for this controller.
    /// </summary>
    /// <value>
    /// User interface object.
    /// </value>
    public IUserInterface UserInterface { set; }

    /// <summary>
    /// Handles menu of expense tracker
    /// </summary>
    /// <remarks>
    /// Lists all available action options user and prompt the user to select action by index,
    /// calls the respective action handling methods.
    /// </remarks>
    public void HandleMenu();

    /// <summary>
    /// Handle getting user inputs and add a income transaction to user's <see cref="Account"/>.
    /// </summary>
    public void HandleAddIncome();

    /// <summary>
    /// Handle getting user inputs and add a expense transaction to user's <see cref="Account"/>.
    /// </summary>
    public void HandleAddExpense();

    /// <summary>
    /// Handle showing the transaction details to user from user's <see cref="Account"/>.
    /// </summary>
    public void HandleViewTransactions();
}
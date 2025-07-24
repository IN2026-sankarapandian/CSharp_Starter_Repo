using ExpenseTracker.Handlers;
using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;

namespace ExpenseTracker;

/// <summary>
/// The main class contains the main method, entry point of a expense tracker, c# application.
/// </summary>
public class ExpenseTracker
{
    /// <summary>
    /// The Main method is the entry point of the expense tracker, creates a new instance of <see cref="Account"/>.
    /// </summary>
    public static void Main()
    {
        IAccount userAccount = new Account();
        IUserInterface consoleUI = new ConsoleUI();
        IController controller = new Controller(userAccount, consoleUI);
        controller.HandleMenu();
    }
}
using ExpenseTracker.Constants;
using ExpenseTracker.Constants.Enums;
using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;

namespace ExpenseTracker.Handlers;

/// <summary>
/// Provides method to handle higher level operations of expense tracker to add, edit, delete transactions.
/// </summary>
public class Controller : IController
{
    /// <summary>
    /// Sets the user account to implement user actions.
    /// </summary>
    private readonly IAccount _userAccount;

    /// <summary>
    /// Sets UI object for this controller.
    /// </summary>
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Controller"/> class.
    /// Sets the user account to access user transactions details.
    /// </summary>
    /// <param name="userAccount">Users account.</param>
    /// <param name="consoleUI">User Interface.</param>
    public Controller(IAccount userAccount, IUserInterface consoleUI)
    {
        this._userAccount = userAccount;
        this._userInterface = consoleUI;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Lists all available action options user and prompt the user to select action by index,
    /// calls the respective action handling methods.
    /// </remarks>
    public void HandleMenu()
    {
        this._userInterface.MoveToAction(string.Format(Headings.Menu));
        do
        {
            this._userInterface.ShowInfoMessage(this.AccountStatsFormat(
                this._userAccount.TotalIncome,
                this._userAccount.TotalIncome,
                this._userAccount.TotalExpense));
            this._userInterface.ShowInfoMessage(PromptMessages.MenuPrompt);
            string userChoice = this.GetInputFromUser(PromptMessages.EnterChoice);
            switch (userChoice)
            {
                // Add income transaction
                case "1":
                    this.HandleAddIncome();
                    break;

                // Add expense transaction
                case "2":
                    this.HandleAddExpense();
                    break;

                // View transaction
                case "3":
                    this.HandleViewTransactions();
                    break;

                // Exit application
                case "4":
                    return;

                // Invalid choice
                default:
                    this._userInterface.ShowWarningMessage(ErrorMessages.EnterValidChoice);
                    break;
            }
        }
        while (true);
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method gets the inputs details like amount and source of income and add it to the user's <see cref="Account"/>.
    /// </remarks>
    public void HandleAddIncome()
    {
        this._userInterface.MoveToAction(Headings.AddIncome);
        decimal incomeAmount = this.GetAmountFromUser(PromptMessages.EnterIncome);
        string source = this.GetTagFromUser(PromptMessages.EnterSource, TransactionType.Income);
        this._userAccount.AddIncome(incomeAmount, source);
        this._userInterface.ShowSuccessMessage(StatusMessages.IncomeAddedSuccessfully);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(string.Format(Headings.Menu));
        return;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method gets the inputs details like amount and category of expense and add it to the user's <see cref="Account"/>.
    /// </remarks>
    public void HandleAddExpense()
    {
        this._userInterface.MoveToAction(Headings.AddExpense);
        decimal expenseAmount = this.GetAmountFromUser(PromptMessages.EnterExpense);
        string category = this.GetTagFromUser(PromptMessages.EnterCategory, TransactionType.Expense);
        this._userAccount.AddExpense(expenseAmount, category);
        this._userInterface.ShowSuccessMessage(StatusMessages.ExpenseAddedSuccessfully);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(string.Format(Headings.Menu));
    }

    /// <inheritdoc/>
    public void HandleViewTransactions()
    {
        this._userInterface.MoveToAction(Headings.Entries);
        do
        {
            this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionType.All);
            if (this._userAccount.TotalTransactionDataList.Count == 0)
            {
                this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
                this._userInterface.MoveToAction(string.Format(Headings.Menu));
            }
            else
            {
                this._userInterface.ShowInfoMessage(PromptMessages.ViewPrompt);
                string userViewChoice = this.GetInputFromUser(PromptMessages.EnterChoice);
                switch (userViewChoice)
                {
                    // Show income entries
                    case "1":
                        this.ShowIncomeEntries();
                        break;

                    // Show expense entries
                    case "2":
                        this.ShowExpenseEntries();
                        break;

                    // Go back
                    case "3":
                        this._userInterface.MoveToAction(string.Format(Headings.Menu));
                        return;

                    // Invalid choice
                    default:
                        this._userInterface.ShowWarningMessage(ErrorMessages.EnterValidChoice);
                        break;
                }
            }
        }
        while (true);
    }

    /// <summary>
    /// Gives account stats as structured string format.
    /// </summary>
    /// <param name="currentBalance">Current balance of user account.</param>
    /// <param name="totalIncome">Current total income of user account.</param>
    /// <param name="totalExpense">Current total expense of user account.</param>
    /// <returns>Structured string format of account stats.</returns>
    private string AccountStatsFormat(decimal currentBalance, decimal totalIncome, decimal totalExpense)
        => string.Format(PromptMessages.AccountStats, currentBalance, totalIncome, totalExpense);

    /// <summary>
    /// Gets the valid amount from user.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <returns>Amount value</returns>
    private decimal GetAmountFromUser(string prompt)
    {
        do
        {
            string amountString = this.GetInputFromUser(prompt);
            if (!decimal.TryParse(amountString, out decimal amount))
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.NotValidAmount);
            }
            else if (amount <= 0)
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.AmountCantBeLessThanZero);
            }
            else
            {
                return amount;
            }
        }
        while (true);
    }

    /// <summary>
    /// Get the valid tag for the transaction type from user.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <param name="transactionType">Get the respective tag(Source, category) of the type.</param>
    /// <returns>Selected prompt by user.</returns>
    private string GetTagFromUser(string prompt, TransactionType transactionType)
    {
        List<string> tags;
        string tagName;
        if (transactionType == TransactionType.Income)
        {
            tags = this._userAccount.Sources;
            tagName = Headings.Source;
            this._userInterface.ShowInfoMessage(PromptMessages.Sources);
        }
        else
        {
            tags = this._userAccount.Categories;
            tagName = Headings.Category;
            this._userInterface.ShowInfoMessage(PromptMessages.Categories);
        }

        for (int i = 0; i < tags.Count; i++)
        {
            this._userInterface.ShowInfoMessage($"{i + 1}. {tags[i]}");
        }

        this._userInterface.ShowInfoMessage($"{tags.Count + 1}. {string.Format(PromptMessages.NewTag, tagName)}");
        do
        {
            string? tagIndexString = this._userInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(tagIndexString, out int tagIndex))
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, tags.Count + 1));
            }
            else if (tagIndex > 0 && tagIndex <= tags.Count)
            {
                return tags[tagIndex - 1];
            }
            else if (tagIndex == tags.Count + 1)
            {
                string newTag = this.GetInputFromUser(string.Format(PromptMessages.EnterNewTag, tagName));
                this._userAccount.Categories.Add(newTag);
                return newTag;
            }
        }
        while (true);
    }

    /// <summary>
    /// Shows all the expense entries.
    /// </summary>
    private void ShowExpenseEntries()
    {
        this._userInterface.MoveToAction(Headings.ExpenseEntries);
        this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionType.Expense);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(Headings.Entries);
    }

    /// <summary>
    /// Shows all the income entries.
    /// </summary>
    private void ShowIncomeEntries()
    {
        this._userInterface.MoveToAction(Headings.IncomeEntries);
        this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionType.Income);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(Headings.Entries);
    }

    /// <summary>
    /// Get non null string.
    /// </summary>
    /// <param name="prompt">Prompt to show.</param>
    /// <returns>User input string.</returns>
    private string GetInputFromUser(string prompt)
    {
        do
        {
            string? input = this._userInterface.PromptAndGetInput(prompt);
            if (string.IsNullOrEmpty(input))
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
            }
            else
            {
                return input;
            }
        }
        while (true);
    }
}
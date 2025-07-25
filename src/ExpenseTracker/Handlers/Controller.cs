using ExpenseTracker.Constants;
using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;

namespace ExpenseTracker.Handlers;

/// <summary>
/// Provides method to handle higher level operations of expense tracker to add, edit, delete transactions.
/// </summary>
public class Controller : IController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Controller"/> class.
    /// Sets the user account to access user transactions details.
    /// </summary>
    /// <param name="userAccount">Users account.</param>
    /// <param name="consoleUI">User Interface.</param>
    public Controller(IAccount userAccount, IUserInterface consoleUI)
    {
        this.UserAccount = userAccount;
        this.UserInterface = consoleUI;
    }

    /// <inheritdoc/>
    public IAccount UserAccount { private get; set; }

    /// <inheritdoc/>
    public IUserInterface UserInterface { private get; set; }

    /// <inheritdoc/>
    /// <remarks>
    /// Lists all available action options user and prompt the user to select action by index,
    /// calls the respective action handling methods.
    /// </remarks>
    public void HandleMenu()
    {
        this.UserInterface.MoveToAction(string.Format(Headings.Menu));
        do
        {
            this.UserInterface.ShowInfoMessage(this.AccountStatsFormat(this.UserAccount.TotalIncome, this.UserAccount.TotalIncome, this.UserAccount.TotalExpense));
            this.UserInterface.ShowInfoMessage(PromptMessages.MenuPrompt);
            string? userChoice = this.UserInterface.PromptAndGetInput(PromptMessages.EnterChoice);
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
                    this.UserInterface.ShowWarningMessage(ErrorMessages.EnterValidChoice);
                    continue;
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
        this.UserInterface.MoveToAction(Headings.AddIncome);
        decimal incomeAmount = this.GetAmountFromUser(PromptMessages.EnterIncome);
        string source = this.GetSourceFromUser(PromptMessages.EnterSource);
        this.UserAccount.AddIncome(incomeAmount, source);
        this.UserInterface.ShowSuccessMessage(StatusMessages.IncomeAddedSuccessfully);
        this.UserInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this.UserInterface.MoveToAction(string.Format(Headings.Menu));
        return;
    }

    /// <inheritdoc/>
    /// /// <remarks>
    /// This method gets the inputs details like amount and category of expense and add it to the user's <see cref="Account"/>.
    /// </remarks>
    public void HandleAddExpense()
    {
        this.UserInterface.MoveToAction(Headings.AddExpense);
        decimal expenseAmount = this.GetAmountFromUser(PromptMessages.EnterExpense);
        string category = this.GetCategoryFromUser(PromptMessages.EnterCategory);
        this.UserAccount.AddExpense(expenseAmount, category);
        this.UserInterface.ShowSuccessMessage(StatusMessages.ExpenseAddedSuccessfully);
        this.UserInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this.UserInterface.MoveToAction(string.Format(Headings.Menu));
    }

    /// <inheritdoc/>
    public void HandleViewTransactions()
    {
        do
        {
            this.UserInterface.MoveToAction(Headings.Entries);
            this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, true, true);
            if (this.UserAccount.TotalTransactionDataList.Count == 0)
            {
                this.UserInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
                this.UserInterface.MoveToAction(string.Format(Headings.Menu));
                return;
            }

            this.UserInterface.ShowInfoMessage(PromptMessages.ViewPrompt);
            string? userViewChoice = this.UserInterface.PromptAndGetInput(PromptMessages.EnterChoice);
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
                    this.UserInterface.MoveToAction(string.Format(Headings.Menu));
                    return;

                // Invalid choice
                default:
                    this.UserInterface.ShowWarningMessage(ErrorMessages.EnterValidChoice);
                    continue;
            }
        }
        while (true);
    }

    /// <summary>
    /// Gets the valid amount from user.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <returns>Amount value</returns>
    private decimal GetAmountFromUser(string prompt)
    {
        string? amountString;
        do
        {
            amountString = this.UserInterface.PromptAndGetInput(prompt);
            if (!decimal.TryParse(amountString, out decimal amount))
            {
                this.UserInterface.ShowWarningMessage(ErrorMessages.NotValidAmount);
                continue;
            }

            if (amount <= 0)
            {
                this.UserInterface.ShowWarningMessage(ErrorMessages.AmountCantBeLessThanZero);
                continue;
            }

            return amount;
        }
        while (true);
    }

    /// <summary>
    /// Get the valid source from user.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <returns>Selected source by user.</returns>
    private string GetSourceFromUser(string prompt)
    {
        List<string> sources = this.UserAccount.Sources;
        this.UserInterface.ShowInfoMessage(PromptMessages.Sources);
        for (int i = 0; i < sources.Count; i++)
        {
            this.UserInterface.ShowInfoMessage($"{i + 1}. {sources[i]}");
        }

        this.UserInterface.ShowInfoMessage($"{sources.Count + 1}. {PromptMessages.NewSource}");

        do
        {
            string? sourceIndexString = this.UserInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(sourceIndexString, out int sourceIndex))
            {
                this.UserInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, sources.Count + 1));
                continue;
            }

            if (sourceIndex > 0 && sourceIndex <= sources.Count)
            {
                return sources[sourceIndex - 1];
            }
            else if (sourceIndex == sources.Count + 1)
            {
                string? newSource;
                do
                {
                    newSource = this.UserInterface.PromptAndGetInput(PromptMessages.EnterNewSource);
                    if (string.IsNullOrEmpty(newSource))
                    {
                        this.UserInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);
                this.UserAccount.Sources.Add(newSource);
                return newSource;
            }
            else
            {
                this.UserInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, sources.Count + 1));
                continue;
            }
        }
        while (true);
    }

    /// <summary>
    /// Get the valid category from user.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <returns>Selected prompt by user.</returns>
    private string GetCategoryFromUser(string prompt)
    {
        List<string> categories = this.UserAccount.Categories;
        this.UserInterface.ShowInfoMessage(PromptMessages.Categories);
        for (int i = 0; i < categories.Count; i++)
        {
            this.UserInterface.ShowInfoMessage($"{i + 1}. {categories[i]}");
        }

        this.UserInterface.ShowInfoMessage($"{categories.Count + 1}. {PromptMessages.NewCategory}");
        do
        {
            string? categoryIndexString = this.UserInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(categoryIndexString, out int categoryIndex))
            {
                this.UserInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, categories.Count + 1));
                continue;
            }

            if (categoryIndex > 0 && categoryIndex <= categories.Count)
            {
                return categories[categoryIndex - 1];
            }
            else if (categoryIndex == categories.Count + 1)
            {
                string? newCategory;
                do
                {
                    newCategory = this.UserInterface.PromptAndGetInput(PromptMessages.EnterCategory);
                    if (string.IsNullOrEmpty(newCategory))
                    {
                        this.UserInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);
                this.UserAccount.Categories.Add(newCategory);
                return newCategory;
            }
            else
            {
                this.UserInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, categories.Count + 1));
                continue;
            }
        }
        while (true);
    }

    /// <summary>
    /// Get the valid index that represents the any existing transaction from transaction list from user.
    /// </summary>
    /// <returns>Selected index value by user.</returns>
    private int GetIndexFromUser(string prompt)
    {
        int selectedIndex;
        do
        {
            string? selectedIndexString = this.UserInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(selectedIndexString, out selectedIndex))
            {
                this.UserInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, this.UserAccount.TotalTransactionDataList.Count));
                continue;
            }

            if (selectedIndex <= 0 || selectedIndex - 1 >= this.UserAccount.TotalTransactionDataList.Count)
            {
                this.UserInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, this.UserAccount.TotalTransactionDataList.Count));
                continue;
            }

            break;
        }
        while (true);
        return selectedIndex - 1;
    }

    /// <summary>
    /// Shows all the expense entries.
    /// </summary>
    private void ShowExpenseEntries()
    {
        this.UserInterface.MoveToAction(Headings.ExpenseEntry);
        this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, false, true);
        this.UserInterface.ShowInfoMessage(PromptMessages.PressEnterToGoBack);
        Console.ReadKey();
    }

    /// <summary>
    /// Shows all the income entries.
    /// </summary>
    private void ShowIncomeEntries()
    {
        this.UserInterface.MoveToAction(Headings.ExpenseEntry);
        this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, true, false);
        this.UserInterface.ShowInfoMessage(PromptMessages.PressEnterToGoBack);
        Console.ReadKey();
    }

    /// <summary>
    /// Gives account stats as structured string format.
    /// </summary>
    /// <param name="currentBalance">Current balance of user account.</param>
    /// <param name="totalIncome">Current total income of user account.</param>
    /// <param name="totalExpense">Current total expense of user account.</param>
    /// <returns>Structured string format of account stats.</returns>
    private string AccountStatsFormat(decimal currentBalance, decimal totalIncome, decimal totalExpense)
    {
        return string.Format(PromptMessages.AccountStats, currentBalance, totalIncome, totalExpense);
    }
}
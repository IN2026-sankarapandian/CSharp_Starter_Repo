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
            this._userInterface.ShowInfoMessage(this.AccountStatsFormat(this._userAccount.TotalIncome, this._userAccount.TotalIncome, this._userAccount.TotalExpense));
            this._userInterface.ShowInfoMessage(PromptMessages.MenuPrompt);
            string? userChoice = this._userInterface.PromptAndGetInput(PromptMessages.EnterChoice);
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

                // Edit transaction
                case "4":
                    this.HandleEditTransaction();
                    break;

                // Delete transaction
                case "5":
                    this.HandleDeleteTransaction();
                    break;

                // Exit application
                case "6":
                    return;

                // Invalid choice
                default:
                    this._userInterface.ShowWarningMessage(ErrorMessages.EnterValidChoice);
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
        this._userInterface.MoveToAction(Headings.AddIncome);
        decimal incomeAmount = this.GetAmountFromUser(PromptMessages.EnterIncome);
        string source = this.GetSourceFromUser(PromptMessages.EnterSource);
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
        string category = this.GetCategoryFromUser(PromptMessages.EnterNewCategory);
        this._userAccount.AddExpense(expenseAmount, category);
        this._userInterface.ShowSuccessMessage(StatusMessages.ExpenseAddedSuccessfully);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(string.Format(Headings.Menu));
    }

    /// <inheritdoc/>
    public void HandleViewTransactions()
    {
        do
        {
            this._userInterface.MoveToAction(Headings.Entries);
            this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionFilter.All);
            if (this._userAccount.TotalTransactionDataList.Count == 0)
            {
                this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
                this._userInterface.MoveToAction(string.Format(Headings.Menu));
                return;
            }

            this._userInterface.ShowInfoMessage(PromptMessages.ViewPrompt);
            string? userViewChoice = this._userInterface.PromptAndGetInput(PromptMessages.EnterChoice);
            if (string.IsNullOrEmpty(userViewChoice))
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
                continue;
            }

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
                    continue;
            }
        }
        while (true);
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method Lists all the transaction to user and prompt the user to select the transaction to edit by index
    /// and then shows the selected transaction data and prompt the user to select field to edit and new value fot it.
    /// Finally it will edit the transaction from user <see cref="Acccount"/>
    /// </remarks>
    public void HandleEditTransaction()
    {
    SelectIndex:
        this._userInterface.MoveToAction(Headings.Edit);
        this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionFilter.All);
        if (this._userAccount.TotalTransactionDataList.Count == 0)
        {
            this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
            this._userInterface.MoveToAction(string.Format(Headings.Menu));
            return;
        }

        int indexToEdit = this.GetIndexFromUser(PromptMessages.EnterIndexToEdit);
        this._userInterface.MoveToAction(Headings.Edit);
        ITransaction selectedTransactionData = this._userAccount.TotalTransactionDataList[indexToEdit];
        this._userInterface.ShowTransactionData(selectedTransactionData);
        do
        {
            this._userInterface.ShowInfoMessage(string.Format(PromptMessages.EditPrompt, selectedTransactionData is IncomeTransactionData ? "Source" : "Category"));
            string? fieldToEdit = this._userInterface.PromptAndGetInput(PromptMessages.EnterWhatToEdit);
            if (string.IsNullOrEmpty(fieldToEdit))
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
                continue;
            }

            switch (fieldToEdit)
            {
                // Edit amount
                case "1":
                    this.EditAmountOfTransaction(indexToEdit);
                    goto promptSuccess;

                // Edit source or category
                case "2":
                    this.EditCategoryOrSourceFromUser(indexToEdit);
                    goto promptSuccess;

                // Go back to select index
                case "3":
                    goto SelectIndex;

                // Invalid choice
                default:
                    this._userInterface.ShowWarningMessage(ErrorMessages.EnterValidChoice);
                    continue;
            }
        }
        while (true);

    // The updated content is shown to the user
    promptSuccess:
        this._userInterface.MoveToAction(Headings.Edit);
        selectedTransactionData = this._userAccount.TotalTransactionDataList[indexToEdit];
        this._userInterface.ShowTransactionData(selectedTransactionData);
        this._userInterface.ShowSuccessMessage(StatusMessages.EditedSucccessfuly);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(string.Format(Headings.Menu));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method lists all the transaction to the user and prompt the user to select transaction to delete by index.
    /// </remarks>
    public void HandleDeleteTransaction()
    {
        this._userInterface.MoveToAction(Headings.Delete);
        this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionFilter.All);
        if (this._userAccount.TotalTransactionDataList.Count == 0)
        {
            this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
            this._userInterface.MoveToAction(string.Format(Headings.Menu));
            return;
        }

        int indexToDelete = this.GetIndexFromUser(PromptMessages.EnterIndexToDelete);
        this._userAccount.DeleteTransaction(indexToDelete);
        this._userInterface.ShowSuccessMessage(StatusMessages.EditedSucccessfuly);
        this._userInterface.PromptAndGetInput(PromptMessages.PressEnterToGoBack);
        this._userInterface.MoveToAction(string.Format(Headings.Menu));
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
            amountString = this._userInterface.PromptAndGetInput(prompt);
            if (!decimal.TryParse(amountString, out decimal amount))
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.NotValidAmount);
                continue;
            }

            if (amount <= 0)
            {
                this._userInterface.ShowWarningMessage(ErrorMessages.AmountCantBeLessThanZero);
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
        List<string> sources = this._userAccount.Sources;
        this._userInterface.ShowInfoMessage(PromptMessages.Sources);
        for (int i = 0; i < sources.Count; i++)
        {
            this._userInterface.ShowInfoMessage($"{i + 1}. {sources[i]}");
        }

        this._userInterface.ShowInfoMessage($"{sources.Count + 1}. {PromptMessages.NewSource}");

        do
        {
            string? sourceIndexString = this._userInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(sourceIndexString, out int sourceIndex))
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, sources.Count + 1));
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
                    newSource = this._userInterface.PromptAndGetInput(PromptMessages.EnterNewSource);
                    if (string.IsNullOrEmpty(newSource))
                    {
                        this._userInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);
                this._userAccount.Sources.Add(newSource);
                return newSource;
            }
            else
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, sources.Count + 1));
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
        List<string> categories = this._userAccount.Categories;
        this._userInterface.ShowInfoMessage(PromptMessages.Categories);
        for (int i = 0; i < categories.Count; i++)
        {
            this._userInterface.ShowInfoMessage($"{i + 1}. {categories[i]}");
        }

        this._userInterface.ShowInfoMessage($"{categories.Count + 1}. {PromptMessages.NewCategory}");
        do
        {
            string? categoryIndexString = this._userInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(categoryIndexString, out int categoryIndex))
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, categories.Count + 1));
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
                    newCategory = this._userInterface.PromptAndGetInput(PromptMessages.EnterCategory);
                    if (string.IsNullOrEmpty(newCategory))
                    {
                        this._userInterface.ShowWarningMessage(ErrorMessages.InputCannotBeEmpty);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);
                this._userAccount.Categories.Add(newCategory);
                return newCategory;
            }
            else
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, categories.Count + 1));
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
            string? selectedIndexString = this._userInterface.PromptAndGetInput(prompt);
            if (!int.TryParse(selectedIndexString, out selectedIndex))
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, this._userAccount.TotalTransactionDataList.Count));
                continue;
            }

            if (selectedIndex <= 0 || selectedIndex - 1 >= this._userAccount.TotalTransactionDataList.Count)
            {
                this._userInterface.ShowWarningMessage(string.Format(ErrorMessages.EnterValidIndex, this._userAccount.TotalTransactionDataList.Count));
                continue;
            }

            break;
        }
        while (true);
        return selectedIndex - 1;
    }

    /// <summary>
    /// Edits the category or source of the specified transaction based on its type with user inputs.
    /// </summary>
    /// <param name="indexToEdit">Index value of transaction in users account.</param>
    private void EditCategoryOrSourceFromUser(int indexToEdit)
    {
        ITransaction selectedTransactionData = this._userAccount.TotalTransactionDataList[indexToEdit];
        if (selectedTransactionData is IncomeTransactionData)
        {
            string newSource = this.GetSourceFromUser(PromptMessages.EnterNewSource);
            this._userAccount.EditIncomeTransactionSource(indexToEdit, newSource);
        }
        else
        {
            string newCategory = this.GetCategoryFromUser(PromptMessages.EnterNewCategory);
            this._userAccount.EditExpenseTransactionCategory(indexToEdit, newCategory);
        }
    }

    /// <summary>
    /// Edits the amount of specified transaction with user inputs.
    /// <param name="indexToEdit"></param>
    private void EditAmountOfTransaction(int indexToEdit)
    {
        decimal newAmount = this.GetAmountFromUser(PromptMessages.EnterNewAmount);
        this._userAccount.EditTransactionAmount(indexToEdit, newAmount);
    }

    /// <summary>
    /// Shows all the expense entries.
    /// </summary>
    private void ShowExpenseEntries()
    {
        this._userInterface.MoveToAction(Headings.ExpenseEntry);
        this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionFilter.Expense);
        this._userInterface.ShowInfoMessage(PromptMessages.PressEnterToGoBack);
        Console.ReadKey();
    }

    /// <summary>
    /// Shows all the income entries.
    /// </summary>
    private void ShowIncomeEntries()
    {
        this._userInterface.MoveToAction(Headings.ExpenseEntry);
        this._userInterface.ShowTransactionList(this._userAccount.TotalTransactionDataList, TransactionFilter.Income);
        this._userInterface.ShowInfoMessage(PromptMessages.PressEnterToGoBack);
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
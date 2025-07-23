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

    /// <summary>
    /// Sets the user account to implement user actions.
    /// </summary>
    /// <value>
    /// The user account to implement user actions.
    /// </value>
    public IAccount UserAccount { private get; set; }

    /// <summary>
    /// Sets UI object for this controller.
    /// </summary>
    /// <value>
    /// User interface object.
    /// </value>
    public IUserInterface UserInterface { private get; set; }

    /// <summary>
    /// Handles menu of expense tracker
    /// </summary>
    /// <remarks>
    /// Lists all available action options user and prompt the user to select action by index,
    /// calls the respective action handling methods.
    /// </remarks>
    public void HandleMenu()
    {
        do
        {
            this.UserInterface.ShowTitle("Menu");
            this.UserInterface.ShowInfoMessage($"Current balance : {this.UserAccount.CurrentBalance}");
            this.UserInterface.ShowInfoMessage($"Total income : +{this.UserAccount.TotalIncome}");
            this.UserInterface.ShowInfoMessage($"Total expense : -{this.UserAccount.TotalExpense}\n");
            this.UserInterface.ShowInfoMessage("1. Add income\n2. Add expense\n3. View all transactions\n4. Edit transaction \n5. Delete transaction\n6. Exit application\n");
            string userChoice = this.UserInterface.PromptAndGetNonNullInput("What do you want to do : ");
            switch (userChoice)
            {
                case "1":
                    this.HandleAddIncome();
                    break;
                case "2":
                    this.HandleAddExpense();
                    break;
                case "3":
                    this.HandleViewTransactions();
                    break;
                case "4":
                    this.HandleEditTransaction();
                    break;
                case "5":
                    this.HandleDeleteTransaction();
                    break;
                case "6":
                    return;
            }
        }
        while (true);
    }

    /// <summary>
    /// Handle getting user inputs and add a income transaction to user's <see cref="Account"/>.
    /// </summary>
    /// <remarks>
    /// This method gets the inputs details like amount and source of income and add it to the user's <see cref="Account"/>.
    /// </remarks>
    public void HandleAddIncome()
    {
        this.UserInterface.ShowTitle("Add income");
        decimal incomeAmount = this.GetAmountFromUser("Enter the income  : ");
        string source = this.GetSourceFromUser("Enter a source : ");
        this.UserAccount.AddIncome(incomeAmount, source);
        this.UserInterface.ShowSuccessMessage("Income Entry Added successfully !");
        this.UserInterface.ShowInfoMessage("Key Press to go back exit ....");
        Console.ReadKey();
        return;
    }

    /// <summary>
    /// Handle getting user inputs and add a expense transaction to user's <see cref="Account"/>.
    /// </summary>
    /// /// <remarks>
    /// This method gets the inputs details like amount and category of expense and add it to the user's <see cref="Account"/>.
    /// </remarks>
    public void HandleAddExpense()
    {
        this.UserInterface.ShowTitle("Add expense");
        decimal expenseAmount = this.GetAmountFromUser("Enter the expense  : ");
        string category = this.GetCategoryFromUser("Enter a category : ");
        this.UserAccount.AddExpense(expenseAmount, category);
        this.UserInterface.ShowSuccessMessage("Expense Entry Added successfully !");
        this.UserInterface.ShowInfoMessage("Key Press to go back ....");
        Console.ReadKey();
    }

    /// <summary>
    /// Handle showing the transaction details to user from user's <see cref="Account"/>.
    /// </summary>
    public void HandleViewTransactions()
    {
        do
        {
            this.UserInterface.ShowTitle("Entries");
            this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, true, true);
            this.UserInterface.ShowInfoMessage("1. View incomes alone\n2. View expenses alone\n3. Go back");
            string userViewChoice = this.UserInterface.PromptAndGetNonNullInput("Enter your choice : ");
            switch (userViewChoice)
            {
                case "1":
                    this.ShowIncomeEntries();
                    break;
                case "2":
                    this.ShowExpenseEntries();
                    break;
                case "3":
                    return;
            }
        }
        while (true);
    }

    /// <summary>
    /// Handle getting user inputs and edit a transaction to user's <see cref="Account"/>.
    /// </summary>
    /// <remarks>
    /// This method Lists all the transaction to user and prompt the user to select the transaction to edit by index
    /// and then shows the selected transaction data and prompt the user to select field to edit and new value fot it.
    /// Finally it will edit the transaction from user <see cref="Acccount"/>
    /// </remarks>
    public void HandleEditTransaction()
    {
    SelectIndex:
        this.UserInterface.ShowTitle("Edit");
        this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, true, true);
        int indexToEdit = this.GetIndexFromUser();
        this.UserInterface.ShowTitle("Edit");
        ITransaction selectedTransactionData = this.UserAccount.TotalTransactionDataList[indexToEdit];
        this.UserInterface.ShowTransactionData(selectedTransactionData);
        do
        {
            this.UserInterface.ShowInfoMessage($"\n1. Amount\n2. {(selectedTransactionData is IncomeTransactionData ? "Source" : "Category")}\n3. Go back\n");
            string fieldToEdit = this.UserInterface.PromptAndGetNonNullInput("What do you to edit : ");
            switch (fieldToEdit)
            {
                case "1":
                    this.EditAmountOfTransaction(indexToEdit);
                    goto promptSuccess;
                case "2":
                    this.EditCategoryOrSourceFromUser(indexToEdit);
                    goto promptSuccess;
                case "3":
                    goto SelectIndex;
                default:
                    this.UserInterface.ShowWarningMessage("Enter a valid choice !");
                    continue;
            }
        }
        while (true);

    // The updated content is shown to the user
    promptSuccess:
        this.UserInterface.ShowTitle("Edit");
        selectedTransactionData = this.UserAccount.TotalTransactionDataList[indexToEdit - 1];
        this.UserInterface.ShowTransactionData(selectedTransactionData);
        this.UserInterface.ShowSuccessMessage("Edited successfully");
        Console.ReadKey();
    }

    /// <summary>
    /// Handle getting user inputs and delete a transaction from user's <see cref="Account"/>.
    /// </summary>
    /// <remarks>
    /// This method lists all the transaction to the user and prompt the user to select transaction to delete by index.
    /// </remarks>
    public void HandleDeleteTransaction()
    {
        this.UserInterface.ShowTitle("Delete");
        this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, true, true);
        int indexToDelete = this.GetIndexFromUser();
        this.UserAccount.DeleteTransaction(indexToDelete);
        this.UserInterface.ShowSuccessMessage("Deleted successfully");
        Console.ReadKey();
    }

    /// <summary>
    /// Gets the valid amount from user.
    /// </summary>
    /// <param name="prompt">Prompt to show the user.</param>
    /// <returns>Amount value</returns>
    private decimal GetAmountFromUser(string prompt)
    {
        string amountString;
        do
        {
            amountString = this.UserInterface.PromptAndGetNonNullInput(prompt);
            if (!decimal.TryParse(amountString, out decimal amount))
            {
                this.UserInterface.ShowWarningMessage("Amount cant contain characters !");
                continue;
            }

            if (amount <= 0)
            {
                this.UserInterface.ShowWarningMessage("Amount cant be less than 0 !");
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
        this.UserInterface.ShowInfoMessage("\nSources : ");
        for (int i = 0; i < sources.Count; i++)
        {
            this.UserInterface.ShowInfoMessage($"{i + 1}. {sources[i]}");
        }

        do
        {
            this.UserInterface.ShowInfoMessage($"{sources.Count + 1}. new category");
            string sourceIndexString = this.UserInterface.PromptAndGetNonNullInput(prompt);
            int.TryParse(sourceIndexString, out int sourceIndex);
            if (sourceIndex >= 0 && sourceIndex < sources.Count)
            {
                return sources[sourceIndex - 1];
            }
            else if (sourceIndex == sources.Count + 1)
            {
                string newSource = this.UserInterface.PromptAndGetNonNullInput("Enter a new source : ");
                this.UserAccount.Sources.Add(newSource);
                return newSource;
            }
            else
            {
                this.UserInterface.ShowWarningMessage($"Give a valid input between 1 and {sources.Count}");
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
        this.UserInterface.ShowInfoMessage("\nCategory : ");
        for (int i = 0; i < categories.Count; i++)
        {
            this.UserInterface.ShowInfoMessage($"{i + 1}. {categories[i]}");
        }

        do
        {
            this.UserInterface.ShowInfoMessage($"{categories.Count + 1}. new category");
            string categoryIndexString = this.UserInterface.PromptAndGetNonNullInput(prompt);
            int.TryParse(categoryIndexString, out int categoryIndex);
            if (categoryIndex >= 0 && categoryIndex < categories.Count)
            {
                return categories[categoryIndex - 1];
            }
            else if (categoryIndex == categories.Count + 2)
            {
                string newSource = this.UserInterface.PromptAndGetNonNullInput("Enter a new category : ");
                this.UserAccount.Categories.Add(newSource);
                return newSource;
            }
            else
            {
                this.UserInterface.ShowWarningMessage($"Give a valid input between 1 and {categories.Count}");
                continue;
            }
        }
        while (true);
    }

    /// <summary>
    /// Get the valid index that represents the any existing transaction from transaction list from user.
    /// </summary>
    /// <returns>Selected index value by user.</returns>
    private int GetIndexFromUser()
    {
        int selectedIndex;
        do
        {
            string selectedIndexString = this.UserInterface.PromptAndGetNonNullInput("\nSelect the index of transaction to edit : ");
            int.TryParse(selectedIndexString, out selectedIndex);
            if (selectedIndex <= 0 || selectedIndex - 1 >= this.UserAccount.TotalTransactionDataList.Count)
            {
                this.UserInterface.ShowWarningMessage($"Enter a valid index between 0 and {this.UserAccount.TotalTransactionDataList.Count}");
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
        ITransaction selectedTransactionData = this.UserAccount.TotalTransactionDataList[indexToEdit];
        if (selectedTransactionData is IncomeTransactionData)
        {
            string newSource = this.GetSourceFromUser("\nEnter the new source : ");
            this.UserAccount.EditIncomeTransactionSource(indexToEdit, newSource);
        }
        else
        {
            string newCategory = this.GetCategoryFromUser("\nEnter the new category : ");
            this.UserAccount.EditExpenseTransactionCategory(indexToEdit, newCategory);
        }
    }

    /// <summary>
    /// Edits the amount of specified transaction with user inputs.
    /// <param name="indexToEdit"></param>
    private void EditAmountOfTransaction(int indexToEdit)
    {
        decimal newAmount = this.GetAmountFromUser("\nEnter the new amount : ");
        this.UserAccount.EditTransactionAmount(indexToEdit, newAmount);
    }

    /// <summary>
    /// Shows all the expense entries.
    /// </summary>
    private void ShowExpenseEntries()
    {
        this.UserInterface.ShowTitle("Expense entries");
        this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, false, true);
        this.UserInterface.ShowInfoMessage("Key Press to go back ....");
        Console.ReadKey();
    }

    /// <summary>
    /// Shows all the income entries.
    /// </summary>
    private void ShowIncomeEntries()
    {
        this.UserInterface.ShowTitle("Income entries");
        this.UserInterface.ShowTransactionList(this.UserAccount.TotalTransactionDataList, true, false);
        this.UserInterface.ShowInfoMessage("Key Press to go back ....");
        Console.ReadKey();
    }
}
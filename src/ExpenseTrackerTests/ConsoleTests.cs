using ExpenseTracker.Constants.Enums;
using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;
using Xunit;

namespace ExpenseTrackerTests;

/// <summary>
/// Provide methods to test console UI.
/// </summary>
public class ConsoleTests
{
    /// <summary>
    /// Tests whether prompt and get input gets the input from user
    /// </summary>
    [Fact]
    public void PromptAndGetInput_ShouldReturnNotNullInput()
    {
        StringReader stringWriter = new StringReader("Helo");

        Console.SetIn(stringWriter);

        ConsoleUI userInterface = new ConsoleUI();

        string? input = userInterface.PromptAndGetInput("Enter : ");
        Assert.Equal("Helo", input);
    }

    /// <summary>
    /// Tests whether ShowInfoMessage prints specified data.
    /// </summary>
    [Fact]
    public void ShowInfoMessage_DisplaysMessage()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowInfoMessage("Information");

        Assert.Contains("Information", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether ShowWarningMessage prints specified data.
    /// </summary>
    [Fact]
    public void ShowWarningMessage_DisplaysMessage()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowWarningMessage("Information");

        Assert.Contains("Information", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether ShowSuccessMessage prints specified data.
    /// </summary>
    [Fact]
    public void ShowSuccessMessage_DisplaysMessage()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowSuccessMessage("Information");

        Assert.Contains("Information", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether ShowTransactionList shows all data when specified.
    /// </summary>
    [Fact]
    public void ShowTransactionList_ShouldShowIncomesAlone_WhenSpecified()
    {
        List<ITransaction> transactions = new List<ITransaction>
        {
            new IncomeTransactionData
            {
                Amount = 100,
                Source = "Salary",
            },

            new ExpenseTransactionData
            {
                Amount = 200,
                Category = "Food",
            },
        };

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowTransactionList(transactions, TransactionType.Income);

        Assert.Contains("100", stringWriter.ToString());
        Assert.DoesNotContain("200", stringWriter.ToString());
        Assert.Contains("Salary", stringWriter.ToString());
        Assert.DoesNotContain("Food", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether ShowTransactionList shows all expense data alone when specified.
    /// </summary>
    [Fact]
    public void ShowTransactionList_ShouldShowExpenseAlone_WhenSpecified()
    {
        List<ITransaction> transactions = new List<ITransaction>
        {
            new IncomeTransactionData
            {
                Amount = 100,
                Source = "Salary",
            },

            new ExpenseTransactionData
            {
                Amount = 200,
                Category = "Food",
            },
        };

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowTransactionList(transactions, TransactionType.Expense);

        Assert.DoesNotContain("100", stringWriter.ToString());
        Assert.Contains("200", stringWriter.ToString());
        Assert.DoesNotContain("Salary", stringWriter.ToString());
        Assert.Contains("Food", stringWriter.ToString());
    }

    /// <summary>
    /// Tests whether ShowTransactionList shows all income data alone when specified.
    /// </summary>
    [Fact]
    public void ShowTransactionList_ShouldShowAll_WhenSpecified()
    {
        List<ITransaction> transactions = new List<ITransaction>
        {
            new IncomeTransactionData
            {
                Amount = 100,
                Source = "Salary",
            },

            new ExpenseTransactionData
            {
                Amount = 200,
                Category = "Food",
            },
        };

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        IUserInterface userInterface = new ConsoleUI();

        userInterface.ShowTransactionList(transactions, TransactionType.All);

        Assert.Contains("100", stringWriter.ToString());
        Assert.Contains("200", stringWriter.ToString());
        Assert.Contains("Salary", stringWriter.ToString());
        Assert.Contains("Food", stringWriter.ToString());
    }
}

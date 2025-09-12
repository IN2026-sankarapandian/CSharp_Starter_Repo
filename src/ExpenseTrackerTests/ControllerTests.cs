using ExpenseTracker.Constants;
using ExpenseTracker.Constants.Enums;
using ExpenseTracker.Handlers;
using ExpenseTracker.Models;
using ExpenseTracker.UserInterface;
using Moq;
using Xunit;

namespace ExpenseTrackerTest;

/// <summary>
/// Provide methods to test controller
/// </summary>
public class ControllerTests
{
    /// <summary>
    /// Tests whether addIncome adds income to the account.
    /// </summary>
    [Fact]
    public void HandleAddIncome_ShouldAddIncome()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.Sources).Returns(new List<string> { "Salary" });
        mockUI.Setup(ui => ui.PromptAndGetInput(PromptMessages.EnterIncome)).Returns("1000");
        mockUI.Setup(ui => ui.PromptAndGetInput(PromptMessages.EnterSource)).Returns("1");

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleAddIncome();

        mockAccount.Verify(acc => acc.AddIncome(1000m, "Salary"), Times.Once());
    }

    /// <summary>
    /// Tests whether addExpense add expense to the account.
    /// </summary>
    [Fact]
    public void HandleAddExpense_ShouldAddExpense()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.Categories).Returns(new List<string> { "Food" });
        mockUI.Setup(ui => ui.PromptAndGetInput(PromptMessages.EnterExpense)).Returns("1000");
        mockUI.Setup(ui => ui.PromptAndGetInput(PromptMessages.EnterCategory)).Returns("1");

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleAddExpense();

        mockAccount.Verify(acc => acc.AddExpense(1000m, "Food"), Times.Once());
    }

    /// <summary>
    /// Tests whether handleViewTransaction returns when there are no transactions.
    /// </summary>
    [Fact]
    public void HandleViewTransaction_ShouldReturn_WhenNoAvailableTransaction()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>());

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleEditTransaction();

        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.All), Times.Once);
        mockUI.Verify(ui => ui.PromptAndGetInput(PromptMessages.PressEnterToGoBack), Times.Once);
    }

    /// <summary>
    /// Tests whether handle delete returns when there are no transactions.
    /// </summary>
    [Fact]
    public void HandleDeleteTransaction_ShouldReturn_WhenNoAvailableTransaction()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>());

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleDeleteTransaction();

        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.All), Times.Once);
        mockUI.Verify(ui => ui.PromptAndGetInput(PromptMessages.PressEnterToGoBack), Times.Once);
    }

    /// <summary>
    /// Tests whether handle edit returns when there are no transactions.
    /// </summary>
    [Fact]
    public void HandleEditTransaction_ShouldReturn_WhenNoAvailableTransaction()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>());

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleViewTransactions();

        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.All), Times.Once);
        mockUI.Verify(ui => ui.PromptAndGetInput(PromptMessages.PressEnterToGoBack), Times.Once);
    }

    /// <summary>
    /// Tests whether handleView shows expense transactions.
    /// </summary>
    [Fact]
    public void HandleViewTransaction_ShouldShowExpense_WhenGetValidInput()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(It.IsAny<string>()))
          .Returns("2")
          .Returns("4")
          .Returns("")
          .Returns("3");
        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>
        {
            new ExpenseTransactionData
            {
                Amount = 1000,
                Category = "Food",
            },
        });

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleViewTransactions();

        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.All), Times.Exactly(2));
        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.Expense), Times.Once);
    }

    /// <summary>
    /// Tests whether handle view shows income transaction
    /// </summary>
    [Fact]
    public void HandleViewTransaction_ShouldShowIncome_WhenGetValidInput()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(It.IsAny<string>()))
          .Returns("5")
          .Returns("1")
          .Returns("4")
          .Returns("")
          .Returns("3");
        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>
        {
            new IncomeTransactionData
            {
                Amount = 1000,
                Source = "Salary",
            },
        });

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleViewTransactions();

        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.All), Times.Exactly(3));
        mockUI.Verify(ui => ui.ShowTransactionList(mockAccount.Object.TotalTransactionDataList, TransactionType.Income), Times.Once);
    }

    /// <summary>
    /// Tests whether HandleEditTransaction edits transaction amount.
    /// </summary>
    [Fact]
    public void HandleEditTransaction_EditsTransactionAmountInAccount()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>
        {
            new ExpenseTransactionData
            {
                Amount = 1000,
                Category = "Food",
            },
        });

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterIndexToEdit))
            .Returns(string.Empty)
            .Returns("3")
            .Returns("1");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterWhatToEdit))
            .Returns(string.Empty)
            .Returns("3")
            .Returns("1");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterNewAmount))
            .Returns(string.Empty)
            .Returns("word")
            .Returns("-100")
            .Returns("100");

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleEditTransaction();

        mockAccount.Verify(mockAccount => mockAccount.EditTransactionAmount(0, 100));
    }

    /// <summary>
    /// Tests HandleEditTransaction edits the data with new tag.
    /// </summary>
    [Fact]
    public void HandleEditTransaction_EditsTransactionTagInAccount_ForNewTag()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>
        {
            new ExpenseTransactionData
            {
                Amount = 1000,
                Category = "Food",
            },
        });

        mockAccount.SetupGet(acc => acc.Categories).Returns(new List<string> { "Food" });

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterIndexToEdit))
            .Returns(string.Empty)
            .Returns("3")
            .Returns("1");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterWhatToEdit))
            .Returns(string.Empty)
            .Returns("3")
            .Returns("2");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(string.Format(PromptMessages.NewTag, Headings.Category)))
            .Returns(string.Empty)
            .Returns("2")
            .Returns("1");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(string.Format(string.Format(PromptMessages.EnterNewTag, Headings.Category))))
            .Returns(string.Empty)
            .Returns("New Category");

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleEditTransaction();

        mockAccount.Verify(mockAccount => mockAccount.EditExpenseTransactionCategory(0, "New Category"));
    }

    /// <summary>
    /// Tests whether the HandleEditTransaction edits the data with existing tag.
    /// </summary>
    [Fact]
    public void HandleEditTransaction_EditsTransactionTagInAccount_ForExistingTag()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>
        {
            new IncomeTransactionData
            {
                Amount = 1000,
                Source = "Salary",
            },
        });

        mockAccount.SetupGet(acc => acc.Sources).Returns(new List<string> { "Food" });

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterIndexToEdit))
            .Returns(string.Empty)
            .Returns("3")
            .Returns("1");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterWhatToEdit))
            .Returns(string.Empty)
            .Returns("3")
            .Returns("2");

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(string.Format(PromptMessages.NewTag, Headings.Source)))
            .Returns(string.Empty)
            .Returns("1");

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleEditTransaction();

        mockAccount.Verify(mockAccount => mockAccount.EditIncomeTransactionSource(0, "Food"));
    }

    /// <summary>
    /// Tests whether the HandleDeleteTransaction deletes the transaction for valid input.
    /// </summary>
    [Fact]
    public void HandleDeleteTransaction_ShouldDeleteTransaction_WhenGetValidInput()
    {
        Mock<IAccount> mockAccount = new Mock<IAccount>();
        Mock<IUserInterface> mockUI = new Mock<IUserInterface>();

        mockUI.SetupSequence(ui => ui.PromptAndGetInput(PromptMessages.EnterIndexToDelete))
            .Returns("1");

        mockAccount.SetupGet(acc => acc.TotalTransactionDataList).Returns(new List<ITransaction>
        {
            new IncomeTransactionData
            {
                Amount = 1000,
                Source = "Salary",
            },
        });

        Controller controller = new Controller(mockAccount.Object, mockUI.Object);

        controller.HandleDeleteTransaction();

        mockAccount.Verify(account => account.DeleteTransaction(0), Times.Once);
    }
}

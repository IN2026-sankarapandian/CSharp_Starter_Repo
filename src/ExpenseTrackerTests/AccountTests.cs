using ExpenseTracker.Models;
using Xunit;

namespace ExpenseTrackerTests;

/// <summary>
/// Provide testing method for accounts
/// </summary>
public class AccountTests
{
    /// <summary>
    /// Test AddIncome whether it is adding income.
    /// </summary>
    [Fact]
    public void AddIncome_ShouldAddIncome()
    {
        Account account = new Account();

        account.AddIncome(100, "Salary");

        Assert.Equal(100, account.CurrentBalance);
    }

    /// <summary>
    /// Test AddExpense whether it is adding income.
    /// </summary>
    [Fact]
    public void AddExpense_ShouldAddExpense()
    {
        Account account = new Account();

        account.AddExpense(100, "Food");

        Assert.Equal(-100, account.CurrentBalance);
    }

    /// <summary>
    /// Tests whether the EditTransactionAmount edit the correct transaction data
    /// </summary>
    /// <param name="index">Index to edit.</param>
    /// <param name="editAmount">New amount to edit.</param>
    [Theory]
    [InlineData(0, 50)]
    [InlineData(1, 50)]
    public void EditTransactionAmount_ShouldEditForMentionedType(int index, decimal editAmount)
    {
        IAccount mockAccount = new Account();

        mockAccount.AddExpense(100, "Food");
        mockAccount.AddIncome(100, "Salary");

        mockAccount.EditTransactionAmount(index, editAmount);

        Assert.Equal(editAmount, mockAccount.TotalTransactionDataList[index].Amount);
    }

    /// <summary>
    /// Tests whether the EditTransactionSource edit the correct transaction data
    /// </summary>
    [Fact]
    public void EditIncomeTransactionSource_ShouldEditSource()
    {
        IAccount mockAccount = new Account();

        mockAccount.AddIncome(100, "Salary");

        mockAccount.EditIncomeTransactionSource(0, "Stock");

        IncomeTransactionData transaction = (IncomeTransactionData)mockAccount.TotalTransactionDataList[0];
        Assert.Equal("Stock", transaction.Source);
    }

    /// <summary>
    /// Tests whether the EditTransactionCategory edit the correct transaction data
    /// </summary>
    [Fact]
    public void EditIncomeTransactionCategory_ShouldEditSource()
    {
        IAccount mockAccount = new Account();

        mockAccount.AddExpense(100, "Food");

        mockAccount.EditExpenseTransactionCategory(0, "Games");

        ExpenseTransactionData transaction = (ExpenseTransactionData)mockAccount.TotalTransactionDataList[0];
        Assert.Equal("Games", transaction.Category);
    }

    /// <summary>
    /// Tests whether the DeleteTransactionAmount deletes the correct transaction data
    /// </summary>
    /// <param name="index">Index to delete.</param>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void DeleteTransactionAmount_ShouldDeleteForMentionedIndex(int index)
    {
        IAccount mockAccount = new Account();

        mockAccount.AddExpense(100, "Food");
        mockAccount.AddIncome(100, "Salary");

        mockAccount.DeleteTransaction(index);

        Assert.Equal(1, mockAccount.TotalTransactionDataList.Count);
    }
}

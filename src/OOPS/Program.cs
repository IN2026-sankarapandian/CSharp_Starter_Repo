using OOPS.Task1;
using OOPS.Task2;
using OOPS.Task3;

namespace OOPS;

/// <summary>
/// The entry point class of application which contains main method which serves as a starting point.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point of application which is called when application starts.
    /// </summary>
    public static void Main()
    {
        CallShapeClass();
        CallEmployeeClass();
        CallBankAccount();
        Console.ReadKey();
    }

    /// <summary>
    /// Calls the derived classes of <see cref="Shape"/> for testing
    /// </summary>
    private static void CallShapeClass()
    {
        Circle circle = new ()
        {
            Color = "Red",
            Radius = 7,
        };
        circle.CalculateArea();
        circle.PrintDetails();
        Console.WriteLine();

        Rectangle rectangle = new ()
        {
            Color = "Red",
            Width = 10,
            Height = 20,
        };
        rectangle.CalculateArea();
        rectangle.PrintDetails();
        Console.WriteLine();
    }

    /// <summary>
    /// Calls the derived classes of <see cref="Employee"/> for testing
    /// </summary>
    private static void CallEmployeeClass()
    {
        Developer developer = new ()
        {
            Salary = 50000,
            Name = "Sankar",
        };
        developer.CalculateBonus();
        developer.PrintDetails();
        Console.WriteLine();

        Manager manager = new ()
        {
            Salary = 50000,
            Name = "Arthur",
        };
        manager.CalculateBonus();
        manager.PrintDetails();
        Console.WriteLine();
    }

    /// <summary>
    /// Calls the derived classes of <see cref="BankAccount"/> for testing
    /// </summary>
    private static void CallBankAccount()
    {
        SavingsAccount savingsAccount = new ()
        {
            AccountNumber = "IOB312424JK",
            Balance = 0,
        };
        savingsAccount.Deposit(20000);
        savingsAccount.Withdraw(10000);
        savingsAccount.Withdraw(10000);
        savingsAccount.Withdraw(9000);
        savingsAccount.PrintDetails();
        Console.WriteLine();

        CheckingAccount checkingAccount = new ()
        {
            AccountNumber = "IOB2342244JK",
            Balance = 0,
        };
        checkingAccount.Deposit(20000);
        checkingAccount.Withdraw(10000);
        checkingAccount.Withdraw(10000);
        checkingAccount.Withdraw(10000);
        checkingAccount.PrintDetails();
        Console.WriteLine();
    }
}

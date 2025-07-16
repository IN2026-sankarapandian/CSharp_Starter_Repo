using System;
global using OOPS.Task1;
global using OOPS.Task2;
global using OOPS.Task3;

public class OOPS
{
	public static void Main()
	{
        Console.WriteLine();
        Circle circle = new()
        {
            Color = "Red",
            Radius = 7,
        };
        circle.CalculateArea();
        circle.PrintDetails();
        Console.WriteLine();

        Rectangle rectangle = new()
        {
            Color = "Red",
            Width = 10,
            Height = 20,
        };
        rectangle.CalculateArea();
        rectangle.PrintDetails();
        Console.WriteLine();

        Developer developer = new()
        {
            Salary = 50000,
            Name = "Sankar",
        };
        developer.CalculateBonus();
        developer.PrintDetails();
        Console.WriteLine();

        Manager manager = new()
        {
            Salary = 50000,
            Name = "Arthur",
        };
        manager.CalculateBonus();
        manager.PrintDetails();
        Console.WriteLine();

        SavingsAccount savingsAccount = new()
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

        CheckingAccount checkingAccount = new()
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

        Console.ReadKey();
    }
}

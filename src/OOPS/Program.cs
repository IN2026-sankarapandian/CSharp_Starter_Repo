using OOPS.Task1;
using OOPS.Task2;
using OOPS.Task3;

Console.WriteLine();
Circle cir = new Circle
{
    Color = "Red",
    Radius = 7,
};
cir.CalculateArea();
cir.PrintDetails();
Console.WriteLine();

Rectangle rec = new Rectangle
{
    Color = "Red",
    Width = 10,
    Height = 20,
};
rec.CalculateArea();
rec.PrintDetails();
Console.WriteLine();

Developer dev = new Developer
{
    Salary = 50000,
    Name = "Sankar",
};
dev.CalculateBonus();
dev.PrintDetails();
Console.WriteLine();

Manager man = new Manager
{
    Salary = 50000,
    Name = "Arthur",
};
man.CalculateBonus();
man.PrintDetails();
Console.WriteLine();

SavingsAccount save = new SavingsAccount
{
    AccountNumber = "IOB312424JK",
    Balance = 0,
};
save.Deposit(20000);
save.Withdraw(10000);
save.Withdraw(10000);
save.Withdraw(9000);
save.PrintDetails();
Console.WriteLine();

CheckingAccount check = new CheckingAccount
{
    AccountNumber = "IOB2342244JK",
    Balance = 0,
};
check.Deposit(20000);
check.Withdraw(10000);
check.Withdraw(10000);
check.Withdraw(10000);
check.PrintDetails();
Console.WriteLine();

Console.ReadKey();
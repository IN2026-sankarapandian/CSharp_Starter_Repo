namespace ExpenseTracker.Models;

public class Account
{
    public string Name { get; set; }
    public int CurrentBalance { get; set; }

    public Account(string name, int currentBalance)
    {
        Name = name;
        this.CurrentBalance = currentBalance;
    }

}

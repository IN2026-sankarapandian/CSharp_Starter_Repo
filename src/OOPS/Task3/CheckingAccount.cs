namespace OOPS.Task3;

/// <summary>
/// Represents a checking account.
/// It has unrestricted withdraw policy.
/// </summary>
public class CheckingAccount : BankAccount
{
    /// <summary>
    /// Withdraw amount from the account. <see cref="CheckingAccount"/> has unrestricted withdraw policy.
    /// </summary>
    /// <param name="amount">Withdraw amount</param>
    public override void Withdraw(float amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
            }
            else if (amount > this.Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                this.Balance -= amount;
                Console.WriteLine($"Withdrawn amount: {amount}");
            }
        }

    /// <summary>
    /// Print all the details about checking account.
    /// </summary>
    public override void PrintDetails()
        {
            Console.WriteLine("Account Type: Checking");
            base.PrintDetails();
        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPS.Task3
{
    using System;

    /// <summary>
    /// Represents a savings account
    /// It has restricted withdraw policy.
    /// </summary>
    public class SavingsAccount : BankAccount
        {
            private const float MinimumBalance = 1000;

            /// <summary>
            /// Withdraw amount from the account. <see cref="SavingsAccount"/> has restricted withdraw policy.
            /// </summary>
            /// <param name="amount">Amount to withdraw</param>
            public override void Withdraw(float amount)
                {
                    if (amount <= 0)
                    {
                        Console.WriteLine("Withdrawal amount must be positive.");
                    }
                    else if (this.Balance - amount < MinimumBalance)
                    {
                        Console.WriteLine("Minimum balance not met, withdrawal denied.");
                    }
                    else
                    {
                        this.Balance -= amount;
                        Console.WriteLine($"Withdrawn amount: {amount}");
                    }
                }

            /// <summary>
            /// Print all the details about saving account.
            /// </summary>
            public override void PrintDetails()
                {
                    Console.WriteLine("Account Type: Savings");
                    base.PrintDetails();
                }
            }
}

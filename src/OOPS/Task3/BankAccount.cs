namespace OOPS.Task3
{
    /// <summary>
    /// Represent a bank account allow all other types of bank account to inherit from it.
    /// </summary>
    public abstract class BankAccount
        {
            /// <summary>
            /// Gets or initialize the account number of bank account.
            /// </summary>
            /// <value>Account number of user.</value>
            public string? AccountNumber { get; init; }

            /// <summary>
            /// Gets or sets initialize the balance of bank account.
            /// </summary>
            /// <value>Balance in the account</value>
            public float Balance { get; set; }

            /// <summary>
            /// Deposit amount in the account.
            /// </summary>
            /// <param name="amount">Amount to deposit.</param>
            public void Deposit(float amount)
            {
                if (amount > 0)
                {
                    this.Balance += amount;
                    Console.WriteLine($"Deposited amount: {amount}");
                }
                else
                {
                    Console.WriteLine("Deposit amount must be positive.");
                }
            }

            /// <summary>
            /// Withdraw amount from the account.
            /// </summary>
            /// <param name="amount">Withdraw amount.</param>
            public abstract void Withdraw(float amount);

            /// <summary>
            /// Print all the details about account.
            /// </summary>
            public virtual void PrintDetails()
            {
                Console.WriteLine($"Account Number: {this.AccountNumber}");
                Console.WriteLine($"Balance: {this.Balance}");
            }
        }
}

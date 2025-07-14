namespace OOPS.Task2
{
        /// <summary>
        /// Represent a employee all other kind of employee to inherit from it
        /// </summary>
        public abstract class Employee
        {
            /// <summary>
            /// Gets or initialize the name of the employee.
            /// </summary>
            /// <value>
            /// name of employee
            /// </value>
            public string? Name { get; init; }

            /// <summary>
            /// Gets or initialize the salary of the employee.
            /// </summary>
            /// <value>
            /// salary of employee
            /// </value>
            public float Salary { get; init; }

            /// <summary>
            /// Calculates ansd shows the bonus amount of the employee.
            /// </summary>
            /// <returns>bous amount of employee</returns>
            public abstract float CalculateBonus();

            /// <summary>
            /// Print all the details about the employee.
            /// </summary>
            public virtual void PrintDetails()
            {
                Console.WriteLine($"Name: {this.Name}");
                Console.WriteLine($"Salary: {this.Salary}");
                Console.WriteLine($"Bonus: {this.CalculateBonus()}");
            }
        }
}
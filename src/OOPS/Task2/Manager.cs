namespace OOPS.Task2
{
    /// <summary>
    /// Represent a employee manager.
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Calculates ansd shows the bonus amount of the manager.
        /// Bonus : 25%
        /// </summary>
        /// <returns>bonus of employee</return>
        public override float CalculateBonus()
        {
            return this.Salary * (25f / 100f);
        }

        /// <summary>
        /// Print all the details about the manager.
        /// </summary>
        public override void PrintDetails()
        {
            Console.WriteLine("Position: Manager");
            base.PrintDetails();
        }
    }
}

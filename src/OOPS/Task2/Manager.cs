namespace OOPS.Task2
{
    /// <summary>
    /// Represent an employee manager.
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Calculates and shows the bonus amount of the manager.
        /// Bonus : 25%
        /// </summary>
        /// <returns>Bonus of manager.</return>
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

namespace OOPS.Task2
{
    /// <summary>
    /// Represents an employee developer.
    /// </summary>
    public class Developer : Employee
    {
        /// <summary>
        /// Calculates and shows the bonus amount of the developer.
        /// Bonus : 10%
        /// </summary>
        /// <returns>Bonus of developer.</return>
        public override float CalculateBonus()
        {
            return this.Salary * (10f / 100f);
        }

        /// <summary>
        /// Print all the details about the developer.
        /// </summary>
        public override void PrintDetails()
        {
            Console.WriteLine("Position: Developer");
            base.PrintDetails();
        }
    }
}

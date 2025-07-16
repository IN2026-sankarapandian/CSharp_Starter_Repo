namespace OOPS.Task1
{
    /// <summary>
    /// Represents a shape rectangle.
    /// </summary>
    public class Rectangle : Shape
    {
        /// <summary>
        /// Gets or initializes width of the rectangle.
        /// </summary>
        /// <value>
        /// Width of the rectangle.
        /// </value>
        public double Width { get; init; }

        /// <summary>
        /// Gets or initialize height of the rectangle.
        /// </summary>
        /// <value>
        /// Height of the rectangle.
        /// </value>
        public double Height { get; init; }

        /// <summary>
        /// Calculate the area of rectangle.
        /// </summary>
        /// <returns>Area of the rectangle.</returns>
        public override double CalculateArea()
        {
            return this.Width * this.Height;
        }

        /// <summary>
        /// Print all the details about rectangle shape.
        /// </summary>
        public override void PrintDetails()
        {
            Console.WriteLine("Shape: Rectangle");
            base.PrintDetails();
        }
    }
}

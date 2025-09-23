namespace CSharpAdvanced.Tasks.PatternMatching.Shapes;

/// <summary>
/// Represents a shape circle.
/// </summary>
public class Triangle : Shape
{
    /// <summary>
    /// Gets or initializes base of the triangle.
    /// </summary>
    /// <value>
    /// Width of the rectangle.
    /// </value>
    public double Base { get; init; }

    /// <summary>
    /// Gets or initialize height of the triangle.
    /// </summary>
    /// <value>
    /// Height of the rectangle.
    /// </value>
    public double Height { get; init; }

    /// <summary>
    /// Calculate the area of circle.
    /// </summary>
    /// <returns>Area of the circle</returns>
    public override double CalculateArea() => 0.5 * this.Base * this.Height;
}

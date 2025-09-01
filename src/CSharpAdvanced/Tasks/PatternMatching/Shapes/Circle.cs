namespace CSharpAdvanced.Tasks.PatternMatching.Shapes;

/// <summary>
/// Represents a shape circle.
/// </summary>
public class Circle : Shape
{
    /// <summary>
    /// Gets or initializes radius of circle
    /// </summary>
    /// <value>
    /// Radius of circle
    /// </value>
    public double Radius { get; init; }

    /// <summary>
    /// Calculate the area of circle.
    /// </summary>
    /// <returns>Area of the circle</returns>
    public override double CalculateArea() => Math.PI * this.Radius * this.Radius;
}

namespace CSharpAdvanced.Tasks.PatternMatching.Shapes;

/// <summary>
/// Represents a shape, allow all other individual shape to inherit from it.
/// </summary>
public abstract class Shape
{
    /// <summary>
    /// Gets or initialize color of the shape.
    /// </summary>
    /// <value>
    /// Holds the color name.</value>
    public string? Color { get; init; }

    /// <summary>
    /// Calculate the area of the shape.
    /// </summary>
    /// <returns>Returns the area</returns>
    public abstract double CalculateArea();
}

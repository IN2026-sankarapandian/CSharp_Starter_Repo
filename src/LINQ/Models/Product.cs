namespace LINQ.Models;

/// <summary>
/// Represents the product with it's details.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the Id of the product.
    /// </summary>
    /// <value>Holds the Id of the product.</value>
    public string? ID { get;  set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    /// <value>Holds the Name of the product.</value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    /// <value>Holds the price of the product.</value>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    /// <value>Holds the category of the product.</value>
    public string? Category { get; set; }
}

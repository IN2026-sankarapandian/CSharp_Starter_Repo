namespace CSharpAdvanced.Tasks.DelegatesAdvanced;

/// <summary>
/// Represents a product data like name, category and price of it.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets name of the product.
    /// </summary>
    /// <value>
    /// Name of the product.
    /// </value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets category of the product.
    /// </summary>
    /// <value>
    /// Category of the product.
    /// </value>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets price of the product.
    /// </summary>
    /// <value>
    /// Price of the product.
    /// </value>
    public decimal Price { get; set; }
}

/// <summary>
/// It's a sort comparison logic for <see cref="Product"/>.
/// </summary>
/// <param name="productA">The first product to compare.</param>
/// <param name="productB">The second product to compare.</param>
/// <returns>
/// Less than zero if <paramref name="productA"/> precedes <paramref name="productB"/> in the sort order.
/// Zero if <paramref name="productA"/> is the same position as <paramref name="productB"/> in the sort order.
/// Greater than zero if <paramref name="productA"/> follows <paramref name="productB"/> in the sort order.
/// </returns>
public delegate int SortDelegate(Product productA, Product productB);

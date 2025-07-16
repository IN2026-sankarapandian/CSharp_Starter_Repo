namespace InventoryManager.Models;

/// <summary>
/// Represents a product and holds data of it.
/// </summary>
public class Product
{
    private Dictionary<string, object> _product;

    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    /// <param name="newProduct">New product details</param>
    public Product(Dictionary<string, object> newProduct)
    {
        this._product = newProduct;
    }

    /// <summary>
    /// Indexer for easy access of values with fields as keys.
    /// </summary>
    /// <param name="field">Name of the field to get or set.</param>
    public object? this[string field]
    {
        get => this._product.ContainsKey(field) ? this._product[field] : null;
        set => this._product[field] = value!;
    }

    /// <summary>
    /// Holds the template of product details that includes field names and it type.
    /// </summary>
    /// <returns>Template of product</returns>
    public static Dictionary<string, Type> GetTemplate()
    {
        return new Dictionary<string, Type>
        {
            { "Name", typeof(string) },
            { "Id", typeof(string) },
            { "Price", typeof(int) },
            { "Quantity", typeof(int) },
            { "Time", typeof(int) },
        };
    }
}

using InventoryManager.Constants;

namespace InventoryManager.Models;

/// <summary>
/// Represent a product with Name, Id, Price, and Quantity fields.
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
    /// Indexer for easy access of values of <see cref="Dictionary{TKey, TValue}"/> containing fields as key, value in <see cref="Product"/>.
    /// </summary>
    /// <param name="key">Key of the dictionary.</param>
    /// <returns>Value for the key.</returns>
    public object this[string key]
    {
        get => this._product[key];
        set => this._product[key] = value!;
    }

    /// <summary>
    /// Holds the template of <see cref="Product"/> details.
    /// </summary>
    /// <returns>Template of <see cref="Product"/>.</returns>
    public static Dictionary<string, Type> GetTemplate()
    {
        return new Dictionary<string, Type>
        {
            { ProductFieldNames.Name, typeof(string) },
            { ProductFieldNames.Id, typeof(string) },
            { ProductFieldNames.Price, typeof(int) },
            { ProductFieldNames.Quantity, typeof(int) },
        };
    }

    /// <summary>
    /// Holds the string array of available fields in <see cref="Product"/>.
    /// </summary>
    /// <returns>Available fields name.</returns>
    public static string[] GetFields() => GetTemplate().Keys.ToArray();
}
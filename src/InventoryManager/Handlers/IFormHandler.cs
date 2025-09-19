using InventoryManager.Models;

namespace InventoryManager.Handlers;

/// <summary>
/// Provide methods to get input from user
/// </summary>
public interface IFormHandler
{
    /// <summary>
    /// Get the index from the user until user enters a valid input.
    /// </summary>
    /// <param name="productList">Product list to the index for.</param>
    /// <returns>Returns the index given by user.</returns>
    public int GetIndex(ProductList productList);

    /// <summary>
    /// Get field name from the user until user enters a valid input.
    /// </summary>
    /// <param name="productList">Product list to the field for.</param>
    /// <returns>Returns the field given by user.</returns>
    public string GetFieldName(ProductList productList);

    /// <summary>
    /// Shows the product as table
    /// </summary>
    /// <param name="productList">Product list to show the details.</param>
    /// <returns>Returns false if no products available; otherwise return true.</returns>
    public bool ShowProducts(ProductList productList);
}

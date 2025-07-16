namespace InventoryManager.Models;

/// <summary>
/// Represents the lists of <see cref="Product"s/>. Provides method to add, get and manipulate product list.
/// </summary>
internal class ProductList
{
    private List<Product> newname = new List<Product>();

    /// <summary>
    /// Adds an new <see cref="Product"/> to the end of an product list.
    /// </summary>
    /// <param name="newProduct"><see cref="Product"/> to add in <see cref="ProductList"/></param>
    public void Add(Product newProduct)
    {
        this.newname.Add(newProduct);
        Console.WriteLine("Added..");
        List<Product> products = new List<Product>();
        nameof(newname);
    }

    /// <summary>
    /// Holds the product list.
    /// </summary>
    /// <returns>PEntire product list.</returns>
    public List<Product> Get()
    {
        return this.newname;
    }
}

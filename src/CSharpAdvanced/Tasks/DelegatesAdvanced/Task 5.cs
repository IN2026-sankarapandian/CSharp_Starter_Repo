using CSharpAdvanced.Constants;
using CSharpAdvanced.Enums;
using CSharpAdvanced.UserInterface;

namespace CSharpAdvanced.Tasks.DelegatesAdvanced;

/// <summary>
/// Demonstrates the usage and working of delegates.
/// </summary>
public class Task5 : ITask
{
    private readonly IUserInterface _userInterface;

    /// <summary>
    /// Initializes a new instance of the <see cref="Task5"/> class.
    /// </summary>
    /// <param name="userInterface">Provides operations to interact with user.</param>
    public Task5(IUserInterface userInterface) => this._userInterface = userInterface;

    /// <inheritdoc/>
    public string Name => Titles.TaskTitle5;

    /// <inheritdoc/>
    public void Run()
    {
        this._userInterface.ShowMessage(MessageType.Title, this.Name);
        this._userInterface.ShowMessage(MessageType.Highlight, Messages.DelegatesAdvancedDescription);

        List<Product> products = new List<Product>
        {
            new Product { Name = "Notebook", Category = "Stationery", Price = 45.00m },
            new Product { Name = "Water Bottle", Category = "Kitchen", Price = 12.75m },
            new Product { Name = "Desk Lamp", Category = "Furniture", Price = 350.00m },
            new Product { Name = "Orange", Category = "Fruits", Price = 3.50m },
            new Product { Name = "Headphones", Category = "Electronics", Price = 899.99m },
        };

        SortDelegate nameSorter = this.SortByProductName;
        SortDelegate categorySorter = this.SortByProductCategory;
        SortDelegate priceSorter = this.SortByProductPrice;

        this._userInterface.ShowMessage(MessageType.Information, Messages.SortedByName);
        this.SortAndDisplay(nameSorter, products);

        this._userInterface.ShowMessage(MessageType.Information, Messages.SortedByCategory);
        this.SortAndDisplay(categorySorter, products);

        this._userInterface.ShowMessage(MessageType.Information, Messages.SortedByPrice);
        this.SortAndDisplay(priceSorter, products);

        this._userInterface.ShowMessage(MessageType.Prompt, string.Format(Messages.TaskExitPrompt, 5));
        this._userInterface.GetInput();
    }

    /// <summary>
    /// Sorts the list of <see cref="Product"/>s with a specified <see cref="SortDelegate"/>.
    /// </summary>
    /// <param name="sortDelegate">Contains comparison logic to sort.</param>
    /// <param name="products">List of products</param>
    private void SortAndDisplay(SortDelegate sortDelegate, List<Product> products)
    {
        List<Product> sortedProducts = products;
        sortedProducts.Sort(new Comparison<Product>(sortDelegate));
        foreach (Product product in sortedProducts)
        {
            this._userInterface.ShowMessage(MessageType.Information, string.Format(Messages.DisplayProduct, product.Name, product.Category, product.Price));
        }
    }

    /// <summary>
    /// Compares two products by name and return the integer that indicates their relative position in the sort order.
    /// </summary>
    /// <param name="productA">The first product to compare.</param>
    /// <param name="productB">The second product to compare.</param>
    /// <returns>
    /// Less than zero if <paramref name="productA"/> precedes <paramref name="productB"/> in the sort order.
    /// Zero if <paramref name="productA"/> is the same position as <paramref name="productB"/> in the sort order.
    /// Greater than zero if <paramref name="productA"/> follows <paramref name="productB"/> in the sort order.
    /// </returns>
    private int SortByProductName(Product productA, Product productB)
        => string.Compare(productA?.Name, productB?.Name, StringComparison.Ordinal);

    /// <summary>
    /// Compares two products by category and return the integer that indicates their relative position in the sort order.
    /// </summary>
    /// <param name="productA">The first product to compare.</param>
    /// <param name="productB">The second product to compare.</param>
    /// <returns>
    /// Less than zero if <paramref name="productA"/> precedes <paramref name="productB"/> in the sort order.
    /// Zero if <paramref name="productA"/> is the same position as <paramref name="productB"/> in the sort order.
    /// Greater than zero if <paramref name="productA"/> follows <paramref name="productB"/> in the sort order.
    /// </returns>
    private int SortByProductCategory(Product productA, Product productB)
        => string.Compare(productA?.Category, productB?.Category, StringComparison.Ordinal);

    /// <summary>
    /// Compares two products by price and return the integer that indicates their relative position in the sort order.
    /// </summary>
    /// <param name="productA">The first product to compare.</param>
    /// <param name="productB">The second product to compare.</param>
    /// <returns>
    /// Less than zero if <paramref name="productA"/> precedes <paramref name="productB"/> in the sort order.
    /// Zero if <paramref name="productA"/> is the same position as <paramref name="productB"/> in the sort order.
    /// Greater than zero if <paramref name="productA"/> follows <paramref name="productB"/> in the sort order.
    /// </returns>
    private int SortByProductPrice(Product productA, Product productB)
        => productA.Price.CompareTo(productB.Price);
}

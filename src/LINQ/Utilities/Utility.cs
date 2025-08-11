using ConsoleTables;
using LINQ.Models;

namespace LINQ.Utilities;

/// <summary>
/// Have utilities function related to assignment.
/// </summary>
public static class Utility
{
    /// <summary>
    /// Print the products as table.
    /// </summary>
    /// <param name="products">Products list to print.</param>
    public static void PrintProducts(List<Product> products)
    {
        ConsoleTable productTable = new ("Id", "Product", "Price", "Category");
        foreach (Product product in products)
        {
            string?[] fields = new string[4];
            fields[0] = product?.ID;
            fields[1] = product?.Name;
            fields[2] = product?.Price.ToString();
            fields[3] = product?.Category;
            productTable.AddRow(fields);
        }

        productTable.Write();
    }
}

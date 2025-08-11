using ConsoleTables;
using LINQ.Helpers;
using LINQ.Models;

namespace LINQ.Tasks;

/// <summary>
/// Task 4 of the LINQ assignment.
/// </summary>
/// <remarks>Performance Considerations with LINQ </remarks>
public class Task4
{
    /// <summary>
    /// Runs the task 4.
    /// </summary>
    public void Run()
    {
        List<Product> products = new ();
        Helper.AddDummyProducts(products);

        List<Product> filteredProducts = products
            .Where(product => product.Category == "Books")
            .OrderBy(product => product.Price)
            .ToList();

        Console.WriteLine("All products under the category \"Books\" and sorted by price : \n");
        ConsoleTable productTable = new ("Id", "Product", "Price", "Category");
        foreach (Product product in filteredProducts)
        {
            string?[] fields = new string[4];
            fields[0] = product?.ID;
            fields[1] = product?.Name;
            fields[2] = product?.Price.ToString();
            fields[3] = product?.Category;
            productTable.AddRow(fields);
        }

        productTable.Write();

        // Here we use sort to optimize the query as the sort wont create any duplicates.
        List<Product>? filtered = products.Where(product => product.Category == "Books").ToList();

        filtered.Sort((productA, productB) => productA.Price.CompareTo(productB.Price));

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

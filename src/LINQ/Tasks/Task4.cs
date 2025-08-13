using LINQ.Datasets;
using LINQ.Models;
using LINQ.Utilities;

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
        Dataset.AddDummyProducts(products);

        List<Product> filteredProducts = products
            .Where(product => product.Category == "Books")
            .OrderBy(product => product.Price)
            .ToList();

        Console.WriteLine("All products under the category \"Books\" and sorted by price : \n");
        Utility.PrintProducts(filteredProducts);

        // Here we use sort to optimize the query as the sort wont create any duplicates.
        List<Product>? filtered = products.Where(product => product.Category == "Books").ToList();

        filtered.Sort((productA, productB) => productA.Price.CompareTo(productB.Price));

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

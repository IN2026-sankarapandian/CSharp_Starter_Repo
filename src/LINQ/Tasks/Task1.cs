using ConsoleTables;
using LINQ.Datasets;
using LINQ.Models;
using LINQ.Utilities;

namespace LINQ.Tasks;

/// <summary>
/// Task 1 of the LINQ assignment.
/// </summary>
/// <remarks>Task 1: Basic LINQ Queries </remarks>
public class Task1
{
    /// <summary>
    /// Runs the task 1.
    /// </summary>
    public void Run()
    {
        List<Product> products = new ();
        Dataset.AddDummyProducts(products);
        Console.WriteLine("All products");
        Utility.PrintProducts(products);

        var filteredProducts = products.Where(product => product.Category == "Electronics" && product.Price > 500)
                                       .OrderByDescending(product => product.Price).ToList()
                                       .Select(product => new { Name = (string?)product.Name, product.Price })
                                       .ToList();

        Console.WriteLine("\n\nFiltered products under the category \"Electronics\" with a price greater than $500 and with only ProductName and Price ( In descending order by price ) : ");
        ConsoleTable filteredProductsTable = new ("Product", "Price");
        foreach (var product in filteredProducts)
        {
            string?[] fields = new string[2];
            fields[0] = product?.Name;
            fields[1] = product?.Price.ToString();
            filteredProductsTable.AddRow(fields);
        }

        filteredProductsTable.Write();

        decimal average = filteredProducts.Average(product => product.Price);
        Console.WriteLine("Average price : {0}", average.ToString());

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

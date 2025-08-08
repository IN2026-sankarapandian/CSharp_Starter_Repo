using ConsoleTables;
using LINQ.Helpers;
using LINQ.Models;

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
        Helper.AddDummyProducts(products);
        Console.WriteLine("All products");
        PrintData(products);

        var filteredProducts = products.Where(product =>
        {
            if (product is not null && product.Category is not null)
            {
                return product.Price > 500 && product.Category.Equals("Electronics");
            }

            return false;
        })
            .Select(product => new { product.Name, product.Price })
            .ToList();

        Console.WriteLine("\n\nFiltered products under the category \"Electronics\" with a price greater than $500 and with only ProductName and Price : ");
        ConsoleTable filteredProductTable = new ("Product", "Price");
        foreach (var product in filteredProducts)
        {
            string?[] fields = new string[2];
            fields[0] = product?.Name;
            fields[1] = product?.Price.ToString();
            filteredProductTable.AddRow(fields);
        }

        filteredProductTable.Write();

        Console.WriteLine("Ordered products based on price : ");
        var sortedProducts = filteredProducts.OrderBy(product => product.Price).ToList();
        ConsoleTable sortedProductTable = new ("Product", "Price");
        foreach (var product in sortedProducts)
        {
            string?[] fields = new string[2];
            fields[0] = product?.Name;
            fields[1] = product?.Price.ToString();
            sortedProductTable.AddRow(fields);
        }

        sortedProductTable.Write();

        decimal average = sortedProducts.Average(product => product.Price);
        Console.WriteLine("Average price : {0}", average.ToString());

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }

    private static void PrintData(List<Product> products)
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

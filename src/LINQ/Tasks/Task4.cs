using ConsoleTables;
using LINQ.Models;

namespace LINQ.Tasks;

/// <summary>
/// Task4 of the LINQ assignment.
/// </summary>
public class Task4
{
    /// <summary>
    /// Runs the task4.
    /// </summary>
    public void Run()
    {
        List<Product> products = new ();
        this.AddDummyProducts(products);

        List<Product> filteredProducts = products
            .Where(product => product.Category.Equals("Books"))
            .OrderBy(product => product.Price)
            .ToList();

        Console.WriteLine("All products under the category \"Books\" and sorted by price : \n");
        ConsoleTable productTable = new ConsoleTable("Id", "Product", "Price", "Category");
        foreach (Product product in filteredProducts)
        {
            string[] fields = new string[4];
            fields[0] = product.ID;
            fields[1] = product.Name;
            fields[2] = product.Price.ToString();
            fields[3] = product.Category;
            productTable.AddRow(fields);
        }

        productTable.Write();

        // Here we use sort to optimize the query as the sort wont create any duplicates.
        List<Product> filtered = products.Where(product => product.Category.Equals("Books")).ToList();
        filtered.Sort((productA, productB) => productA.Price.CompareTo(productB.Price));

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }

    private void AddDummyProducts(List<Product> products)
    {
        products.Add(new Product { ID = "001", Name = "Keychain", Price = 2000, Category = "Others" });
        products.Add(new Product { ID = "002", Name = "USB Cable", Price = 1500, Category = "Electronics" });
        products.Add(new Product { ID = "003", Name = "Notebook", Price = 500, Category = "Stationery" });
        products.Add(new Product { ID = "004", Name = "Bluetooth Speaker", Price = 3500, Category = "Electronics" });
        products.Add(new Product { ID = "005", Name = "Pen", Price = 100, Category = "Stationery" });
        products.Add(new Product { ID = "006", Name = "Book - C# Programming", Price = 1200, Category = "Books" });
        products.Add(new Product { ID = "007", Name = "Book - LINQ Essentials", Price = 1300, Category = "Books" });
        products.Add(new Product { ID = "008", Name = "Book - ASP.NET Core Guide", Price = 1400, Category = "Books" });
        products.Add(new Product { ID = "009", Name = "Book - Entity Framework Deep Dive", Price = 1500, Category = "Books" });
        products.Add(new Product { ID = "010", Name = "Book - Clean Code", Price = 1600, Category = "Books" });
        products.Add(new Product { ID = "011", Name = "Smartwatch", Price = 10000, Category = "Electronics" });
        products.Add(new Product { ID = "012", Name = "Desk Organizer", Price = 1800, Category = "Stationery" });
        products.Add(new Product { ID = "013", Name = "Laptop Stand", Price = 3000, Category = "Electronics" });
        products.Add(new Product { ID = "014", Name = "Book - Design Patterns", Price = 1700, Category = "Books" });
        products.Add(new Product { ID = "015", Name = "Book - The Pragmatic Programmer", Price = 1800, Category = "Books" });
    }
}

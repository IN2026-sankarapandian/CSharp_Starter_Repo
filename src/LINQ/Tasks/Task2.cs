using ConsoleTables;
using LINQ.Models;
using LINQ.Utilities;

namespace LINQ.Tasks;

/// <summary>
/// Task 2 of the LINQ assignment.
/// </summary>
public class Task2
{
    /// <summary>
    /// Runs the task 2.
    /// </summary>
    /// <remarks>Complex LINQ Queries</remarks>
    public void Run()
    {
        List<Product> products = new ();
        Utility.AddDummyProducts(products);
        var groupedProducts = products.GroupBy(product => product.Category)
                                      .Select(category => new
                                      {
                                          Category = category.Key,
                                          Count = category.Count(),
                                          ExpensiveProduct = category.OrderByDescending(product => product.Price).First(),
                                      });
        foreach (var category in groupedProducts)
        {
            Console.WriteLine();
            Console.WriteLine("Category : {0}", category.Category);
            Console.WriteLine("Count : {0}", category.Count);
            Console.WriteLine("Expensive product : {0}", category.ExpensiveProduct.Name);
        }

        List<Supplier> suppliers = new ();
        Utility.AddDummySuppliers(suppliers);

        var productDetails = from product in products
                             join supplier in suppliers
                             on product.ID equals supplier.ProductId
                             select new
                             {
                                 ProductId = product.ID,
                                 ProductName = product.Name,
                                 supplier.SupplierId,
                                 supplier.SupplierName,
                                 product.Category,
                                 product.Price,
                             };

        Console.WriteLine("\n\nProducts with suppliers data : ");
        ConsoleTable productDetailsTable = new ("Product ID", "Product Name", "Supplier ID", "Supplier Name", "Category", "Price");
        foreach (var product in productDetails)
        {
            string[] fields = new string[6];
            fields[0] = product.ProductId;
            fields[1] = product.ProductName;
            fields[2] = product.SupplierId;
            fields[3] = product.SupplierName;
            fields[4] = product.Category;
            fields[5] = product.Price.ToString();
            productDetailsTable.AddRow(fields);
        }

        productDetailsTable.Write();

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

using ConsoleTables;
using LINQ.Helpers;
using LINQ.Models;

namespace LINQ.Tasks.Task5;

/// <summary>
/// Task5 of the LINQ assignment.
/// </summary>
public class Task5
{
    /// <summary>
    /// Runs the task5.
    /// </summary>
    public void Run()
    {
        List<Product> products = new ();
        Helper.AddDummyProducts(products);

        List<Supplier> suppliers = new ();
        Helper.AddDummySuppliers(suppliers);

        var result = new QueryBuilder<Product>(products)
            .Filter(product => product.Category.Equals("Stationery"))
            .SortBy(product => product.Price)
            .Join(suppliers,  (p, s) => p.ID == s.ProductId, (s, p) => new
            {
                s.ID,
                s.Name,
                s.Category,
                s.Price,
                p.SupplierName,
                p.SupplierId,
            })
            .Execute();

        Console.WriteLine("Filtered products using custom query builder : \n");
        ConsoleTable filteredProductTable = new ConsoleTable("Product ID", "Product Name", "Supplier ID", "Supplier Name", "Category", "Price");
        foreach (var product in result)
        {
            string[] fields = new string[6];
            fields[0] = product.ID;
            fields[1] = product.Name;
            fields[2] = product.SupplierId;
            fields[3] = product.SupplierName;
            fields[4] = product.Category;
            fields[5] = product.Price.ToString();
            filteredProductTable.AddRow(fields);
        }

        filteredProductTable.Write();

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

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

        var result = new QueryBuilder<Product, Supplier>(products, suppliers)
            .Filter(product => product.Category.Equals("Stationery"))
            .SortBy(product => product.Price)
            .Join((product, supplier) => product.ID.Equals(supplier.ProductId))
            .Execute();

        Console.WriteLine("Filtered products using custom query builder : \n");
        ConsoleTable filteredProductTable = new ConsoleTable("Product ID", "Product Name", "Supplier ID", "Supplier Name", "Category", "Price");
        foreach (var product in result)
        {
            Product item1 = ((Tuple<Product, Supplier>)product).Item1;
            Supplier item2 = ((Tuple<Product, Supplier>)product).Item2;
            string[] fields = new string[6];
            fields[0] = item1.ID;
            fields[1] = item1.Name;
            fields[2] = item2.SupplierId;
            fields[3] = item2.SupplierName;
            fields[4] = item1.Category;
            fields[5] = item1.Price.ToString();
            filteredProductTable.AddRow(fields);
        }

        filteredProductTable.Write();

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

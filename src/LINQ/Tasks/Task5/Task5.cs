using ConsoleTables;
using LINQ.Datasets;
using LINQ.Models;

namespace LINQ.Tasks.Task5;

/// <summary>
/// Task5 of the LINQ assignment.
/// </summary>
/// <remarks>Query Builder</remarks>
public class Task5
{
    /// <summary>
    /// Runs the task5.
    /// </summary>
    public void Run()
    {
        List<Product> products = new ();
        Dataset.AddDummyProducts(products);

        List<Supplier> suppliers = new ();
        Dataset.AddDummySuppliers(suppliers);

        var result = new QueryBuilder<Product>(products)
            .Filter(product => product.Category == "Stationery")
            .SortBy(product => product.Price)
            .Join(suppliers,  (product, supplier) => product.ID == supplier.ProductId, (supplier, product) => new
            {
                supplier.ID,
                supplier.Name,
                supplier.Category,
                supplier.Price,
                product.SupplierName,
                product.SupplierId,
            })
            .Execute();

        Console.WriteLine("Filtered products using custom query builder : \n");
        ConsoleTable filteredProductTable =
            new ("Product ID", "Product Name", "Supplier ID", "Supplier Name", "Category", "Price");
        foreach (var product in result)
        {
            if (product is null)
            {
                continue;
            }

            string?[] fields = new string[6];
            fields[0] = product?.ID;
            fields[1] = product?.Name;
            fields[2] = product?.SupplierId;
            fields[3] = product?.SupplierName;
            fields[4] = product?.Category;
            fields[5] = product?.Price.ToString();
            filteredProductTable.AddRow(fields);
        }

        filteredProductTable.Write();

        Console.WriteLine("Press enter to go back...");
        Console.ReadLine();
        Console.Clear();
    }
}

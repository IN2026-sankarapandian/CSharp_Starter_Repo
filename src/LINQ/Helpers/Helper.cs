using LINQ.Models;

namespace LINQ.Helpers;

/// <summary>
/// Have helper function related to assignment.
/// </summary>
public class Helper
{
    /// <summary>
    /// Add dummy products to the list.
    /// </summary>
    /// <param name="products">Products list to add dummy values.</param>
    public static void AddDummyProducts(List<Product> products)
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
        products.Add(new Product { ID = "016", Name = "Whiteboard Marker", Price = 150, Category = "Stationery" });
        products.Add(new Product { ID = "017", Name = "Power Bank", Price = 5000, Category = "Electronics" });
        products.Add(new Product { ID = "018", Name = "Desk Lamp", Price = 2200, Category = "Electronics" });
        products.Add(new Product { ID = "019", Name = "Scissors", Price = 250, Category = "Stationery" });
        products.Add(new Product { ID = "020", Name = "Flash Drive", Price = 1200, Category = "Electronics" });
        products.Add(new Product { ID = "021", Name = "Monitor", Price = 15000, Category = "Electronics" });
        products.Add(new Product { ID = "022", Name = "Keyboard", Price = 3000, Category = "Electronics" });
        products.Add(new Product { ID = "023", Name = "Notebook Stand", Price = 2800, Category = "Electronics" });
        products.Add(new Product { ID = "024", Name = "Pen Holder", Price = 400, Category = "Stationery" });
        products.Add(new Product { ID = "025", Name = "Smartphone", Price = 25000, Category = "Electronics" });
        products.Add(new Product { ID = "026", Name = "Tablet", Price = 18000, Category = "Electronics" });
        products.Add(new Product { ID = "027", Name = "Wireless Mouse", Price = 2000, Category = "Electronics" });
        products.Add(new Product { ID = "028", Name = "Notebook Pouch", Price = 900, Category = "Stationery" });
        products.Add(new Product { ID = "029", Name = "Cable Clip", Price = 300, Category = "Electronics" });
        products.Add(new Product { ID = "030", Name = "Phone Holder", Price = 1100, Category = "Electronics" });
    }

    /// <summary>
    /// Add dummy suppliers to the list.
    /// </summary>
    /// <param name="suppliers">Products list to add dummy values.</param>
    public static void AddDummySuppliers(List<Supplier> suppliers)
    {
        suppliers.Add(new Supplier { SupplierId = "SUP001", SupplierName = "KeyWorld", ProductId = "001" });
        suppliers.Add(new Supplier { SupplierId = "SUP002", SupplierName = "CableTech", ProductId = "002" });
        suppliers.Add(new Supplier { SupplierId = "SUP003", SupplierName = "NoteCraft", ProductId = "003" });
        suppliers.Add(new Supplier { SupplierId = "SUP004", SupplierName = "SoundWave", ProductId = "004" });
        suppliers.Add(new Supplier { SupplierId = "SUP005", SupplierName = "WriteRight", ProductId = "005" });
        suppliers.Add(new Supplier { SupplierId = "SUP006", SupplierName = "CaseMate", ProductId = "006" });
        suppliers.Add(new Supplier { SupplierId = "SUP007", SupplierName = "HydroGear", ProductId = "007" });
        suppliers.Add(new Supplier { SupplierId = "SUP008", SupplierName = "AudioMax", ProductId = "008" });
        suppliers.Add(new Supplier { SupplierId = "SUP009", SupplierName = "StickyNotez", ProductId = "009" });
        suppliers.Add(new Supplier { SupplierId = "SUP010", SupplierName = "SmartTech", ProductId = "010" });
        suppliers.Add(new Supplier { SupplierId = "SUP011", SupplierName = "MouseMatters", ProductId = "011" });
        suppliers.Add(new Supplier { SupplierId = "SUP012", SupplierName = "ChargeUp", ProductId = "012" });
        suppliers.Add(new Supplier { SupplierId = "SUP013", SupplierName = "DeskPro", ProductId = "013" });
        suppliers.Add(new Supplier { SupplierId = "SUP014", SupplierName = "LapLift", ProductId = "014" });
        suppliers.Add(new Supplier { SupplierId = "SUP015", SupplierName = "CableNest", ProductId = "015" });
        suppliers.Add(new Supplier { SupplierId = "SUP016", SupplierName = "MarkerZone", ProductId = "016" });
        suppliers.Add(new Supplier { SupplierId = "SUP017", SupplierName = "PowerPlus", ProductId = "017" });
        suppliers.Add(new Supplier { SupplierId = "SUP018", SupplierName = "BrightDesk", ProductId = "018" });
        suppliers.Add(new Supplier { SupplierId = "SUP019", SupplierName = "CutSmart", ProductId = "019" });
        suppliers.Add(new Supplier { SupplierId = "SUP020", SupplierName = "FlashTech", ProductId = "020" });
        suppliers.Add(new Supplier { SupplierId = "SUP021", SupplierName = "DisplayHub", ProductId = "021" });
        suppliers.Add(new Supplier { SupplierId = "SUP022", SupplierName = "KeyMasters", ProductId = "022" });
        suppliers.Add(new Supplier { SupplierId = "SUP023", SupplierName = "StandUp", ProductId = "023" });
        suppliers.Add(new Supplier { SupplierId = "SUP024", SupplierName = "PenNest", ProductId = "024" });
        suppliers.Add(new Supplier { SupplierId = "SUP025", SupplierName = "MobileMart", ProductId = "025" });
        suppliers.Add(new Supplier { SupplierId = "SUP026", SupplierName = "TabZone", ProductId = "026" });
        suppliers.Add(new Supplier { SupplierId = "SUP027", SupplierName = "MouseWorld", ProductId = "027" });
        suppliers.Add(new Supplier { SupplierId = "SUP028", SupplierName = "PouchPro", ProductId = "028" });
        suppliers.Add(new Supplier { SupplierId = "SUP029", SupplierName = "ClipTech", ProductId = "029" });
        suppliers.Add(new Supplier { SupplierId = "SUP030", SupplierName = "HolderHub", ProductId = "030" });
    }
}

namespace LINQ.Models;

/// <summary>
/// Represents the supplier with it's details.
/// </summary>
public class Supplier
{
    /// <summary>
    /// Gets or sets the Id of the supplier.
    /// </summary>
    /// <value>Holds the Id of the supplier.</value>
    public string? SupplierId { get; set; }

    /// <summary>
    /// Gets or sets the name of the supplier name.
    /// </summary>
    /// <value>Holds the Name of the supplier name.</value>
    public string? SupplierName { get; set; }

    /// <summary>
    /// Gets or sets the Product Id of the supplier.
    /// </summary>
    /// <value>Holds the Product Id of the supplier.</value>
    public string? ProductId { get; set; }
}

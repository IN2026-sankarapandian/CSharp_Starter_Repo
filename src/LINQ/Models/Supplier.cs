namespace LINQ.Models;

/// <summary>
/// Represents the supplier with it's details.
/// </summary>
public class Supplier
{
    private string _supplierId = null!;
    private string _supplierName = null!;
    private string _productId = null!;

    /// <summary>
    /// Gets or sets the Id of the supplier.
    /// </summary>
    /// <value>Holds the Id of the supplier.</value>
    public string SupplierId
    {
        get => this._supplierId;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException("Supplier id can`t be empty");
            }

            this._supplierId = value;
        }
    }

    /// <summary>
    /// Gets or sets the name of the supplier name.
    /// </summary>
    /// <value>Holds the Name of the supplier name.</value>
    public string SupplierName
    {
        get => this._supplierName;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException("Supplier name can`t be empty");
            }

            this._supplierName = value;
        }
    }

    /// <summary>
    /// Gets or sets the Id of the product.
    /// </summary>
    /// <value>Holds the Id of the product.</value>
    public string ProductId
    {
        get => this._productId;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException("Product Id can`t be empty");
            }

            this._productId = value;
        }
    }
}

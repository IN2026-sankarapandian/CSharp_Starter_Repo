namespace LINQ.Models;

/// <summary>
/// Represents the product with it's details.
/// </summary>
public class Product
{
    private string _id = null!;
    private string _name = null!;
    private decimal _price;
    private string _category = null!;

    /// <summary>
    /// Gets or sets the Id of the product.
    /// </summary>
    /// <value>Holds the Id of the product.</value>
    public string ID
    {
        get => this._id;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException("Name cant be empty");
            }

            this._id = value;
        }
    }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    /// <value>Holds the Name of the product.</value>
    public string Name
    {
        get => this._name;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException("Name cant be empty");
            }

            this._name = value;
        }
    }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    /// <value>Holds the price of the product.</value>
    public decimal Price
    {
        get => this._price;
        set { this._price = value; }
    }

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    /// <value>Holds the category of the product.</value>
    public string Category
    {
        get => this._category;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException("Category cant be empty");
            }

            this._category = value;
        }
    }
}

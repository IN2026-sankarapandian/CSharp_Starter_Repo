namespace InventoryManager.Models;

/// <summary>
/// Represent list of <see cref="Product"/>s.
/// </summary>
public class ProductList
{
    private List<Product> _productList = new ();

    /// <summary>
    /// Add a <see cref="Product"/> to the list.
    /// </summary>
    /// <param name="newProduct">New <see cref="Product"/> to add.</param>
    public void Add(Product newProduct)
    {
        this._productList.Add(newProduct);
    }

    /// <summary>
    /// Holds the list of <see cref="Product"/>s."/>
    /// </summary>
    /// <returns>List of <see cref="Product"/>s.</returns>
    public List<Product> Get()
    {
        return this._productList;
    }

    /// <summary>
    /// Edits the value of a field in a <see cref="Product"/> at specified index in the inventory.
    /// </summary>
    /// <param name="index">Index of <see cref="Product"/> to edit in the list.</param>
    /// <param name="field">Field of <see cref="Product"/> to edit.</param>
    /// <param name="newValue">New value to edit with.</param>
    public void Edit(int index, string field, object newValue)
    {
        this._productList[index][field] = newValue;
    }

    /// <summary>
    /// Delete the <see cref="Product"/> in inventory at given index.
    /// </summary>
    /// <param name="index">Index of <see cref="Product"/> to delete./param>
    public void Delete(int index)
    {
        this._productList.RemoveAt(index);
    }

    /// <summary>
    /// Search for the keyword and return all the matched <see cref="Product"/>s as list.
    /// </summary>
    /// <param name="keyword">Key word to search with.</param>
    /// <returns>The list of matched <see cref="Product"/>s</returns>
    public List<Product> Search(string keyword)
    {
        if (keyword is null)
        {
            return new List<Product>();
        }

        List<Product> filteredProducts = this._productList.FindAll(product =>
            {
                bool result = false;
                foreach (string field in Product.GetFields())
                {
                    if (field is null)
                    {
                        continue;
                    }

                    object? value = product[field];
                    if (value is null)
                    {
                        continue;
                    }

                    string? valueString = value.ToString();
                    if (valueString is null)
                    {
                        continue;
                    }

                    Console.WriteLine(valueString);
                    result |= valueString.ToUpper().Contains(keyword.ToUpper());
                }

                return result;
            });
        return filteredProducts;
    }

    /// <summary>
    /// Checks for dupicates for specified value and field.
    /// </summary>
    /// <param name="field">Field to check.</param>
    /// <param name="value">Target value.</param>
    /// <returns><see cref="true"/> if duplicate exists; <see cref="false"/> otherwise.</returns>
    public bool HasDuplicate(string field, object value)
    {
        return this._productList.Exists(product =>
        {
            string? valueInList = product[field].ToString();
            if (valueInList is null)
            {
                return false;
            }

            return valueInList.Equals(value.ToString());
        });
    }

    /// <summary>
    /// Holds the count of <see cref="Product"/>s in the list.
    /// </summary>
    /// <returns>Total count of <see cref="Product"/>s in the list.</returns>
    public int Count() => this._productList.Count;
}
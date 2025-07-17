namespace InventoryManager.Models;

/// <summary>
/// Represent list of <see cref="Product"/>s.
/// </summary>
internal class ProductList
{
    private List<Product> _productList = new ();

    /// <summary>
    /// Add a <see cref="Product"/> to the inventory.
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
    /// Search for the keywrd and return all the matched <see cref="Product"/>s as list.
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
                    if (product is null || product[field] is null || field is null)
                    {
                        continue;
                    }

                    string value = product[field].ToString().ToUpper();
                    result |= value.Contains(keyword.ToUpper());
                }

                return result;
            });
        return filteredProducts;
    }

    /// <summary>
    /// Holds the count of <see cref="Product"/> in the list.
    /// </summary>
    /// <returns>Total count of <see cref="Product"/> in the list.</returns>
    public int Count() => this._productList.Count;
}
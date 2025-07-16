using InventoryManager.Models;
using InventoryManager.Parsers;
using InventoryManager.UI;
using InventoryManager.Validators;

namespace InventoryManager.Handlers;

/// <summary>
/// Handles the features of inventory manager (Add, Show, Edit, Delete)
/// </summary>
internal class FeatureHandlers
{
    /// <summary>
    /// Handles adding a new product to <see cref="ProductList"/>
    /// </summary>
    /// <param name="list">Gives access to user <see cref="ProductList"/></param>
    public static void HandleAddProduct(ProductList list)
    {
        Dictionary<string, object>? newProductDetails = new Dictionary<string, object>();
        Dictionary<string, Type>? productTemplate = Product.GetTemplate();
        foreach (var field in productTemplate)
        {
            object result;
            string? input;
            do
            {
                input = ConsoleUI.PromptAndGetInput($"{field.Key} : ");
                if (input is null)
                {
                    continue;
                }
            }
            while (!(Parser.TryParseValue(input, field.Value, out result) && Validator.Validate(field.Key, result)));
            newProductDetails[field.Key] = result;
        }
        Product newProduct = new Product(newProductDetails);
        list.Add(newProduct);
    }
}

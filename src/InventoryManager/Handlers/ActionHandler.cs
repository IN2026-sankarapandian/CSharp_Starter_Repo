using InventoryManager.Constants;
using InventoryManager.Models;
using InventoryManager.Parsers;
using InventoryManager.UI;
using InventoryManager.Validators;

namespace InventoryManager.Handlers;

/// <summary>
/// Provides methods to handle actions of the inventory manager.
/// </summary>
public class ActionHandler
{
    private readonly IUserInterface _userInterface;
    private readonly IFormHandler _formHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionHandler"/> class.
    /// </summary>
    /// <param name="userInterface">Provide methods to interact with user via UI</param>
    /// <param name="formHandler">Provide methods to get input from user.</param>
    public ActionHandler(IUserInterface userInterface, IFormHandler formHandler)
    {
        this._userInterface = userInterface;
        this._formHandler = formHandler;
    }

    /// <summary>
    /// Handle getting user inputs and adds a new product to the <see cref="ProductList"/>.
    /// </summary>
    /// <remarks>
    /// This method prompts the user to enter all the details for the product
    /// and create a new product with the user input details in <see cref="ProductList"/>.
    /// It also validate every inputs from the user using <see cref="Validator"/>
    /// </remarks>
    /// <param name="productList">Give access to user list</param>
    public void HandleAddProduct(ProductList productList)
    {
        this._userInterface.CreateNewPageFor("Add");
        this._userInterface.PromptLine("Enter the details of product :");
        Dictionary<string, object>? newProductDetails = new ();
        Dictionary<string, Type>? productTemplate = Product.GetTemplate();
        foreach (KeyValuePair<string, Type> field in productTemplate)
        {
            object result;
            string input;
            string? errorMessage = string.Empty;
            do
            {
                this._userInterface.PromptLine(errorMessage, ConsoleColor.Yellow);
                input = this._userInterface.PromptAndGetInput($"{field.Key} : ");
            }
            while (!(Parser.TryParseValue(input, field.Value, out result, out errorMessage) && Validator.IsValid(productList, field.Key, result, out errorMessage)));
            newProductDetails[field.Key] = result;
        }

        Product newProduct = new (newProductDetails);
        productList.Add(newProduct);
        this._userInterface.PromptLine("Added successfully", ConsoleColor.Green);
        this._userInterface.WaitAndNavigateToMenu();
    }

    /// <summary>
    /// Handles showing the products in the inventory to the user.
    /// </summary>
    /// <remarks>
    /// This method lists all the products as a table format to the user. It informs user
    /// if product list was empty and ask user to return back to menu.
    /// </remarks>
    /// <param name="productList">Give access to user list</param>
    public void HandleShowProducts(ProductList productList)
    {
        this._userInterface.CreateNewPageFor("Show");
        if (!this._formHandler.ShowProducts(productList))
        {
            return;
        }

        this._userInterface.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Handles editing a product in the inventory by getting user input and applying the changes.
    /// </summary>
    /// <remarks> This method lists all the products as a table format to the user and prompt the user to
    /// select a index of product to edit (It informs user if product list was empty and ask user to return
    /// back to menu). It list all the available fields of the product and prompt the user to select the field
    /// to edit. Then it prompt the user to enter the new value to change and apply changes to the list via <see cref="ProductList"/>.
    /// This function validates the index, field, and new value of the field with <see cref="Validator"/>.
    /// </remarks>
    /// <param name="productList">Give access to user list</param>
    public void HandleEditProduct(ProductList productList)
    {
        this._userInterface.CreateNewPageFor("Edit");
        if (!this._formHandler.ShowProducts(productList))
        {
            return;
        }

        int index = this._formHandler.GetIndex(productList);
        string field = this._formHandler.GetFieldName(productList);
        object value;
        string valueString;
        string? errorMessage = string.Empty;
        do
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                this._userInterface.PromptLine(errorMessage, ConsoleColor.Yellow);
            }

            valueString = this._userInterface.PromptAndGetInput($"New {field} : ");
        }
        while (!(Parser.TryParseValue(valueString, Product.GetTemplate()[field], out value, out errorMessage) && Validator.IsValid(productList, field, value, out errorMessage)));
        productList.Edit(index - 1, field, value);
        this._userInterface.PromptLine("Edited successfully", ConsoleColor.Green);
        this._userInterface.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Handles the deletion of a product from the provided <see cref="ProductList"/>.
    /// </summary>
    /// <remarks>This method lists the products as table format with index to the user and prompt the user
    /// to select a index of product to delete. Deletes the product at specified index in <see cref="ProductList"/>
    /// If no products are in the list, then it inform the user
    /// that there is no products in the list so navigate back to menu. It also check the input is a valid index using <see cref="Validator"/>.
    /// </remarks>
    /// <param name="productList">Give access to user list</param>
    public void HandleDeleteProduct(ProductList productList)
    {
        this._userInterface.CreateNewPageFor("Delete");
        if (!this._formHandler.ShowProducts(productList))
        {
            return;
        }

        productList.Delete(this._formHandler.GetIndex(productList) - 1);
        this._userInterface.PromptLine("Deleted successfully", ConsoleColor.Green);
        this._userInterface.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Handle search actions on the product list.
    /// </summary>
    /// <remarks>
    /// This method prompt the user to give keyword to search and
    /// pass the keyword to <see cref="ProductList"/> via search function
    /// and list the matched results, if there is no matches it will inform
    /// user that there is no matches so navigate to menu. If no products
    /// are in the list, then it inform the user that there is no products
    /// in the list so navigate back to menu.
    /// </remarks>
    /// <param name="productList">Give access to user list.</param>
    public void HandleSearchProduct(ProductList productList)
    {
        this._userInterface.CreateNewPageFor("Search");
        if (productList.Count() <= 0)
        {
            this._userInterface.PromptLine(ErrorMessages.NoProducts, ConsoleColor.Yellow);
            this._userInterface.WaitAndNavigateToMenu();
            return;
        }

        string keyword = this._userInterface.PromptAndGetInput($"Keyword : ");
        List<Product> products = productList.Search(keyword);
        if (products.Count == 0)
        {
            this._userInterface.PromptLine(ErrorMessages.NoMatches, ConsoleColor.Yellow);
            this._userInterface.WaitAndNavigateToMenu();
            return;
        }

        this._userInterface.CreateNewPageFor("Search results");
        this._userInterface.PrintAsTable(products);
        this._userInterface.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Ask the user for confirmation to exit with a warning of loss of data.
    /// </summary>
    /// <returns><see cref="true"/> /if user chooses to exit; otherwise <see cref="false"/></returns>
    public async Task<bool> ConfirmExitAsync()
    {
        do
        {
            string input = this._userInterface.PromptAndGetInput(ErrorMessages.AppClosingPrompt, ConsoleColor.Magenta);
            if (input.ToUpper() == "Y")
            {
                this._userInterface.PromptLine("Closing the app...", ConsoleColor.Magenta);
                await Task.Delay(1000);
                return false;
            }
            else if (input.ToUpper() == "N")
            {
                this._userInterface.CreateNewPageFor("Menu");
                break;
            }
        }
        while (true);
        return true;
    }
}
using InventoryManager.Constants;
using InventoryManager.Models;
using InventoryManager.Parsers;
using InventoryManager.UI;
using InventoryManager.Validators;

namespace InventoryManager.ActionHandlers;

/// <summary>
/// Provides methods to handle actions of the inventory manager.
/// </summary>
public class ActionHandler
{
    /// <summary>
    /// Handle getting user inputs and adds a new product to the <see cref="ProductList"/>.
    /// </summary>
    /// <remarks>
    /// This method prompts the user to enter all the details for the product
    /// and create a new product with the user input details in <see cref="ProductList"/>.
    /// It also validate every inputs from the user using <see cref="Validator"/>
    /// </remarks>
    /// <param name="productList">Give access to user list</param>
    public static void HandleAddProduct(ProductList productList)
    {
        ConsoleUI.CreateNewPageFor("Add");
        ConsoleUI.PromptLine("Enter the details of product :");
        Dictionary<string, object>? newProductDetails = new Dictionary<string, object>();
        Dictionary<string, Type>? productTemplate = Product.GetTemplate();
        foreach (KeyValuePair<string, Type> field in productTemplate)
        {
            object result;
            string input;
            string? errorMessage = string.Empty;
            do
            {
                ConsoleUI.PromptLine(errorMessage, ConsoleColor.Yellow);
                input = ConsoleUI.PromptAndGetInput($"{field.Key} : ");
            }
            while (!(Parser.TryParseValue(input, field.Value, out result, out errorMessage) && Validator.IsValid(productList, field.Key, result, out errorMessage)));
            newProductDetails[field.Key] = result;
        }

        Product newProduct = new Product(newProductDetails);
        productList.Add(newProduct);
        ConsoleUI.PromptLine("Added successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
    }

    /// <summary>
    /// Handles showing the products in the inventory to the user.
    /// </summary>
    /// <remarks>
    /// This method lists all the products as a table format to the user. It informs user
    /// if product list was empty and ask user to return back to menu.
    /// </remarks>
    /// <param name="productList">Give access to user list</param>
    public static void HandleShowProducts(ProductList productList)
    {
        ConsoleUI.CreateNewPageFor("Show");
        if (!Helper.ShowProducts(productList))
        {
            return;
        }

        ConsoleUI.WaitAndNavigateToMenu();
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
    public static void HandleEditProduct(ProductList productList)
    {
        ConsoleUI.CreateNewPageFor("Edit");
        if (!Helper.ShowProducts(productList))
        {
            return;
        }

        int index = Helper.GetIndex(productList);
        string field = Helper.GetFieldName(productList);
        object value;
        string valueString;
        string? errorMessage = string.Empty;
        do
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                ConsoleUI.PromptLine(errorMessage, ConsoleColor.Yellow);
            }

            valueString = ConsoleUI.PromptAndGetInput($"New {field} : ");
        }
        while (!(Parser.TryParseValue(valueString, Product.GetTemplate()[field], out value, out errorMessage) && Validator.IsValid(productList, field, value, out errorMessage)));
        productList.Edit(index - 1, field, value);
        ConsoleUI.PromptLine("Edited successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
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
    public static void HandleDeleteProduct(ProductList productList)
    {
        ConsoleUI.CreateNewPageFor("Delete");
        if (!Helper.ShowProducts(productList))
        {
            return;
        }

        productList.Delete(Helper.GetIndex(productList) - 1);
        ConsoleUI.PromptLine("Deleted successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
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
    public static void HandleSearchProduct(ProductList productList)
    {
        ConsoleUI.CreateNewPageFor("Search");
        if (productList.Count() <= 0)
        {
            ConsoleUI.PromptLine(ErrorMessages.NoProducts, ConsoleColor.Yellow);
            ConsoleUI.WaitAndNavigateToMenu();
            return;
        }

        string keyword = ConsoleUI.PromptAndGetInput($"Keyword : ");
        List<Product> products = productList.Search(keyword);
        if (products.Count == 0)
        {
            ConsoleUI.PromptLine(ErrorMessages.NoMatches, ConsoleColor.Yellow);
            ConsoleUI.WaitAndNavigateToMenu();
            return;
        }

        ConsoleUI.CreateNewPageFor("Search results");
        ConsoleUI.PrintAsTable(products);
        ConsoleUI.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Ask the user for confirmation to exit with a warning of loss of data.
    /// </summary>
    /// <returns><see cref="true"/> /if user chooses to exit; otherwise <see cref="false"/></returns>
    public static async Task<bool> ConfirmExitAsync()
    {
        do
        {
            string input = ConsoleUI.PromptAndGetInput(ErrorMessages.AppCLosingPrompt, ConsoleColor.Magenta);
            if (input.ToUpper() == "Y")
            {
                ConsoleUI.PromptLine("Closing the app...", ConsoleColor.Magenta);
                await Task.Delay(1000);
                return false;
            }
            else if (input.ToUpper() == "N")
            {
                ConsoleUI.CreateNewPageFor("Menu");
                break;
            }
        }
        while (true);
        return true;
    }

    // Consist of helper methods to get values from the user(used only on action handlers)
    private static class Helper
    {
        // Get the index from the user until user enters a valid input.
        public static int GetIndex(ProductList productList)
        {
            int index;
            do
            {
                string indexString = ConsoleUI.PromptAndGetInput("Enter the index of the product : ");
                if (!int.TryParse(indexString, out index))
                {
                    ConsoleUI.PromptLine(ErrorMessages.NotValidIndex, ConsoleColor.Yellow);
                    continue;
                }

                if (index > productList.Count() || index <= 0)
                {
                    ConsoleUI.PromptLine(ErrorMessages.NoProductAtGivenIndex + index, ConsoleColor.Yellow);
                    continue;
                }

                return index;
            }
            while (true);
        }

        // Get field name from the user until user enters a valid input.
        public static string GetFieldName(ProductList productList)
        {
            do
            {
                string[] fields = Product.GetFields();
                for (int i = 1; i < fields.Length; i++)
                {
                    ConsoleUI.Prompt($"{i} {fields[i]} ");
                }

                string fieldChoiceString = ConsoleUI.PromptAndGetInput("\nEnter a field : ");
                if (!(int.TryParse(fieldChoiceString, out int fieldChoice) && fieldChoice < fields.Length && fieldChoice > 0))
                {
                    ConsoleUI.PromptLine(ErrorMessages.NotValidField, ConsoleColor.Yellow);
                    continue;
                }

                return fields[fieldChoice];
            }
            while (true);
        }

        // Shows the product as table
        // return false if there is no products available; false otherwise
        public static bool ShowProducts(ProductList productList)
        {
            if (productList.Count() <= 0)
            {
                ConsoleUI.PromptLine(ErrorMessages.NoProducts, ConsoleColor.Yellow);
                ConsoleUI.WaitAndNavigateToMenu();
                return false;
            }

            List<Product> currentProductList = productList.Get();
            ConsoleUI.PrintAsTable(currentProductList);
            return true;
        }
    }
}
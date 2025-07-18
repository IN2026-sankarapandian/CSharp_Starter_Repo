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
    /// <param name="list">Give access to user list</param>
    public static void HandleAddProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Add");
        ConsoleUI.PromptLine("Enter the details of product :");
        Dictionary<string, object>? newProductDetails = new Dictionary<string, object>();
        Dictionary<string, Type>? productTemplate = Product.GetTemplate();
        foreach (var field in productTemplate)
        {
            object result;
            string input, erroressage = string.Empty;
            do
            {
                if (erroressage != string.Empty)
                {
                    ConsoleUI.PromptLine(erroressage, ConsoleColor.Yellow);
                }

                input = ConsoleUI.PromptAndGetInput($"{field.Key} : ");
            }
            while (!(Parser.TryParseValue(input, field.Value, out result, out erroressage) && Validator.Validate(field.Key, result, out erroressage)));
            newProductDetails[field.Key] = result;
        }

        Product newProduct = new Product(newProductDetails);
        list.Add(newProduct);
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
    /// <param name="list">Give access to user list</param>
    public static void HandleShowProducts(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Show");
        if (!Helper.ShowProducts(list))
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
    /// <param name="list">Give access to user list</param>
    public static void HandleEditProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Edit");
        if (!Helper.ShowProducts(list))
        {
            return;
        }

        int index = Helper.GetIndex(list);
        string field = Helper.GetFieldName(list);
        object value;
        string valueString, errorMessage = string.Empty;
        do
        {
            if (errorMessage != string.Empty)
            {
                ConsoleUI.PromptLine(errorMessage, ConsoleColor.Yellow);
            }

            valueString = ConsoleUI.PromptAndGetInput($"New {field} : ");
        }
        while (!(Parser.TryParseValue(valueString, Product.GetTemplate()[field], out value, out errorMessage) && Validator.Validate(field, value, out errorMessage)));
        list.Edit(index - 1, field, value);
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
    /// <param name="list">Give access to user list</param>
    public static void HandleDeleteProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Delete");
        if (!Helper.ShowProducts(list))
        {
            return;
        }

        list.Delete(Helper.GetIndex(list) - 1);
        ConsoleUI.PromptLine("Deleted successfully", ConsoleColor.Green);
        ConsoleUI.WaitAndNavigateToMenu();
        return;
    }

    /// <summary>
    /// Handle search actions on the product list.
    /// </summary>
    /// <remarks>
    /// This method prompt the user to give keyword to search and
    /// pass the keyword to <see cref="ProductList"/> via search funcion
    /// and list the matched results, if there is no matches it will inform
    /// user that there is no matches so navigate to menu. If no products
    /// are in the list, then it inform the user that there is no products
    /// in the list so navigate back to menu.
    /// </remarks>
    /// <param name="list">Give access to user list.</param>
    public static void HandleSearchProduct(ProductList list)
    {
        ConsoleUI.CreateNewPageFor("Search");
        if (list.Count() <= 0)
        {
            ConsoleUI.PromptLine("No products available !", ConsoleColor.Yellow);
            ConsoleUI.WaitAndNavigateToMenu();
            return;
        }

        string keyword = ConsoleUI.PromptAndGetInput($"Keyword : ");
        List<Product> products = list.Search(keyword);
        if (products.Count == 0)
        {
            ConsoleUI.PromptLine("No matches found !", ConsoleColor.Yellow);
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
            string input = ConsoleUI.PromptAndGetInput("Warning : Closing the app will erase all added product details. Are you sure you want to continue? (y/n) :", ConsoleColor.Magenta);
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

    private static class Helper
    {
        public static int GetIndex(ProductList list)
        {
            int index;
            do
            {
                string indexString = ConsoleUI.PromptAndGetInput("Enter the index of the product : ");
                if (!int.TryParse(indexString, out index))
                {
                    ConsoleUI.PromptLine("Enter a valid index !", ConsoleColor.Yellow);
                    continue;
                }

                if (index > list.Count() || index <= 0)
                {
                    ConsoleUI.PromptLine("No product exist at the given index " + index, ConsoleColor.Yellow);
                    continue;
                }

                return index;
            }
            while (true);
        }

        public static string GetFieldName(ProductList list)
        {
            do
            {
                string[] fields = Product.GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    ConsoleUI.Prompt($"{i + 1} {fields[i]} ");
                }

                string fieldChoiceString = ConsoleUI.PromptAndGetInput("\nEnter a field : ");
                if (!int.TryParse(fieldChoiceString, out int fieldChoice) && fieldChoice < fields.Length && fieldChoice > 0)
                {
                    ConsoleUI.PromptLine("Enter a valid choice", ConsoleColor.Yellow);
                    continue;
                }

                return fields[fieldChoice - 1];
            }
            while (true);
        }

        public static bool ShowProducts(ProductList list)
        {
            if (list.Count() <= 0)
            {
                ConsoleUI.PromptLine("No products found.", ConsoleColor.Yellow);
                ConsoleUI.WaitAndNavigateToMenu();
                return false;
            }

            List<Product> currentProductList = list.Get();
            ConsoleUI.PrintAsTable(currentProductList);
            return true;
        }
    }
}
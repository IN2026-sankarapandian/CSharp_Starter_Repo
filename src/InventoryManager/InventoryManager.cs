using InventoryManager.ActionHandlers;
using InventoryManager.Constants;
using InventoryManager.Models;
using InventoryManager.UI;

namespace LINQ;

/// <summary>
/// The main class contains the main method, entry point of a inventory manager, c# application.
/// </summary>
public class InventoryManager
{
    /// <summary>
    /// The Main method is the entry point of the inventory manager, creates a new instance of <see cref="ProductList"/>, prompts the user for actions, and handles them accordingly.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A task that represents the asynchronous operation of the application.</returns>
    public static async Task Main(string[] args)
    {
        ConsoleUI.CreateNewPageFor("Menu");
        ProductList productList = new ();
        while (true)
        {
            ConsoleUI.Prompt("1. Add Product\n2. Show Product\n3. Edit Product\n4. Delete Product\n5. Search Product\n");
            ConsoleUI.PromptLine("6. Exit\n", ConsoleColor.Red);
            string choice = ConsoleUI.PromptAndGetInput("\nWhat do you want to do : ");
            switch (choice)
            {
                case "1":
                    ActionHandler.HandleAddProduct(productList);
                    break;
                case "2":
                    ActionHandler.HandleShowProducts(productList);
                    break;
                case "3":
                    ActionHandler.HandleEditProduct(productList);
                    break;
                case "4":
                    ActionHandler.HandleDeleteProduct(productList);
                    break;
                case "5":
                    ActionHandler.HandleSearchProduct(productList);
                    break;
                case "6":
                    bool confirmExit = await ActionHandler.ConfirmExitAsync();
                    if (!confirmExit)
                    {
                        return;
                    }

                    break;
                default:
                    ConsoleUI.PromptLine(ErrorMessages.InvalidActionChoice, ConsoleColor.Yellow);
                    break;
            }
        }
    }
}
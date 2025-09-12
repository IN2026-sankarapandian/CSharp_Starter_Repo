using InventoryManager.Constants;
using InventoryManager.Handlers;
using InventoryManager.Models;
using InventoryManager.UI;

namespace InventoryManager;

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
        IUserInterface userInterface = new ConsoleUI();
        IFormHandler formHandler = new FormHandler(userInterface);
        ActionHandler actionHandler = new (userInterface, formHandler);
        userInterface.CreateNewPageFor("Menu");
        ProductList productList = new ();
        while (true)
        {
            userInterface.Prompt("1. Add Product\n2. Show Product\n3. Edit Product\n4. Delete Product\n5. Search Product\n");
            userInterface.PromptLine("6. Exit\n", ConsoleColor.Red);
            string choice = userInterface.PromptAndGetInput("\nWhat do you want to do : ");
            switch (choice)
            {
                case "1":
                    actionHandler.HandleAddProduct(productList);
                    break;
                case "2":
                    actionHandler.HandleShowProducts(productList);
                    break;
                case "3":
                    actionHandler.HandleEditProduct(productList);
                    break;
                case "4":
                    actionHandler.HandleDeleteProduct(productList);
                    break;
                case "5":
                    actionHandler.HandleSearchProduct(productList);
                    break;
                case "6":
                    bool confirmExit = await actionHandler.ConfirmExitAsync();
                    if (!confirmExit)
                    {
                        return;
                    }

                    break;
                default:
                    userInterface.PromptLine(ErrorMessages.InvalidActionChoice, ConsoleColor.Yellow);
                    break;
            }
        }
    }
}
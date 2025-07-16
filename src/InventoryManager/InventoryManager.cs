using ConsoleTables;
using InventoryManager.Handlers;
using InventoryManager.Models;
using InventoryManager.UI;

namespace Assignments
{
    /// <summary>
    /// The main class contains the main method, entry point of a inventory manager, c# application
    /// </summary>
    internal class InventoryManager
    {
        /// <summary>
        /// The Main method is the entry point of a inventory manager, C# application. When the application is started, the Main method is the first method that is invoked.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            ProductList list = new ProductList();
            ConsoleUI.PromptInfo("Inventory manager - Menu");
            ConsoleUI.PromptInfo("1. Add\n2. Show\n3. Delete");
            ConsoleUI.PromptInfo("What do you want to do : ");
            while (true)
            {
                string? choice = ConsoleUI.PromptAndGetInput("Select option : ");
                switch (choice)
                {
                    case "1":
                        FeatureHandlers.HandleAddProduct(list);
                        break;
                    case "2":
                        List<Product> userProductList = list.Get();
                        ConsoleUI.PrintTable(userProductList);

                        break;
                    case "3":
                        break;
                }
            }
        }
    }
}
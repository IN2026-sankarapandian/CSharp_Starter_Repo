using InventoryManager;
using InventoryManager.Models;

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
                        FeatureHandlers.AddProduct(list);
                        break;
                    case "2":
                        list.Show();
                        break;
                    case "3":
                        break;
                }
            }
        }
    }
}
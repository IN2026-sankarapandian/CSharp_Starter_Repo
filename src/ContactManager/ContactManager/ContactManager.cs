global using ContactManager.Handlers;
global using ContactManager.Model;
global using ContactManager.Repository;
global using ContactManager.UI;

namespace ContactManager
{
    /// <summary>
    /// this is the main class of the entire project
    /// </summary>
    internal class ContactManager
    {
        /// <summary>
        /// The Main method is the entry point of a C# application. When the application is started, the Main method is the first method that is invoked.
        /// </summary>
        /// <param name="args">Inputs from console while executing file</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            ContactRepository userContactList = new ContactRepository();
            ConsoleUI.PrintAppHeader("Menu");
            do
            {
                Console.WriteLine("1. Add  \n2. Show  \n3. Edit  \n4. Delete  \n5. Sort  \n6. Search  \n7. Exit\n");
                string choice = ConsoleUI.GetInputWithPrompt("What do you want to do : ");
                if (choice == string.Empty)
                {
                    ConsoleUI.PromptWarning("Option cant be empty !\n");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        FeatureController.ManageAddContact(userContactList);
                        break;
                    case "2":
                        FeatureController.ManageShowContact(userContactList);
                        break;
                    case "3":
                        FeatureController.ManageEditContact(userContactList);
                        break;
                    case "4":
                        FeatureController.ManageDeleteContact(userContactList);
                        break;
                    case "5":
                        FeatureController.ManageSortContact(userContactList);
                        break;
                    case "6":
                        FeatureController.ManageSearchContact(userContactList);
                        break;
                    case "7":
                        ConsoleUI.PrintAppHeader("Closing the application.....");
                        await Task.Delay(3000);
                        return;
                    default:
                        ConsoleUI.PromptWarning("Enter a valid options !\n");
                        break;
                }
            }
            while (true);
        }
    }
}
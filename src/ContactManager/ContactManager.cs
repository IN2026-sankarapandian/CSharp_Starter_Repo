namespace ContactManager;

/// <summary>
/// This is the main class of the entire project.
/// </summary>
internal class ContactManager
{
    /// <summary>
    /// The Main method is the entry point of a C# application. When the application is started, the Main method is the first method that is invoked.
    /// </summary>
    /// <param name="args">Inputs from console while executing file</param>
    /// <returns>A <see cref="Task"/> Representing the asynchronous operation.</returns>
    public static async Task Main()
    {
        ContactRepository userContactList = new ContactRepository();
        ConsoleUI.PrintAppHeader("Menu");
        do
        {
            Console.WriteLine("1. Add  \n2. Show  \n3. Edit  \n4. Delete  \n5. Sort  \n6. Search  \n7. Exit\n");
            string choice = ConsoleUI.GetInputWithPrompt("What do you want to do : ");
            switch (choice)
            {
                case "1":
                    FeatureHandler.ManageAddContact(userContactList);
                    break;
                case "2":
                    FeatureHandler.ManageShowContact(userContactList);
                    break;
                case "3":
                    FeatureHandler.ManageEditContact(userContactList);
                    break;
                case "4":
                    FeatureHandler.ManageDeleteContact(userContactList);
                    break;
                case "5":
                    FeatureHandler.ManageSortContact(userContactList);
                    break;
                case "6":
                    FeatureHandler.ManageSearchContact(userContactList);
                    break;
                case "7":
                    ConsoleUI.PrintAppHeader("Closing the application.....");
                    await Task.Delay(1000);
                    return;
                default:
                    ConsoleUI.PromptInfoWithColor("Enter a valid options !\n", ConsoleColor.Yellow);
                    break;
            }
        }
        while (true);
    }
}
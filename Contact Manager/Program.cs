using Contact_Manager.ConsoleUI;

namespace Contact_Manager
{
    /// <summary>
    /// Main class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">Inputs from console while executing file</param>
        public static void Main(string[] args)
        {
            ContactListData userContactList = new ContactListData();
            string choice;
            do
            {
                Utilities.PrintHeader();
                Console.WriteLine("1. Add  2. Show  3. Edit  4. Delete  5. Sort  6. Search  7.Exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Services.ManageAddContact(userContactList);
                        break;
                    case "2":
                        Services.ManageShowContact(userContactList);
                        break;
                    case "3":
                        Services.ManageEditContact(userContactList);
                        break;
                    case "4":
                        Services.ManageDeleteContact(userContactList);
                        break;
                    case "5":
                        Services.ManageSortContact(userContactList);
                        break;
                    case "6":
                        Services.ManageSearchContact(userContactList);
                        break;
                    case "7":
                        return;
                    default:
                        break;
                }
            }
            while (true);
        }
    }
}

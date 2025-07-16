namespace ContactManager;

/// <summary>
/// Handle all the main features of contact manager Like Add, Show, Edit, Delete, Sort and Search
/// </summary>
internal class FeatureHandler
{
    private static readonly Dictionary<string, string> _contactTemplate = ContactRepository.GetContactTemplate();
    private static readonly string[] _fields = _contactTemplate.Keys.ToArray();

    /// <summary>
    /// Handle the process of adding a new contact
    /// get the all the required input and save a new contact
    /// and navigate to menu based on user option
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    public static void ManageAddContact(ContactRepository userContactList)
    {
        ConsoleUI.PrintAppHeader("Add");
        Dictionary<string, string> contactDetails = _contactTemplate;
        ConsoleUI.PromptInfoWithColor("You have to enter the name*, email*, phone*, additional notes ");
        foreach (string field in _fields)
        {
            contactDetails[field] = UserFormHandler.GetFieldValue(field, userContactList) ?? " ";
        }

        ConsoleUI.PromptInfoWithColor(string.Empty);
        userContactList.AddContact(contactDetails);
        ConsoleUI.PromptInfoWithColor("Contact added successfully !", ConsoleColor.Green);
        ConsoleUI.WaitAndReturnToMenu();
    }

    /// <summary>
    /// Get the whole contact list and displays them as a table
    /// and navigate to menu based on user option
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    public static void ManageShowContact(ContactRepository userContactList)
    {
        ConsoleUI.PrintAppHeader("Show");
        List<Contact> contactList = userContactList.GetContacts();
        ConsoleUI.PrintContacts(contactList);
        ConsoleUI.WaitAndReturnToMenu();
    }

    /// <summary>
    /// Handles all editing process, list all the contact to user, prompt the user
    /// to get index of contact to edit, field of contact to edit and apply the changes
    /// and navigate to menu based on user option
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    public static void ManageEditContact(ContactRepository userContactList)
    {
        ConsoleUI.PrintAppHeader("Edit");
        List<Contact> contactList = userContactList.GetContacts();
        ConsoleUI.PrintContacts(contactList);
        if (contactList.Count <= 0)
        {
            ConsoleUI.WaitAndReturnToMenu();
            return;
        }

        int index = UserFormHandler.GetValidContactIndex(userContactList);
        ConsoleUI.PrintAppHeader("Edit");
        Contact selectedContact = userContactList.GetContact(index - 1);
        ConsoleUI.PrintContact(selectedContact, true);
        ConsoleUI.PromptInfoWithColor("\nEdit ");
        string selectedField = UserFormHandler.GetField(_fields);
        ConsoleUI.PromptInfoWithColor("New ");
        string key = UserFormHandler.GetFieldValue(selectedField, userContactList);
        userContactList.EditContact(index - 1, selectedField, key);
        ConsoleUI.PromptInfoWithColor("Edited successfully !", ConsoleColor.Green);
        ConsoleUI.WaitAndReturnToMenu();
    }

    /// <summary>
    /// Handles entire deleting process, list all the contact list,
    /// prompt the user to get the index of contact to delete,
    /// delete it and navigate to menu based on user option
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    public static void ManageDeleteContact(ContactRepository userContactList)
    {
        ConsoleUI.PrintAppHeader("Delete");
        List<Contact> contactList = userContactList.GetContacts();
        ConsoleUI.PrintContacts(contactList);
        if (contactList.Count <= 0)
        {
            ConsoleUI.WaitAndReturnToMenu();
            return;
        }

        int index = UserFormHandler.GetValidContactIndex(userContactList);
        userContactList.DeleteContacts(index - 1);
        ConsoleUI.PromptInfoWithColor("Contact deleted successfully !", ConsoleColor.Green);
        ConsoleUI.WaitAndReturnToMenu();
    }

    /// <summary>
    /// Handles the search operation, prompt the user to get field to search and key word and
    /// list the matched result
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    public static void ManageSearchContact(ContactRepository userContactList)
    {
        ConsoleUI.PrintAppHeader("Search");
        string fieldName = UserFormHandler.GetField(_fields);
        string key = ConsoleUI.GetInputWithPrompt("Keyword : ");
        List<Contact> filteredContacts = userContactList.Search(fieldName, key);
        ConsoleUI.PrintAppHeader("Search");
        if (filteredContacts.Count <= 0)
        {
            ConsoleUI.PromptInfoWithColor("No matches found", ConsoleColor.Yellow);
        }
        else
        {
            ConsoleUI.PromptInfoWithColor("Matched results : ", ConsoleColor.Green);
            ConsoleUI.PrintContacts(filteredContacts);
        }

        ConsoleUI.WaitAndReturnToMenu();
    }

    /// <summary>
    /// Handles the user input for sort operation, list all the available sort option and prompt the user to select one
    /// apply the selected sort operation and display the sorted list.
    /// and navigate to menu based on user option
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    public static void ManageSortContact(ContactRepository userContactList)
    {
        ConsoleUI.PrintAppHeader("Sort");
        List<Contact> contactList = userContactList.GetContacts();
        ConsoleUI.PrintContacts(contactList);
        if (contactList.Count <= 0)
        {
            ConsoleUI.WaitAndReturnToMenu();
            return;
        }

        string optionName = UserFormHandler.GetSortOption(_fields);
        if (optionName == "Exit")
        {
            ConsoleUI.WaitAndReturnToMenu();
            return;
        }

        userContactList.SortContacts(optionName);
        ConsoleUI.PrintAppHeader("Sort");
        contactList = userContactList.GetContacts();
        ConsoleUI.PrintContacts(contactList);
        ConsoleUI.PromptInfoWithColor($"Sorted by {optionName}", ConsoleColor.Green);
        ConsoleUI.WaitAndReturnToMenu();
    }
}
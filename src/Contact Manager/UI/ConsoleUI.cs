using ContactManager.Model;
using ContactManager.Repository;

namespace ContactManager.UI;

/// <summary>
/// Provides Console UI operations for the Contact Manager application.
/// Get user input anddisplay messages in structured format
/// handle table formats and navigation to menu.
/// </summary>
internal static class ConsoleUI
{
    private static readonly Dictionary<string, string> _contactTemplate = ContactRepository.GetContactTemplate();
    private static readonly string[] _fields = new[] { "S. no" }.Concat(_contactTemplate.Keys).ToArray();

    /// <summary>
    /// Prints the list of contact in table structure
    /// </summary>
    /// <param name="contacts">Contact list</param>
    public static void PrintContacts(List<Contact> contacts)
    {
        if (contacts.Count <= 0)
        {
            PromptWarning("You don't have any contacts yet.");
            return;
        }

        PrintTableHeader();
        for (int i = 0; i < contacts.Count; i++)
        {
            PrintContact(contacts[i], false, $"{i + 1}");
        }
    }

    /// <summary>
    /// Print a single contact in a table structure
    /// </summary>
    /// <param name="contact">Contact object</param>
    /// <param name="printHeader">If true, prints the table header. Default is false.</param>
    /// <param name="index">Index value to print with contact</param>
    public static void PrintContact(Contact contact, bool printHeader = false, string index = "1")
    {
        string[] values = new string[_fields.Length];
        values[0] = index;
        for (int i = 1; i < _fields.Length; i++)
        {
            values[i] = contact[_fields[i]];
        }

        string rowFormat = GetRowFormat();
        if (printHeader)
        {
            PrintTableHeader();
        }

        Console.WriteLine(string.Format(rowFormat, values));
    }

    /// <summary>
    /// Prompt the user waits for the key press and navigates to menu
    /// </summary>
    public static void WaitAndReturnToMenu()
    {
        Console.WriteLine("\n\n\nPress any key to return menu.....");
        Console.ReadKey();
        PrintAppHeader("Menu");
    }

    /// <summary>
    /// This function is responsible to print the header of app name and mode name(if any) in all pages
    /// </summary>
    /// <param name="modeName">Name of the mode</param>
    public static void PrintAppHeader(string modeName)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Contact Manager");
        Console.Write($" - {modeName}\n\n");
        Console.ResetColor();
        return;
    }

    /// <summary>
    /// Display waring message in yellow color
    /// </summary>
    /// <param name="prompt">Prompt to be shown</param>
    public static void PromptWarning(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(prompt);
        Console.ResetColor();
    }

    /// <summary>
    /// Display success message in green color
    /// </summary>
    /// <param name="prompt">Prompt to be shown</param>
    public static void PromptSuccess(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(prompt);
        Console.ResetColor();
    }

    /// <summary>
    /// Display info in default color
    /// </summary>
    /// <param name="prompt">Prompt to be shown</param>
    public static void PromptInfo(string prompt)
    {
        Console.WriteLine(prompt);
        Console.ResetColor();
    }

    /// <summary>
    /// Prompt the user and returns the user input
    /// </summary>
    /// <param name="prompt">Prompt shown to the user</param>
    /// <returns>user input</returns>
    public static string GetInputWithPrompt(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    /// <summary>
    /// Create a row format according to the number of fields, column width can be customised if needed
    /// </summary>
    /// <returns> Row format as string.</returns>
    private static string GetRowFormat()
    {
        string rowFormat = string.Empty;
        for (int i = 0; i < _fields.Length; i++)
        {
            if (i == 0)
            {
                rowFormat += $"|{{{i},-5}}";
            }
            else
            {
                rowFormat += $"|{{{i},-18}}";
            }
        }

        rowFormat += "|";
        return rowFormat;
    }

    /// <summary>
    /// Prints the header of the table with field names
    /// </summary>
    private static void PrintTableHeader()
    {
        string rowFormat = GetRowFormat();
        string header = string.Format(rowFormat, _fields);
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length));
    }
}
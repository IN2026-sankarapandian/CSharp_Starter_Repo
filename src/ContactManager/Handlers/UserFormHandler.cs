using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContactManager;

/// <summary>
/// Handles entire data collection for contact manager
/// Contain input logic, validation, field and option selectors
/// </summary>
internal class UserFormHandler
{
    /// <summary>
    /// Call the get function according to its field.
    /// </summary>
    /// <param name="selectedField">Field name.</param>
    /// <param name="userContactList">Give access to user contact list.</param>
    /// <returns>return the user input</returns>
    public static string? GetFieldValue(string? selectedField, ContactRepository userContactList)
    {
        return selectedField switch
        {
            "Name" => GetName(userContactList),
            "Phone" => GetPhone(userContactList),
            "Email" => GetEmail(userContactList),
            "Notes" => GetNotes(),
            _ => GetWitOutValidation(selectedField),
        };
    }

    /// <summary>
    /// Get the index from user until a valid input and return
    /// check for integer
    /// Check with the range limits of user contact list
    /// Future validation can be added with an additional if else
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    /// <returns>valid index as int</returns>
    public static int GetValidContactIndex(ContactRepository userContactList)
    {
        int index = -1;
        while (true)
        {
            string? indexString = ConsoleUI.GetInputWithPrompt("\nEnter the index of contact : ");
            if (!int.TryParse(indexString, out index))
            {
                ConsoleUI.PromptInfoWithColor("Index must be a number !", ConsoleColor.Yellow);
                continue;
            }
            else if (!userContactList.IsValidIndex(index - 1))
            {
                ConsoleUI.PromptInfoWithColor($"Contact with {index} does not exists !", ConsoleColor.Yellow);
                continue;
            }
            else
            {
                return index;
            }
        }
    }

    /// <summary>
    /// Get the index from user until a valid input and return
    /// current field are : name, email, phone, notes
    /// can add extra fields if needed
    /// </summary>
    /// <param name="fields">
    /// Available fields
    /// </param>
    /// <returns>valid field as string</returns>
    public static string GetField(string[] fields)
    {
        while (true)
        {
            string? fieldsPrompt = null;
            for (int i = 0; i < fields.Length; i++)
            {
                fieldsPrompt += $"\n{i + 1}. {fields[i]} ";
            }

            fieldsPrompt += "\nEnter field : ";
            string? option = ConsoleUI.GetInputWithPrompt(fieldsPrompt);
            string? optionName = null;
            if (int.TryParse(option, out int choice) && (choice - 1 < fields.Length && choice - 1 >= 0))
            {
                optionName = fields[choice - 1];
                return optionName;
            }
            else
            {
                ConsoleUI.PromptInfoWithColor("Choose a valid option.", ConsoleColor.Yellow);
            }
        }
    }

    /// <summary>
    /// List all the fields and prompt the user to choose a field to sort
    /// </summary>
    /// <param name="fields">
    /// Available fields
    /// </param>
    /// <returns>valid sort option as string</returns>
    public static string GetSortOption(string[] fields)
    {
        while (true)
        {
            string fieldsPrompt = string.Empty;
            string[] availableSortingOptions = fields.Concat(new[] { "Time" }).ToArray();
            for (int i = 0; i < availableSortingOptions.Length; i++)
            {
                fieldsPrompt += $"\n{i + 1}. {availableSortingOptions[i]} ";
            }

            fieldsPrompt += "\nSort by : ";
            string? optionName = null;
            string? option = ConsoleUI.GetInputWithPrompt(fieldsPrompt);
            if (int.TryParse(option, out int choice) && (choice - 1 < availableSortingOptions.Length && choice - 1 >= 0))
            {
                optionName = availableSortingOptions[choice - 1];
                return optionName;
            }
            else
            {
                ConsoleUI.PromptInfoWithColor("Choose a valid option.", ConsoleColor.Yellow);
            }
        }
    }

    /// <summary>
    /// Get the name from user until a valid input and return
    /// Check for duplicates from user contact list
    /// Future validation can be added with an additional if else
    /// </summary>
    /// <param name="userContactList">
    /// Gives access to users contact list
    /// </param>
    /// <returns>valid name in string</returns>
    private static string? GetName(ContactRepository userContactList)
    {
        string? name;
        name = ConsoleUI.GetInputWithPrompt("Name : ");
        return name;
    }

    /// <summary>
    /// Get the phone from user until a valid input and return,
    /// Check whether it is a number,
    /// Check for 10 digits,
    /// Future validation can be added with an additional if else.
    /// </summary>
    /// <returns>valid phone as string</returns>
    private static string GetPhone(ContactRepository userContactList)
    {
        string? phone;
        string pattern = @"^\d{10}$";
        do
        {
            phone = ConsoleUI.GetInputWithPrompt("Phone :") ?? " ";
            if (!Regex.IsMatch(phone, pattern))
            {
                ConsoleUI.PromptInfoWithColor("Enter a valid phone !", ConsoleColor.Yellow);
                continue;
            }
            else if (userContactList.HaveDuplicate("Phone", phone))
            {
                ConsoleUI.PromptInfoWithColor("Contact with this phone already exists !", ConsoleColor.Yellow);
                continue;
            }
            else
            {
                break;
            }
        }
        while (true);

        return phone;
    }

    /// <summary>
    /// Get the Email from user until a valid input and return
    /// uses inbuilt email validator from <see cref="EmailAddressAttribute"/>
    /// Future validation can be added with an additional if else
    /// </summary>
    /// <returns>valid email as string</returns>
    private static string? GetEmail(ContactRepository userContactList)
    {
        string? email;
        EmailAddressAttribute emailValidator = new EmailAddressAttribute();
        while (true)
        {
            email = ConsoleUI.GetInputWithPrompt("Email ID : ");
            if (!emailValidator.IsValid(email))
            {
                ConsoleUI.PromptInfoWithColor("Invalid Email format !", ConsoleColor.Yellow);
                continue;
            }
            else if (userContactList.HaveDuplicate("Email", email))
            {
                ConsoleUI.PromptInfoWithColor("Contact with this email already exists !", ConsoleColor.Yellow);
            }
            else
            {
                return email;
            }
        }
    }

    /// <summary>
    /// Get the notes from user until a valid input and return
    /// Currently have no validation as it is a optional one
    /// Future validation can be added with an additional if else
    /// </summary>
    /// <returns>valid notes as string</returns>
    private static string? GetNotes()
    {
        string? notes;
        notes = ConsoleUI.GetInputWithPrompt("Notes : ");
        return notes;
    }

    /// <summary>
    /// Handles input for unhandled new fields
    /// Prevent app crashing when a new field is added and for unhandled fields
    /// </summary>
    /// <param name="newField">
    /// Unhandled or new field.
    /// </param>
    /// <returns>valid notes as string</returns>
    private static string? GetWitOutValidation(string? newField)
    {
        string? value;
        value = ConsoleUI.GetInputWithPrompt($"{newField} : ");
        return value;
    }
}
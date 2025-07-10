using System.ComponentModel.DataAnnotations;

namespace Contact_Manager.ConsoleUI
{
    /// <summary>
    /// These handle direct user input and repetitive tasks
    /// </summary>
    internal class Utilities
    {
        /// <summary>
        /// Get the name from user until a valid input and return
        /// Check for duplicates from user contact list
        /// Future validation can be added with an additional if else
        /// </summary>
        /// <param name="userContactList">
        /// Gives access to users contact list
        /// </param>
        /// <returns>valid name in string</returns>
        public static string GetName(ContactListData userContactList)
        {
            string name;
            while (true)
            {
                Console.Write("Name : ");
                name = Console.ReadLine();
                if (userContactList.ExistWithName(name))
                {
                    Console.WriteLine("No duplicates");
                    continue;
                }
                else
                {
                    break;
                }
            }

            return name;
        }

        /// <summary>
        /// Get the phone from user until a valid input and return
        /// check whether it is a number
        /// Check for 10 digits
        /// Future validation can be added with an additional if else
        /// </summary>
        /// <returns>valid phone as string</returns>
        public static string GetPhone()
        {
            string phone;
            while (true)
            {
                Console.Write("Phone : ");
                phone = Console.ReadLine();
                if (!long.TryParse(phone, out long number))
                {
                    Console.WriteLine("Phone must be numbers !");
                    continue;
                }
                else if (phone.Length != 10)
                {
                    Console.WriteLine("Must contain 10 numbers !");
                    continue;
                }
                else
                {
                    break;
                }
            }

            return phone;
        }

        /// <summary>
        /// Get the Email from user until a valid input and return
        /// uses inbuilt email validator from 'EmailAddressAttribute'
        /// Future validation can be added with an additional if else
        /// </summary>
        /// <returns>valid email as string</returns>
        public static string GetEmail()
        {
            string email;
            var emailValidator = new EmailAddressAttribute();
            while (true)
            {
                Console.Write("Email Id :");
                email = Console.ReadLine();
                if (!emailValidator.IsValid(email))
                {
                    Console.WriteLine("Invalid email !");
                    continue;
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
        public static string GetNotes()
        {
            string notes;
            Console.Write("Notes : ");
            notes = Console.ReadLine();
            return notes;
        }

        /// <summary>
        /// Get the index from user until a valid input and return
        /// check for integer
        /// Check with the range limits of user contact list
        /// Future validation can be added with anadditional if else
        /// </summary>
        /// <param name="userContactList">
        /// Gives access to users contact list
        /// </param>
        /// <returns>valid index as int</returns>
        public static int GetIndex(ContactListData userContactList)
        {
            int index = -1;
            while (true)
            {
                Console.Write("\nEnter the index of contact : ");
                string indexString = Console.ReadLine();
                if (!int.TryParse(indexString, out index))
                {
                    Console.WriteLine("Must be a number !");
                    continue;
                }
                else if (!userContactList.IsValidIndex(index))
                {
                    Console.WriteLine("Invalid syntax !");
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
        /// <returns>valid field as string</returns>
        public static string GetField()
        {
            string optionName = null, option;
            while (true)
            {
                Console.WriteLine("1. Name  2. Email  3. Phone  4. Notes");
                option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3" || option == "4")
                {
                    optionName = option switch
                    {
                        "1" => "name",
                        "2" => "email",
                        "3" => "phone",
                        "4" => "notes",
                        _ => string.Empty
                    };
                    return optionName;
                }
                else
                {
                    Console.WriteLine("Enter a valid field option !");
                }
            }
        }

        /// <summary>
        /// Get the index from user until a valid input and return
        /// current sort options are : name, time
        /// can add extra sort option if needed
        /// </summary>
        /// <returns>valid sort option as string</returns>
        public static string GetSortOption()
        {
            while (true)
            {
                Console.WriteLine("\n1. Sort by name  2. Sort by time  3. Exit");
                string option = Console.ReadLine();
                if (option == "1" || option == "2" || option == "3" || option == "4")
                {
                    string optionName = option switch
                    {
                        "1" => "name",
                        "2" => "time",
                        _ => "exit"
                    };
                    return optionName;
                }
                else
                {
                    Console.WriteLine("Not valid option !");
                    continue;
                }
            }
        }

        /// <summary>
        /// Prints the heading at each page
        /// </summary>
        public static void PrintHeader()
        {
            Console.Clear();
            Console.WriteLine("Contact Manager\n");
            return;
        }

        /// <summary>
        /// Handles exits after every successful operations
        /// </summary>
        public static void HandleExit()
        {
            Console.WriteLine("\n\n\nPress any key to exit.....");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

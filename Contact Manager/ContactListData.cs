using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager
{
    /// <summary>
    /// Create and manages the main list of contactsData and provides core operations
    /// </summary>
    internal class ContactListData
    {
        /// <summary>
        /// List of custom ontact data type
        /// </summary>
        private List<ContactData> _contactList = new List<ContactData>();

        /// <summary>
        /// Add a contact
        /// </summary>
        /// <param name="name">Name to added</param>
        /// <param name="email">Email to added</param>
        /// <param name="phone">Phone to added</param>
        /// <param name="notes">Notes to added</param>
        /// <returns>return the new Contact</returns>
        public ContactData AddContact(string name, string email, string phone, string notes)
        {
            ContactData newContact = new ContactData(name, email, phone, notes);
            this._contactList.Add(newContact);
            return newContact;
        }

        /// <summary>
        /// Return the entire contact list
        /// </summary>
        /// <returns>return entire list</returns>
        public List<ContactData> GetContacts()
        {
            return this._contactList;
        }

        /// <summary>
        /// Print all contacts of the recieved list in console
        /// </summary>
        /// <param name="listToShow">List to show</param>
        public void ShowContacts(List<ContactData> listToShow)
        {
            Console.WriteLine(string.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-20}|", "S. no", "Name", "Email ID", "Phone", "Additional Notes"));
            Console.WriteLine(new string('-', 76));
            for (int i = 0; i < listToShow.Count; i++)
            {
                Console.WriteLine(string.Format("|{0,-5}|{1,-15}|{2,-15}|{3,-15}|{4,-20}|", i + 1, listToShow[i].Name, listToShow[i].Email, listToShow[i].Phone, listToShow[i].Notes));
            }
        }

        /// <summary>
        /// Print the details of single contact
        /// </summary>
        /// <param name="contactToShow">Contact to show</param>
        public void ShowContact(ContactData contactToShow)
        {
            Console.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-20}|", "Name", "Email ID", "Phone", "Additional Notes"));
            Console.WriteLine(string.Format("|{0,-15}|{1,-15}|{2,-15}|{3,-20}|", contactToShow.Name, contactToShow.Email, contactToShow.Phone, contactToShow.Notes));
        }

        /// <summary>
        /// Print the details of contact in specific index at list
        /// </summary>
        /// <param name="index">index of contact to show</param>
        /// <returns>bool of validation of index</returns>
        public bool ShowContactByIndex(int index)
        {
            if (index < 0 || index >= this._contactList.Count)
            {
                return false;
            }

            this.ShowContact(this._contactList[index]);
            return true;
        }

        /// <summary>
        /// Edit contact in the list with its index
        /// use field to get the type of field
        /// </summary>
        /// <param name="index">index of contact to be edited</param>
        /// <param name="key">changes</param>
        /// <param name="field">field of contact to edit</param>
        /// <returns>bool of validation of index</returns>
        public bool EditContact(int index, string key, string field)
        {
            if (index >= this._contactList.Count || index < 0)
            {
                return false;
            }

            switch (field)
            {
                case "name":
                    this._contactList[index].Name = key;
                    return true;
                case "email":
                    this._contactList[index].Email = key;
                    return true;
                case "phone":
                    this._contactList[index].Phone = key;
                    return true;
                case "notes":
                    this._contactList[index].Notes = key;
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Delete contact at index
        /// </summary>
        /// <param name="index">index of contact to delete</param>
        /// <returns>success status</returns>
        public bool DeleteContacts(int index)
        {
            if (index >= this._contactList.Count || index < 0)
            {
                return false;
            }

            this._contactList.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// search with the keyword in specific field
        /// </summary>
        /// <param name="keyword">Keyword to find matches</param>
        /// <param name="field">field to search for</param>
        /// <returns>Filtered list</returns>
        public List<ContactData> Search(string keyword, string field)
        {
            Console.WriteLine();
            List<ContactData> filtered;
            switch (field)
            {
                case "name":
                    filtered = this._contactList.FindAll(a => a.Name.ToUpper().Contains(keyword.ToUpper()));
                    break;
                case "email":
                    filtered = this._contactList.FindAll(a => a.Email.ToUpper().Contains(keyword.ToUpper()));
                    break;
                case "phone":
                    filtered = this._contactList.FindAll(a => a.Phone.ToUpper().Contains(keyword.ToUpper()));
                    break;
                case "notes":
                    filtered = this._contactList.FindAll(a => a.Phone.ToUpper().Contains(keyword.ToUpper()));
                    break;
                default:
                    filtered = this._contactList.FindAll(a => a.Name.ToUpper().Contains(keyword.ToUpper()));
                    break;
            }

            return filtered;
        }

        /// <summary>
        /// Sort contacts by option
        /// Current options : name, time
        /// </summary>
        /// <param name="field">field to sort</param>
        public void SortContacts(string field)
        {
            switch (field)
            {
                case "name":
                    this._contactList.Sort((a, b) => a.Name.CompareTo(b.Name));
                    break;
                case "time":
                    Console.WriteLine("yoyo");
                    this._contactList.Sort((a, b) => a.CreatedAt.CompareTo(b.CreatedAt));
                    break;
            }
        }

        /// <summary>
        /// check whether there is a name for it
        /// </summary>
        /// <param name="name">name to check</param>
        /// <returns>returns true if exists</returns>
        public bool ExistWithName(string name)
        {
            if (this._contactList.Exists(a => a.Name == name))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// To handle out of range exceptions
        /// </summary>
        /// <param name="index">input index to check</param>
        /// <returns>true if it is in range</returns>
        public bool IsValidIndex(int index)
        {
            if (index >= 0 && index <= this._contactList.Count)
            {
                return true;
            }

            return false;
        }
    }
}

namespace ContactManager.Repository
{
    /// <summary>
    /// Create and manages the main list of contacts data and handles core operations
    /// </summary>
    internal class ContactRepository
    {
        private readonly string[] _fields = { "Name", "Phone", "Email", "Notes" };
        private List<Contact> _contactList = new List<Contact>();

        /// <summary>
        /// Holds the template of the contact.
        /// </summary>
        /// <returns>Template of contact as a dictionary.</returns>
        public static Dictionary<string, string> GetContactTemplate()
        {
            return Contact.GetContactTemplate();
        }

        /// <summary>
        /// Adds a new contact object to the list and holds the new contact.
        /// </summary>
        /// <param name="contactDetails">Dictionary of contact data.</param>
        /// <returns>Returns the contact that has been added.</returns>
        public Contact AddContact(Dictionary<string, string> contactDetails)
        {
            Contact newContact = new Contact(contactDetails);
            this._contactList.Add(newContact);
            return newContact;
        }

        /// <summary>
        /// Holds the entire contact list.
        /// </summary>
        /// <returns>Contacts as list.</returns>
        public List<Contact> GetContacts()
        {
            return this._contactList;
        }

        /// <summary>
        /// Holds the contact object of specified index.
        /// </summary>
        /// <param name="index">The index of the required contact in list.</param>
        /// <returns>Contact object in specified index.</returns>
        public Contact GetContact(int index)
        {
            return this._contactList[index];
        }

        /// <summary>.
        /// Updates contact based on index, field and new value to change.
        /// </summary>
        /// <param name="index">index of contact to be edited</param>
        /// <param name="field">field of contact to edit</param>
        /// <param name="newValue">changes</param>
        public void EditContact(int index, string field, string newValue)
        {
            if (index >= this._contactList.Count || index < 0)
            {
                return;
            }

            this._contactList[index][field] = newValue;
        }

        /// <summary>
        /// Removes selected contact from list at specified index.
        /// </summary>
        /// <param name="index">Index of contact to delete.</param>
        public void DeleteContacts(int index)
        {
            if (index >= this._contactList.Count || index < 0)
            {
                return;
            }

            this._contactList.RemoveAt(index);
        }

        /// <summary>
        /// Searches contacts by field with keyword, Holds the matched results as list.
        /// </summary>
        /// <param name="field">field to search for.</param>
        /// <param name="keyword">Keyword to find matches.</param>
        /// <returns>Filtered list</returns>
        public List<Contact> Search(string field, string keyword)
        {
            List<Contact> filtered = this._contactList.FindAll(contact => contact[field].ToUpper().Contains(keyword.ToUpper()));
            return filtered;
        }

        /// <summary>
        /// Sorts the contact list by field(name, time).
        /// </summary>
        /// <param name="field">Field to sort.</param>
        public void SortContacts(string field)
        {
            this._contactList.Sort((contactA, contactB) => contactA[field].CompareTo(contactB[field]));
        }

        /// <summary>
        /// Checks for duplicate on any specified field and return true if duplicate exist else return false.
        /// </summary>
        /// <param name="field">Field to check for in contact list.</param>
        /// <param name="value">Value to check with existing values.</param>
        /// <returns>Return true if there is duplicate and false if there is no duplicates.</returns>
        public bool HaveDuplicate(string field, string value)
        {
            if (this._contactList.Exists(contact => contact[field] == value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// To handle out of range exceptions, check whether the contact exist in a specified index.
        /// </summary>
        /// <param name="index">The index value to check with.</param>
        /// <returns>return true if it is in range and false if index it out of range.</returns>
        public bool IsValidIndex(int index)
        {
            if (index >= 0 && index < this._contactList.Count)
            {
                return true;
            }

            return false;
        }
    }
}
namespace ContactManager.Model
{
    /// <summary>
    /// This class represents the data of an individual contact
    /// </summary>
    internal class Contact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class.
        /// Constructor to set the inital data.
        /// </summary>
        /// <param name="contactDetails">Is a dictionary of contact data.</param>
        public Contact(Dictionary<string, string> contactDetails)
        {
            this.ContactInfo = GetContactTemplate();
            foreach (var field in contactDetails)
            {
                this.ContactInfo[field.Key] = field.Value;
            }

            this.ContactInfo["Time"] = DateTime.Now.ToString();
        }

        private Dictionary<string, string> ContactInfo { get; set; } = new ();

        /// <summary>
        /// Its an indexer that gets the value with getting the key(field) for easy access of data with fields.
        /// </summary>
        /// <param name="key">Field name to get</param>
        /// <returns>Value of the field.</returns>
        public string? this[string key]
        {
            get => this.ContactInfo.ContainsKey(key) ? this.ContactInfo[key] : null;
            set => this.ContactInfo[key] = value!;
        }

        /// <summary>
        /// Gets a template of the contact data.
        /// </summary>
        /// <returns>Contact template as dictionary.</returns>
        public static Dictionary<string, string> GetContactTemplate()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Name", string.Empty },
                    { "Phone", string.Empty },
                    { "Email", string.Empty },
                    { "Notes", string.Empty },
                    { "Social ID", string.Empty },
                };
        }
    }
}
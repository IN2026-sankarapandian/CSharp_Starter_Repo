namespace ContactManager;

/// <summary>
/// This class represents the data of an individual contact
/// </summary>
internal class Contact
{
    private Dictionary<string, string> _contactInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="Contact"/> class.
    /// Constructor to set the initial data.
    /// </summary>
    /// <param name="contactDetails">Is a dictionary of contact data.</param>
    public Contact(Dictionary<string, string> contactDetails)
    {
        this._contactInfo = GetContactTemplate();
        foreach (var field in contactDetails)
        {
            if (this._contactInfo[field.Key] != null)
            {
                this._contactInfo[field.Key] = field.Value;
            }
        }

        this._contactInfo["Time"] = DateTime.Now.ToString();
    }

    /// <summary>
    /// Its an indexer that gets the value with getting the key(field) for easy access of data with fields.
    /// </summary>
    /// <param name="key">Field name to get</param>
    /// <returns>Value of the field.</returns>
    public string? this[string key]
    {
        get => this._contactInfo.ContainsKey(key) ? this._contactInfo[key] : null;
        set => this._contactInfo[key] = value!;
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
            };
    }
}
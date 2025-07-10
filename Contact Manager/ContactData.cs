using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager
{
    /// <summary>
    /// Have the name, phone, email and info of single contact
    /// </summary>
    internal class ContactData
    {
        /// <summary>
        /// private variable of name, email, phone, notes
        /// </summary>]
        /// <param name="_name"">private variable contains contact name</param>
        /// <param name="_email"">private variable contains contact email</param>
        /// <param name="_phone"">private variable contains contact phone</param>
        /// <param name="_notes"">private variable contains contact notes</param>
        private string _name;
        private string _email;
        private string _phone;
        private string _notes;
        private DateTime _createdAt = DateTime.Now;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactData"/> class.
        /// Constructor to set the inital data.
        /// </summary>
        /// <param name="name">name of contact</param>
        /// <param name="email">email of contact</param>
        /// <param name="phone">phone of contact</param>
        /// <param name="notes">nots of contact</param>
        public ContactData(string name, string email, string phone, string notes)
        {
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.Notes = notes;
        }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        /// <value>
        /// name
        /// </value>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        /// <value>
        /// email
        /// </value>
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        /// <summary>
        /// Gets or sets phone
        /// </summary>
        /// <value>
        /// phone
        /// </value>
        public string Phone
        {
            get { return this._phone; }
            set { this._phone = value; }
        }

        /// <summary>
        /// Gets or sets notes
        /// </summary>
        /// <value>
        /// notes
        /// </value>
        public string Notes
        {
            get { return this._notes; }
            set { this._notes = value; }
        }

        /// <summary>
        /// Gets Createdat
        /// </summary>
        /// <value>
        /// date time
        /// </value>
        public DateTime CreatedAt
        {
            get { return this._createdAt; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager
{
    internal class ContactData
    {
        private string _name;
        private string _email;
        private string _phone;
        private string _notes;
        DateTime _createdAt = DateTime.Now;

        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Notes { get { return _notes; } set { _notes = value; } }
        public DateTime CreatedAt { get { return _createdAt; } }

        public ContactData(string name, string email, string phone, string notes)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Notes = notes;
        }
    }
}

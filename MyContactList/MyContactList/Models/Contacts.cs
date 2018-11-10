using System;
using System.Collections.Generic;
using System.Text;

namespace MyContactList.Models
{
    public class Contacts
    {

       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactType { get; set; }
        public string NameSort => LastName[0].ToString();

        public string FirstLastName { get { return FirstName + " " + LastName; } }

    }
}

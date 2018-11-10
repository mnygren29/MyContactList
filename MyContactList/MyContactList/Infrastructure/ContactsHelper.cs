using MyContactList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace MyContactList.Infrastructure
{
    public static class ContactsHelper
    {

        private static Random random;

        public static Contacts GetRandomContact()
        {
       
            return Contacts_[random.Next(0, Contacts_.Count)];
        }

        public static ObservableCollection<Grouping<string, Contacts>> ContactsGrouped_ { get; set; }

        public static ObservableCollection<Contacts> Contacts_ { get; set; }

       
        static ContactsHelper()
        {
            random = new Random();
            Contacts_ = new ObservableCollection<Contacts>();
            Contacts_.Add(new Contacts
            {
                //id="1",
                FirstName = "Matt",
                LastName = "Damon",
                ContactType = "FirstName"
                
            });

            Contacts_.Add(new Contacts
            {
                // id = "2",
                FirstName = "Jane",
                LastName = "Hathaway",
                ContactType = "FirstName"

            });

            Contacts_.Add(new Contacts
            {
                // id = "3",
                FirstName = "Alex",
                LastName = "Trabec",
                ContactType = "FirstName"

            });


            Contacts_.Add(new Contacts
            {
                //  id = "4",
                FirstName = "John",
                LastName = "Wayne",
                ContactType = "LastName"

            });

            Contacts_.Add(new Contacts
            {
                //id = "5",
                FirstName = "Sally",
                LastName = "Fields",
                ContactType = "LastName"

            });

            Contacts_.Add(new Contacts
            {
                // id = "6",
                FirstName = "Mark",
                LastName = "Burgess",
                ContactType = "LastName"

            });

            Contacts_.Add(new Contacts
            {
                //id = "7",
                FirstName = "Albert",
                LastName = "Enstein",
                ContactType = "FirstName"

            });
            

            var sorted = from contact in Contacts_
                         orderby contact.FirstName
                         group contact by contact.NameSort into borrowerGroup
                         select new Grouping<string, Contacts>(borrowerGroup.Key, borrowerGroup);

            ContactsGrouped_ = new ObservableCollection<Grouping<string, Contacts>>(sorted);

        }
    }
}

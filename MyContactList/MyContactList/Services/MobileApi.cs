using MyContactList.Infrastructure;
using MyContactList.Interfaces;
using MyContactList.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MyContactList.Services
{
    public class MobileApi : IMobileApi
    {
        private Random random;
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public Contacts GetRandomContact()
        {
            //var output = Newtonsoft.Json.JsonConvert.SerializeObject(Monkeys);
            return Contactsm[random.Next(0, Contactsm.Count)];
        }


        public ObservableCollection<Grouping<string, Contacts>> ContacsGroupedm { get; set; }
        public List<Contacts> Contactsm { get; set; }
        public List<Grouping<string, Contacts>> ContactsGroupedList { get; set; }

        private IList<ContactsDb> _listDbContacts;
        public IList<ContactsDb> ListDbContacts
        {
            get
            {
                return _listDbContacts;
            }
            set
            {
                _listDbContacts = value;
               // RaisePropertyChanged();
            }
        }


        public ObservableCollection<Grouping<string, Contacts>> GetContactsGrouped()
        {
            random = new Random();
            Contactsm = new List<Contacts>();
            Contactsm.Add(new Contacts
            {
               
                FirstName = "John",
                LastName = "Doe",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Jane",
                LastName = "Hathaway",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Alex",
                LastName = "Ray",
                ContactType = "LastName"

            });


            Contactsm.Add(new Contacts
            {

                FirstName = "John",
                LastName = "Wayne",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Matt",
                LastName = "Hershey",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Donald",
                LastName = "Sutherland",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Barney",
                LastName = "Rubble",
                ContactType = "FirstName"

            });


            var sorted = from contact in Contactsm
                         orderby contact.LastName
                         group contact by contact.NameSort into contactGroup
                         select new Grouping<string, Contacts>(contactGroup.Key, contactGroup);

            return ContacsGroupedm = new ObservableCollection<Grouping<string, Contacts>>(sorted);

        }

        public ObservableCollection<Grouping<string, Contacts>> GetContactsGrouped(string FilterType)
        {
            random = new Random();
            Contactsm = new List<Contacts>();
            Contactsm.Add(new Contacts
            {

                FirstName = "John",
                LastName = "Doe",
                ContactType = "First Name"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Jane",
                LastName = "Hathaway",
                ContactType = "First Name"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Alex",
                LastName = "Ray",
                ContactType = "Last Name"

            });


            Contactsm.Add(new Contacts
            {

                FirstName = "John",
                LastName = "Wayne",
                ContactType = "First Name"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Matt",
                LastName = "Hershey",
                ContactType = "Last Name"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Donald",
                LastName = "Sutherland",
                ContactType = "Last Name"

            });

            Contactsm.Add(new Contacts
            {

                FirstName = "Barney",
                LastName = "Rubble",
                ContactType = "First Name"

            });


            var sorted = from contact in Contactsm
                         orderby contact.ContactType == FilterType
                         group contact by contact.NameSort into contactGroup
                         select new Grouping<string, Contacts>(contactGroup.Key, contactGroup);

            return ContacsGroupedm = new ObservableCollection<Grouping<string, Contacts>>(sorted);

        }


        public List<Contacts> GetContactsm(string FilterType)
        {

            random = new Random();
            Contactsm = new List<Contacts>();
            Contactsm.Add(new Contacts
            {
                //id="1",
                FirstName = "Matt",
                LastName = "Damon",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {
                // id = "2",
                FirstName = "Jane",
                LastName = "Hathaway",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {
                // id = "3",
                FirstName = "Alex",
                LastName = "Trabec",
                ContactType = "FirstName"

            });


            Contactsm.Add(new Contacts
            {
                //  id = "4",
                FirstName = "John",
                LastName = "Wayne",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {
                //id = "5",
                FirstName = "Sally",
                LastName = "Fields",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {
                // id = "6",
                FirstName = "Mark",
                LastName = "Burgess",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {
                //id = "7",
                FirstName = "Albert",
                LastName = "Enstein",
                ContactType = "FirstName"

            });


            return Contactsm.Where(r => r.ContactType == FilterType).ToList();
        }

        public List<Contacts> GetContactsm()
        {

            random = new Random();
            Contactsm = new List<Contacts>();
            Contactsm.Add(new Contacts
            {
                //id="1",
                FirstName = "Matt",
                LastName = "Damon",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {
                // id = "2",
                FirstName = "Jane",
                LastName = "Hathaway",
                ContactType = "FirstName"

            });

            Contactsm.Add(new Contacts
            {
                // id = "3",
                FirstName = "Alex",
                LastName = "Trabec",
                ContactType = "FirstName"

            });


            Contactsm.Add(new Contacts
            {
                //  id = "4",
                FirstName = "John",
                LastName = "Wayne",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {
                //id = "5",
                FirstName = "Sally",
                LastName = "Fields",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {
                // id = "6",
                FirstName = "Mark",
                LastName = "Burgess",
                ContactType = "LastName"

            });

            Contactsm.Add(new Contacts
            {
                //id = "7",
                FirstName = "Albert",
                LastName = "Enstein",
                ContactType = "FirstName"

            });


            return Contactsm;
        }


        public IList<ContactsDb> GetAllContactsDb()
        {


            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()
                                //where cust.Country == countryName
                            select contact;
                _listDbContacts = query.ToList();
              //  RaisePropertyChanged("ListDbContacts");
            }

            return _listDbContacts;
        }


        public IList<Contacts> GetContacts()
        {

            IList<Contacts> contactList = new List<Contacts>() {
                new Contacts(){ FirstName="Joe", LastName="z", EmailAddress="Anaheim@fg.com",ContactType="Email"},
                new Contacts(){ FirstName="Mary", LastName="y",EmailAddress="df@sdf.com",ContactType="Text"},
                new Contacts(){ FirstName="John", LastName="d" ,EmailAddress="Orange@rt.com",ContactType="Mobile"},

            };

            return contactList;
        }

        public IList<Contacts> GetContacts(string FilterType)
        {
            IList<Contacts> contactList = new List<Contacts>() {
              new Contacts(){ FirstName="Joe", LastName="z", EmailAddress="Anaheim@fg.com",ContactType="Email"},
                new Contacts(){ FirstName="Mary", LastName="y",EmailAddress="df@sdf.com",ContactType="Text"},
                new Contacts(){ FirstName="John", LastName="d" ,EmailAddress="Orange@rt.com",ContactType="Mobile"},

            };

            var theList = contactList.Where(r => r.ContactType == FilterType).ToList();

            return theList.ToList();
        }

       // List<Contacts> IMobileApi.GetContactsm()
        //{
        //    throw new NotImplementedException();
       // }
    }
}

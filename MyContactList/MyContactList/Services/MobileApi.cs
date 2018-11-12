using MyContactList.Infrastructure;
using MyContactList.Interfaces;
using MyContactList.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyContactList.Services
{
    public class MobileApi : IMobileApi
    {
        private Random random;
        private SQLiteConnection database;
        private static object collisionLock = new object();

        //public Contacts GetRandomContact()
        //{
        //    //var output = Newtonsoft.Json.JsonConvert.SerializeObject(Monkeys);
        //    return Contactsm[random.Next(0, Contactsm.Count)];
        //}

        public ContactsDb GetRandomContact()
        {
            //var output = Newtonsoft.Json.JsonConvert.SerializeObject(Monkeys);
            return Contactsm[random.Next(0, Contactsm.Count)];
        }


     
        public ObservableCollection<Grouping<string, ContactsDb>> ContacsGroupedm { get; set; }
        public List<ContactsDb> Contactsm { get; set; }
        public List<Grouping<string, ContactsDb>> ContactsGroupedList { get; set; }

        private List<ContactsDb> _listDbContacts;
        public List<ContactsDb> ListDbContacts
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
        public ObservableCollection<Grouping<string, ContactsDb>> GetContactsGroupedDb()
        {
            random = new Random();
            Contactsm = new List<ContactsDb>();

            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()

                            select contact;
                Contactsm = query.ToList();

            }

            var sorted = from contact in Contactsm
                         orderby contact.LastName
                         group contact by contact.NameSort into contactGroup
                         select new Grouping<string, ContactsDb>(contactGroup.Key, contactGroup);

            return ContacsGroupedm = new ObservableCollection<Grouping<string, ContactsDb>>(sorted);

        }

     

        public ObservableCollection<Grouping<string, ContactsDb>> GetContactsGroupedDb(string FilterType)
        {
            random = new Random();
            Contactsm = new List<ContactsDb>();

            database =
DependencyService.Get<IDatabaseConnection>().
DbConnection();

            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()

                            select contact;
                Contactsm = query.ToList();

            }


            var sorted = from contact in Contactsm
                         orderby contact.ContactType == FilterType
                         group contact by contact.NameSort into contactGroup
                         select new Grouping<string, ContactsDb>(contactGroup.Key, contactGroup);

            return ContacsGroupedm = new ObservableCollection<Grouping<string, ContactsDb>>(sorted);

        }

       

        public List<ContactsDb> GetContactsmDb(string FilterType)
        {
            database =
 DependencyService.Get<IDatabaseConnection>().
 DbConnection();
            random = new Random();
            Contactsm = new List<ContactsDb>();

            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()

                            select contact;
                Contactsm = query.ToList();

            }
            return Contactsm.Where(r => r.ContactType == FilterType).ToList();
        }

     

        public List<ContactsDb> GetContactsmDb()
        {

            database =
  DependencyService.Get<IDatabaseConnection>().
  DbConnection();
           
         
            random = new Random();
            Contactsm = new List<ContactsDb>();

            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()
                            select contact;
                _listDbContacts = query.ToList();
                
            }

            return _listDbContacts;
        }


        public IList<ContactsDb> GetContactsDb()
        {

            database =
 DependencyService.Get<IDatabaseConnection>().
 DbConnection();
            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()
                              
                            select contact;
                _listDbContacts = query.ToList();
            
            }

            return _listDbContacts;
        }


     
        public IList<ContactsDb> GetContactsDb(string FilterType)
        {
            database =
 DependencyService.Get<IDatabaseConnection>().
 DbConnection();


            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()

                            select contact;
                _listDbContacts = query.ToList();

            }

            var theList = _listDbContacts.Where(r => r.ContactType == FilterType).ToList();

            return theList.ToList();
        }

       
    }
}

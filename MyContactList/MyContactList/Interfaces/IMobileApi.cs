using MyContactList.Infrastructure;
using MyContactList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyContactList.Interfaces
{
    public interface IMobileApi
    {

        //List<Contacts> GetContactsm();
        //List<Contacts> GetContactsm(string FilterType);
        //IList<Contacts> GetContacts();
        //IList<Contacts> GetContacts(string FilterType);

        List<ContactsDb> GetContactsmDb();
        List<ContactsDb> GetContactsmDb(string FilterType);
        IList<ContactsDb> GetContactsDb();
        IList<ContactsDb> GetContactsDb(string FilterType);
       // IList<ContactsDb> GetAllContactsDb();

        //ObservableCollection<Grouping<string, Contacts>> GetContactsGrouped();
        //ObservableCollection<Grouping<string, Contacts>> GetContactsGrouped(string FilterType);

        ObservableCollection<Grouping<string, ContactsDb>> GetContactsGroupedDb();
        ObservableCollection<Grouping<string, ContactsDb>> GetContactsGroupedDb(string FilterType);
    }
}

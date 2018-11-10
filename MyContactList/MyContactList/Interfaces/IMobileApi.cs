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

        List<Contacts> GetContactsm();
        List<Contacts> GetContactsm(string FilterType);
        IList<Contacts> GetContacts();
        IList<Contacts> GetContacts(string FilterType);
        IList<ContactsDb> GetAllContactsDb();

        ObservableCollection<Grouping<string, Contacts>> GetContactsGrouped();
        ObservableCollection<Grouping<string, Contacts>> GetContactsGrouped(string FilterType);

    }
}

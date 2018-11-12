using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using MyContactList.Models;
using Prism.Navigation;
using Unity;
using MyContactList.Interfaces;
using Prism.Services;

namespace MyContactList.ViewModels
{
	public class AddNewContactViewModel : ViewModelBase
    {
        IPageDialogService _pageDialog;
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<ContactsDb> ContactsTable { get; set; }
        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;
        private IMobileApi _mobileApi;
        public DelegateCommand SubmitAddContact { get; set; }
        public DelegateCommand GetContacts { get; set; }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }
        private string _emailAddress;
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                RaisePropertyChanged();
            }
        }

        private string _contactType;
        public string ContactType
        {
            get
            {
                return _contactType;
            }
            set
            {
                _contactType = value;
                RaisePropertyChanged();
            }
        }

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
                RaisePropertyChanged();
            }
        }

        public AddNewContactViewModel(INavigationService navigationService, IMobileApi mobileApi, IUnityContainer unityContainer, IPageDialogService pageDialog)
            : base(navigationService)
        {
            this._navigationService = navigationService;
            this._unityContainer = unityContainer;

            Title = "Add Contact";

            _mobileApi = mobileApi;
            _pageDialog = pageDialog;

            database =
                Xamarin.Forms.DependencyService.Get<IDatabaseConnection>().
                DbConnection();

            database.CreateTable<ContactsDb>();
            this.ContactsTable =
            new ObservableCollection<ContactsDb>(database.Table<ContactsDb>());
           
            SubmitAddContact = new DelegateCommand(AddNewCustomer);
            GetContacts = new DelegateCommand(GetAllContacts);
        }

        public void showAlert(string title,string AlertMessage)
        {
            _pageDialog.DisplayAlertAsync(title, AlertMessage , "OK");
        }

        public void AddNewCustomer()
        {

            try
            {
                if (!database.Table<ContactsDb>().Any())
                {

                    this.ContactsTable.
                      Add(new ContactsDb
                      {
                          FirstName = _firstName,
                          LastName = _lastName,
                          EmailAddress = _emailAddress,
                          ContactType = _contactType

                      });

                    lock (collisionLock)
                    {
                        foreach (var contactInstance in this.ContactsTable)
                        {
                            if (contactInstance.Id != 0)
                            {
                                database.Update(contactInstance);
                                showAlert("Success","Record succesfully updated!");
                            }
                            else
                            {
                                database.Insert(contactInstance);
                                showAlert("Success", "Record succesfully added!");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                showAlert("Failed", "We're sorry we could not add the record at this time.");
            }
        }

        public void GetAllContacts()
        {
            lock (collisionLock)
            {
                var query = from contact in database.Table<ContactsDb>()
                            //where cust.Country == countryName
                            select contact;
                _listDbContacts= query.ToList();
                RaisePropertyChanged("ListDbContacts");
            }
        }

    }
}

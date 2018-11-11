using MyContactList.Interfaces;
using MyContactList.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyContactList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<ContactsDb> ContactsTable { get; set; }
        private SQLiteConnection database;
        private static object collisionLock = new object();


        private IMobileApi _mobileApi;
        private readonly INavigationService _navigationService;
        public DelegateCommand SubmitCommand { get; set; }

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




        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged();
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }

        public MainPageViewModel(INavigationService navigationService,IMobileApi mobileApi)
            : base(navigationService)
        {
           
            this._navigationService = navigationService;

            Title = "User Login";
            _mobileApi = mobileApi;
            SubmitCommand = new DelegateCommand(GoToTabPage);

            database =
    DependencyService.Get<IDatabaseConnection>().
    DbConnection();
            database.CreateTable<ContactsDb>();
            this.ContactsTable =
              new ObservableCollection<ContactsDb>(database.Table<ContactsDb>());

          //  RaisePropertyChanged("ContactsTable");

            AddNewCustomer();

        }


        private async void GoToTabPage()
        {
          
            await NavigationService.NavigateAsync("app:///NavigationPage/PrismBaseTabbedPage");
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
                          FirstName = "Matt",
                          LastName = "Nygren",
                          EmailAddress = "mattn@matt.com",
                          ContactType = "email"

                      });

                    lock (collisionLock)
                    {
                        foreach (var contactInstance in this.ContactsTable)
                        {
                            if (contactInstance.Id != 0)
                            {
                                database.Update(contactInstance);
                            }
                            else
                            {
                                database.Insert(contactInstance);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}

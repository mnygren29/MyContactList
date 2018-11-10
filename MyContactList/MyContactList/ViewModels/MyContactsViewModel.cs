using MyContactList.Infrastructure;
using MyContactList.Interfaces;
using MyContactList.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace MyContactList.ViewModels
{
	public class MyContactsViewModel : ViewModelBase
    {


        public ObservableCollection<Contacts> Contactsm { get; set; }
        public ObservableCollection<Grouping<string, Contacts>> ContactsGrouped { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;

        public DelegateCommand SearchCommand { get; set; }
      //  public ICommand GetContactsDetailsCommand => new Command<Contacts>(async (item) => await GetContactsDetailsCommandAsync(item));

        ObservableCollection<SortTypes> sortTypes = new ObservableCollection<SortTypes>
        {
             new SortTypes {SortNameType="First Name" },
            new SortTypes {SortNameType="Last Name" }
            //new SortTypes{SortNameType="Conventional"}
        };

        public ObservableCollection<SortTypes> SortTypes { get => sortTypes; }
        private async Task GetContactsDetailsCommandAsync(Contacts contact)
        {

            var navigationParamaters = new NavigationParameters();
          // navigationParamaters.Add("quotedetail", contact.EmailAddress);

            await _navigationService.NavigateAsync("ContactDetails", navigationParamaters);
        }
        SortTypes selectedType;
        public SortTypes SelectedType
        {

            get
            {

                if (selectedType.SortNameType == "Select Contact Type")
                {
                    
                   ContactsGrouped = _mobileApi.GetContactsGrouped();
                    RaisePropertyChanged("ContactsGrouped");
                }


                return selectedType;

            }

            set
            {
                SetProperty(ref selectedType, value);

                if (selectedType.SortNameType == "First Name" || selectedType.SortNameType == "Last Name")
                {

                   ContactsGrouped = _mobileApi.GetContactsGrouped(selectedType.SortNameType);
                    RaisePropertyChanged("ContactsGrouped");
                }


            }
        }

        private bool _isGroupListViewEnabled;
        public bool IsGroupListViewEnabled
        {
            get { return _isGroupListViewEnabled; }
            set
            {
                _isGroupListViewEnabled = value;
                RaisePropertyChanged();
            }
        }

        private bool _isListViewEnabled;
        public bool IsListViewEnabled
        {
            get { return _isListViewEnabled; }
            set
            {
                _isListViewEnabled = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Contacts> Contact
        {
            get { return _Items; }
            set
            {
                _Items = value;
                RaisePropertyChanged();
            }
        }


        private Contacts _selectedContact;
        public Contacts SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                if (_selectedContact != null)
                {
                  //  var navigationParameters = new NavigationParameters();
                   // navigationParameters.Add("borrowerdetail", _selectedContact);
                   // NavigationService.NavigateAsync("ContactDetails", navigationParameters);
                }
                RaisePropertyChanged();
            }
        }

       private IMobileApi _mobileApi;

       // public ICommand GetBorrowerDetailsCommand => new Command<Contacts>(async (item) => await GetBorrowerDetailsAsync(item));

        private ObservableCollection<Contacts> _Items;
        private ObservableCollection<Contacts> _ItemsFiltered;
        private ObservableCollection<Contacts> _ItemsUnfiltered;

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged();
            }
        }

        public MyContactsViewModel(INavigationService navigationService, IMobileApi mobileApi, IUnityContainer unityContainer)
            : base(navigationService)
        {
            _isGroupListViewEnabled = true;
            _isListViewEnabled = false;
            RaisePropertyChanged("IsGroupListViewEnabled");
            RaisePropertyChanged("IsListViewEnabled");

            Contact = new ObservableCollection<Contacts>();

            this._navigationService = navigationService;
            this._unityContainer = unityContainer;

            Title = "Contact Information";

            _mobileApi = mobileApi;
            SearchCommand = new Prism.Commands.DelegateCommand(PerformSearch);


            SelectedType = SortTypes[0];

           LoadContactsm();
            ContactsGrouped = _mobileApi.GetContactsGrouped();

           LoadContact();
            _navigationService = navigationService;

           
        }

        public void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(this._searchText))
            {
                ContactsGrouped = _mobileApi.GetContactsGrouped();
                RaisePropertyChanged("ContactsGrouped");
                Contact = _ItemsUnfiltered;
                _isGroupListViewEnabled = true;
                _isListViewEnabled = false;
                RaisePropertyChanged("IsGroupListViewEnabled");
                RaisePropertyChanged("IsListViewEnabled");
            }

            else
            {
                ContactsGrouped = _mobileApi.GetContactsGrouped();
                RaisePropertyChanged("ContactsGrouped");


                _ItemsFiltered = new ObservableCollection<Contacts>(_ItemsUnfiltered
                    .Where(i => (i is Contacts && (((Contacts)i)
                    .FirstLastName.ToLower()
                    .Contains(_searchText.ToLower())))));
                Contact = _ItemsFiltered;
                RaisePropertyChanged("ContactsGrouped");
                RaisePropertyChanged("Contactsm");
                _isGroupListViewEnabled = false;
                _isListViewEnabled = true;
                RaisePropertyChanged("IsGroupListViewEnabled");
                RaisePropertyChanged("IsListViewEnabled");
            }
        }

        public void LoadContactsm()
        {
            try
            {
               
                _Items.Clear();
                var contacts = _mobileApi.GetContactsm();
                foreach (var contact in contacts)
                {
                    _Items.Add(contact);
                }
            }
            catch(Exception ex)
            {

            }

        Contactsm = _Items;

        }

        public void LoadContactsm(string FilterType)
        {
            _Items.Clear();
            var contacts = _mobileApi.GetContactsm(FilterType);
            foreach (var contact in contacts)
            {
                _Items.Add(contact);
            }


            Contactsm = _Items;

        }

        public void LoadContacts(string FilterType)
        {
            _Items.Clear();
            var contacts = _mobileApi.GetContacts(FilterType);
            foreach (var borrower in contacts)
            {
                _Items.Add(borrower);
            }


            Contact = _Items;

        }

        public void LoadContact()
        {

            var contacts = _mobileApi.GetContacts();
            foreach (var contact in contacts)
            {
                _Items.Add(contact);
            }

            var contacts_search = Contact;

            _ItemsUnfiltered = new ObservableCollection<Contacts>(contacts_search);
            Contact = new ObservableCollection<Contacts>(contacts_search);

        }

       


        //private async Task GetBorrowerDetailsAsync(Borrower borrower)
        //{

        //    var navigationParamaters = new NavigationParameters();
        //    navigationParamaters.Add("quotedetail", borrower.id);

        //    await _navigationService.NavigateAsync("BorrowerDetails", navigationParamaters);
        //}
    }
}

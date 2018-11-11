using MyContactList.Infrastructure;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Xamarin.Forms;

namespace MyContactList.ViewModels
{
	public class MyContactsViewModel : ViewModelBase
    {      

        public ObservableCollection<ContactsDb> Contactsm { get; set; }
        public ObservableCollection<Grouping<string, ContactsDb>> ContactsGrouped { get; set; }

        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;

        public DelegateCommand SearchCommand { get; set; }
      //  public ICommand GetContactsDetailsCommand => new Command<Contacts>(async (item) => await GetContactsDetailsCommandAsync(item));

        ObservableCollection<SortTypes> sortTypes = new ObservableCollection<SortTypes>
        {
             new SortTypes {SortNameType="First Name" },
            new SortTypes {SortNameType="Last Name" }
        
        };

        public ObservableCollection<SortTypes> SortTypes { get => sortTypes; }
        private async Task GetContactsDetailsCommandAsync(ContactsDb contact)
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
                    
                   ContactsGrouped = _mobileApi.GetContactsGroupedDb();
                    RaisePropertyChanged("ContactsGrouped");
                }
                return selectedType;
            }

            set
            {
                SetProperty(ref selectedType, value);

                if (selectedType.SortNameType == "First Name" || selectedType.SortNameType == "Last Name")
                {

                   ContactsGrouped = _mobileApi.GetContactsGroupedDb(selectedType.SortNameType);
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

        public ObservableCollection<ContactsDb> Contact
        {
            get { return _Items; }
            set
            {
                _Items = value;
                RaisePropertyChanged();
            }
        }

        private ContactsDb _selectedContact;
        public ContactsDb SelectedContact
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

       
        private ObservableCollection<ContactsDb> _Items;
        private ObservableCollection<ContactsDb> _ItemsFiltered;
        private ObservableCollection<ContactsDb> _ItemsUnfiltered;

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

            try
            {

                _isGroupListViewEnabled = true;
                _isListViewEnabled = false;
                RaisePropertyChanged("IsGroupListViewEnabled");
                RaisePropertyChanged("IsListViewEnabled");
                Contact = new ObservableCollection<ContactsDb>();

                this._navigationService = navigationService;
                this._unityContainer = unityContainer;

                Title = "Contact Information";

                _mobileApi = mobileApi;
                SearchCommand = new Prism.Commands.DelegateCommand(PerformSearch);


                SelectedType = SortTypes[0];

                LoadContactsm();
                ContactsGrouped = _mobileApi.GetContactsGroupedDb();

                LoadContactDb();
                _navigationService = navigationService;
            }
            catch(Exception ex)
            {

            }
        }

        public void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(this._searchText))
            {
                ContactsGrouped = _mobileApi.GetContactsGroupedDb();
                RaisePropertyChanged("ContactsGrouped");
                Contact = _ItemsUnfiltered;
                _isGroupListViewEnabled = true;
                _isListViewEnabled = false;
                RaisePropertyChanged("IsGroupListViewEnabled");
                RaisePropertyChanged("IsListViewEnabled");
            }

            else
            {
                ContactsGrouped = _mobileApi.GetContactsGroupedDb();
                RaisePropertyChanged("ContactsGrouped");
               
                _ItemsFiltered = new ObservableCollection<ContactsDb>(_ItemsUnfiltered
                .Where(i => (i is ContactsDb && (((ContactsDb)i)
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
                var contacts = _mobileApi.GetContactsmDb();
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
            var contacts = _mobileApi.GetContactsmDb(FilterType);
            foreach (var contact in contacts)
            {
                _Items.Add(contact);
            }


            Contactsm = _Items;

        }

        public void LoadContacts(string FilterType)
        {
            _Items.Clear();
            var contacts = _mobileApi.GetContactsDb(FilterType);
            foreach (var borrower in contacts)
            {
                _Items.Add(borrower);
            }


            Contact = _Items;

        }

      
        public void LoadContactDb()
        {

            var contacts = _mobileApi.GetContactsDb();
            foreach (var contact in contacts)
            {
                _Items.Add(contact);
            }

            var contacts_search = Contact;

            _ItemsUnfiltered = new ObservableCollection<ContactsDb>(contacts_search);
            Contact = new ObservableCollection<ContactsDb>(contacts_search);

        }
    }
}

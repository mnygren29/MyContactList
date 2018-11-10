using MyContactList.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyContactList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IMobileApi _mobileApi;
        private readonly INavigationService _navigationService;
        public DelegateCommand SubmitCommand { get; set; }
       
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


        }


        private async void GoToTabPage()
        {
          
            await NavigationService.NavigateAsync("app:///NavigationPage/PrismBaseTabbedPage");
        }

    }
}

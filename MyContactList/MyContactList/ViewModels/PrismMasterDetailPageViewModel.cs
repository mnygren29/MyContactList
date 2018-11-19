using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyContactList.ViewModels
{
	public class PrismMasterDetailPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public PrismMasterDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            // _navigationService.NavigateAsync(new System.Uri("app:///NavigationPage/PrismBaseTabbedPage",System.UriKind.Absolute));
            _navigationService.NavigateAsync(new System.Uri("app:///NavigationPage/PrismBaseTabbedPage", System.UriKind.Relative));



        }
	}
}

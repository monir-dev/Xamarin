using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecord.ViewModels.Base;

namespace TravelRecord.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigationCommand { get; set; }

        public HomeViewModel()
        {
            NavigationCommand = new RelayCommand(Navigate);
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}

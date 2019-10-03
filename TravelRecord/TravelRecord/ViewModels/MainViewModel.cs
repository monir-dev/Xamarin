using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelRecord.Model;
using TravelRecord.Pages;
using TravelRecord.ViewModels.Base;

namespace TravelRecord.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public User AppUser { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private async void Register()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }


        public bool CanExecute(object parameter)
        {
            var user = (User) parameter;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) return false;

            return true;
        }

        /// <summary>
        /// Indicate if the current item is expanded or not
        /// </summary>
        public bool IsAbleToLogin
        {
            get => string.IsNullOrEmpty(AppUser.Email) || string.IsNullOrEmpty(AppUser.Password);
        }

        public async void Login()
        {
            bool canLogin = await User.Login(Email, Password);

            if (!canLogin)
                await App.Current.MainPage.DisplayAlert("Warning", "Email and Password does not match", "ok");
            else
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
    }
}

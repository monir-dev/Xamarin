using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text != confirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Password does not match", "Ok");
                return;
            }

            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            int rows = await User.Insert(user);

            if (rows > 0)
            {
                await DisplayAlert("Success", "Registration Successful", "Ok");
                await Navigation.PushAsync(new MainPage());
            }
            else
                await DisplayAlert("Failure", "Registration failed", "Ok");
        }
    }
}
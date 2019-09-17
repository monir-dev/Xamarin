using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Model;
using TravelRecord.Pages;
using Xamarin.Forms;

namespace TravelRecord
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecord.Assets.Images.plane.png", assembly);
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            bool canLogin = await User.Login(emailEntry.Text, passwordEntry.Text);

            if (!canLogin)
                await DisplayAlert("Warning", "Email and Password does not match", "ok");
            else
                await Navigation.PushAsync(new HomePage());
        }

        private void RegisterUserButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}

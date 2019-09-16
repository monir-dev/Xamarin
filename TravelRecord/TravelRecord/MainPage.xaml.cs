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
            User user = null;


            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Warning", "Email or Password can not be empty", "ok");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
            {
                conn.CreateTable<User>();
                user = conn.Table<User>().SingleOrDefault(u => u.Email == emailEntry.Text);
            }

            if (user == null)
            {
                await DisplayAlert("Warning", "Email and Password does not match", "ok");
                return;
            }

            // set static user
            App.user = user;

            Navigation.PushAsync(new HomePage());
        }

        private void RegisterUserButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}

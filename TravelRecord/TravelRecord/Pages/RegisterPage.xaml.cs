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

        private void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                User user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
                {
                    conn.CreateTable<User>();
                    int rows = conn.Insert(user);

                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Registration Successful", "Ok");
                        Navigation.PushAsync(new MainPage());
                    }
                    else
                        DisplayAlert("Failure", "Registration failed", "Ok");
                }
            }
            else
            {
                DisplayAlert("Error", "Password does not match", "Ok");
            }
        }
    }
}
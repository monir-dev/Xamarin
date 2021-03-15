using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinStarter.View
{
    public class MainCsPage : ContentPage
    {

        public MainCsPage()
        {
            Title = "Google Login";
            BackgroundColor = Color.White;

            var loginButton = new Button
            {
                Text = "Login with Google",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#db4437"),
                Font = Font.BoldSystemFontOfSize(26),
                FontSize = 26
            };

            loginButton.Clicked += LoginWithFacebook_Clicked;

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label
                    {
                        Text = "Login with Google API",
                        FontSize = 66,
                        TextColor = Color.Black
                    },
                    loginButton
                }
            };
        }

        private async void LoginWithFacebook_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new GoogleProfileCsPage());
            await Navigation.PushModalAsync(new GoogleProfileCsPage());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Forms;
using XamarinStarter.Services;

namespace XamarinStarter.View
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LinkedInLogin_OnClicked(object sender, EventArgs e)
        {
            var auth = new OAuth2Authenticator(
                clientId: "867axju64n5qkn",
                clientSecret: "GbfM3rXis5AjwrqR",
                scope: "r_fullprofile r_contactinfo",
                authorizeUrl: new Uri("https://www.linkedin.com/uas/oauth2/authorization"),
                redirectUrl: new Uri("https://api.yescoders.com/"),
                accessTokenUrl: new Uri("https://www.linkedin.com/uas/oauth2/accessToken")
            );

            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.AllowCancel = true;

            auth.Completed += LinkedInAuth_Completed;

            if (Device.RuntimePlatform == Device.iOS)
            {
                //global::Android.Content.Intent ui_object = auth.GetUI(this);
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                //UIKit.UIViewController ui_object = auth.GetUI();
            }
        }

        private void LinkedInAuth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                string dd = eventArgs.Account.Username;
                var values = eventArgs.Account.Properties;
                var access_token = values["access_token"];
                try
                {

                    var request = HttpWebRequest.Create(string.Format(
                        @"https://api.linkedin.com/v1/people/~:(id,firstName,lastName,headline,picture-url,summary,educations,three-current-positions,honors-awards,site-standard-profile-request,location,api-standard-profile-request,phone-numbers)?oauth2_access_token=" +
                        access_token + "&format=json", ""));
                    request.ContentType = "application/json";
                    request.Method = "GET";

                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        System.Console.Out.WriteLine("Stautus Code is: {0}", response.StatusCode);

                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            var content = reader.ReadToEnd();
                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                System.Console.Out.WriteLine(content);
                            }

                            var result = JsonConvert.DeserializeObject<dynamic>(content);
                        }
                    }
                }
                catch (Exception exx)
                {
                    System.Console.WriteLine(exx.ToString());
                }
            }
        }

        private void GoogleLogin_OnClicked(object sender, EventArgs e)
        {
            var authRequest =
                "https://accounts.google.com/o/oauth2/v2/auth"
                + "?response_type=code"
                + "&scope=openid"
                + "&redirect_uri=" + GoogleServices.RedirectUri
                + "&client_id=" + GoogleServices.ClientId;

            Device.OpenUri(new Uri(authRequest));
        }


    }
}

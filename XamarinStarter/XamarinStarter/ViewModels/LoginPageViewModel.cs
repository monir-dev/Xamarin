using System;
using System.Diagnostics;
using XamarinStarter.OAuth;
using XamarinStarter.OAuth.Services;
using XamarinStarter.Views;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinStarter.ViewModels;
using System.Threading.Tasks;

namespace XamarinStarter.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        OAuth2Authenticator oAuth2Authenticator;
        OAuth2ProviderType OAuth2ProviderType { get; set; }

        public static EventHandler OnPresenter;

        private void Authenticate(OAuth2ProviderType providerType)
        {
            oAuth2Authenticator = OAuthAuthenticatorHelper.CreateOAuth2(providerType);
            oAuth2Authenticator.Completed += OAuth2Authenticator_Completed;
            oAuth2Authenticator.Error += OAuth2Authenticator_Error;

            OAuth2ProviderType = providerType;
            var presenter = new OAuthLoginPresenter();
            // This is workaround for iOS because when open presenter on iOS, 
            // view is not correctly shown. Then is necessary  modify view on 
            // iOS LoginPageRenderer renderer. On Android, view is correctly shown.
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    if (providerType == OAuth2ProviderType.FACEBOOK)
                    {
                        OnPresenter?.Invoke(oAuth2Authenticator, EventArgs.Empty);
                    }
                    else
                    {
                        presenter.Login(oAuth2Authenticator);
                    }

                    break;
                case Device.Android:
                case Device.UWP:
                    presenter.Login(oAuth2Authenticator);
                    break;
            }
        }

        #region COMMANDS

        public Command FacebookClickedCommand => new Command(FacebookClicked);
        public Command GoogleClickedCommand => new Command(GoogleClickedAsync);
        public Command MicrosoftClickedCommand => new Command(MicrosoftClicked);

        #endregion

        #region AUTH COMPLETED

        private async void OAuth2Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                try
                {
                    await SecureStorage.SetAsync(OAuth2ProviderType.ToString(), JsonConvert.SerializeObject(e.Account.Properties));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                string email = string.Empty;

                switch (OAuth2ProviderType)
                {
                    case OAuth2ProviderType.NONE:
                        break;
                    case OAuth2ProviderType.TRADITIONAL:
                        break;
                    case OAuth2ProviderType.FACEBOOK:
                        email = await ProviderService.GetFacebookEmailAsync();
                        break;
                    case OAuth2ProviderType.GOOGLE:
                        email = await ProviderService.GetGoogleEmailAsync();
                        break;
                    case OAuth2ProviderType.MICROSOFT:
                        email = await ProviderService.GetMicrosoftEmailAsync();
                        break;
                }

                await SecureStorage.SetAsync("Email", email);
                await SecureStorage.SetAsync("Provider", OAuth2ProviderType.ToString());

                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

            }
            else
            {
                oAuth2Authenticator.OnCancelled();
                oAuth2Authenticator = default(OAuth2Authenticator);
            }
        }

        #endregion

        #region AUTH ERRORS

        private async void OAuth2Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            OAuth2Authenticator authenticator = (OAuth2Authenticator)sender;
            if (authenticator != null)
            {
                authenticator.Completed -= OAuth2Authenticator_Completed;
                authenticator.Error -= OAuth2Authenticator_Error;
            }

            string title = "Authentication Error";
            string msg = e.Message;

            Debug.WriteLine($"Error authenticating with {OAuth2ProviderType}! Message: {e.Message}");
            await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }

        #endregion

        #region METHODS

        private void FacebookClicked()
        {
            Authenticate(OAuth2ProviderType.FACEBOOK);
        }

        private async void GoogleClickedAsync()
        {
            var test = await SecureStorage.GetAsync("Email");

            Authenticate(OAuth2ProviderType.GOOGLE);
        }

        private void MicrosoftClicked()
        {
            Authenticate(OAuth2ProviderType.MICROSOFT);
        }

        #endregion

    }
}

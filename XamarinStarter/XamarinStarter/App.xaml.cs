using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinStarter.View;
using XamarinStarter.ViewModels;

namespace XamarinStarter
{
    public partial class App : Application
    {
        public static string UserName { get; set; }
        public static ImageSource ImageUser { get; set; }
        public static string Occupation { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new OAuthNativeFlowPage();
            //MainPage = new MainCsPage();
            //MainPage = new LoginView();
        }

        public async static void GetGoogleCode(string code)
        {
            try
            {
                if (code != "")
                {
                    GoogleViewModel _vm = new GoogleViewModel();
                    string accessToken = _vm.GetAccessTokenAsync(code).ToString();

                    await _vm.SetGoogleUserProfileAsync(accessToken);
                    App.UserName = _vm.GoogleProfile.DisplayName;
                    App.Occupation = _vm.GoogleProfile.Occupation;

                    Debug.WriteLine(App.UserName);

                    string photo = _vm.GoogleProfile.Image.Url;
                    App.ImageUser = ImageSource.FromUri(new Uri(photo));
                    App.Current.MainPage = new GoogleProfilePage();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

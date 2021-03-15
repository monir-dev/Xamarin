using System;
using System.Diagnostics;
using Xamarin.Forms;
using XamarinStarter.Views;

namespace XamarinStarter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // For Curious Drive Oauth Login
            //MainPage = new OAuthNativeFlowPage();

            // For Xamarin.Auth Login 
            MainPage = new LoginPage();
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

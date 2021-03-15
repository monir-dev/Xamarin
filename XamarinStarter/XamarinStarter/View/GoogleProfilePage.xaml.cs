using System;
using System.Linq;
using System.Net.Http;
using XamarinStarter.Services;
using XamarinStarter.ViewModels;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace XamarinStarter.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleProfilePage : ContentPage
    {

        private readonly GoogleViewModel _googleViewModel;

        public GoogleProfilePage()
        {
            InitializeComponent();

            _googleViewModel = BindingContext as GoogleViewModel;

            var authRequest =
                "https://accounts.google.com/o/oauth2/v2/auth"
                + "?response_type=code"
                + "&scope=openid"
                + "&redirect_uri=" + GoogleServices.RedirectUri
                + "&client_id=" + GoogleServices.ClientId;


            string ua = "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X)" + "AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25";
            Uri targetUri = new Uri(authRequest);
            HttpRequestMessage hrm = new HttpRequestMessage(HttpMethod.Get, targetUri);
            hrm.Headers.Add("User-Agent", ua);

            var webView = new WebView
            {
                Source = authRequest,
                HeightRequest = 1
            };


            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var code = ExtractCodeFromUrl(e.Url);

            if (code != "")
            {

                var accessToken = await _googleViewModel.GetAccessTokenAsync(code);

                await _googleViewModel.SetGoogleUserProfileAsync(accessToken);

                Content = MainStackLayout;
            }
        }

        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var attributes = url.Split('&');

                var code = attributes.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];

                return code;
            }

            return string.Empty;
        }
    }
}
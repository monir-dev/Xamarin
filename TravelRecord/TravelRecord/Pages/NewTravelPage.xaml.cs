using System;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using TravelRecord.Model;
using TravelRecord.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        TravelViewModel travelViewModel;
        Post post;
        public NewTravelPage()
        {
            InitializeComponent();

            post = new Post();
            travelViewModel = new TravelViewModel();
            BindingContext = travelViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }
    }
}
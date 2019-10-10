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

        private async void SaveExperience_OnClicked(object sender, EventArgs e)
        {
            try
            {
                // Get selected item
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = selectedVenue.location.address;
                post.Distance = selectedVenue.location.distance;
                post.Latitude = selectedVenue.location.lat;
                post.Longitude = selectedVenue.location.lng;
                post.VenueName = selectedVenue.name;
                post.UserId = App.user.Id;

                int rows = await Post.Insert(post);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully added", "Ok");
                else
                    DisplayAlert("Failure", "Experience add operation failed", "Ok");

            }
            catch (NullReferenceException nre) { }
            catch (Exception ex) { }
        }
    }
}
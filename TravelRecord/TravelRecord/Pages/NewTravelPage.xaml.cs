using System;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

            try
            {
                var status =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission
                        .Location);
                
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need Permission", "We will have to access your location", "Ok");
                    }

                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (result.ContainsKey(Permission.Location))
                    {
                        status = result[Permission.Location];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();

                    var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
                    venueListView.ItemsSource = venues;
                }
                else
                {
                    await DisplayAlert("No permission",
                        "You didn't granted permission to access your location, we cannot proceed", "Ok");
                }
                
            }
            catch (Exception e)
            {
            }
            
        }
    }
}
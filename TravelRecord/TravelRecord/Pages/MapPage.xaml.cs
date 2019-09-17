using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            // Check for permission and set user current location on map
            Task.Run(async () => await CheckMapPermission());
        }

        /// <summary>
        /// Check if the user Granded Location permission or not
        /// </summary>
        /// <returns></returns>
        private async Task CheckMapPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    if (results.ContainsKey(Permission.Location))
                    {
                        status = results[Permission.Location];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    //Query permission
                    //var results = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));

                    // Set user location on map
                    locationsMap.IsShowingUser = true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //location denied
                    await DisplayAlert("Location Denied", "Can not continue, try again", "OK");
                }
            }
            catch (Exception ex)
            {
                //Something went wrong
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 100);

            var position = await locator.GetPositionAsync();

            locationsMap.MoveToRegion(new MapSpan(new Position(position.Latitude, position.Longitude), 2, 2));


            var posts = await Post.Read();
            DisplayInMap(posts);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;

            await locator.StopListeningAsync();
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Position(post.Latitude, post.Longitude);

                    var pin = new Pin
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre) { }
                catch (Exception e) { }
                
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            locationsMap.MoveToRegion(new MapSpan(new Position(e.Position.Latitude, e.Position.Longitude), 2, 2));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SQLite;
using TravelRecord.Model;
using TravelRecord.ViewModels.Base;
using Xamarin.Forms;

namespace TravelRecord.ViewModels
{
    public class TravelViewModel : BaseViewModel
    {
        [MaxLength(250)]
        public string Experience { get; set; }

        public string VenueName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }


        public Venue venue { get; set; }

        public ICommand SaveExperienceCommand { get; set; }


        public TravelViewModel()
        {
            SaveExperienceCommand = new RelayCommand(SaveExperience);
        }

        public async void SaveExperience()
        {
            try
            {
                Post post = new Post();
                var firstCategory = venue.categories.FirstOrDefault();
                post.Experience = Experience;
                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = venue.location.address;
                post.Distance = venue.location.distance;
                post.Latitude = venue.location.lat;
                post.Longitude = venue.location.lng;
                post.VenueName = venue.name;


                int rows = await Post.Insert(post);

                if (rows > 0)
                    await App.Current.MainPage.DisplayAlert("Success", "Experience successfully added", "Ok");
                else
                    await App.Current.MainPage.DisplayAlert("Failure", "Experience add operation failed", "Ok");

            }
            catch (NullReferenceException nre)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience add operation failed", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Experience add operation failed", "Ok");
            }
        }
    }
}

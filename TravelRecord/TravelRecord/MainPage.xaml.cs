using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Model;
using TravelRecord.Pages;
using TravelRecord.ViewModels;
using Xamarin.Forms;

namespace TravelRecord
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainViewModel mainViewModel;
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecord.Assets.Images.plane.png", assembly);
            
            mainViewModel = new MainViewModel();
            containerStackLayout.BindingContext = mainViewModel;
        }

    }
}

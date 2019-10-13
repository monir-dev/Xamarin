using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Model;
using TravelRecord.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel historyViewModel;

        public HistoryPage()
        {
            InitializeComponent();

            historyViewModel = new HistoryViewModel();
            BindingContext = historyViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            historyViewModel.RenderPosts();
        }
    }
}
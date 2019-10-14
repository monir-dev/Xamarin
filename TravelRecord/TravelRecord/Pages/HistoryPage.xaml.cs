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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await historyViewModel.RenderPosts();
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var post = ((MenuItem) sender).CommandParameter as Post;
            historyViewModel.DeletePosts(post);

            await historyViewModel.RenderPosts();
        }

        private async void PostListView_OnRefreshing(object sender, EventArgs e)
        {
            await historyViewModel.RenderPosts();
            postListView.IsRefreshing = false;
        }
    }
}
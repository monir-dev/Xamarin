using System.Collections.Generic;
using System.Linq;
using SQLite;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
            {
                var posts = conn.Table<Post>().ToList();

                Dictionary<string, int> categoriesCount = posts
                    .Where(p => !string.IsNullOrEmpty(p.CategoryName))
                    .GroupBy(p => p.CategoryName)
                    .Select(p => new
                    {
                        Category = p.Key,
                        Count = p.Count()
                    }).ToDictionary(p => p.Category, p => p.Count);

                categoriesListView.ItemsSource = categoriesCount;

                postCountLabel.Text = posts.Count().ToString();
            }
        }
    }
}
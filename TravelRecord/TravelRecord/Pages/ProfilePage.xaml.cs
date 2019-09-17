using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var posts = await Post.Read();

            Dictionary<string, int> categoriesCount = await Post.PostCategories(posts);

            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = posts.Count().ToString();
        }
    }
}
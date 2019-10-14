using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TravelRecord.Model;

namespace TravelRecord.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryViewModel()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async Task RenderPosts()
        {
            var posts = await Post.Read();

            if (posts != null)
            {
                Posts.Clear();

                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
            
        }

        public async void DeletePosts(Post postToDelete)
        {
           await Post.Delete(postToDelete);
        }
    }
}

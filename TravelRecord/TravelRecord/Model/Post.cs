using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;
using TravelRecord.Annotations;
using TravelRecord.ViewModels;
using Xamarin.Forms;

namespace TravelRecord.Model
{
    public class Post : BaseViewModel
    {
        [PrimaryKey] 
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(250)]
        public string Experience { get; set; }

        public string VenueName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }

        public string UserId { get; set; } = App.user.Id;

        public DateTimeOffset CreatedAt { get; set; }


        #region Methods

        /// <summary>
        /// Insert a record to post table
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static async Task<int> Insert(Post post)
        {
            post.CreatedAt = DateTimeOffset.Now;
            int rows = 0;

            using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
            {
                conn.CreateTable<Post>();
                rows = conn.Insert(post);
            }

            return rows;
        }

        /// <summary>
        /// Return all post of currently logged in user
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Post>> Read()
        {
            List<Post> posts = new List<Post>();

            using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
            {
                conn.CreateTable<Post>();
                posts = conn.Table<Post>().Where(p => p.UserId == App.user.Id).ToList();
            }

            return posts;
        }

        /// <summary>
        /// Return Categories along with count of post of that categories
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, int>> PostCategories(List<Post> posts)
        {
            return posts
                .Where(p => !string.IsNullOrEmpty(p.CategoryName))
                .GroupBy(p => p.CategoryName)
                .Select(p => new
                {
                    Category = p.Key,
                    Count = p.Count()
                }).ToDictionary(p => p.Category, p => p.Count);
        }

        #endregion

    }
}
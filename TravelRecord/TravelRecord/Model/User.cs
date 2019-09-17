using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TravelRecord.Model
{
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public static async Task<bool> Login(string email, string password)
        {
            User user = null;

            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty) return false;

            using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
            {
                conn.CreateTable<User>();
                user = conn.Table<User>().SingleOrDefault(u => u.Email == email);
            }

            if (user == null) return false;

            // set static user
            App.user = user;

            return true;
        }

        public static async Task<int> Insert(User user)
        {
            int rows = 0;

            using (SQLiteConnection conn = new SQLiteConnection(App.dbLocation))
            {
                conn.CreateTable<User>();
                rows = conn.Insert(user);
            }

            return rows;
        }
    }
}

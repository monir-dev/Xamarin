using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TravelRecord.Model
{
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

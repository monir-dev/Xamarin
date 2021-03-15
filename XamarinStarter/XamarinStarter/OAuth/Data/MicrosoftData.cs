using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinStarter.OAuth.Data
{
    public class MicrosoftData
    {
        public Emails Emails { get; set; }
    }

    public class Emails
    {
        public string Preferred { get; set; }
        public string Account { get; set; }
        public string Personal { get; set; }
        public string Business { get; set; }
    }
}

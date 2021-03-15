using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using SimpleAuth;
using SimpleAuth.Providers;

namespace gMusic.Apis
{
    public class GMusicApi : GoogleApi
    {
        public GMusicApi() : base("google", Constants.ApiConstants.GoogleClientId, Constants.ApiConstants.GoogleClientSecret)
        {
            Scopes = new[]
            {
                "email",
            };
        }

        public GMusicApi(string identifier, string clientId, HttpMessageHandler handler = null) : base(identifier, clientId, handler)
        {
        }

        public GMusicApi(string identifier, string clientId, string clientSecret, HttpMessageHandler handler = null) : base(identifier, clientId, clientSecret, handler)
        {
        }
    }
}

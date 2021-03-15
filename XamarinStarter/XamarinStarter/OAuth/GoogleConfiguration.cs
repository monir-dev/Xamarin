namespace XamarinStarter.OAuth
{
    public static class GoogleConfiguration
    {
        public static readonly string ClientId = "45489150392-6jbncpulb46hkdd3s09brbb5fjkas2jg.apps.googleusercontent.com";
        public static readonly string Scope = "email profile";
        public static readonly string ClientSecret = "66BXdsRvqb9stIM3n72JBlRx";
        public static readonly string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static readonly string RedirectUrl = "com.googleusercontent.apps.45489150392-6jbncpulb46hkdd3s09brbb5fjkas2jg:/oauth2redirect";
        public static readonly string AcessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static bool IsUsingNativeUI = true;
    }
}

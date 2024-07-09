namespace Facebook.FacebookSDK{
    public class AppAccessTokenUrl{
        public const string AppAccessTokenUrlString = "https://graph.facebook.com/v16.0/oauth/access_token";
        public string client_id = FacebookSDK.Instance.clientId;
        public string client_secret = FacebookSDK.Instance.clientSecret;
        public const string grant_type = "client_credentials";
        public string query(){
            string query = "?";
            query += "client_id=" + client_id;
            query += "&client_secret=" + client_secret;
            query += "&grant_type=" + grant_type;
            return query;
        }
        public string getUrl(){
            string url = AppAccessTokenUrlString + query();
            return url;
        }
        public static string getAppAccessTokenUrl(){
            AppAccessTokenUrl appAccessTokenUrl = new AppAccessTokenUrl();
            return appAccessTokenUrl.getUrl();
        }
    }
}
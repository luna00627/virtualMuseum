namespace Google.GoogleSDK
{
    public class AuthUrl
    {
        public const string AuthUrlString = "https://accounts.google.com/o/oauth2/v2/auth";
        public string client_id = GoogleSDK.Instance.clientId;
        public string redirect_uri = GoogleSDK.Instance.redirectUri;
        public const string response_type = "code";
        public string scope;
        public string access_type;
        public string state;
        public bool include_granted_scopes;
        public string login_hint;
        public string prompt;
        public AuthUrl(
            string scope,
            string access_type = null,
            string state = null,
            bool include_granted_scopes = false,
            string login_hint = null,
            string prompt = null
            )
        {
            this.scope = scope;
            this.access_type = access_type;
            this.state = state;
            this.include_granted_scopes = include_granted_scopes;
            this.login_hint = login_hint;
            this.prompt = prompt;
        }

        public string query()
        {
            string query = "?";
            query += "client_id=" + client_id;
            query += "&redirect_uri=" + redirect_uri;
            query += "&response_type=" + response_type;
            query += "&scope=" + scope;
            if (!string.IsNullOrEmpty(access_type) || !string.IsNullOrWhiteSpace(access_type)) query += "&access_type=" + access_type;
            if (!string.IsNullOrEmpty(state) || !string.IsNullOrWhiteSpace(state)) query += "&state=" + state;
            query += "&include_granted_scopes=" + (include_granted_scopes?"true":"false");
            if (!string.IsNullOrEmpty(login_hint) || !string.IsNullOrWhiteSpace(login_hint)) query += "&login_hint=" + login_hint;
            if (!string.IsNullOrEmpty(prompt) || !string.IsNullOrWhiteSpace(prompt)) query += "&prompt=" + prompt;
            return query;
        }

        public string getUrl()
        {
            string url = AuthUrlString + query();
            return url;
        }
        public static string getAuthUrl(
            string scope,
            string access_type = null,
            string state = null,
            bool include_granted_scopes = false,
            string login_hint = null,
            string prompt = null
            )
        {
            AuthUrl authUrl = new AuthUrl(scope, access_type, state, include_granted_scopes, login_hint, prompt);
            return authUrl.getUrl();
        }
    }
}
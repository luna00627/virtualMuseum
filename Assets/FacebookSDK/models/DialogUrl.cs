namespace Facebook.FacebookSDK{
    public class DialogUrl{
        public const string DialogUrlString = "https://www.facebook.com/v16.0/dialog/oauth";
        public string client_id = FacebookSDK.Instance.clientId;
        public string redirect_uri = FacebookSDK.Instance.redirectUrl;
        public const string response_type = "code";
        public string scope;
        public string state;

        public DialogUrl(string scope, string state = null){
            this.scope = scope;
            this.state = state;
        }

        public string query(){
            string query = "?";
            query += "response_type=" + response_type;
            query += "&client_id=" + client_id;
            query += "&redirect_uri=" + redirect_uri;
            if(!string.IsNullOrEmpty(state) || !string.IsNullOrWhiteSpace(state)){
                query += "&state=" + state;
            }
            query += "&scope=" + scope;
            return query;
        }

        public string getUrl(){
            string url = DialogUrlString + query();
            return url;
        }

        public static string getDialogUrl(string scope, string state = null){
            DialogUrl dialogUrl = new DialogUrl(scope, state);
            return dialogUrl.getUrl();
        }
    }
}
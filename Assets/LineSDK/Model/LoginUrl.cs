namespace Line.LineSDK {
    public class LoginUrl
    {
        public const string response_type = "code";
        public string client_id =LineSDK.Instance.channelID;
        public string redirect_uri = LineSDK.Instance.redirect_uri;
        public string state { get; set; }
        public string scope { get; set; }
        public string bot_prompt { get; set; }
        public string query()
        {
            return $"response_type={response_type}&client_id={client_id}&redirect_uri={redirect_uri}&state={state}&scope={scope}&bot_prompt={bot_prompt}";
        }
        public string getUrl(){
            string url= $"https://access.line.me/oauth2/v2.1/authorize?{query()}";
            url= url.Replace(" ", "%20");
            return url;
        }
        public static string genLoginUrl(string scopes, string bot_prompt, string state){
            LoginUrl loginUrl = new LoginUrl();
            loginUrl.scope = scopes;
            loginUrl.state = state;
            loginUrl.bot_prompt = bot_prompt;
            return loginUrl.getUrl();
        }
    }
}
namespace Google.GoogleSDK{
    using System;
    using UnityEngine;

    [Serializable]
    public class AccessTokenRefreshResult{
        [SerializeField]
        public string access_token;
        [SerializeField]
        public long expires_in;
        [SerializeField]
        public string scope;
        [SerializeField]
        public string token_type;
        [SerializeField]
        public string id_token;

        public AccessToken toAccessToken(string refresh_token){
            return JsonUtility.FromJson<AccessToken>(
                "{\"access_token\":\""+access_token+
                "\",\"expires_in\":"+expires_in.ToString()+
                ",\"refresh_token\":\""+refresh_token+
                "\",\"scope\":\""+scope+
                "\",\"token_type\":\""+token_type+
                "\",\"id_token\":\""+id_token+
                "\"}");
        }
    }
}
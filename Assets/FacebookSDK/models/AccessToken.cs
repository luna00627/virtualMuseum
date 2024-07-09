namespace Facebook.FacebookSDK{
    using System;
    using UnityEngine;

    [Serializable]
    public class AccessToken{
        [SerializeField]
        private string access_token;
        [SerializeField]
        private string token_type;
        [SerializeField]
        private long expires_in;

        public string Value { get { return access_token; } }
        
        public string TokenType { get { return token_type; } }

        public long ExpiresIn { get { return expires_in; } }


        public StoredAccessToken storedAccessToken(){
            string json="{\"access_token\":\""+access_token+"\",\"expires_in\":"+expires_in+"}";
            return JsonUtility.FromJson<StoredAccessToken>(json);
        }
    }
}
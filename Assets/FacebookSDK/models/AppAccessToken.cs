using System;
using UnityEngine;

namespace Facebook.FacebookSDK{

    public class AppAccessToken{
        [SerializeField]
        private string access_token;
        [SerializeField]
        private string token_type;

        public string AccessToken{ get { return access_token; } }

        public string TokenType{ get { return token_type; } }
    }
}
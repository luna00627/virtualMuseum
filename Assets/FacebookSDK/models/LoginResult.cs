using System;
using UnityEngine;

namespace Facebook.FacebookSDK {

    [Serializable]
    public class LoginResult {
        [SerializeField]
        private AccessToken access_token;
        [SerializeField]
        private UserProfile user_profile;

        public AccessToken Access_token { get { return access_token; } }

        public UserProfile UserProfile { get { return user_profile; } }
    }
}
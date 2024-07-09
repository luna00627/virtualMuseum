namespace Google.GoogleSDK
{
    using System;
    using UnityEngine;
    [Serializable]
    public class LoginResult
    {
        [SerializeField]
        private AccessToken access_token;
        [SerializeField]
        private UserProfile user_profile;

        /// <summary>
        /// The access token obtained by the login process.
        /// </summary>
        public AccessToken Access_token { get { return access_token; } }

        /// <summary>
        /// Contains the user profile including the user ID, display name, and so on.
        /// The value exists only when the "profile" scope is set in the authorization request.
        /// </summary>
        public UserProfile UserProfile { get { return user_profile; } }
    }
}
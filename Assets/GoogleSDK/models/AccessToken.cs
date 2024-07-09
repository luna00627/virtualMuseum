namespace Google.GoogleSDK
{
    using System;
    using UnityEngine;
    [Serializable]
    public class AccessToken
    {
        [SerializeField]
        private string access_token;
        [SerializeField]
        private long expires_in;
        [SerializeField]
        private string refresh_token;
        [SerializeField]
        private string scope;
        [SerializeField]
        private string token_type;
        [SerializeField]
        private string id_token;

        /// <summary>
        /// The value of the access token.
        /// </summary>
        public string Value { get { return access_token; } }

        /// <summary>
        /// Number of seconds until the access token expires.
        /// </summary>
        public long ExpiresIn { get { return expires_in; } }

        /// <summary>
        /// The raw string value of the ID token bound to the access token. The
        /// value exists only if the access token is obtained with the "openID"
        /// permission.
        /// </summary>
        public string IdTokenRaw { get { return id_token; } }
        /// <summary>
        /// The refresh token bound to the access token.
        /// </summary>
        public string RefreshToken { get { return refresh_token; } }

        /// <summary>
        /// Permissions granted by the user.
        /// </summary>
        public string Scope { get { return scope; } }
        /// <summary>
        /// The expected authorization type when this token is used in a request
        /// header. Fixed to "Bearer" for now.
        /// </summary>
        public string TokenType { get { return token_type; } }


        public StoredAccessToken ToStoredAccessToken()
        {
            string json="{\"access_token\":\""+access_token+"\",\"expires_in\":"+expires_in+"}";
            return JsonUtility.FromJson<StoredAccessToken>(json);
        }
    }
}
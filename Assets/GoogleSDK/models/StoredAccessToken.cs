using System;
using UnityEngine;

namespace Google.GoogleSDK {
    /// <summary>
    /// Represents the access token stored on the device.
    /// </summary>
    [Serializable]
    public class StoredAccessToken {
        [SerializeField]
        private string access_token;
        [SerializeField]
        private long expires_in;

        /// <summary>
        /// The value of the access token.
        /// </summary>
        public string Value { get { return access_token; } }

        /// <summary>
        /// Expiration time of the token in seconds **at the time the token was
        /// created**. This value is never updated. To get the up-to-date
        /// `ExpiresIn` value for a token, call `API.VerifyAccessToken`.
        /// </summary>
        public long ExpiresIn { get { return expires_in; } }
    }
}

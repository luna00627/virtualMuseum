using System;
using UnityEngine;

namespace Google.GoogleSDK {
    /// <summary>
    /// Represents a response to the token verification API.
    /// </summary>
    public class AccessTokenVerifyResult {
        [SerializeField]
        private string azp;
        [SerializeField]
        private string aud;
        [SerializeField]
        private long sub;

        [SerializeField]
        private string scope;
        [SerializeField]
        private long exp;
        [SerializeField]
        private long expires_in;
        [SerializeField]
        private string email;
        [SerializeField]
        private bool email_verified;

        [SerializeField]
        private string access_type;

        /// <summary>
        /// The client_id of the authorized presenter.
        /// </summary>
        public string Azp { get { return azp; } }
        /// <summary>
        /// Identifies the audience that this ID token is intended for. It must be one of the OAuth 2.0 client IDs of your application.
        /// </summary>
        public string Aud { get { return aud; } }
        /// <summary>
        /// The subject of the token.An identifier for the user, unique among all Google accounts and never reused.
        /// </summary>
        public long Sub { get { return sub; } }
        /// <summary>
        /// The space-delimited list of scopes granted to the access token.
        /// </summary>
        public string Scope { get { return scope; } }
        /// <summary>
        /// The expiration time of the token, as number of seconds from 1970-01-01T0:0:0Z.
        /// </summary>
        public long Exp { get { return exp; } }
        /// <summary>
        /// The lifetime in seconds of the access token.
        /// </summary>
        public long ExpiresIn { get { return expires_in; } }
        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get { return email; } }
        /// <summary>
        /// Whether the email address has been verified.
        /// </summary>
        public bool EmailVerified { get { return email_verified; } }
        /// <summary>
        /// Whether the token was issued to an app or a web server.
        /// </summary>
        public string AccessType { get { return access_type; } }
    }
}

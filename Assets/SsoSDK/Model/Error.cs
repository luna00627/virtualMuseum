using System;
using UnityEngine;

namespace Sso.SsoSDK {
    /// <summary>
    /// Represents an error that happens in Sso SDK.
    /// </summary>
    [Serializable]
    public class Error {
        [SerializeField]
        private int code;
        [SerializeField]
        private string message;

        /// <summary>
        /// Error code showing the type of error.
        /// </summary>
        /// <value>
        /// This value differs per operating system. For details, see the
        /// reference documentation for Sso SDK for iOS Swift and Sso SDK for
        /// Android.
        /// </value>
        public int Code { get { return code; } }

        /// <summary>
        /// Human-readable error description.
        /// </summary>
        public string Message { get { return message; } }

        internal Error(int code, string message) {
            this.code = code;
            this.message = message;
        }
    }
}

using System;
using UnityEngine;

namespace Facebook.FacebookSDK{
    [Serializable]
    public class StoredAccessToken{
        [SerializeField]
        private string access_token;
        [SerializeField]
        private long expires_in;

        public string Value { get { return access_token; } }

        public long ExpiresIn { get { return expires_in; } }
    }
}
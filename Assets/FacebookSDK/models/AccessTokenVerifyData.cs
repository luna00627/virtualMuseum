using System;
using UnityEngine;

namespace Facebook.FacebookSDK {
    [Serializable]
    public class AccessTokenVerifyData {
        [SerializeField]
        private string app_id;
        [SerializeField]
        private string type;
        [SerializeField]
        private string application;
        [SerializeField]
        private long data_access_expires_at;
        [SerializeField]
        private long expires_at;
        [SerializeField]
        private bool is_valid;
        [SerializeField]
        private long issued_at;
        [SerializeField]
        private string[] scopes;
        [SerializeField]
        private string user_id;
        [SerializeField]
        private AccessTokenVerifyDataError error;

        public string AppId { get { return app_id; } }
        
        public string Type { get { return type; } }
        
        public string Application { get { return application; } }

        public long DataAccessExpiresAt { get { return data_access_expires_at; } }

        public long ExpiresAt { get { return expires_at; } }

        public bool IsValid { get { return is_valid; } }

        public long IssuedAt { get { return issued_at; } }

        public string[] Scopes { get { return scopes; } }

        public string UserId { get { return user_id; } }

        public AccessTokenVerifyDataError Error { get { return error; } }
    }
}
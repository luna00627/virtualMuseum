namespace Google.GoogleSDK{
    using System;
    using UnityEngine;
    [Serializable]
    public class UserProfile{
        [SerializeField]
        public string sub;
        [SerializeField]
        public string name;
        [SerializeField]
        public string given_name;
        [SerializeField]
        public string family_name;
        [SerializeField]
        public string picture;
        [SerializeField]
        public string email;
        [SerializeField]
        public bool email_verified;
        [SerializeField]
        public string locale;
    }
}
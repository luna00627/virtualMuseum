namespace Sso.SsoSDK{
    using System;
    using UnityEngine;

    [Serializable]
    public class UserProfile{
        [SerializeField]
        public long userId;

        [SerializeField]
        public string userName;

        [SerializeField]
        public string userPhotoUrl;
    }
}
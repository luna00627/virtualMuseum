using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facebook.FacebookSDK{
    [Serializable]
    public class UserProfile{
        [SerializeField]
        public string id;
        [SerializeField]
        public string first_name;
        [SerializeField]
        public string last_name;
        [SerializeField]
        public string name;
        [SerializeField]
        public Picture picture;
        [SerializeField]
        public string email;

    }
}
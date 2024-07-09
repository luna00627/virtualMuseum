using System;
using UnityEngine;

namespace Facebook.FacebookSDK{
    public class Me{
        [SerializeField]
        private string name;
        [SerializeField]
        private string id;

        public string Name{ get { return name; } }

        public string Id{ get { return id; } }
    }
}
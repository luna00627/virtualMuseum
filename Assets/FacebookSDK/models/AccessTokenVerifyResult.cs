using System;
using UnityEngine;

namespace Facebook.FacebookSDK {

    [Serializable]
    public class AccessTokenVerifyResult {
        [SerializeField]
        private AccessTokenVerifyData data;

        public AccessTokenVerifyData Data{ get { return data; } }
    }
}
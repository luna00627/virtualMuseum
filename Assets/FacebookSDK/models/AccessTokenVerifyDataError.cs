using System;
using UnityEngine;

namespace Facebook.FacebookSDK{
    [Serializable]
    public class AccessTokenVerifyDataError{
        [SerializeField]
        private int code;
        [SerializeField]
        private string message;
        [SerializeField]
        private int subcode;

        public int Code { get { return code; } }

        public string Message { get { return message; } }

        public int Subcode { get { return subcode; } }
    }
}
namespace Facebook.FacebookSDK{
    using System;
    using UnityEngine;

    [Serializable]
    public class BackendPayload{
        [SerializeField]
        public string type;
        [SerializeField]
        public LoginResult payload;

        public BackendPayload(string type, LoginResult payload){
            this.type = type;
            this.payload = payload;
        }

        public string toJson(){
            return JsonUtility.ToJson(this);
        }

        public static BackendPayload fromJson(string json){
            return JsonUtility.FromJson<BackendPayload>(json);
        }

        public static string wrapValue(string type, string payload){
            BackendPayload backendPayload = new BackendPayload(type, JsonUtility.FromJson<LoginResult>(payload));
            return backendPayload.toJson();
        }
    }
}
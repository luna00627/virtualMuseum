using System;
using UnityEngine;

namespace Facebook.FacebookSDK{
    
    public class FacebookSDK : MonoBehaviour {
        static FacebookSDK instance;

        public string clientId = "139714262066055";
        public string clientSecret = "a7cc060163eaebd300eb16551640ad31";
        public string redirectUrl = "https://893c-140-121-17-48.ngrok-free.app/FacebookApi/requestAccessToken";
        public string backendAccessTokenUrl = "https://893c-140-121-17-48.ngrok-free.app/FacebookApi/getLoginResult";

        void Awake() {
            if (instance == null){
                instance = this;
            } else{
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            
        }
        
        public static FacebookSDK Instance{
            get{
                if(instance == null){
                    GameObject go = new GameObject("FacebookSDK");
                    instance = go.AddComponent<FacebookSDK>();
                }
                return instance;
            }
        }

        void SetupSDK(){
            if(string.IsNullOrEmpty(clientId)){
                throw new System.Exception("Facebook SDK client ID is not set.");
            }
            if(string.IsNullOrEmpty(clientSecret)){
                throw new System.Exception("Facebook SDK client Secret is not set.");
            }
            if(string.IsNullOrEmpty(redirectUrl)){
                throw new System.Exception("Facebook SDK redirect url is not set.");
            }
            NativeInterface.SetupSDK(clientId, clientSecret,redirectUrl);
        }

        public void Login(string[] scopes, Action<Result<LoginResult>> action){
            Login(scopes, null, action);
        }

        public void Login(string[] scopes, LoginOption option, Action<Result<LoginResult>> action){
            FacebookAPI.Login(scopes, option, action);
        }

        public void Logout(Action<Result<Unit>> action){
            FacebookAPI.Logout(action);
        }

        public StoredAccessToken CurrentAccessToken{
            get{
                var result = NativeInterface.GetCurrentAccessToken();
                if(string.IsNullOrEmpty(result)) { return null; }
                return JsonUtility.FromJson<StoredAccessToken>(result);
            }
        }

        internal void OnApiOk(string result){
            FacebookAPI._OnApiOk(result);
        }

        internal void OnApiError(string result){
            FacebookAPI._OnApiError(result);
        }

    }


}
using System;
using UnityEngine;
namespace Google.GoogleSDK
{
    public class GoogleSDK : MonoBehaviour
    {
        static GoogleSDK instance;

        /// <summary>
        /// The Client ID for your app.
        /// </summary>
        public string clientId = "1084427337318-5vst16jc8kng2pv6ojhochp17dm1bt06.apps.googleusercontent.com";
        /// <summary>
        /// The Client Secret of your app.
        /// </summary>
        public string clientSecret = "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";
        /// <summary>
        /// The Redirect Uri of your app.
        /// </summary>
        public string redirectUri = "http://hi-ntou-usr.cse.ntou.edu.tw:80/GoogleApi/requestAccessToken";

        /// <summary>
        /// The Url of the backend server that handles the access token exchange.
        /// </summary>
        public string backendAccessTokenUrl = "http://hi-ntou-usr.cse.ntou.edu.tw:80/GoogleApi/getLoginResult";

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// The shared instance of `GoogleSDK`. Always use this instance to
        /// interact with the login process of the Google SDK.
        /// </summary>
        /// <value>
        /// The shared instance of `GoogleSDK`.
        /// </value>
        public static GoogleSDK Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("GoogleSDK");
                    instance = go.AddComponent<GoogleSDK>();
                }
                return instance;

            }
        }

        void SetUpSDK()
        {
            if (string.IsNullOrEmpty(clientId))
            {
                Debug.LogError("clientId is null");
            }
            if (string.IsNullOrEmpty(clientSecret))
            {
                Debug.LogError("clientSecret is null");
            }
            if (string.IsNullOrEmpty(redirectUri))
            {
                Debug.LogError("RedirectUri is null");
            }
            NativeInterface.SetUpSDK(clientId, clientSecret, redirectUri);
        }


        /// <summary>
        /// Logs in to the LINE Platform with the specified scopes.
        /// </summary>
        /// <param name="scopes">
        /// The set of permissions requested by your app. If `null` or empty,
        /// the "profile" scope is used.
        /// </param>
        /// <param name="action">
        /// The callback action to be invoked when the login process finishes.
        /// </param>
        public void Login(string[] scopes, Action<Result<LoginResult>> action)
        {
            Login(scopes, null, action);
        }
        /// <summary>
        /// Logs in to the LINE Platform with the specified scopes and an option.
        /// </summary>
        /// <param name="scopes">
        /// The set of permissions requested by your app. If `null` or empty,
        /// the "profile" scope is used.
        /// </param>
        /// <param name="option">
        /// The options used during the login process.
        /// </param>
        /// <param name="action">
        /// The callback action to be invoked when the login process finishes.
        /// </param>
        public void Login(string[] scopes, LoginOption option, Action<Result<LoginResult>> action)
        {
            GoogleAPI.Login(scopes, option, action);
        }

        /// <summary>
        /// Logs out the current user by revoking the access token.
        /// </summary>
        /// <param name="action">
        /// The callback action to be invoked when the logout process finishes.
        /// </param>
        public void Logout(Action<Result<Unit>> action)
        {
            GoogleAPI.Logout(action);
        }

        /// <summary>
        /// Gets the current access token in use.
        /// </summary>
        /// <value>
        /// A `StoredAccessToken` object which contains the access token value
        /// currently in use.
        /// </value>
        public StoredAccessToken CurrentAccessToken
        {
            get
            {
                var result = NativeInterface.GetCurrentAccessToken();
                if (string.IsNullOrEmpty(result)) { return null; }
                return JsonUtility.FromJson<StoredAccessToken>(result);
            }
        }

        internal void OnApiOk(string result)
        {
            GoogleAPI._OnApiOk(result);
        }

        internal void OnApiError(string result)
        {
            GoogleAPI._OnApiError(result);
        }
    }

}

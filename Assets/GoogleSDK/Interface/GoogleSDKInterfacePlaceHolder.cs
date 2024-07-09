#if false
namespace Google.GoogleSDK{
    using UnityEngine;
    internal class NativeInterface{
        internal static void SetUpSDK(string clientId, string clientSecret, string redirectUri){}
        internal static void Login(string scope,string access_type,string state,bool include_granted_scopes,string login_hint,string prompt,string identifier){}
        internal static void Logout(string identifier){}
        internal static void RefreshAccessToken(string identifier){}
        internal static void RevokeAccessToken(string identifier){}
        internal static void VerifyAccessToken(string identifier){}
        internal static void GetProfile(string identifier){}
        internal static string GetCurrentAccessToken(){
            return null;
        }
    }
}
#endif
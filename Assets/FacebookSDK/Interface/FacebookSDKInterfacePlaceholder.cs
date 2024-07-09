#if true
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Facebook.FacebookSDK{
    internal class NativeInterface{

        private static AccessToken currentAccessToken;

        internal static void SetupSDK(string clientId, string clientSecret, string redirectUrl) {
            if (!Application.isPlaying) { return; }

            currentAccessToken = null;
        }
        internal static void Login(string scope,string identifier) {
            Application.OpenURL(DialogUrl.getDialogUrl(scope,identifier));
            FacebookSDK.Instance.StartCoroutine(getLoginResult(identifier));

        }

        static IEnumerator getLoginResult(string identifier){
            WWWForm form = new WWWForm();
            form.AddField("id",identifier);
            int requestCount = 0;
            do{
                if(++requestCount > 1000){
                    FacebookSDK.Instance.OnApiError(
                        CallbackPayload.WrapValue(identifier,JsonUtility.ToJson(new Error(408, "login request timeout"))
                        )
                    );
                    break;
                }
                UnityWebRequest www = UnityWebRequest.Post(FacebookSDK.Instance.backendAccessTokenUrl, form);
                yield return www.SendWebRequest();
                if(www.result != UnityWebRequest.Result.Success){
                    FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(500, "login request error: "+ www.error))));
                    break;
                }
                else{
                    string result = www.downloadHandler.text;
                    Debug.Log(result);
                    BackendPayload payload = JsonUtility.FromJson<BackendPayload>(result);
                    if(payload.type == "loading"){
                        ;
                    }
                    else if(payload.type == "success"){
                        currentAccessToken = payload.payload.Access_token;
                        FacebookSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(payload.payload)));
                        break;
                    }
                }
                yield return new WaitForSeconds(1);
            }while(true);
            yield break;
        }
        internal static void Logout(string identifier) {
            currentAccessToken = null;
            FacebookSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, ""));
        }
        internal static void RefreshAccessToken(string identifier) {
            if(!Application.isPlaying) { return; }
            FacebookSDK.Instance.StartCoroutine(getRefreshResult(identifier));
        }
        static IEnumerator getRefreshResult(string identifier){
            if(currentAccessToken == null){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(401, " no access token"))));
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Get(AppAccessTokenUrl.getAppAccessTokenUrl());
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson( new Error(500,"app access token request error: " + www.error))));
            }
            else{
                string res = www.downloadHandler.text;
                AppAccessToken appAccessToken = JsonUtility.FromJson<AppAccessToken>(res);
                UnityWebRequest WWW = UnityWebRequest.Get("https://graph.facebook.com/debug_token?input_token="+currentAccessToken.Value+"&access_token="+appAccessToken.AccessToken);
                yield return WWW.SendWebRequest();
                if(WWW.result != UnityWebRequest.Result.Success){
                    FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(500, "verify request error: " + WWW.error))));
                }
                else{
                    string result = WWW.downloadHandler.text;
                    AccessTokenVerifyResult accessTokenVerifyResult = JsonUtility.FromJson<AccessTokenVerifyResult>(result);
                    if(accessTokenVerifyResult.Data.IsValid){
                        UnityWebRequest refreshRequest = UnityWebRequest.Get("https://graph.facebook.com/v16.0/oauth/access_token?grant_type=fb_exchange_token&client_id=" + FacebookSDK.Instance.clientId + "&client_secret=" + FacebookSDK.Instance.clientSecret + "&fb_exchange_token=" + currentAccessToken.Value);
                        yield return refreshRequest.SendWebRequest();
                        if(refreshRequest.result != UnityWebRequest.Result.Success){
                            FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(500, "refresh request error: " + refreshRequest.error))));
                        }
                        else{
                            AccessToken refreshAccessToken = JsonUtility.FromJson<AccessToken>(refreshRequest.downloadHandler.text);
                            currentAccessToken = refreshAccessToken;
                            Debug.Log(currentAccessToken.Value);
                            FacebookSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, refreshRequest.downloadHandler.text));
                        }
                    }
                    else{
                        Debug.Log("access token is not valid, login again");
                    }
                }
            }
            
        }
        internal static void RevokeAccessToken(string identifier) {
            if(!Application.isPlaying) { return; }
            FacebookSDK.Instance.StartCoroutine(getRevokeResult(identifier));
        }
        static IEnumerator getRevokeResult(string identifier){
            if(currentAccessToken == null){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(401, " no access token"))));
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Get("https://graph.facebook.com/me?access_token=" + currentAccessToken.Value);
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson( new Error(500, "me request error: " + www.error))));
            }
            else{
                string res = www.downloadHandler.text;
                Me me = JsonUtility.FromJson<Me>(res);
                Debug.Log(me.Name);
                UnityWebRequest WWW = UnityWebRequest.Delete("https://graph.facebook.com/" + me.Id + "/permissions?access_token=" + currentAccessToken.Value);
                yield return WWW.SendWebRequest();
                if(WWW.result != UnityWebRequest.Result.Success){
                    FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(500, "verify request error: " + WWW.error))));
                }
                else{
                    FacebookSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, ""));
                }
            }
        }
    
        internal static void VerifyAccessToken(string identifier) {
            if(!Application.isPlaying){ return; }
            FacebookSDK.Instance.StartCoroutine(getVerifyResult(identifier));
        }
        static IEnumerator getVerifyResult(string identifier){
            if(currentAccessToken == null){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson( new Error(401, "no access token"))));
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Get(AppAccessTokenUrl.getAppAccessTokenUrl());
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson( new Error(500,"app access token request error: " + www.error))));
            }
            else{
                string res = www.downloadHandler.text;
                AppAccessToken appAccessToken = JsonUtility.FromJson<AppAccessToken>(res);
                Debug.Log(appAccessToken.AccessToken);
                UnityWebRequest WWW = UnityWebRequest.Get("https://graph.facebook.com/debug_token?input_token="+currentAccessToken.Value+"&access_token="+appAccessToken.AccessToken);
                yield return WWW.SendWebRequest();
                if(WWW.result != UnityWebRequest.Result.Success){
                    FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(500, "verify request error: " + WWW.error))));
                }
                else{
                    FacebookSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, WWW.downloadHandler.text));
                }
            }
        }
        internal static void GetProfile(string identifier) {
            if(!Application.isPlaying) { return; }
            FacebookSDK.Instance.StartCoroutine(getProfileResult(identifier));
        }
        static IEnumerator getProfileResult(string identifier){
            if(currentAccessToken == null){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson( new Error(401, "no access token"))));
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Get("https://graph.facebook.com/me?access_token=" + currentAccessToken.Value);
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success){
                FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson( new Error(500, "me request error: " + www.error))));
            }
            else{
                string res = www.downloadHandler.text;
                Me me = JsonUtility.FromJson<Me>(res);
                UnityWebRequest WWW = UnityWebRequest.Get("https://graph.facebook.com/" + me.Id + "?fields=id,first_name,last_name,name,email,picture&access_token=" + currentAccessToken.Value);
                yield return WWW.SendWebRequest();
                if(WWW.result != UnityWebRequest.Result.Success){
                    FacebookSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(new Error(500, "verify request error: " + WWW.error))));
                }
                else{
                    FacebookSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, WWW.downloadHandler.text));
                }
            }
        }
        internal static string GetCurrentAccessToken() {
            if(currentAccessToken == null){
                return null;
            } 
            return JsonUtility.ToJson(currentAccessToken.storedAccessToken());
        }
    }   
}
#endif
namespace Sso.SsoSDK{
    using System;
    using UnityEngine;
    using Line.LineSDK;
    using Google.GoogleSDK;
    using Facebook.FacebookSDK;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.Networking;

    internal class SsoAPI{
        internal static void SsoAuth(string loginType, Action<Result<AuthResult>> action){
            
            string token="";
            if(loginType=="line"){
                token=LineSDK.Instance.CurrentAccessToken.Value;
            }else if(loginType=="google"){
                token=GoogleSDK.Instance.CurrentAccessToken.Value;
            }else if(loginType=="facebook"){
                token=FacebookSDK.Instance.CurrentAccessToken.Value;
            }
            SsoSDK.Instance.StartCoroutine(AuthRequest(loginType, token,action));
        }
        static IEnumerator AuthRequest(string loginType, string token, Action<Result<AuthResult>> action)
        {
            Debug.Log(loginType+" "+token);
            WWWForm form = new WWWForm();
            form.AddField("loginType", loginType);
            form.AddField("token", token);
            string url = SsoSDK.Instance.backendUrl + "/auth";
            Debug.Log(url);
            UnityWebRequest www =UnityWebRequest.Post(url, form);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                    
                Error err=new Error(500, www.error);
                action(Result<AuthResult>.Error(err));
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                AuthResult res = JsonUtility.FromJson<AuthResult>(www.downloadHandler.text);
                if(res==null) Debug.Log("res is null");
                SsoSDK.currentUser=res.ToUserProfile();
                action(Result<AuthResult>.Ok(res));
            }
            yield break;
        }

        internal static void Logout(Action<Result<Unit>> action){
            SsoSDK.currentUser=null;
            action(Result<Unit>.Ok(Unit.Value));
        }
    }
    
}
//  Copyright (c) 2019-present, LINE Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LINE Corporation.
//
//  As with any software that integrates with the LINE Corporation platform, your use of this software
//  is subject to the LINE Developers Agreement [http://terms2.line.me/LINE_Developers_Agreement].
//  This copyright notice shall be included in all copies or substantial portions of the software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if !UNITY_IOS && !UNITY_ANDROID
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Line.LineSDK
{
    internal class NativeInterface
    {
        public static AccessToken currentAccessToken = null;
        internal static void SetupSDK(string channelId, string universalLinkURL) { }
        internal static void Login(string scope, bool onlyWebLogin, string botPrompt, string identifier)
        {
            Application.OpenURL(Line.LineSDK.LoginUrl.genLoginUrl(scope, botPrompt, identifier));
            Line.LineSDK.LineSDK.Instance.StartCoroutine(getLoginResult(identifier));
        }
        static IEnumerator getLoginResult(string identifier)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", identifier);
            int requestCount = 0;
            do
            {
                if (++requestCount > 1000)
                {
                    LineSDK.Instance.OnApiError(
                        CallbackPayload.WrapValue(
                            identifier,
                            JsonUtility.ToJson(
                                new Error(408, "login request timeout")
                            )
                        )
                    );
                    break;
                }
                UnityWebRequest www = UnityWebRequest.Post(LineSDK.Instance.backendAccessTokenUrl, form);
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                    break;
                }
                else
                {
                    string res = www.downloadHandler.text;
                    BackendPayload payload = JsonUtility.FromJson<BackendPayload>(res);
                    if (payload.type == "loading")
                    {
                        ;
                    }
                    else
                    {
                        currentAccessToken=payload.payload.AccessToken;
                        Line.LineSDK.LineSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(payload.payload)));
                        break;
                    }
                }
                yield return new WaitForSeconds(1);
            } while (true);
        }
        internal static void Logout(string identifier)
        {

            Line.LineSDK.LineSDK.Instance.StartCoroutine(getRevokeResult(identifier));
            // Line.LineSDK.LineSDK.Instance.StartCoroutine(getRevokeResult(""));
            // Line.LineSDK.LineSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, ""));
        }
        internal static void RefreshAccessToken(string identifier)
        {
            Line.LineSDK.LineSDK.Instance.StartCoroutine(getRefreshResult(identifier));
        }
        static IEnumerator getRefreshResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                Error err = new Error(401, "no access token");
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            WWWForm form = new WWWForm();
            form.AddField("grant_type", "refresh_token");
            form.AddField("refresh_token", currentAccessToken.RefreshToken);
            form.AddField("client_id", Line.LineSDK.LineSDK.Instance.channelID);
            if (Line.LineSDK.LineSDK.Instance.channelID == null)
            {
                Error err = new Error(401, "no channel id");
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Post("https://api.line.me/oauth2/v2.1/token", form);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Error err = new Error(400, www.error);
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            else
            {
                var result = JsonUtility.FromJson<AccessToken>(www.downloadHandler.text);
                currentAccessToken = result;
                Line.LineSDK.LineSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, www.downloadHandler.text));
            }
        }
        internal static void RevokeAccessToken(string identifier)
        {
            Line.LineSDK.LineSDK.Instance.StartCoroutine(getRevokeResult(identifier));
        }
        static IEnumerator getRevokeResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                Error err = new Error(401, "no access token");
                if(identifier!="") Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            WWWForm form = new WWWForm();
            form.AddField("access_token", currentAccessToken.Value);
            form.AddField("client_id", Line.LineSDK.LineSDK.Instance.channelID);
            if (currentAccessToken == null)
            {
                Error err = new Error(401, "no access token");
                if (identifier != "") Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            if (Line.LineSDK.LineSDK.Instance.channelID == null)
            {
                Error err = new Error(401, "no channel id");
                if (identifier != "") Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Post("https://api.line.me/oauth2/v2.1/revoke", form);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Error err = new Error(400, www.error);
                if (identifier != "") Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            else
            {
                currentAccessToken = null;
                if (identifier != "") Line.LineSDK.LineSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, null));
            }
        }
        internal static void VerifyAccessToken(string identifier)
        {
            Line.LineSDK.LineSDK.Instance.StartCoroutine(getVerifyResult(identifier));
        }
        static IEnumerator getVerifyResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                Error err = new Error(401, "no access token");
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            string url = "https://api.line.me/oauth2/v2.1/verify";
            string payload = "access_token=" + currentAccessToken.Value;
            string queryUrl = url + "?" + payload;
            UnityWebRequest www = UnityWebRequest.Get(queryUrl);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Error err = new Error(400, www.error);
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
            }
            else
            {
                string res = www.downloadHandler.text;
                AccessTokenVerifyResult result = JsonUtility.FromJson<AccessTokenVerifyResult>(res);
                Line.LineSDK.LineSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(result)));
            }
        }
        internal static void GetProfile(string identifier)
        {
            Line.LineSDK.LineSDK.Instance.StartCoroutine(getProfileResult(identifier));
        }
        static IEnumerator getProfileResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                Error err = new Error(401, "no access token");
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
                yield break;
            }
            string url = "https://api.line.me/v2/profile";
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SetRequestHeader("Authorization", "Bearer " + currentAccessToken.Value);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Error err = new Error(400, www.error);
                Line.LineSDK.LineSDK.Instance.OnApiError(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(err)));
            }
            else
            {
                string res = www.downloadHandler.text;
                UserProfile result = JsonUtility.FromJson<UserProfile>(res);
                Line.LineSDK.LineSDK.Instance.OnApiOk(CallbackPayload.WrapValue(identifier, JsonUtility.ToJson(result)));
            }
        }
        internal static void GetBotFriendshipStatus(string identifier) {

        }
        internal static string GetCurrentAccessToken() {
            // if(currentAccessToken == null)
            // {
            //     return null;
            // }else{
            //     return currentAccessToken.Value;
            // }

            if (currentAccessToken == null)
            {
                return null;
            }
            return JsonUtility.ToJson(currentAccessToken.ToStoredAccessToken());
        }
    }
}
#endif
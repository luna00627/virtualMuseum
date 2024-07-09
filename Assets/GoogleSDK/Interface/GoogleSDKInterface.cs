#if true
namespace Google.GoogleSDK
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    internal class NativeInterface
    {
        private static AccessToken currentAccessToken;
        internal static void SetUpSDK(string clientId, string clientSecret, string redirectUri)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            currentAccessToken = null;
        }
        internal static void Login(string scope, string access_type, string state, bool include_granted_scopes, string login_hint, string prompt, string identifier)
        {
            Application.OpenURL(AuthUrl.getAuthUrl(scope, access_type, identifier, include_granted_scopes, login_hint, prompt));
            GoogleSDK.Instance.StartCoroutine(getLoginResult(identifier));
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
                    GoogleSDK.Instance.OnApiError(
                        CallbackPayload.WrapValue(
                            identifier,
                            JsonUtility.ToJson(
                                new Error(408, "login request timeout")
                            )
                        )
                    );
                    break;
                }
                UnityWebRequest www = UnityWebRequest.Post(GoogleSDK.Instance.backendAccessTokenUrl, form);
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    GoogleSDK.Instance.OnApiError(
                        CallbackPayload.WrapValue(
                            identifier,
                            JsonUtility.ToJson(
                                new Error(500, "login request error: " + www.error)
                            )
                        )
                    );
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
                    else if (payload.type == "success")
                    {
                        currentAccessToken = payload.payload.Access_token;
                        GoogleSDK.Instance.OnApiOk(
                            CallbackPayload.WrapValue(
                                identifier,
                                JsonUtility.ToJson(payload.payload)
                            )
                        );
                        break;
                    }
                }
                yield return new WaitForSeconds(1);
            } while (true);
            yield break;
        }
        internal static void Logout(string identifier)
        {
            RevokeAccessToken(identifier);
        }
        internal static void RefreshAccessToken(string identifier)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            GoogleSDK.Instance.StartCoroutine(getRefreshResult(identifier));
        }
        static IEnumerator getRefreshResult(string identifier){
            if (currentAccessToken == null)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(401, "no access token")
                        )
                    )
                );
                yield break;
            }
            WWWForm form = new WWWForm();
            form.AddField("client_id", GoogleSDK.Instance.clientId);
            form.AddField("client_secret", GoogleSDK.Instance.clientSecret);
            form.AddField("redirect_uri", GoogleSDK.Instance.redirectUri);
            form.AddField("refresh_token", currentAccessToken.RefreshToken);
            form.AddField("grant_type", "refresh_token");
            UnityWebRequest www = UnityWebRequest.Post("https://oauth2.googleapis.com/token", form);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(500, "refresh request error: " + www.error)
                        )
                    )
                );
            }
            else
            {
                AccessTokenRefreshResult res = JsonUtility.FromJson<AccessTokenRefreshResult>(www.downloadHandler.text);
                currentAccessToken=res.toAccessToken(currentAccessToken.RefreshToken);
                GoogleSDK.Instance.OnApiOk(
                    CallbackPayload.WrapValue(
                        identifier,
                        www.downloadHandler.text
                    )
                );
            }
        }

        internal static void RevokeAccessToken(string identifier)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            GoogleSDK.Instance.StartCoroutine(getRevokeResult(identifier));
        }
        static IEnumerator getRevokeResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(401, "no access token")
                        )
                    )
                );
                yield break;
            }
            WWWForm form = new WWWForm();
            form.AddField("token", currentAccessToken.Value);
            UnityWebRequest www = UnityWebRequest.Post("https://oauth2.googleapis.com/revoke", form);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(500, "revoke request error: " + www.error)
                        )
                    )
                );
            }
            else
            {
                currentAccessToken = null;
                GoogleSDK.Instance.OnApiOk(
                    CallbackPayload.WrapValue(
                        identifier,
                        www.downloadHandler.text
                    )
                );
            }
        }

        internal static void VerifyAccessToken(string identifier)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            GoogleSDK.Instance.StartCoroutine(getVerifyResult(identifier));
        }
        static IEnumerator getVerifyResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(401, "no access token")
                        )
                    )
                );
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Get("https://oauth2.googleapis.com/tokeninfo?access_token=" + currentAccessToken.Value);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(500, "verify request error: " + www.error)
                        )
                    )
                );
            }
            else
            {
                GoogleSDK.Instance.OnApiOk(
                    CallbackPayload.WrapValue(
                        identifier,
                        www.downloadHandler.text
                    )
                );
            }
        }

        internal static void GetProfile(string identifier)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            GoogleSDK.Instance.StartCoroutine(getProfileResult(identifier));
        }
        static IEnumerator getProfileResult(string identifier)
        {
            if (currentAccessToken == null)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(401, "no access token")
                        )
                    )
                );
                yield break;
            }
            UnityWebRequest www = UnityWebRequest.Get("https://openidconnect.googleapis.com/v1/userinfo");
            www.SetRequestHeader("Authorization", "Bearer " + currentAccessToken.Value);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                GoogleSDK.Instance.OnApiError(
                    CallbackPayload.WrapValue(
                        identifier,
                        JsonUtility.ToJson(
                            new Error(500, "profile request error: " + www.error)
                        )
                    )
                );
            }
            else
            {
                GoogleSDK.Instance.OnApiOk(
                    CallbackPayload.WrapValue(
                        identifier,
                        www.downloadHandler.text
                    )
                );
            }
        }
        internal static string GetCurrentAccessToken()
        {
            if (currentAccessToken == null)
            {
                return null;
            }
            return JsonUtility.ToJson(currentAccessToken.ToStoredAccessToken());
            // return "{\"access_token\":\"" + currentAccessToken.Value + "\",\"expires_in\":" + currentAccessToken.ExpiresIn.ToString() + "}";
        }
    }
}
#endif
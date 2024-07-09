namespace Sso.SsoSDK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Line.LineSDK;
    using Google.GoogleSDK;
    using Facebook.FacebookSDK;
    using System;

    public class SsoSDK : MonoBehaviour
    {
        static SsoSDK instance;

        public string backendUrl = "http://hi-ntou-usr.cse.ntou.edu.tw:80";

        public static SsoSDK Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("SSO");
                    instance = go.AddComponent<SsoSDK>();
                }
                return instance;
            }
        }
        internal static UserProfile currentUser;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            currentUser=null;
        }
        public void Login(string loginType,Action<Result<AuthResult>> action){
            if(loginType=="line"){
                LineLogin(action);
            }else if(loginType=="google"){
                GoogleLogin(action);
            }else if(loginType=="facebook"){
                FacebookLogin(action);
            }

        }

        public void LineLogin(Action<Result<AuthResult>> action)
        {
            var scopes = new string[] { "profile", "openid", "email" };
            var loginOption = new Line.LineSDK.LoginOption();
            loginOption.BotPrompt = "normal";
            loginOption.OnlyWebLogin = true;
            LineSDK.Instance.Login(scopes, loginOption, result =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login success");
                        SsoAPI.SsoAuth("line",action);
                    },
                    error =>
                    {
                        Debug.Log(error.Message);
                        Result<AuthResult> result =Result<AuthResult>.Error(new Error(error.Code, error.Message));
                        action(result);
                    }
                );
            });
        }

        public void GoogleLogin(Action<Result<AuthResult>> action)
        {
            var scopes = new string[] { "openid", "email", "profile" };
            Google.GoogleSDK.LoginOption options = new Google.GoogleSDK.LoginOption();
            options.access_type = "offline";
            GoogleSDK.Instance.Login(scopes, options, (result) =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login Success");
                        SsoAPI.SsoAuth("google",action);
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                        Result<AuthResult> result = Result<AuthResult>.Error(new Error(error.Code, error.Message));
                        action(result);
                    }
                );
            });
        }

        public void FacebookLogin(Action<Result<AuthResult>> action){
            var scopes = new string[] {"email","public_profile"};
            Facebook.FacebookSDK.LoginOption options = new Facebook.FacebookSDK.LoginOption();
            FacebookSDK.Instance.Login(scopes, options, (result) =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login Success");
                        SsoAPI.SsoAuth("facebook", action);
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                        Result<AuthResult> result = Result<AuthResult>.Error(new Error(error.Code, error.Message));
                        action(result);
                    }
                );

            });
        }


        public void Logout(Action<Result<Unit>> action)
        {
            LineSDK.Instance.Logout((result) =>
            {
                result.Match(
                    _ =>
                    {
                        Debug.Log("Logout Success");
                        SsoAPI.Logout(action);
                    },
                    error =>
                    {
                        Debug.Log("Logout Error:" + error.Message);
                        action(Result<Unit>.Error(new Error(error.Code, error.Message)));
                    }
                );
            });
            GoogleSDK.Instance.Logout(
                (result) =>
                {
                    result.Match(
                        _ =>
                        {
                            Debug.Log("Logout Success");
                            SsoAPI.Logout(action);
                        },
                        error =>
                        {
                            Debug.Log("Logout Error:" + error.Message);
                            action(Result<Unit>.Error(new Error(error.Code, error.Message)));
                        }
                    );
                }
            );
            FacebookSDK.Instance.Logout(
                (result) =>
                {
                    result.Match(
                        _ =>
                        {
                            Debug.Log("Logout Success");
                            SsoAPI.Logout(action);
                        },
                        error =>
                        {
                            Debug.Log("Logout Error:" + error.Message);
                            action(Result<Unit>.Error(new Error(error.Code, error.Message)));
                        }
                    );
                }
            );
        }


    }
}
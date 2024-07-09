using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.GoogleSDK;
using System;

public class SC : MonoBehaviour
{
    public Button LoginBtn;
    public Button RevokeBtn;
    public Button AccessTokenBtn;
    public Button LogoutBtn;
    public Button RefreshBtn;
    public Button ProfileBtn;
    public Button VerifyBtn;
    public Text showText;
    // Start is called before the first frame update
    void Start()
    {
        LoginBtn.onClick.AddListener(() =>
        {
            Debug.Log("LoginBtn Click");
            var scopes = new string[] { "openid", "email", "profile" };
            Google.GoogleSDK.LoginOption options = new Google.GoogleSDK.LoginOption();
            options.access_type = "offline";
            GoogleSDK.Instance.Login(scopes, options, (result) =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login Success");
                        showText.text = "成功登入:" + value.UserProfile.name;
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                    }
                );
            });
        });

        RevokeBtn.onClick.AddListener(() =>
        {
            Debug.Log("RevokeBtn Click");
            GoogleAPI.RevokeAccessToken((result) =>
            {
                result.Match(
                    _ =>
                    {
                        Debug.Log("Revoke Success");
                        showText.text = "accessToken=" + JsonUtility.ToJson(GoogleSDK.Instance.CurrentAccessToken);
                    },
                    error =>
                    {
                        Debug.Log("Revoke Error:" + error.Message);
                    }
                );
            });
        });


        AccessTokenBtn.onClick.AddListener(() =>
        {
            showText.text = "accessToke=" + JsonUtility.ToJson(GoogleSDK.Instance.CurrentAccessToken);
        });

        LogoutBtn.onClick.AddListener(() =>
        {
            Debug.Log("LogoutBtn Click");
            GoogleAPI.Logout((result) =>
            {
                result.Match(
                    _ =>
                    {
                        Debug.Log("Logout Success");
                        showText.text = "登出成功";
                    },
                    error =>
                    {
                        Debug.Log("Logout Error:" + error.Message);
                    }
                );
            });
        });

        RefreshBtn.onClick.AddListener(() =>
        {
            GoogleAPI.RefreshAccessToken(
                (result) =>
                {
                    result.Match(
                        value =>
                        {
                            Debug.Log("Refresh Success");
                            showText.text = JsonUtility.ToJson(value);
                        },
                        error =>
                        {
                            Debug.Log("Refresh Error:" + error.Message);
                        }
                    );
                }
            );
        });

        ProfileBtn.onClick.AddListener(() =>
        {
            GoogleAPI.GetProfile(
                (result) =>
                {
                    result.Match(
                        value =>
                        {
                            Debug.Log("GetProfile Success");
                            showText.text = JsonUtility.ToJson(value);
                        },
                        error =>
                        {
                            Debug.Log("GetProfile Error:" + error.Message);
                        }
                    );
                }
            );
        });
    
        VerifyBtn.onClick.AddListener(() =>
        {
            GoogleAPI.VerifyAccessToken(
                (result) =>
                {
                    result.Match(
                        value =>
                        {
                            Debug.Log("VerifyAccessToken Success");
                            showText.text = JsonUtility.ToJson(value);
                        },
                        error =>
                        {
                            Debug.Log("VerifyAccessToken Error:" + error.Message);
                        }
                    );
                }
            );
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}

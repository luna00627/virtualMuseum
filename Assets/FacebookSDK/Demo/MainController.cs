using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.FacebookSDK;
using UnityEngine.Networking;
public class MainController : MonoBehaviour
{
    public Button LoginBtn;
    public Button LogoutBtn;
    public Button RevokeBtn;
    public Button AccessTokenBtn;
    public Button RefreshBtn;
    public Button VerifyBtn;
    public Button ProfileBtn;
    public Text show;
    // Start is called before the first frame update
    void Start()
    {
        LoginBtn.onClick.AddListener(() =>{
            var scopes = new string[] {"email","public_profile"};
            Facebook.FacebookSDK.LoginOption options = new Facebook.FacebookSDK.LoginOption();
            FacebookSDK.Instance.Login(scopes, options, (result) => {
                result.Match(
                    value => {
                        Debug.Log("Login Success");
                        Debug.Log(value.UserProfile.picture.data.url);
                        show.text = "成功登入: " + value.UserProfile.name;
                    },
                    error => {
                        Debug.Log("Login Error: " + error.Message);
                    }
                );
            });
        });

        LogoutBtn.onClick.AddListener(() => {
            FacebookAPI.Logout(
                (result) =>{
                    result.Match(
                        _ =>{
                            Debug.Log("Logout Success");
                            show.text = "登出成功";
                        },
                        error =>{
                            Debug.Log("logout error" + error.Message);
                        }
                    );
                }
            );
        });

        AccessTokenBtn.onClick.AddListener(() => {
            show.text = "accessToken=" + JsonUtility.ToJson(FacebookSDK.Instance.CurrentAccessToken);
        });

        RefreshBtn.onClick.AddListener(() => {
            FacebookAPI.RefreshAccessToken(
                (result) =>{
                    result.Match(
                        value =>{
                            Debug.Log("Refresh Success");
                            show.text = JsonUtility.ToJson(value);
                        },
                        error =>{
                            Debug.Log("Refresh error: " + error.Message);
                        }
                    );
                }
            );
        });

        RevokeBtn.onClick.AddListener(() => {
            FacebookAPI.RevokeAccessToken(
                (result) =>{
                    result.Match(
                        _ =>{
                            Debug.Log("Revoke Success");
                            show.text = "accessToken=" + JsonUtility.ToJson(FacebookSDK.Instance.CurrentAccessToken);
                        },
                        error =>{
                            Debug.Log("Revoke error: " + error.Message);
                        }
                    );
                }
            );
        });

        ProfileBtn.onClick.AddListener(() => {
            FacebookAPI.GetProfile(
                (result) =>{
                    result.Match(
                        value =>{
                            Debug.Log("Get Profile Success");
                            show.text = JsonUtility.ToJson(value);
                        },
                        error =>{
                            Debug.Log("Get Profile error: " + error.Message);
                        }
                    );
                }
            );
        });

        VerifyBtn.onClick.AddListener(() =>{
            FacebookAPI.VerifyAccessToken(
                (result) =>{
                    result.Match(
                        value =>{
                            Debug.Log("Get Verify Success");
                            show.text = JsonUtility.ToJson(value);
                        },
                        error =>{
                            Debug.Log("Get Verify error: " + error.Message);
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

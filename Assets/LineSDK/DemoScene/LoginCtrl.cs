using UnityEngine;
using UnityEngine.UI;
using Line.LineSDK;

public class LoginCtrl : MonoBehaviour
{
    public Button LineBtn;
    public Button LogoutBtn;
    public Button RefreshBtn;
    public Button VerifyBtn;
    public Button RevokeBtn;
    public Button ProfileBtn;
    public Text text;
    public void Awake()
    {
        LineBtn.onClick.AddListener(lineLogin);
        VerifyBtn.onClick.AddListener(()=>{
            string id=text.text;
            Line.LineSDK.LineAPI.VerifyAccessToken(result=>{
                result.Match(
                    value=>{
                        text.text = JsonUtility.ToJson(value);
                    },
                    error=>{
                        Debug.Log("VerifyAccessToken error");
                    }
                    );
            });
        });
        LogoutBtn.onClick.AddListener(()=>{
            LineSDK.Instance.Logout(result =>
            {
                result.Match(
                    _ =>
                    {
                        text.text = "登出成功\n access Token=" + JsonUtility.ToJson(Line.LineSDK.LineSDK.Instance.CurrentAccessToken) + "\n";
                    },
                    error =>
                    {
                        Debug.Log(error.Message);
                    }
                );
            });
        });
        RefreshBtn.onClick.AddListener(()=>{
            Line.LineSDK.LineAPI.RefreshAccessToken(result =>
            {
                result.Match(
                    value =>
                    {
                        text.text="accessToken=" + JsonUtility.ToJson(Line.LineSDK.LineSDK.Instance.CurrentAccessToken);
                    },
                    error =>
                    {
                        Debug.Log(error.Message);
                    }
                );
            });
        });
        RevokeBtn.onClick.AddListener(()=>{
            Line.LineSDK.LineAPI.RevokeAccessToken(result=>{
                result.Match(
                    value=>{
                        text.text = "access Token=" + JsonUtility.ToJson(Line.LineSDK.LineSDK.Instance.CurrentAccessToken);
                    },
                    error=>{
                        Debug.Log("RevokeAccessToken error");
                    }
                    );
            });
        });
        ProfileBtn.onClick.AddListener(()=>{
            Line.LineSDK.LineAPI.GetProfile(result=>{
                result.Match(
                    value=>{
                        text.text = JsonUtility.ToJson(value);
                    },
                    error=>{
                        Debug.Log("GetProfile error");
                    }
                    );
            });
        });
    }
    
    void lineLogin(){
        var scopes = new string[] { "profile", "openid", "email" };
        var loginOption = new LoginOption();
        loginOption.BotPrompt = "normal";
        loginOption.OnlyWebLogin = true;
        LineSDK.Instance.Login(scopes, loginOption, result =>
        {
            result.Match(
                value =>
                {
                    Debug.Log("Login success");
                    Debug.Log(value.AccessToken.Value);
                    text.text="已登入:"+value.UserProfile.DisplayName;
                    // StartCoroutine(UpdateProfile(value.UserProfile));
                    // UpdateRawSection(value);
                },
                error =>
                {
                    Debug.Log(error.Message);
                }
            );
        });
    }
}
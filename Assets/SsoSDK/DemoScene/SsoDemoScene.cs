using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sso.SsoSDK;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SsoDemoScene : MonoBehaviour
{
    public Text userNameText;
    public Text userIdText;
    public Image userPhotoImage;
    public Button lineBtn;
    public Button googleBtn;
    public Button facebookBtn;
    public Button logoutBtn;
    void Start()
    {
        lineBtn.onClick.AddListener(() =>
        {
            SsoSDK.Instance.Login("line", (result) =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login Success");
                        showResult(value);
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                    }
                );
            });
        });
        googleBtn.onClick.AddListener(() =>
        {
            SsoSDK.Instance.Login("google", (result) =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login Success");
                        showResult(value);
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                    }
                );
            });
        });
        facebookBtn.onClick.AddListener(() =>
        {
            SsoSDK.Instance.Login("facebook", (result) =>
            {
                result.Match(
                    value =>
                    {
                        Debug.Log("Login Success");
                        showResult(value);
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                    }
                );
            });
        });
        logoutBtn.onClick.AddListener(() =>
        {
            SsoSDK.Instance.Logout(
                (result) =>
                {
                    result.Match(
                        _ =>
                        {
                            Debug.Log("Logout Success");
                            userNameText.text = "登出";
                            userIdText.text = "";
                            userPhotoImage.sprite = null;
                        },
                        error =>
                        {
                            Debug.Log("Logout Error:" + error.Message);
                        }
                    );
                }
            );
        });
    }

    void showResult(AuthResult res)
    {
        userNameText.text = res.userName;
        userIdText.text = res.userId.ToString();
        StartCoroutine(LoadImage(res.userPhotoUrl));
    }
    IEnumerator LoadImage(string url)
    {

        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError(www.error);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(www);
            userPhotoImage.color = Color.white;
            userPhotoImage.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

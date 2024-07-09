using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sso.SsoSDK;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SsoLogin : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject loginCanvas;
    public GameObject playerInfoCanvas;
    public Button lineBtn;
    public Button googleBtn;
    public Button facebookBtn;

    void Start()
    {
        lineBtn.onClick.AddListener(() =>
        {
            SsoSDK.Instance.Login("line", (result) =>
            {
                result.Match(
                    value =>
                    {
                        scoreValue.id = value.userId;
                        loginCanvas.SetActive(false);
                        startCanvas.SetActive(false);
                        playerInfoCanvas.SetActive(true);
                        Debug.Log("Login Success");
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
                        scoreValue.id = value.userId;
                        loginCanvas.SetActive(false);
                        startCanvas.SetActive(false);
                        playerInfoCanvas.SetActive(true);
                        Debug.Log("Login Success");
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
                        scoreValue.id = value.userId;
                        loginCanvas.SetActive(false);
                        startCanvas.SetActive(false);
                        playerInfoCanvas.SetActive(true);
                        Debug.Log("Login Success");
                    },
                    error =>
                    {
                        Debug.Log("Login Error:" + error.Message);
                    }
                );
            });
        });
    }
}
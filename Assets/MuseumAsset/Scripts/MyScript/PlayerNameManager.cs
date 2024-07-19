using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public Button confirmButton;
    public GameObject playerNamePanel;

    private string playerName;

    private void Start()
    {
        playerNamePanel.SetActive(true);
        confirmButton.onClick.AddListener(OnConfirmName);
    }

    private void OnConfirmName()
    {
        playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            playerNamePanel.SetActive(false);
        }
        else
        {
            Debug.Log("名字空白");
        }
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}


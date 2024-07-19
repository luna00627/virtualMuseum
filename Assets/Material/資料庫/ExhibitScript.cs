using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System;
public class ExhibitScript : MonoBehaviour
{
    public TMP_InputField commentInputField;
    public Button submitButton;
    public TMP_Text commentsDisplay; 
    private MongoDBManager dbManager;
    private string exhibitId;
    private PlayerNameManager playerNameManager;

    private void Start()
    {
        dbManager = FindObjectOfType<MongoDBManager>();
        playerNameManager = FindObjectOfType<PlayerNameManager>();
        submitButton.onClick.AddListener(OnSubmitComment);
        exhibitId = gameObject.name; 
        LoadComments();
    }


    private async void OnSubmitComment()
    {
        string commentText = commentInputField.text;
        string playerName = playerNameManager.GetPlayerName(); 

        if (!string.IsNullOrEmpty(commentText))
        {
            Comment newComment = new Comment
            {
                ExhibitId = exhibitId,
                Text = commentText,
                UserName = playerName, 
                CreatedAt = DateTime.Now
            };
            await dbManager.AddComment(exhibitId, newComment);
            commentInputField.text = "";
            LoadComments();
        }
    }

    private async void LoadComments()
    {
        List<Comment> comments = await dbManager.GetComments(exhibitId);
        commentsDisplay.text = "";
        foreach (var comment in comments)
        {
            string formattedUserName = $"<color=#0078A7>{comment.UserName}</color>";
            string formattedDate = $"<size=50%><color=#808080>{comment.CreatedAt.ToString("yyyy-MM-dd HH:mm")}</color></size>";
            commentsDisplay.text += $"{formattedUserName} {comment.Text}\n{formattedDate}\n\n";
        }
    }
}

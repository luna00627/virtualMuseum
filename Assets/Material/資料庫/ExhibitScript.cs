using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class ExhibitScript : MonoBehaviour
{
    public TMP_InputField commentInputField;
    public Button submitButton;
    public Transform commentsContainer;
    public GameObject commentPrefab;

    private MongoDBManager dbManager;
    private AvatarManager avatarManager;
    private string exhibitId;

    private void Start()
    {
        dbManager = FindObjectOfType<MongoDBManager>();
        avatarManager = FindObjectOfType<AvatarManager>();
        submitButton.onClick.AddListener(OnSubmitComment);
        exhibitId = gameObject.name;
        LoadComments();
    }

    private async void OnSubmitComment()
    {
        string commentText = commentInputField.text;
        string playerName = LoginManager.LoggedInUsername;
        int avatarIndex = LoginManager.LoggedInAvatarIndex;

        if (!string.IsNullOrEmpty(commentText))
        {
            Comment newComment = new Comment
            {
                ExhibitId = exhibitId,
                Text = commentText,
                UserName = playerName,
                AvatarIndex = avatarIndex,
                CreatedAt = DateTime.Now
            };

            // 添加評論到資料庫
            await dbManager.AddComment(exhibitId, newComment);
            
            // 清空輸入欄位
            commentInputField.text = "";

            // 直接在 UI 上添加新評論
            AddCommentToUI(newComment);
            
            // 等待一幀後強制更新佈局
            await Task.Yield();
            LayoutRebuilder.ForceRebuildLayoutImmediate(commentsContainer.GetComponent<RectTransform>());
        }
    }

    private async void LoadComments()
    {
        List<Comment> comments = await dbManager.GetComments(exhibitId);

        // 清空現有的評論顯示
        foreach (Transform child in commentsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var comment in comments)
        {
            AddCommentToUI(comment);
        }
        
        // 等待一幀後強制更新佈局
        await Task.Yield();
        LayoutRebuilder.ForceRebuildLayoutImmediate(commentsContainer.GetComponent<RectTransform>());
    }

    private void AddCommentToUI(Comment comment)
    {
        GameObject newCommentObj = Instantiate(commentPrefab, commentsContainer);
        newCommentObj.transform.SetParent(commentsContainer, false); // 確保保持佈局

        // 獲取各顯示區塊的引用
        Image avatarImage = newCommentObj.transform.Find("Panel/AvatarImage").GetComponent<Image>();
        TMP_Text userNameText = newCommentObj.transform.Find("Panel/UserName").GetComponent<TMP_Text>();
        TMP_Text commentText = newCommentObj.transform.Find("CommentText").GetComponent<TMP_Text>();
        TMP_Text dateText = newCommentObj.transform.Find("DateText").GetComponent<TMP_Text>();

        // 設置各顯示區塊的內容
        avatarImage.sprite = avatarManager.GetAvatar(comment.AvatarIndex);
        userNameText.text = comment.UserName;
        commentText.text = comment.Text;
        dateText.text = comment.CreatedAt.ToString("yyyy-MM-dd HH:mm");
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegistrationManager : MonoBehaviour
{
    public GameObject registerPanel;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public Button registerButton;
    public TextMeshProUGUI messageText;
    public Image avatarImage; // 用來顯示當前選擇的頭像
    public Button leftArrowButton; // 左箭頭按鈕
    public Button rightArrowButton; // 右箭頭按鈕

    private MongoClient client;
    private IMongoDatabase userDatabase;
    private IMongoCollection<BsonDocument> accountCollection;

    private AvatarManager avatarManager;
    private int selectedAvatarIndex;

    private void Start()
    {
        client = new MongoClient("mongodb+srv://popo:K5q4fl0en5NzhkLq@unity.yrrt9gw.mongodb.net/?retryWrites=true&w=majority&appName=unity");
        userDatabase = client.GetDatabase("UserDatabase");
        accountCollection = userDatabase.GetCollection<BsonDocument>("UserAccounts");

        avatarManager = GetComponent<AvatarManager>();

        registerButton.onClick.AddListener(OnRegister);
        leftArrowButton.onClick.AddListener(SelectPreviousAvatar);
        rightArrowButton.onClick.AddListener(SelectNextAvatar);

        selectedAvatarIndex = 0;
        UpdateAvatar();

        registerPanel.SetActive(false);
    }

    private void SelectPreviousAvatar()
    {
        selectedAvatarIndex = (selectedAvatarIndex - 1 + avatarManager.GetAvatarCount()) % avatarManager.GetAvatarCount();
        UpdateAvatar();
    }

    private void SelectNextAvatar()
    {
        selectedAvatarIndex = (selectedAvatarIndex + 1) % avatarManager.GetAvatarCount();
        UpdateAvatar();
    }

    private void UpdateAvatar()
    {
        avatarImage.sprite = avatarManager.GetAvatar(selectedAvatarIndex);
    }

    private async void OnRegister()
    {
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text.Trim();

        Debug.Log($"Username: '{username}', Password: '{password}'");

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            messageText.text = "使用者名稱和密碼不能為空";
            Debug.Log("使用者名稱和密碼不能為空");
            return;
        }

        var filter = Builders<BsonDocument>.Filter.Eq("username", username);
        var existingAccount = await accountCollection.Find(filter).FirstOrDefaultAsync();

        if (existingAccount != null)
        {
            messageText.text = "此使用者名稱已被使用";
            Debug.Log("此使用者名稱已被使用");
        }
        else
        {
            var document = new BsonDocument
            {
                { "username", username },
                { "password", password },
                { "avatarIndex", selectedAvatarIndex },
                { "timestamp", BsonDateTime.Create(System.DateTime.Now) }
            };

            await accountCollection.InsertOneAsync(document);
            messageText.text = "註冊成功!";
            Debug.Log("註冊成功!");
        }
    }
}

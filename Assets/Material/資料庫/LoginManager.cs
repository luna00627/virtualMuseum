using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public GameObject loginPanel;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public TextMeshProUGUI messageText;

    private MongoClient client;
    private IMongoDatabase userDatabase;
    private IMongoCollection<BsonDocument> accountCollection;

    public static string LoggedInUsername { get; private set; }
    public static int LoggedInAvatarIndex { get; private set; }

    private void Start()
    {
        client = new MongoClient("mongodb+srv://popo:K5q4fl0en5NzhkLq@unity.yrrt9gw.mongodb.net/?retryWrites=true&w=majority&appName=unity");
        userDatabase = client.GetDatabase("UserDatabase");
        accountCollection = userDatabase.GetCollection<BsonDocument>("UserAccounts");

        loginButton.onClick.AddListener(OnLogin);
        loginPanel.SetActive(true);
    }

    private async void OnLogin()
    {
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "使用者名稱和密碼不行為空";
            return;
        }

        var filter = Builders<BsonDocument>.Filter.Eq("username", username) & Builders<BsonDocument>.Filter.Eq("password", password);
        var existingAccount = await accountCollection.Find(filter).FirstOrDefaultAsync();

        if (existingAccount != null)
        {
            LoggedInUsername = username;
            LoggedInAvatarIndex = existingAccount["avatarIndex"].AsInt32;

            messageText.text = "登入成功!";
            loginPanel.SetActive(false);
        }
        else
        {
            messageText.text = "使用者名稱或密碼錯誤";
        }
    }


}

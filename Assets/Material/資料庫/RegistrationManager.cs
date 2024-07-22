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
    public Image avatarImage;
    public TextMeshProUGUI messageText;
    public Sprite[] avatars; 
    private int selectedAvatarIndex;

    private MongoClient client;
    private IMongoDatabase userDatabase;
    private IMongoCollection<BsonDocument> accountCollection;

    private void Start()
    {
        client = new MongoClient("mongodb://localhost:27017");
        userDatabase = client.GetDatabase("UserDatabase");
        accountCollection = userDatabase.GetCollection<BsonDocument>("UserAccounts");

        registerButton.onClick.AddListener(OnRegister);
        registerPanel.SetActive(false);
    }

    public void SelectAvatar(int index)
    {
        selectedAvatarIndex = index;
        avatarImage.sprite = avatars[index];
    }

    private async void OnRegister()
    {
        string username = usernameInputField.text.Trim();
        string password = passwordInputField.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "使用者名稱和密碼不行為空";
            return;
        }

        var filter = Builders<BsonDocument>.Filter.Eq("username", username);
        var existingAccount = await accountCollection.Find(filter).FirstOrDefaultAsync();

        if (existingAccount != null)
        {
            messageText.text = "此使用者名稱已被使用";
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
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    [Header("Confirmation Panel")]
    public GameObject confirmPanel; 
    public Button confirmButton; // 確認按鈕
    public Button cancelButton;  // 取消按鈕
    public GameObject treasure;
    
    [Header("Game Panel")]
    public GameObject gamePanel;
    public TMP_Text questionText; // 顯示題目
    public Button[] answerButtons; // 選項按鈕
    public TMP_Text progressText; // 顯示進度
    public TMP_Text explanationText; // 顯示解析
    public Button nextButton; // 下一題按鈕
    
    [Header("Result Panel")]
    public GameObject resultPanel;
    public GameObject prize;
    public TextMeshProUGUI usernameText;
    public Image avatarImage;
    public TextMeshProUGUI correctAnswersText;
    public TextMeshProUGUI prizeText;
    public Button confirmPrizeButton; // 確認獎品按鈕
    public Button retryButton; // 再玩一次按鈕
    
    [Header("Symbols")]
    public Sprite correctSymbol; // 打勾符號
    public Sprite incorrectSymbol; // 打叉符號

    [Header("Managers")]
    public GameObject manager;
    public GameObject databaseManager;
    private AvatarManager avatarManager;
    private int totalCorrectAnswers = 0; // 答對的題數
    private int totalQuestions; // 總題數
    private int currentQuestionIndex = 0;
    private PrizeController prizeController;

    private ComponentDisabler componentDisabler;
    private StartFirstGame startFirstGame;

    private MongoClient client;
    private IMongoDatabase userDatabase;
    private IMongoCollection<BsonDocument> accountCollection;

    private string[] questions = { 
        "臭肚魚為何稱為臭肚魚?", 
        "下面哪個生物不會住在河口區?", 
        "海蛞蝓跟劍尖槍魷都是軟體動物，請問哪一個比較容易在白天發現?" 
    };

    private List<List<string>> answers = new List<List<string>>()
    {
        new List<string> { 
            "臭肚魚被捕時容易受驚，進而死亡，因而消費者買到時已經臭掉了", 
            "臭肚魚以藻類為食，一般漁民在清理魚肚時常常會聞到難聞的海藻發酵味道，因而得名" 
        },
        new List<string> { 
            "彈塗魚", "龍虎斑", "多鱗四指馬鮁(午仔魚)" 
        },
        new List<string> { 
            "海蛞蝓", "劍尖槍魷(透抽)"
        }
    };

    private List<string> explanations = new List<string>()
    {
        "臭肚魚以藻類為食，一般漁民在清理魚肚時常常會聞到難聞的海藻發酵味道，因而得名",
        "龍虎斑通常生活在海洋的珊瑚礁或岩石區域",
        "海蛞蝓具有隱蔽的顏色和形狀，且多數生活在岩石或珊瑚中，不容易被發現 ; 劍尖槍魷因顏色鮮明、活動活躍，容易被發現"
    };

    private int[] correctAnswers = { 1, 1, 0 }; // 正確選項索引

    

    void Start()
    {
        client = new MongoClient("mongodb+srv://popo:K5q4fl0en5NzhkLq@unity.yrrt9gw.mongodb.net/?retryWrites=true&w=majority&appName=unity");
        userDatabase = client.GetDatabase("UserDatabase");
        accountCollection = userDatabase.GetCollection<BsonDocument>("UserAccounts");
        
        // Confirm Panel
        confirmPanel.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        // Game Panel
        gamePanel.SetActive(false); 
        nextButton.gameObject.SetActive(false);
        explanationText.gameObject.SetActive(false);
        
        
        // Result Panel
        resultPanel.SetActive(false);
        avatarManager = databaseManager.GetComponent<AvatarManager>();
        prizeController = prize.GetComponent<PrizeController>();

        // Parameters
        totalQuestions = questions.Length;

        componentDisabler = manager.GetComponent<ComponentDisabler>();
        startFirstGame = treasure.GetComponent<StartFirstGame>();
    }

    void ShowQuestion(int index)
    {
        if (index < 0 || index >= questions.Length)
        {
            Debug.LogError("Question index is out of range.");
            return;
        }

        questionText.text = questions[index];
        List<string> currentAnswers = answers[index];
        int numOptions = currentAnswers.Count;

        // 更新進度文本
        UpdateProgressText(index + 1, questions.Length);
        foreach (var button in answerButtons)
        {
            ResetAnswerButton(button);
        }
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < numOptions)
            {
                answerButtons[i].gameObject.SetActive(true);
                TMP_Text buttonText = answerButtons[i].GetComponentInChildren<TMP_Text>();
                buttonText.text = currentAnswers[i];

                int localQuestionIndex = index;
                int localAnswerIndex = i;

                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(localQuestionIndex, localAnswerIndex));
                ResetAnswerButton(answerButtons[i]);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }


    void UpdateProgressText(int currentQuestion, int totalQuestions)
    {
        // 答題進度
        progressText.text = $"{currentQuestion}/{totalQuestions}";
    }

    void OnAnswerSelected(int questionIndex, int answerIndex)
    {
        if (answerIndex < 0 || answerIndex >= answerButtons.Length)
        {
            Debug.LogError($"Answer index {answerIndex} is out of bounds for answerButtons array.");
            return;
        }

        bool isCorrect = answerIndex == correctAnswers[questionIndex];

        if (answerIndex >= answerButtons.Length || correctAnswers[questionIndex] >= answerButtons.Length)
        {
            Debug.LogError("Correct answer index is out of bounds for answerButtons array.");
            return;
        }

        ShowAnswerFeedback(answerButtons[answerIndex], isCorrect);

        // 顯示正確答案
        ShowAnswerFeedback(answerButtons[correctAnswers[questionIndex]], true);

        // 顯示解析
        explanationText.text = explanations[questionIndex];
        explanationText.gameObject.SetActive(true);
        
        // 更新得分
        if (isCorrect)
        {
            totalCorrectAnswers++;
        }

        // 隱藏選項按鈕，顯示下一題按鈕
        foreach (var button in answerButtons)
        {
            button.interactable = false; // 禁用選項按鈕
        }
        nextButton.gameObject.SetActive(true);

        // 設置下一題按鈕的點擊事件
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() => OnNextQuestion());
    }

    void ShowAnswerFeedback(Button button, bool isCorrect)
    {
        Image background = button.GetComponent<Image>(); // 假設按鈕背景是 Image 元件
        Transform symbol = button.transform.Find("Symbol"); // 假設符號是按鈕的子物件

        if (isCorrect)
        {
            background.color = Color.green; // 將背景設為綠色
            symbol.GetComponent<Image>().sprite = correctSymbol; // 設置打勾符號
        }
        else
        {
            background.color = Color.red; // 將背景設為紅色
            symbol.GetComponent<Image>().sprite = incorrectSymbol; // 設置打叉符號
        }

        symbol.gameObject.SetActive(true); // 顯示符號
    }

    void ResetAnswerButton(Button button)
    {
        Image background = button.GetComponent<Image>(); 
        if (background != null)
        {
            background.color = new Color32(242, 242, 242, 255); // 設置顏色為 #F2F2F2
        }
        Transform symbol = button.transform.Find("Symbol"); // 假設符號是按鈕的子物件
        if (symbol != null)
        {
            symbol.gameObject.SetActive(false); // 隱藏符號
        }
        button.interactable = true; // 確保按鈕可用
    }


    void OnNextQuestion()
    {
        explanationText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResult();
        }
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);
        gamePanel.SetActive(false);

        // 顯示使用者資訊
        usernameText.text = $"{UserData.Instance.Username}";
        Sprite avatarSprite = avatarManager.GetAvatar(UserData.Instance.AvatarIndex);
        if (avatarSprite != null)
        {
            avatarImage.sprite = avatarSprite;
        }
        else
        {
            // 預設頭像
            avatarImage.sprite = avatarManager.GetAvatar(0);
        }
        
        // 顯示答對的題數
        string correct = $"{totalCorrectAnswers} / {totalQuestions}";
        correctAnswersText.text = correct;
        if (totalCorrectAnswers == totalQuestions)
        {
            prizeText.text = $"恭喜獲得收藏品";
            confirmPrizeButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(false);
            confirmPrizeButton.onClick.AddListener(() => OnConfirmPrizeButtonClick());
        }
        else
        {
            prizeText.text = $"真可惜這次沒有獲得收藏品";
            confirmPrizeButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            retryButton.onClick.AddListener(() => OnRetryButtonClick());
        }
    }

    public void OnConfirmButtonClick()
    {
        confirmPanel.SetActive(false);
        gamePanel.SetActive(true); // 顯示題目
        ShowQuestion(currentQuestionIndex);

    }

    public void OnCancelButtonClick()
    {
        confirmPanel.SetActive(false); 
        componentDisabler.EnableComponents();
        startFirstGame.CloseTreasure();
    }

    void OnConfirmPrizeButtonClick()
    {
        prizeController.ShowPrize();
        AddPrizeToUser("PrizeName");
    }

    void OnRetryButtonClick()
    {
        totalCorrectAnswers = 0;
        currentQuestionIndex = 0;
        resultPanel.SetActive(false);
        gamePanel.SetActive(true);
        ShowQuestion(currentQuestionIndex);
    } 

    private async void AddPrizeToUser(string prizeName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("username", UserData.Instance.Username);
        var update = Builders<BsonDocument>.Update.AddToSet("prizes", prizeName);

        await accountCollection.UpdateOneAsync(filter, update);

        UserData.Instance.Prizes.Add(prizeName);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    [Header("Confirmation Panel")]
    public GameObject confirmPanel; 
    public Button confirmButton; // 确认按钮
    public Button cancelButton;  // 取消按钮
    
    [Header("Game Panel")]
    public GameObject gamePanel;
    public TMP_Text questionText;
    
    [Header("Result Panel")]
    public GameObject resultPanel;
    public GameObject prize;
    public TextMeshProUGUI usernameText;
    public Image avatarImage;
    public TextMeshProUGUI correctAnswersText;
    public TextMeshProUGUI prizeText;
    public Button confirmPrizeButton;
    //public Button retryButton;
    
    [Header("Managers")]
    public GameObject databaseManager;
    private AvatarManager avatarManager;
    private PrizeController prizeController;

    [Header("Card Settings")]
    public GameObject cardPrefab;
    public Transform cardGrid;
    public Sprite[] cardSprites;
    public int rows = 4;
    public int columns = 4;

    private List<GameObject> cards = new List<GameObject>();
    private List<GameObject> flippedCards = new List<GameObject>();

    void Start()
    {
        InitializeCards();
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        confirmPrizeButton.onClick.AddListener(OnConfirmPrizeButtonClick);
        avatarManager = databaseManager.GetComponent<AvatarManager>();
        prizeController = databaseManager.GetComponent<PrizeController>();
    }

    void InitializeCards()
    {
        List<Sprite> sprites = new List<Sprite>(cardSprites);
        sprites.AddRange(cardSprites);

        for (int i = 0; i < rows * columns; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardGrid);
            Image frontImage = cardObj.transform.Find("FrontImage").GetComponent<Image>();
            Image backImage = cardObj.transform.Find("BackImage").GetComponent<Image>();
            if (frontImage == null || backImage == null)
            {
                Debug.LogError("FrontImage or BackImage component not found on cardPrefab");
                continue;
            }

            // Set initial card sprite
            frontImage.sprite = null; // Set an empty sprite initially
            backImage.enabled = true;

            // Add click listener to card
            Button cardButton = cardObj.GetComponent<Button>();
            if (cardButton != null)
            {
                cardButton.onClick.AddListener(() => OnCardClicked(cardObj));
            }
            else
            {
                Debug.LogError("Button component not found on cardPrefab");
            }

            // Store card gameObject in the list
            int spriteIndex = Random.Range(0, sprites.Count);
            frontImage.sprite = sprites[spriteIndex];
            sprites.RemoveAt(spriteIndex);
            cards.Add(cardObj);
        }
    }

    public void OnCardClicked(GameObject cardObj)
    {
        if (flippedCards.Count >= 2 || cardObj == null)
            return;

        Image frontImage = cardObj.transform.Find("FrontImage").GetComponent<Image>();
        Image backImage = cardObj.transform.Find("BackImage").GetComponent<Image>();
        if (frontImage == null || backImage == null)
        {
            Debug.LogError("FrontImage or BackImage component not found on clicked card");
            return;
        }

        if (flippedCards.Contains(cardObj))
            return;

        FlipCard(cardObj, true);
        flippedCards.Add(cardObj);

        if (flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    void FlipCard(GameObject cardObj, bool isFlipped)
    {
        Image frontImage = cardObj.transform.Find("FrontImage").GetComponent<Image>();
        Image backImage = cardObj.transform.Find("BackImage").GetComponent<Image>();
        if (frontImage == null || backImage == null)
        {
            Debug.LogError("FrontImage or BackImage component not found on card");
            return;
        }

        frontImage.enabled = isFlipped;
        backImage.enabled = !isFlipped;
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        var card1 = flippedCards[0];
        var card2 = flippedCards[1];

        Image frontImage1 = card1.transform.Find("FrontImage").GetComponent<Image>();
        Image frontImage2 = card2.transform.Find("FrontImage").GetComponent<Image>();

        if (frontImage1.sprite == frontImage2.sprite)
        {
            Destroy(card1);
            Destroy(card2);
        }
        else
        {
            FlipCard(card1, false);
            FlipCard(card2, false);
        }

        flippedCards.Clear();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        foreach (GameObject cardObj in cards)
        {
            if (cardObj != null)
                return;
        }

        Debug.Log("Game Over!");
        ShowResult();
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);
        gamePanel.SetActive(false);

        usernameText.text = $"{UserData.Instance.Username}";
        Sprite avatarSprite = avatarManager.GetAvatar(UserData.Instance.AvatarIndex);
        if (avatarSprite != null)
        {
            avatarImage.sprite = avatarSprite;
        }
        else
        {
            avatarImage.sprite = avatarManager.GetAvatar(0);
        }
    }

    public void OnConfirmButtonClick()
    {
        confirmPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void OnCancelButtonClick()
    {
        confirmPanel.SetActive(false);
    }

    void OnConfirmPrizeButtonClick()
    {
        prizeController.ShowPrize();
    }
}

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private List<Card> allCards = new List<Card>();
    private List<int> cardIndices = new List<int>();
    private Card firstSelectedCard = null;
    private Card secondSelectedCard = null;
    private bool isCheckingMatch = false; // 是否正在檢查配對

    [Header("Confirmation Panel")]
    public GameObject confirmPanel;
    public Button confirmButton; // 確認按鈕
    public Button cancelButton; // 取消按鈕

    [Header("Game Panel")]
    public GameObject gamePanel;
    public Transform cardParent;
    public GameObject cardPrefab; 
    public Sprite cardBack; // 卡片背面
    public Sprite[] cardFronts; // 卡片正面

    [Header("Result Panel")]
    public GameObject resultPanel; 
    public GameObject prize;
    public TextMeshProUGUI usernameText; 
    public Image avatarImage;  
    public TextMeshProUGUI prizeText; 
    public Button confirmPrizeButton; // 確認獎品按鈕

    [Header("Managers")]
    public GameObject manager; 
    public GameObject databaseManager; 
    private AvatarManager avatarManager;
    private PrizeController prizeController;
    private ComponentDisabler componentDisabler;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Confirm Panel
        confirmPanel.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        // Game Panel
        gamePanel.SetActive(false); 
        
        
        // Result Panel
        resultPanel.SetActive(false);
        avatarManager = databaseManager.GetComponent<AvatarManager>();
        prizeController = prize.GetComponent<PrizeController>();

        componentDisabler = manager.GetComponent<ComponentDisabler>();
    }

    private void SetupCards()
    {
        for (int i = 0; i < cardFronts.Length; i++)
        {
            cardIndices.Add(i);
            cardIndices.Add(i);
        }

        Shuffle(cardIndices);

        for (int i = 0; i < cardIndices.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            Card cardComponent = newCard.GetComponent<Card>();
            cardComponent.SetCard(cardIndices[i], cardFronts[cardIndices[i]], cardBack);
            allCards.Add(cardComponent);
        }
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public bool CardSelected(Card selectedCard)
    {
        if (isCheckingMatch) 
        {
            return true; // 正在檢查配對
        }
        else 
        {
            if (firstSelectedCard == null)
            {
                firstSelectedCard = selectedCard;
            }
            else if (secondSelectedCard == null)
            {
                secondSelectedCard = selectedCard;
                StartCoroutine(CheckMatch());
            }
            return false;
        }
    }

    private IEnumerator<WaitForSeconds> CheckMatch()
    {
        isCheckingMatch = true; // 開始檢查配對
        yield return new WaitForSeconds(1f);

        if (firstSelectedCard.cardIndex == secondSelectedCard.cardIndex)
        {
            firstSelectedCard.HideCard();
            secondSelectedCard.HideCard();
        }
        else
        {
            firstSelectedCard.Flip();
            secondSelectedCard.Flip();
        }

        firstSelectedCard = null;
        secondSelectedCard = null;
        isCheckingMatch = false; // 結束檢查配對

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        bool allMatched = true;
        foreach (Card card in allCards)
        {
            if (!card.isMatched)
            {
                allMatched = false;
                break;
            }
        }

        if (allMatched)
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
        
        prizeText.text = $"恭喜獲得收藏品";
        confirmPrizeButton.gameObject.SetActive(true);
        confirmPrizeButton.onClick.AddListener(() => OnConfirmPrizeButtonClick());
    }

    public void OnConfirmButtonClick()
    {
        confirmPanel.SetActive(false);
        gamePanel.SetActive(true); 
        SetupCards();

    }

    public void OnCancelButtonClick()
    {
        confirmPanel.SetActive(false); 
        componentDisabler.EnableComponents();
    }
    
    void OnConfirmPrizeButtonClick()
    {
        prizeController.ShowPrize();
    }
}

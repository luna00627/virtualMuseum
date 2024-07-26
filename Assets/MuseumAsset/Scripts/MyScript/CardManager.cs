using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public GameObject cardPrefab;
    public Transform cardParent;
    public Sprite[] cardFronts;
    public Sprite cardBack;
    public GameObject confirmPanel;

    private List<Card> allCards = new List<Card>();
    private List<int> cardIndices = new List<int>();
    private Card firstSelectedCard = null;
    private Card secondSelectedCard = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetupCards();
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

    public void CardSelected(Card selectedCard)
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
    }

    private IEnumerator<WaitForSeconds> CheckMatch()
    {
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

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        foreach (Card card in allCards)
        {
            if (card.gameObject.activeSelf)
            {
                return;
            }
        }

        Debug.Log("You win!");
    }
}

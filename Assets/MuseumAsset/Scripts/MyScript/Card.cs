using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardIndex;
    public Sprite cardFront;
    public Sprite cardBack;

    private bool isIgnoreFlip = false;
    private Image cardImage;
    private Button cardButton;
    private bool isFlipped = false;
    public bool isMatched { get; private set; } = false;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(OnCardClicked);
    }

    public void SetCard(int index, Sprite front, Sprite back)
    {
        cardIndex = index;
        cardFront = front;
        cardBack = back;
        cardImage.sprite = cardBack;
    }

    private void OnCardClicked()
    {
        if (!isFlipped)
        {
            isIgnoreFlip = CardManager.Instance.CardSelected(this);
            if (isIgnoreFlip) return;

            Flip();

        }
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        cardImage.sprite = isFlipped ? cardFront : cardBack;
    }

    public void HideCard()
    {
        cardImage.enabled = false;
        isMatched = true;
    }
}

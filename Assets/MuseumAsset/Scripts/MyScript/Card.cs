using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardIndex;
    public Sprite cardFront;
    public Sprite cardBack;

    private Image cardImage;
    private Button cardButton;
    private bool isFlipped = false;

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
            Flip();
            CardManager.Instance.CardSelected(this);
        }
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        cardImage.sprite = isFlipped ? cardFront : cardBack;
    }

    public void HideCard()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    void Start()
    {
        dialogueBox.SetActive(false); // 初始時隱藏對話框
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialogueBox.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
    }
}

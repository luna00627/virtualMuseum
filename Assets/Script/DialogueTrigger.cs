using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueBox dialogueBoxScript; // 連接DialogueBox腳本
    public string dialogueMessage; // 需要顯示的對話內容

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 確保是玩家進入觸發區域
        {
            dialogueBoxScript.ShowDialogue(dialogueMessage);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartFirstGame : MonoBehaviour
{
    public GameObject manager;

    [Header("Chest States")]
    public GameObject closedChest; 
    public GameObject openedChest;

    [Header("Camera")]
    public Camera mainCamera; 

    [Header("Canvas")]
    public Canvas gameCanvas;
    private QuizManager quizManager;
    private bool isInfo = false;
    private ComponentDisabler componentDisabler;

    void Start()
    {
        openedChest.SetActive(false); 
        quizManager = gameCanvas.GetComponent<QuizManager>();
        componentDisabler = manager.GetComponent<ComponentDisabler>();
    }

    public void OpenChest()
    {
        closedChest.SetActive(false);
        openedChest.SetActive(true); 
        StartCoroutine(ShowConfirmPanelWithDelay(0.5f)); // 延遲0.5秒顯示確認畫面
    }

    IEnumerator ShowConfirmPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        quizManager.confirmPanel.SetActive(true);
        componentDisabler.DisableComponents();
    }

    void CloseChest()
    {
        closedChest.SetActive(true);
        openedChest.SetActive(false); 
    }

    
}

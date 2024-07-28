using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartSecondGame : MonoBehaviour
{
    public GameObject manager;

    [Header("Chest States")]
    public GameObject closedChest; 
    public GameObject openedChest;

    [Header("Camera")]
    public Camera mainCamera; 

    [Header("Canvas")]
    public Canvas gameCanvas;
    private CardManager cardManager;
    private bool isInfo = false;
    private ComponentDisabler componentDisabler;

    void Start()
    {
        openedChest.SetActive(false); 
        cardManager = gameCanvas.GetComponent<CardManager>();
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
        cardManager.confirmPanel.SetActive(true);
        componentDisabler.DisableComponents();
    }

    void CloseChest()
    {
        closedChest.SetActive(true);
        openedChest.SetActive(false); 
    }

    
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartSecondGame : MonoBehaviour
{
    public GameObject manager;

    [Header("Treasure States")]
    public GameObject closedTreasure; 
    public GameObject openedTreasure;

    [Header("Camera")]
    public Camera mainCamera; 

    [Header("Canvas")]
    public Canvas gameCanvas;
    private CardManager cardManager;
    private bool isInfo = false;
    private ComponentDisabler componentDisabler;

    void Start()
    {
        openedTreasure.SetActive(false); 
        cardManager = gameCanvas.GetComponent<CardManager>();
        componentDisabler = manager.GetComponent<ComponentDisabler>();
    }

    public void OpenTreasure()
    {
        closedTreasure.SetActive(false);
        openedTreasure.SetActive(true); 
        StartCoroutine(ShowConfirmPanelWithDelay(0.5f)); // 延遲0.5秒顯示確認畫面
    }

    IEnumerator ShowConfirmPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        cardManager.confirmPanel.SetActive(true);
        componentDisabler.DisableComponents();
    }

    public void CloseTreasure()
    {
        closedTreasure.SetActive(true);
        openedTreasure.SetActive(false); 
    }

    
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject closedChest; 
    public GameObject openedChest; 
    public GameObject confirmPanel;
    public Button confirmButton; // 確認按鈕
    public Button cancelButton;  // 取消按鈕
    public GameObject quizPanel; // 顯示題目和選項的 Panel

    private bool isInfo = false;

    void Start()
    {
        openedChest.SetActive(false); 
        confirmPanel.SetActive(false);
        quizPanel.SetActive(false); // 確保在遊戲開始前不顯示題目面板
        
        // 設置按鈕點擊事件
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
    }

    void Update()
    {
        Vector3 pos = Input.mousePosition;
        Ray mouseRay = mainCamera.ScreenPointToRay(pos);

        if (!isInfo && Input.GetMouseButtonDown(0))
        {
            RaycastHit hitObj;
            if (Physics.Raycast(mouseRay, out hitObj))
            {
                if (hitObj.collider.CompareTag("prize"))
                {
                    OpenChest();
                }
            }
        }
    }

    void OpenChest()
    {
        closedChest.SetActive(false);
        openedChest.SetActive(true); 
        StartCoroutine(ShowConfirmPanelWithDelay(0.5f)); // 延遲0.5秒顯示確認畫面
    }

    IEnumerator ShowConfirmPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        confirmPanel.SetActive(true);
    }

    void CloseChest()
    {
        closedChest.SetActive(true);
        openedChest.SetActive(false); 
    }

    public void OnConfirmButtonClick()
    {
        confirmPanel.SetActive(false);
        quizPanel.SetActive(true); // 顯示題目面板
    }

    public void OnCancelButtonClick()
    {
        confirmPanel.SetActive(false); 
        CloseChest();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public Camera mainCamera;
    //public GameObject manager;
    //public GameObject selectRoomCanvas;
    public GameObject closedChest; 
    public GameObject openedChest; 
    public GameObject confirmPanel;
    private bool isInfo = false;
    // private GameObject player;
    // private GameObject discussionObject;
    // private GameObject descriptionObject;

    void Start()
    {
        openedChest.SetActive(false); 
        confirmPanel.SetActive(false);
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
                print(hitObj.collider.gameObject.tag);
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
    }

    public void OnCancelButtonClick()
    {
        confirmPanel.SetActive(false); 
        CloseChest();
    }
}

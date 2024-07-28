using UnityEngine;

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] descriptionCanvases; 
    public Camera mainCamera;
    public Camera infoCamera;
    public GameObject selectRoomCanvas;
    private GameObject player;
    private GameObject discussionObject;
    private GameObject descriptionObject;
    private RaycastManager raycastManager;

    void Start()
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            canvas.SetActive(false);
        }
        infoCamera.gameObject.SetActive(false);
        raycastManager = manager.GetComponent<RaycastManager>();
    }

    public void ShowInfoCanvas(string objectName)
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            if (canvas.name == objectName)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }

        discussionObject = GameObject.Find("討論");
        descriptionObject = GameObject.Find("說明");
        player = GameObject.Find("PlayerArmature 1(Clone)");

        
        
        selectRoomCanvas.SetActive(false);
        infoCamera.gameObject.SetActive(true);
        raycastManager.isInfo = true;

        discussionObject.SetActive(false);
        descriptionObject.SetActive(true);
        player.SetActive(false);
    }

    public void HideInfoCanvas()
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            canvas.SetActive(false);
        }

        player.SetActive(true);
        selectRoomCanvas.SetActive(true);
        infoCamera.gameObject.SetActive(false);
        descriptionObject.SetActive(true);
        discussionObject.SetActive(true);
        raycastManager.isInfo = false;
    }
}

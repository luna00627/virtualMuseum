using UnityEngine;

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject[] descriptionCanvases; 
    public Camera mainCamera;
    public Camera infoCamera;
    public GameObject manager;
    public GameObject selectRoomCanvas;
    private bool isInfo = false;
    private GameObject player;
    private GameObject discussionObject;
    private GameObject descriptionObject;

    void Start()
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            canvas.SetActive(false);
        }
        infoCamera.gameObject.SetActive(false);
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
                if (hitObj.collider.CompareTag("poster"))
                {
                    string objectName = hitObj.transform.parent.gameObject.name;
                    print(objectName);
                    ShowInfoCanvas(objectName);
                }
            }
        }
    }

    void ShowInfoCanvas(string objectName)
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
        isInfo = true;

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
        isInfo = false;
    }
}

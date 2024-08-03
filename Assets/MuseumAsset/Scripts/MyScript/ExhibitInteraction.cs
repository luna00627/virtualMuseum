using UnityEngine;

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] descriptionCanvases; 
    public Camera mainCamera;
    public Camera infoCamera;
    private GameObject player;
    private GameObject discussionObject;
    private GameObject descriptionObject;
    private RaycastManager raycastManager;
    private ComponentDisabler componentDisabler;

    void Start()
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            canvas.SetActive(false);
        }
        infoCamera.gameObject.SetActive(false);
        raycastManager = manager.GetComponent<RaycastManager>();
        componentDisabler = manager.GetComponent<ComponentDisabler>();
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

        infoCamera.gameObject.SetActive(true);
        raycastManager.isInfo = true;

        discussionObject.SetActive(false);
        descriptionObject.SetActive(true);
        player.SetActive(false);
        componentDisabler.DisableComponents();
    }

    public void HideInfoCanvas()
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            canvas.SetActive(false);
        }

        player.SetActive(true);
        infoCamera.gameObject.SetActive(false);
        descriptionObject.SetActive(true);
        discussionObject.SetActive(true);
        raycastManager.isInfo = false;
        componentDisabler.EnableComponents();
    }
}

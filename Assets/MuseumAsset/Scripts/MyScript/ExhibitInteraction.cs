using UnityEngine;

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject[] descriptionCanvases; // 所有展品对应的描述画布数组
    public Camera mainCamera;
    public Camera infoCamera;
    public GameObject manager;
    public GameObject selectRoomCanvas;
    private bool isInfo = false;

    void Start()
    {
        // 初始时隐藏所有描述画布和信息相机
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
        selectRoomCanvas.SetActive(false);
        infoCamera.gameObject.SetActive(true);
        isInfo = true;
    }

    public void HideInfoCanvas()
    {
        foreach (GameObject canvas in descriptionCanvases)
        {
            canvas.SetActive(false);
        }
        selectRoomCanvas.SetActive(true);
        infoCamera.gameObject.SetActive(false);
        isInfo = false;
    }
}

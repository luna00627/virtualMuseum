using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaycastManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject manager;
    public GameObject firstTreasure;
    public GameObject secondTreasure;

    public bool isInfo = false;
    private ExhibitInteraction exhibitInteraction;
    private StartFirstGame startFirstGame;
    private StartSecondGame startSecondGame;
    
    void Start()
    {
        exhibitInteraction = manager.GetComponent<ExhibitInteraction>();
        startFirstGame = firstTreasure.GetComponent<StartFirstGame>();
        startSecondGame = secondTreasure.GetComponent<StartSecondGame>();
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
                // if (hitObj.collider.CompareTag("prize1"))
                // {
                //     OpenTreasure();
                // }
                string hitTag = hitObj.collider.gameObject.tag;
                switch (hitTag)
                {
                    case "Exhibits":
                        //HandleExhibitClick(hitObj.collider.gameObject);
                        string objectName = hitObj.transform.parent.gameObject.name;
                        print(objectName);
                        exhibitInteraction.ShowInfoCanvas(objectName);
                        break;

                    case "prize1":
                        startFirstGame.OpenTreasure();
                        break;

                    case "prize2":
                        startSecondGame.OpenTreasure();
                        break;

                    default:
                        Debug.Log($"Unknown tag: {hitTag}");
                        break;
                }
            }
        }
    }
}

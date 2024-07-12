using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OceanPoster : MonoBehaviour
{
    Canvas InfoCanvas;
    GameObject TitleButton;
    Text TitleText;
    public Camera cam;
    public Sprite[] poster;
    public Image image;
    RaycastHit hitObj;
    void Start()
    {
        InfoCanvas = GameObject.Find("OceanPosterCanvas").GetComponent<Canvas>();
        InfoCanvas.enabled = false;
        TitleButton = GameObject.Find("OceanPosterCanvas").transform.Find("TitleButton").gameObject;
        TitleText = TitleButton.transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = Input.mousePosition;
        Ray mouseRay = cam.ScreenPointToRay(pos);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mouseRay, out hitObj))
            {
                print(hitObj.collider.gameObject.tag);
                if (hitObj.collider.gameObject.tag == "poster"){
                    for(int i = 0; i < 1; i++){
                        if(hitObj.collider.gameObject.name == poster[i].name){
                            changeTitleButtonName(hitObj.collider.gameObject.name);
                            image.sprite = poster[i];
                            InfoCanvas.enabled = true; 
                            break;
                        }
                    }
                }
            }
        }
    }

    public void showInfoCanvas(string tag)
    {
        //Debug.Log(tag);
        int index = int.Parse(tag);
        image.sprite = poster[index];
        InfoCanvas.enabled = true;
    }
    public void hideInfoCanvas()
    {
        InfoCanvas.enabled = false;
    }

    public void changeTitleButtonName(string name){
        TitleText.text = name;
    }
}
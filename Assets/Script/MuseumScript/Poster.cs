using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poster : MonoBehaviour
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
        InfoCanvas = GameObject.Find("PosterCanvas").GetComponent<Canvas>();
        InfoCanvas.enabled = false;
        TitleButton = GameObject.Find("PosterCanvas").transform.Find("TitleButton").gameObject;
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
                    for(int i = 0; i < 56; i++){
                        if(hitObj.collider.gameObject.name == poster[i].name){
                            changeTitleButtonName(hitObj.collider.gameObject.name);
                            image.sprite = poster[i];
                            InfoCanvas.enabled = true; 
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
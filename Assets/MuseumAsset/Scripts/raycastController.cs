using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastController : MonoBehaviour
{
    // Start is called before the first frame update
    string name = "";
    string tempname = "";
    float timer = 0.0f;
    Transform remindTransform; //提醒
    Transform remindChildTransform;
    GameObject remindGameObject;
    Transform otherTransform; //介紹
    Transform otherChildTransform; 
    GameObject otherChildObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
            if(Input.GetMouseButtonDown(0)){
                otherTransform = hit.collider.gameObject.transform;
                otherChildTransform = otherTransform.Find("infoCanvas");
                if(otherChildTransform != null){
                    otherChildObject = otherChildTransform.gameObject;
                    otherChildObject.SetActive(true);
                    Debug.Log(otherChildObject);
                }  
            }
        }
    }

    public void hideBtn(){
        otherChildObject.SetActive(false);
    }
}

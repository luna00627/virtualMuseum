using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class positionTest : MonoBehaviour
{
    // Start is called before the first frame update
    string projectName = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(projectTP.flag1){
            changePosition("project1");
        }
        if(projectTP.flag2){
            changePosition("project2");
        }
        if(projectTP.flag3){
            changePosition("project3");
        }
        if(projectTP.flag4){
            changePosition("project4");
        }
        if(projectTP.flag5){
            changePosition("project5");
        }
    }

    void changePosition(string project){
        if(GetComponent<NetworkObject>().HasStateAuthority){
            if(project == "project1"){
                projectName = "project1";
                transform.position = new Vector3(-75, 1.1f, 50);
                Invoke("check", 0.2f);
                projectTP.flag1 = false;
            }
            else if(project == "project2"){
                Debug.Log("展場2");
                projectName = "project2";
                transform.position = new Vector3(-105, 1.1f, 50);
                Invoke("check", 0.2f);
                projectTP.flag2 = false;
            }
            else  if(project == "project3"){
                projectName = "project3";
                transform.position = new Vector3(-135, 1.1f, 50);
                Invoke("check", 0.2f);
                projectTP.flag3 = false;
            }
            else if(project == "project4"){
                projectName = "project4";
                transform.position = new Vector3(-165, 1.1f, 50);
                Invoke("check", 0.2f);
                projectTP.flag4 = false;
            }
            else if(project == "project5"){
                projectName = "project5";
                transform.position = new Vector3(-195, 1.1f, 50);
                Invoke("check", 0.2f);
                projectTP.flag5 = false;
            }
            Debug.Log(transform.position);
        }
    }

    void check(){
        Debug.Log("check");
        Debug.Log(projectName);
        if(projectName == "project1"){
            if(transform.position.x != -75){
                transform.position = new Vector3(-75, 1.1f, 50);
                Invoke("check", 0.01f);
            }
            else{
                projectTP.flag1 = false;
            }
        }
        if(projectName == "project2"){
            if(transform.position.x != -105){
                transform.position = new Vector3(-105, 1.1f, 50);
                Invoke("check", 0.01f);
            }
            else{
                projectTP.flag2 = false;
            }
        }
        if(projectName == "project3"){
            if(transform.position.x != -135){
                transform.position = new Vector3(-135, 1.1f, 50);
                Invoke("check", 0.01f);
            }
            else{
                projectTP.flag3 = false;
            }
        }
        if(projectName == "project4"){
            if(transform.position.x != -165){
                transform.position = new Vector3(-165, 1.1f, 50);
                Invoke("check", 0.01f);
            }
            else{
                projectTP.flag4 = false;
            }
        }
        if(projectName == "project5"){
            if(transform.position.x != -195){
                transform.position = new Vector3(-195, 1.1f, 50);
                Invoke("check", 0.01f);
            }
            else{
                projectTP.flag5 = false;
            }
        }
    }
}


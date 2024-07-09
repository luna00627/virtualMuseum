using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class projectTP : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool flag1 = false;
    static public bool flag2 = false;
    static public bool flag3 = false;
    static public bool flag4 = false;
    static public bool flag5 = false;
    GameObject entity = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void transportRoom(string name){
        GameObject[] myObjArray;
        myObjArray = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < myObjArray.Length; i++){
            if(myObjArray[i].GetComponent<NetworkObject>().HasStateAuthority){
                if(name == "project1"){
                    projectTP.flag1 = true;
                }
                else if(name == "project2"){
                    projectTP.flag2 = true;
                }
                else if(name == "project3"){
                    projectTP.flag3 = true;
                }
                else if(name == "project4"){
                    projectTP.flag4 = true;
                }
                else if(name == "project5"){
                    projectTP.flag5 = true;
                }
            }
        }  
    }
}

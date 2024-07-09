using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text timetext;
    float cleartime;
    //Canvas overCanvas;
    public GameObject endPanel;
    //public GameObject player;
    bool stop = true;
    // Update is called once per frame
   /* void Start()
    {
        overCanvas = GameObject.Find("OverCanvas").GetComponent<Canvas>();
    }*/

    void Update()
    {
        if (endPanel.activeSelf) { 
            GameObject.Find("Time").GetComponent<Text>().text = timetext.text; 
            stop = true; 
        }
        if (Input.GetKey(KeyCode.Q)) stop = false;
        if (Input.GetKey(KeyCode.X)) stop = true;
        
        if (stop)
        {
           
             return;
        }

        cleartime += Time.fixedDeltaTime;
        timetext.text = System.TimeSpan.FromSeconds(value: cleartime).ToString(format: @"mm\:ss\:ff");

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public GameObject player;
    public GameObject directcanvas;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        directcanvas.SetActive(false);
       // open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        //print("yes");
        
        if (other.gameObject == player)
        {
            //print("yes");
            if (!directcanvas.activeSelf) {
                directcanvas.SetActive(true);
               // open = true;   
            }
            else
            {
                directcanvas.SetActive(false);
                //open = false;
            }

        }
    }
}

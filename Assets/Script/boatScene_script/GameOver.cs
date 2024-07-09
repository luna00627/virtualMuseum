using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject overcanvas;
     bool m_IsPlayerAtExit;
     
    void Start()
    {
       // m_IsPlayerAtExit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            print("yes");
            overcanvas.SetActive(true);
        }
    }

}

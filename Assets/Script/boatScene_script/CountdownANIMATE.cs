using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownANIMATE : MonoBehaviour
{
    [SerializeField] Text timetext;
    int end = 20;
    // Start is called before the first frame update
    /*void Start()
    {
        timetext.enabled = false;
    }*/
    // Update is called once per frame
    void Update()
    {
        end =20-(int)Time.time;
        /*if (end > 0&&end<=10)
        {
            timetext.enabled = true;
            //end = 10 - (int)Time.time;
            timetext.text = end.ToString();
        }*/
        if (end == 0)
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    
    }
}

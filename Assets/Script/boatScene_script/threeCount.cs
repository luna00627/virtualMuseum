using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class threeCount : MonoBehaviour
{
    [SerializeField] Text timetext;
    int end = 3;
    bool stop = true;
    float check = 0;
    // Start is called before the first frame update
    void Start()
    {
        timetext.enabled = true;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Z)){
            stop = false;
            timetext.enabled = true;
        }*/

        if (end > 0 && !stop)
        {
            check += Time.deltaTime;
            if (check > 1) {
                check = 1;
                end = (int)(end - check);
                
                check = 0;
             }
           
             timetext.text = end.ToString();
            
            //end = 10 - (int)Time.time;
           
            
        }
        if (end == 0)
        {
            Destroy(gameObject);
        }
    }
}

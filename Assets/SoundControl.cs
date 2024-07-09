using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject soundON;
    public GameObject soundOff;
    bool on = true;
    public AudioSource audioSource;
    int count=0;
    void Start()
    {
        soundOff.SetActive(false);
        soundON.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.D)&&on&&count==0)
        {
            soundON.SetActive(false);
            soundOff.SetActive(true);
            on = false;
            count = 30;
            audioSource.Stop();
        }
          else if (Input.GetKey(KeyCode.D) && !on && count == 0)
        {
            soundOff.SetActive(false);
            soundON.SetActive(true);

            count = 30;
            on = true;
            audioSource.Play();
        }
        
        
        
        
        if(Input.GetKey(KeyCode.D)==false && count != 0)
        {
            count--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{
    [SerializeField] Text timetext;
    [SerializeField] Text textTen;
    public GameObject endPanel;
    static public int end = 280;
    bool stop = true;
    float check = 0;
    //public AudioClip tenmusic;
    public AudioSource audioSource;
    // AudioSource audiosource;
    int control = 0;
    // Start is called before the first frame update
    /*void Start()
    {
    }*/
    void Start()
    {
        textTen.enabled = false;
        endPanel.SetActive(false);
        //audioSource = GetComponent<AudioSource>();
        //audioSource = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)||Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)) stop = false;
        if (end > 0 && end <= 10&&!stop)
        {
            textTen.enabled = true;
            if (control==0)
            {
                audioSource.Play();
                control++;
            }
            
            textTen.text = end.ToString();
            
            //audioSource.PlayOneShot(tenmusic);
        }
        if (end > 0&&!stop) {
            //end = 15 - (int)Time.time;
            //TimeSpan timespan = timespan.FromSeconds(1000);
            //timer.GetComponent<Text>().text = string.Format("(0:D2),(1:D2),(2:D2)", timespan.Hours, timespan.Minutes, timespan.Seconds);
            check += Time.deltaTime;
            if (check > 1)
            {
                check = 1;
                end = (int)(end - check);

                check = 0;
            }
            timetext.text = System.TimeSpan.FromSeconds(value: end).ToString(format: @"mm\:ss");


        }
        if(end==0) endPanel.SetActive(true);
    }
}

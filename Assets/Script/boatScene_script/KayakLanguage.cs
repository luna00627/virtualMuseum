using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KayakLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    //public bool flag=true;
    public  Text StartTitle;
    public Text  PlayTitle;
    public Text  ContentTitle;
    public Text ExitTitle;
    public Text ContentTable;
    void Start()
    {
      // flag=startLanguage.Instance.flag;
       //Debug.Log(startLanguage.Instance);
       if(startLanguage.getInstance().flag){
            Chinese();
        }
        else{
            English();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void  Chinese(){
      StartTitle.text="歡迎來到獨木舟計時賽!!!!! ";
      PlayTitle.text="開始";
      ContentTitle.text="遊戲說明";
      ExitTitle.text="離開";
      ContentTable.text="Q:左手向前划\nW:右手向前划\nA:左手向後划\nS:右手向後划";

    }
    public void English(){
      StartTitle.text=" Welcome to the canoe racing competition!!!!!";
        PlayTitle.text="Play"; 
        ContentTitle.text="Game Instructions";
        ExitTitle.text="Exit";
        ContentTable.text="Q:Left hand forward stroke\nW:Right hand forward stroke\nA:Left hand backward stroke\nS:Right hand backward stroke";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    public  Text StartTitle;
    //public Text  PlayTitle;
    //public Text  ContentTitle;
    public Text ExitTitle;
    public Text loseTitle;

    public Text loseReturn;
    
    //public Text ContentTable;
    void Start()
    {
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
    public void Chinese(){
       StartTitle.text="恭喜完成獨木舟計時賽!!!!!";
       ExitTitle.text="按X離開遊戲";
       loseTitle.text="未在時間內完成遊戲!!!!";
       loseReturn.text="請按X離開遊戲";       
                  }
    public void English(){
        StartTitle.text="Congratulations on completing the canoe time trial race!!!!!";
       ExitTitle.text="Press X to exit the game";
       loseTitle.text="You did not complete the game within the time limit!";
       loseReturn.text="Please press X to exit the game."; 
    }
}

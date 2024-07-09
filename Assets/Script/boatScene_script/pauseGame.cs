using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class pauseGame : MonoBehaviour
{
   public GameObject PauseWindow;
   private bool isPause;
    // Start is called before the first frame update
    void Start()
    {
       isPause=false;
       PauseWindow.SetActive(false); 
    }

   
     
   void Update(){
       if(Input.GetKey(KeyCode.V))
            PauseGame();
         if(Input.GetKey(KeyCode.Z)) 
         continueGame();  
         if(Input.GetKey(KeyCode.X))
        leaveGame();
   }
    // Update is called once per frame
      void PauseGame(){
        isPause=!isPause;
       print("you click");
        if(isPause){
           PauseWindow.SetActive(true);
            Time.timeScale=0;
        }
        
    }
    public void continueGame(){
        isPause=false;
         Time.timeScale=1;
         PauseWindow.SetActive(false);
    }
    public void leaveGame(){
         Time.timeScale=1;
         SceneManager.LoadScene(2);
          
    }
      

}

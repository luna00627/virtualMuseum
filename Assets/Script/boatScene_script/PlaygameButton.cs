using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine.Networking;

public class PlaygameButton : MonoBehaviour
{
    /*Canvas readyCanvas;
    Canvas playCanvas;
    Canvas overCanvas;*/
    //Text word;
    public GameObject mainCam;
    public GameObject startPanel;
    public GameObject endPanel;
    public GameObject rule;
    public GameObject leave;
    public GameObject scene;
    public GameObject playCanvas;
    public GameObject player;
    public GameObject person;
    public GameObject endline;


    
    //public GameObject number;
    bool is_over = false;
    bool is_start = false;
    bool is_open = false;
    int count = 0;
    //Text time;
    // Start is called before the first frame update
    void Start()
    {
        /*readyCanvas = GameObject.Find("ReadyUI").GetComponent<Canvas>();
        playCanvas = GameObject.Find("GameUI").GetComponent<Canvas>();
        overCanvas= GameObject.Find("OverCanvas").GetComponent<Canvas>();*/
        //word = GameObject.Find("Tim");
        is_over = false;
        is_start = false;
        is_open = false;
        //number= gameObject.GetComponent<Sprite>();
        //time= GameObject.Find("Timer").GetComponent<Text>();
        /*readyCanvas.enabled = true;
        playCanvas.enabled = false;*/
        //overCanvas.enabled = false;
        /*mainCam.SetActive(true);
        startPanel.SetActive(true);
        endPanel.SetActive(false);
        rule.SetActive(false);
        scene.SetActive(false);
        playCanvas.SetActive(false);
        player.SetActive(false);*/

        GameStart();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       /* if (Input.GetKey(KeyCode.Z)) {
            if(!is_over && !is_start){
                //number.SetActive(true);
                is_start = true;
                GameStart();
            }
        }*/
        /*if (Input.GetKey(KeyCode.V)&&count==0)
        {
            if (!is_over && !is_start)
            {
                //number.SetActive(true);
                if (!is_open && count == 0)
                {
                    rule.SetActive(true);
                    is_open = true;
                }
                else if (is_open)
                {
                    rule.SetActive(false);
                    is_open = false;
                }
                

            }
            count = 20;
        }
        else if(Input.GetKey(KeyCode.V) && count != 0)
        {
            count--;
        }*/
        //test用
         /*if (Input.GetKey(KeyCode.Z)) {
            person.SetActive(false);
            
           



        }*/
        if (Input.GetKey(KeyCode.X)) {
             //player.SetActive(false);
            leave.SetActive(true);
            LeaveScene();
           



        }
    }
    public void GameOver() {
        mainCam.SetActive(true);
        endPanel.SetActive(true);
        scene.SetActive(false);
        playCanvas.SetActive(false);
        player.SetActive(false);
    }

    public void GameStart() {
        startPanel.SetActive(false);
        scene.SetActive(true);
        playCanvas.SetActive(true);
        player.SetActive(true);       
    }
    public void LeaveScene()
    {
        SceneManager.LoadScene(2);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            StartCoroutine(scoreRequest(CountDown.end));
            GameOver();
            
        }
    }
    public IEnumerator scoreRequest(int score){
        Debug.Log(scoreValue.id.ToString());
        WWWForm form = new WWWForm();
        form.AddField("id", scoreValue.id.ToString());
        form.AddField("score", score);
        string url = "http://hi-ntou-usr.cse.ntou.edu.tw:8895/submitScore";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success){
            Debug.Log(www.error);
        }
        else{
            // yield return StartCoroutine(getRank());
            Debug.Log("success");
        }

    }


    public IEnumerator getSelfRank(){
        WWWForm form = new WWWForm();
        form.AddField("id",scoreValue.id.ToString());
        UnityWebRequest www = new UnityWebRequest();
        www = UnityWebRequest.Post("http://hi-ntou-usr.cse.ntou.edu.tw:8895/myRank",form);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success){
            Debug.Log(www.downloadHandler.text);
        }
        else{
            myRank selfRank = JsonUtility.FromJson<myRank>(www.downloadHandler.text);
        }

    }

    // public IEnumerator getRank(){
    //     using(UnityWebRequest www = UnityWebRequest.Get("http://hi-ntou-usr.cse.ntou.edu.tw:8895/getScore")){
    //         yield return www.SendWebRequest();
    //         Debug.Log(www.downloadHandler.text);
    //     }
    // }

}


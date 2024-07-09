using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class StartControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject content;
    public GameObject rankUI;
    public Text rank1Name;
    public Text rank2Name;
    public Text rank3Name;
    public Text rank4Name;
    public Text rank5Name;
    public Text rank1Score;
    public Text rank2Score;
    public Text rank3Score;
    public Text rank4Score;
    public Text rank5Score;
    public RawImage rank1RawImage;
    public RawImage rank2RawImage;
    public RawImage rank3RawImage;
    public RawImage rank4RawImage;
    public RawImage rank5RawImage;
    public rank rankInfo;
    // Update is called once per frame
   
   public void GamePlay()
    {
        //print("click");
        SceneManager.LoadScene(6);
    }
    public void GameLeave()
    {
        SceneManager.LoadScene(1);
    }
    public void GameContent()
    {
        content.SetActive(true);
    }
    public void CloseContent()
    {
        content.SetActive(false);
    }
    public void showRank(){
        StartCoroutine(getRank());
        rankUI.SetActive(true);
    }
    public void exitRank(){
        rankUI.SetActive(false);
    }
    public IEnumerator getRank(){
        UnityWebRequest www = new UnityWebRequest();
        www = UnityWebRequest.Get("http://hi-ntou-usr.cse.ntou.edu.tw:8895/getScore");
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        if(www.result != UnityWebRequest.Result.Success){
            Debug.Log(www.downloadHandler.text);
        }
        else{
            Debug.Log(www.downloadHandler.text);
            rankInfo = JsonUtility.FromJson<rank>(www.downloadHandler.text);
            Debug.Log(rankInfo.leaderBoard[0].score);
            rank1Name.text = rankInfo.leaderBoard[0].name;
            rank2Name.text = rankInfo.leaderBoard[1].name;
            rank3Name.text = rankInfo.leaderBoard[2].name;
            rank4Name.text = rankInfo.leaderBoard[3].name;
            rank5Name.text = rankInfo.leaderBoard[4].name;
            rank1Score.text = rankInfo.leaderBoard[0].score.ToString();
            rank2Score.text = rankInfo.leaderBoard[1].score.ToString();
            rank3Score.text = rankInfo.leaderBoard[2].score.ToString();
            rank4Score.text = rankInfo.leaderBoard[3].score.ToString();
            rank5Score.text = rankInfo.leaderBoard[4].score.ToString();
            yield return StartCoroutine(loadImage(rankInfo.leaderBoard[0].photo, rank1RawImage));
            yield return StartCoroutine(loadImage(rankInfo.leaderBoard[1].photo, rank2RawImage));
            yield return StartCoroutine(loadImage(rankInfo.leaderBoard[2].photo, rank3RawImage));
            yield return StartCoroutine(loadImage(rankInfo.leaderBoard[3].photo, rank4RawImage));
            yield return StartCoroutine(loadImage(rankInfo.leaderBoard[4].photo, rank5RawImage));
            // StartCoroutine(loadImage(rankInfo.leaderBoard[1].photo, rank2RawImage));
            // StartCoroutine(loadImage(rankInfo.leaderBoard[2].photo, rank3RawImage));
            // StartCoroutine(loadImage(rankInfo.leaderBoard[3].photo, rank4RawImage));
            // StartCoroutine(loadImage(rankInfo.leaderBoard[4].photo, rank5RawImage));
        }

    }

    public IEnumerator loadImage(string url, RawImage image){
        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success){
            Debug.Log(www.downloadHandler.text);
        }
        else{
            var texture = DownloadHandlerTexture.GetContent(www);
            image.color = Color.white;
            image.texture = texture;
        }
    }
}

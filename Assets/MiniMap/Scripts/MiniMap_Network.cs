// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class MiniMap_Network : MiniMap
{
    
    private List<GameObject> playerPins=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
        while(players.Length>playerPins.Count){
            GameObject playerPin=Instantiate(playerPinPrefab,miniMap.transform);
            playerPin.GetComponent<PlayerPin_Network>().miniMap=this;
            playerPin.transform.SetParent(miniMap.rectTransform);
            playerPins.Add(playerPin);
        }
        if(playerPins.Count>players.Length){
            for(int i=playerPins.Count-1;i>players.Length;--i){
                Destroy(playerPins[i]);
                playerPins.RemoveAt(i);
            }
        }
        for(int i=0;i<players.Length;i++){
            playerPins[i].GetComponent<PlayerPin_Network>().player=players[i];
        }
    }
}

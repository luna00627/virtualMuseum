using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerPin_Network : PlayerPin
{
    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<NetworkObject>().HasStateAuthority){
            GetComponent<RawImage>().color=Color.red;
        }else
        {
            GetComponent<RawImage>().color=Color.blue;
        }
        updatePlayerPin();
    }
}

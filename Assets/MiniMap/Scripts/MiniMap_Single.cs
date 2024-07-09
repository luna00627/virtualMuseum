using UnityEngine;
using UnityEngine.UI;

public class MiniMap_Single : MiniMap
{
    
    private GameObject playerPin = null;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerPin = Instantiate(playerPinPrefab);
        playerPin.transform.SetParent(miniMap.rectTransform);
        playerPin.GetComponent<RawImage>().color = Color.red;
        playerPin.GetComponent<PlayerPin_Single>().player = player;
        playerPin.GetComponent<PlayerPin_Single>().miniMap = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerPin.transform.localPosition=calcInMapPosition(player.transform.position);
        playerPin.transform.localEulerAngles =calcInMapRotation(player.transform.eulerAngles.y);
    }
}

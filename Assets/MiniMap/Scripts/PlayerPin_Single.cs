using UnityEngine;
using UnityEngine.UI;

public class PlayerPin_Single : PlayerPin
{
    
    void Start()
    {
        GetComponent<RawImage>().color=Color.green;
    }
    // Update is called once per frame
    void Update()
    {
        updatePlayerPin();
    }
}

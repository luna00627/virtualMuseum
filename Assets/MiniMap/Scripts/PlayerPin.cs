using UnityEngine;

public class PlayerPin : MonoBehaviour
{
    [SerializeField] public GameObject player{get;set;}
    [SerializeField] public MiniMap miniMap{get;set;}
    protected void updatePlayerPin()
    {
        if(!player||!miniMap){
            Debug.Log("PlayerPin: player or miniMap is null");
            return;
        }
        transform.localPosition=miniMap.calcInMapPosition(player.transform.position);
        transform.localEulerAngles=miniMap.calcInMapRotation(player.transform.eulerAngles.y);
    }
}

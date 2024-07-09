using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPivit : MonoBehaviour
{
    public bool LeftBottom;
    public MiniMap miniMap=null;
    // Start is called before the first frame update
    void Start()
    {
        if(miniMap){
            miniMap.setSceneCorners(LeftBottom,transform);
        }else{
            Debug.LogError("No MiniMap Instance Found");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

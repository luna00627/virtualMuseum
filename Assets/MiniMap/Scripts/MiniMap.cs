using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public RawImage miniMap;
    public Transform[] sceneCorners = new Transform[2];
    public GameObject playerPinPrefab;

    public Vector3 calcInMapPosition(Vector3 playerPos){
        Vector3 sceneLB = sceneCorners[0].position;
        Vector3 sceneRT = sceneCorners[1].position;
        float sceneWidth = sceneRT.x - sceneLB.x;
        float sceneHeight = sceneRT.z - sceneLB.z;
        float x = (playerPos.x - sceneLB.x) / sceneWidth;
        float y = (playerPos.z - sceneLB.z) / sceneHeight;
        return new Vector3(miniMap.rectTransform.rect.xMin + x * miniMap.rectTransform.rect.width, y * miniMap.rectTransform.rect.height - miniMap.rectTransform.rect.height, 5);
    }
    public Vector3 calcInMapRotation(float y){
        Vector3 sceneLB = sceneCorners[0].position;
        Vector3 sceneRT = sceneCorners[1].position;
        float sceneWidth = sceneRT.x - sceneLB.x;
        float sceneHeight = sceneRT.z - sceneLB.z;
        if(sceneWidth>=0&&sceneHeight>=0){
            return new Vector3(0, 0, -y);
        }else if(sceneWidth>=0&&sceneHeight<0){
            return new Vector3(0, 0, -y+270f);
        }else if(sceneWidth<0&&sceneHeight<0){
            return new Vector3(0, 0, -y+180f);
        }else{
            return new Vector3(0, 0, -y+90f);
        }
    }

    public void setSceneCorners(bool LeftBottom, Transform pos)
    {
        if (LeftBottom)
        {
            sceneCorners[0] = pos;
        }
        else
        {
            sceneCorners[1] = pos;
        }
    }
}

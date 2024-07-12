using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Fusion;
public class CamScript : MonoBehaviour
{
    private CinemachineVirtualCamera m_CinemachineVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if(player.GetComponent<NetworkObject>().HasStateAuthority)
            {
                m_CinemachineVirtualCamera.Follow=player.transform.GetChild(0).transform;
//                Debug.Log(player.transform.GetChild(0).transform);
            }
        }
    }
}

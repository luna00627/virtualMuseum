using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectTransfer : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject playerFollowCamera;
    public GameObject playerArmature;
    public GameObject targetPoint_2;
    private CharacterController _controller;
    public GameObject ThirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

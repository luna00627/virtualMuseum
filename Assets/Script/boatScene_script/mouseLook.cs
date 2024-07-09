using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    bool enableMouse = false;

    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //讓頭部的上下仰角視角不超過90度
    }

    void Update()
    {
        /*if(Input.GetKey(KeyCode.Z)){
            enableMouse=true;
            Cursor.lockState=CursorLockMode.C;
        }*/
        if (enableMouse == false)
        {
            //旋轉身體的視角
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 讓頭部旋轉在90度

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
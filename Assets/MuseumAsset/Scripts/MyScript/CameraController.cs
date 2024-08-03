using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 相機移動速度
    public float moveSpeed = 5.0f;
    //public GameObject testCamera;

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        // 計算移動量
        Vector3 move = new Vector3(0, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.position += move;
    }
}

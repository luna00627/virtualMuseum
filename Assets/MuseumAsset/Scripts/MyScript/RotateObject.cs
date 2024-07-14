using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 5f; // 旋轉速度
    public float smoothTime = 0.1f;
    public RectTransform controlArea; // 控制旋轉的區域
    public Camera uiCamera;

    private float currentRotationX;
    private float currentRotationY;
    private float targetRotationX;
    private float targetRotationY;
    private float rotationVelocityX;
    private float rotationVelocityY;

    private bool isTouchingControlArea = false;

    void Start()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotationX = currentRotation.x;
        currentRotationY = currentRotation.y;
        targetRotationX = currentRotation.x;
        targetRotationY = currentRotation.y;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 滑鼠點擊
        {
            isTouchingControlArea = IsTouchingControlArea(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isTouchingControlArea) // 滑鼠拖動
        {
            float deltaX = Input.GetAxis("Mouse X"); 
            float deltaY = -Input.GetAxis("Mouse Y"); 
            targetRotationY -= deltaX * rotationSpeed;
            targetRotationX += deltaY * rotationSpeed; 
        }
        else if (Input.GetMouseButtonUp(0)) // 滑鼠放開
        {
            isTouchingControlArea = false;
        }

        // 平滑旋轉
        currentRotationX = Mathf.SmoothDampAngle(currentRotationX, targetRotationX, ref rotationVelocityX, smoothTime);
        currentRotationY = Mathf.SmoothDampAngle(currentRotationY, targetRotationY, ref rotationVelocityY, smoothTime);
        transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
    }

    private bool IsTouchingControlArea(Vector2 screenPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(controlArea, screenPosition, uiCamera, out localPoint);
        return controlArea.rect.Contains(localPoint);
    }
}

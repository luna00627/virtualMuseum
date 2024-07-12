using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 0.5f; // 旋轉速度
    public float smoothTime = 0.1f;
    public RectTransform controlArea; // 控制旋轉的區域
    public Camera uiCamera;

    private float currentRotationY;
    private float targetRotationY;
    private float rotationVelocity;

    private bool isTouchingControlArea = false;

    void Update()
    {
        if (Input.touchCount > 0){ // 手指
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began){
                isTouchingControlArea = IsTouchingControlArea(touch.position);
            }

            if (isTouchingControlArea && touch.phase == TouchPhase.Moved){
                float deltaX = touch.deltaPosition.x; // 手指移動距離
                targetRotationY -= deltaX * rotationSpeed;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
                isTouchingControlArea = false;
            }
        } else if (Input.GetMouseButtonDown(0)){ // 滑鼠點擊
            isTouchingControlArea = IsTouchingControlArea(Input.mousePosition);
        } else if (Input.GetMouseButton(0) && isTouchingControlArea){ // 滑鼠拖動
            float deltaX = Input.GetAxis("Mouse X"); // 滑鼠移動距離
            targetRotationY -= deltaX * rotationSpeed;
        } else if (Input.GetMouseButtonUp(0)){ // 滑鼠放開
            isTouchingControlArea = false;
        }

        // 平滑旋轉
        currentRotationY = Mathf.SmoothDampAngle(currentRotationY, targetRotationY, ref rotationVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
    }

    private bool IsTouchingControlArea(Vector2 screenPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(controlArea, screenPosition, uiCamera, out localPoint);
        return controlArea.rect.Contains(localPoint);
    }
}

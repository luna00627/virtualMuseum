using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // 要跟随的角色
    public Vector3 offset = new Vector3(0, 5, -10); // 摄像机相对于角色的偏移
    public float mouseSensitivity = 100f; // 鼠标灵敏度

    private float pitch = 0f; // 上下旋转角度
    private float yaw = 0f; // 左右旋转角度

    private bool playerAppeared = false; // 角色是否已出现

    void Start()
    {
        // 锁定鼠标光标
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 检查角色是否已出现
        if (player != null && !playerAppeared)
        {
            playerAppeared = true;
            // 初始化偏移
            transform.position = player.position + offset;
            transform.LookAt(player.position);

            // 初始化旋转角度
            yaw = transform.eulerAngles.y;
            pitch = transform.eulerAngles.x;
        }

        // 如果角色尚未出现，则跳过后续逻辑
        if (!playerAppeared)
        {
            return;
        }

        // 摄像机跟随角色
        transform.position = player.position + offset;

        // 仅在按下鼠标右键时旋转摄像机
        if (Input.GetMouseButton(1)) // 1 代表鼠标右键，0 代表左键
        {
            // 获取鼠标输入
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 更新旋转角度
            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -90f, 90f); // 限制上下旋转角度
        }

        // 旋转摄像机
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        // 更新摄像机位置
        Vector3 targetPosition = player.position + offset;
        transform.position = targetPosition;
    }
}

using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; private set; }

    public string Username { get; set; }
    public int AvatarIndex { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

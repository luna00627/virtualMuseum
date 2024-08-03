using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; private set; }

    public string Username { get; set; }
    public int AvatarIndex { get; set; }
    public List<string> Prizes { get; private set; } 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Prizes = new List<string>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    public Sprite[] avatars;

    public Sprite GetAvatar(int index)
    {
        if (index >= 0 && index < avatars.Length)
        {
            return avatars[index];
        }
        return null;
    }

    public int GetAvatarCount()
    {
        return avatars.Length;
    }
}

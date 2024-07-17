using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSwitcher : MonoBehaviour
{
    public List<Sprite> photos;
    public Image photoDisplay; 
    private int currentIndex = 0; 

    void Start()
    {
        UpdatePhoto();
    }

    public void NextPhoto()
    {
        currentIndex = (currentIndex + 1) % photos.Count;
        UpdatePhoto();
    }

    public void PreviousPhoto()
    {
        currentIndex = (currentIndex - 1 + photos.Count) % photos.Count;
        UpdatePhoto();
    }

    void UpdatePhoto()
    {
        photoDisplay.sprite = photos[currentIndex];
    }
}

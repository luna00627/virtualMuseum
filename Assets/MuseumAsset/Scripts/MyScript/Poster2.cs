using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poster2 : MonoBehaviour
{
    public Canvas InfoCanvas;
    public GameObject TitleButton;
    public Text TitleText;
    public Camera cam;
    public Sprite[] poster;
    public Image image;

    void Start()
    {
        // 在 Start 方法中尋找 Canvas 和按鈕，並初始化設置
        InfoCanvas.enabled = false;
        TitleText = TitleButton.transform.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        // 每幀檢查鼠標點擊事件
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            Ray mouseRay = cam.ScreenPointToRay(pos);

            // 檢測是否點擊到物體
            if (Physics.Raycast(mouseRay, out RaycastHit hitObj))
            {
                Debug.Log("Hit object tag: " + hitObj.collider.gameObject.tag);

                // 如果點擊到 "poster" 標籤的物體，處理顯示相關內容
                if (hitObj.collider.gameObject.CompareTag("poster"))
                {
                    string posterName = hitObj.collider.gameObject.name;

                    // 查找相符的圖片並顯示
                    for (int i = 0; i < poster.Length; i++)
                    {
                        if (posterName == poster[i].name)
                        {
                            ChangeTitleButtonName(posterName);
                            image.sprite = poster[i];
                            ShowInfoCanvas();
                            return; // 確定顯示了 Canvas 後直接返回，避免多次顯示
                        }
                    }
                }
            }
        }
    }

    // 顯示 Canvas 的方法
    void ShowInfoCanvas()
    {
        InfoCanvas.enabled = true;
    }

    // 隱藏 Canvas 的方法
    public void HideInfoCanvas()
    {
        InfoCanvas.enabled = false;
    }

    // 修改按鈕文本的方法
    void ChangeTitleButtonName(string name)
    {
        TitleText.text = name;
    }
}

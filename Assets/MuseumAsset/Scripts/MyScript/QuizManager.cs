using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空間
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText; // 顯示題目的 TMP_Text 元件
    public Button[] answerButtons; // 選項按鈕陣列
    public TMP_Text progressText; // 顯示進度的 TMP_Text 元件
    public TMP_Text explanationText; // 顯示解析的 TMP_Text 元件
    public Button nextButton; // “下一題”按鈕

    public Sprite correctSymbol; // 打勾符號的圖像
    public Sprite incorrectSymbol; // 打叉符號的圖像


    private string[] questions = { 
        "臭肚魚為何稱為臭肚魚?", 
        "下面哪個生物不會住在河口區?", 
        "海蛞蝓跟劍尖槍魷都是軟體動物，請問哪一個比較容易在白天發現?" 
    };

    private List<List<string>> answers = new List<List<string>>()
    {
        new List<string> { 
            "臭肚魚被捕時容易受驚，進而死亡，因而消費者買到時已經臭掉了", 
            "臭肚魚以藻類為食，一般漁民在清理魚肚時常常會聞到難聞的海藻發酵味道，因而得名" 
        },
        new List<string> { 
            "彈塗魚", "龍虎斑", "多鱗四指馬鮁(午仔魚)" 
        },
        new List<string> { 
            "海蛞蝓", "劍尖槍魷(透抽)", "NO"
        }
    };

    private List<string> explanations = new List<string>()
    {
        "解析：臭肚魚以藻類為食，一般漁民在清理魚肚時常常會聞到難聞的海藻發酵味道，因而得名。",
        "解析：多鱗四指馬鮁(午仔魚)不會住在河口區。",
        "解析：劍尖槍魷(透抽)比較容易在白天發現。"
    };

    private int[] correctAnswers = { 1, 1, 0 }; // 正確選項索引

    private int currentQuestionIndex = 0;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        explanationText.gameObject.SetActive(false);
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int index)
    {
        if (index < 0 || index >= questions.Length)
        {
            Debug.LogError("Question index is out of range.");
            return;
        }

        questionText.text = questions[index];
        List<string> currentAnswers = answers[index];
        int numOptions = currentAnswers.Count;

        // 更新進度文本
        UpdateProgressText(index + 1, questions.Length);
        Debug.Log("answerButtons.Length = " + answerButtons.Length);
        foreach (var button in answerButtons)
        {
            ResetAnswerButton(button);
        }
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Debug.Log("i = " + i);
            if (i < numOptions)
            {
                answerButtons[i].gameObject.SetActive(true);
                TMP_Text buttonText = answerButtons[i].GetComponentInChildren<TMP_Text>();
                buttonText.text = currentAnswers[i];

                // 使用局部變量來解決閉包問題
                int localQuestionIndex = index;
                int localAnswerIndex = i;

                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(localQuestionIndex, localAnswerIndex));
                ResetAnswerButton(answerButtons[i]);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }


    void UpdateProgressText(int currentQuestion, int totalQuestions)
    {
        // 設置進度顯示格式
        progressText.text = $"{currentQuestion}/{totalQuestions}";
    }

    void OnAnswerSelected(int questionIndex, int answerIndex)
    {
        Debug.Log($"選擇了第 {questionIndex + 1} 題的第 {answerIndex + 1} 選項");

        if (answerIndex < 0 || answerIndex >= answerButtons.Length)
        {
            Debug.LogError($"Answer index {answerIndex} is out of bounds for answerButtons array.");
            return;
        }

        bool isCorrect = answerIndex == correctAnswers[questionIndex];

        if (answerIndex >= answerButtons.Length || correctAnswers[questionIndex] >= answerButtons.Length)
        {
            Debug.LogError("Correct answer index is out of bounds for answerButtons array.");
            return;
        }

        ShowAnswerFeedback(answerButtons[answerIndex], isCorrect);

        // 顯示正確答案
        ShowAnswerFeedback(answerButtons[correctAnswers[questionIndex]], true);

        // 顯示解析文本
        explanationText.text = explanations[questionIndex];
        explanationText.gameObject.SetActive(true);
        
        // 隱藏選項按鈕，顯示“下一題”按鈕
        foreach (var button in answerButtons)
        {
            button.interactable = false; // 禁用按鈕
        }
        nextButton.gameObject.SetActive(true);

        // 設置“下一題”按鈕的點擊事件
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() => OnNextQuestion());
    }


    void ShowAnswerFeedback(Button button, bool isCorrect)
    {
        Image background = button.GetComponent<Image>(); // 假設按鈕背景是 Image 元件
        Transform symbol = button.transform.Find("Symbol"); // 假設符號是按鈕的子物件

        if (isCorrect)
        {
            background.color = Color.green; // 將背景設為綠色
            symbol.GetComponent<Image>().sprite = correctSymbol; // 設置打勾符號的圖像
        }
        else
        {
            background.color = Color.red; // 將背景設為紅色
            symbol.GetComponent<Image>().sprite = incorrectSymbol; // 設置打叉符號的圖像
        }

        symbol.gameObject.SetActive(true); // 顯示符號
    }

    void ResetAnswerButton(Button button)
    {
        Image background = button.GetComponent<Image>(); // 假設按鈕背景是 Image 元件
        if (background != null)
        {
            background.color = Color.white; // 將背景重置為白色
        }
        Transform symbol = button.transform.Find("Symbol"); // 假設符號是按鈕的子物件
        if (symbol != null)
        {
            symbol.gameObject.SetActive(false); // 隱藏符號
        }
        button.interactable = true; // 確保按鈕可用
    }


    void OnNextQuestion()
    {
        explanationText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.Log("遊戲結束！");
            // 在此處可以添加結束遊戲的邏輯
        }
    }
}

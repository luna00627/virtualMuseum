using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrizeController : MonoBehaviour
{
    public GameObject manager;
    public GameObject gameCanvas;
    private Animator prizeAnimator;
    private Renderer prizeRenderer;
    private QuizManager quizManager;
    private ComponentDisabler componentDisabler;


    void Start()
    {
        prizeAnimator = gameObject.GetComponent<Animator>();
        prizeRenderer = gameObject.GetComponent<Renderer>();
        quizManager = FindObjectOfType<QuizManager>();
        componentDisabler = manager.GetComponent<ComponentDisabler>();

        prizeRenderer.enabled = false;
    }

    public void ShowPrize()
    {
        prizeRenderer.enabled = true;

        StartCoroutine(PlayAnimationAfterFrame());
    }

    private IEnumerator PlayAnimationAfterFrame()
    {
        yield return null; 
        prizeAnimator.SetTrigger("ShowPrize");
    }

    public void OnPrizeCollectComplete()
    {

        prizeRenderer.enabled = false;

        if (quizManager != null && quizManager.resultPanel != null)
        {
            quizManager.resultPanel.SetActive(false);
        }
        gameCanvas.SetActive(false);
        componentDisabler.EnableComponents();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image Frame;
    [SerializeField] Image BGFading;
    [SerializeField] GameObject SlideButton;
    [SerializeField] float LerpDuration;
    [SerializeField] float LeftMargin;
    [SerializeField] float RightMargin;
    RectTransform FrameRect;
    RectTransform btRect;

    float FrameWidth;

    public int SwitchState { get; private set; } = 0;
    int ButtonSwitchState = 0;
    float timeElapsed = 0;

    public void InitialState(int _switchState)
    {
        if (_switchState == 1)
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }
    }

    void Awake()
    {
        FrameRect = (RectTransform)Frame.transform;
        btRect = (RectTransform)SlideButton.transform;

        FrameWidth = FrameRect.rect.width;
    }

    void SwitchOn()
    {
        if (SwitchState == 0)
        {
            SwitchState = 1;
            timeElapsed = 0; // Reset the timer for the lerp
        }
    }

    void SwitchOff()
    {
        if (SwitchState == 1)
        {
            SwitchState = 0;
            timeElapsed = 0; // Reset the timer for the lerp
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SwitchState == 1) // switch off
        {
            SwitchOff();
        }
        else // switch on
        {
            SwitchOn();
        }
    }

    void Update()
    {
        if (SwitchState != ButtonSwitchState)
        {
            SlideMotion(SwitchState);
        }
    }

    void SlideMotion(int _btstate)
    {
        if (_btstate == 1) // on state
        {
            ButtonSwitchState = -1;
            timeElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(timeElapsed / LerpDuration);

            // 計算SlideButton的目標位置，錨點在left middle
            float targetXPosition = (FrameWidth / 2) - btRect.rect.width - RightMargin;

            SlideButton.transform.localPosition = Vector3.Lerp(
                new Vector3(LeftMargin - (FrameWidth / 2), SlideButton.transform.localPosition.y, SlideButton.transform.localPosition.z),
                new Vector3(targetXPosition, SlideButton.transform.localPosition.y, SlideButton.transform.localPosition.z),
                progress);

            BGFading.color = new Color(BGFading.color.r, BGFading.color.g, BGFading.color.b, progress);

            if (progress >= 1)
            {
                ButtonSwitchState = 1;
                SlideButton.transform.localPosition = new Vector3(targetXPosition, SlideButton.transform.localPosition.y, SlideButton.transform.localPosition.z);
                BGFading.color = new Color(BGFading.color.r, BGFading.color.g, BGFading.color.b, 1);
            }
        }
        else // off state
        {
            ButtonSwitchState = -2;
            timeElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(timeElapsed / LerpDuration);

            // 計算SlideButton的目標位置，錨點在left middle
            float targetXPosition = LeftMargin - (FrameWidth / 2);

            SlideButton.transform.localPosition = Vector3.Lerp(
                new Vector3((FrameWidth / 2) - btRect.rect.width - RightMargin, SlideButton.transform.localPosition.y, SlideButton.transform.localPosition.z),
                new Vector3(targetXPosition, SlideButton.transform.localPosition.y, SlideButton.transform.localPosition.z),
                progress);

            BGFading.color = new Color(BGFading.color.r, BGFading.color.g, BGFading.color.b, 1 - progress);

            if (progress >= 1)
            {
                ButtonSwitchState = 0;
                SlideButton.transform.localPosition = new Vector3(targetXPosition, SlideButton.transform.localPosition.y, SlideButton.transform.localPosition.z);
                BGFading.color = new Color(BGFading.color.r, BGFading.color.g, BGFading.color.b, 0);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro; 

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public Button previousButton;
    public Button nextButton;
    public Button playPauseButton;
    public Button speedButton;
    public Image playPauseIcon; 
    public Sprite playIcon; // 播放圖標
    public Sprite pauseIcon; // 暫停圖標
    public TMP_Text speedText; // 速度文字
    public TMP_Text videoTitleText; // 影片名稱
    public Slider progressBar; // 影片進度條

    private int currentVideoIndex = 0;
    private bool isPlaying = true;
    private float[] speedOptions = { 0.5f, 1f, 1.5f, 2f };
    private int currentSpeedIndex = 1;

    void Start()
    {
        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();

        playPauseButton.onClick.AddListener(TogglePlayPause);
        previousButton.onClick.AddListener(PlayPreviousVideo);
        nextButton.onClick.AddListener(PlayNextVideo);
        speedButton.onClick.AddListener(ChangeSpeed);

        videoPlayer.loopPointReached += OnVideoEnd;

        UpdateSpeedText(); 
        UpdatePlayPauseIcon(); 
        UpdateVideoTitle(); 
    }

    void Update()
    {
        // 更新進度條
        if (videoPlayer.frameCount > 0)
        {
            progressBar.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
    }

    void TogglePlayPause()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
        isPlaying = !isPlaying;
        UpdatePlayPauseIcon();
    }

    void PlayPreviousVideo()
    {
        if (videoPlayer.time > 5) 
        {
            videoPlayer.time = 0;
            videoPlayer.Play();
        }
        else 
        {
            currentVideoIndex = (currentVideoIndex - 1 + videoClips.Length) % videoClips.Length;
            PlayVideo();
        }
    }

    void PlayNextVideo()
    {
        if (videoPlayer.time < videoPlayer.length - 5)
        {
            videoPlayer.time = videoPlayer.length;
            videoPlayer.Play();
        }
        else 
        {
            currentVideoIndex = (currentVideoIndex + 1) % videoClips.Length;
            PlayVideo();
        }
    }

    void PlayVideo()
    {
        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
        isPlaying = true;
        UpdatePlayPauseIcon();
        UpdateVideoTitle();
    }

    void ChangeSpeed()
    {
        currentSpeedIndex = (currentSpeedIndex + 1) % speedOptions.Length;
        videoPlayer.playbackSpeed = speedOptions[currentSpeedIndex];
        UpdateSpeedText();
    }

    void UpdateSpeedText()
    {
        speedText.text = speedOptions[currentSpeedIndex] + "x"; // 更新速度文字
    }

    void UpdatePlayPauseIcon()
    {
        playPauseIcon.sprite = isPlaying ? pauseIcon : playIcon; // 根據播放狀態切換圖標
    }

    void UpdateVideoTitle()
    {
        videoTitleText.text = videoClips[currentVideoIndex].name; // 更新影片名稱
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 影片播放結束後自動播放下一個影片
        PlayNextVideo();
    }
}

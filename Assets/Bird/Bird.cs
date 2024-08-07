using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator animator;
    private bool isFlying = false;
    public float flightSpeed = 2.0f;  // 飞行速度
    public float landingSpeed = 2.0f; // 降落速度
    public float forwardSpeed = 5.0f; // 前进速度
    public float maxAltitude = 1.11f; // 最大高度
    public float minAltitude = 0.0f;  // 最小高度
    public Vector3 relativePosition = new(-1.32f, 2.12f, 2.22f); // 相对于玩家的偏移量
    public Vector3 relativeLookDirection = new(90, 0, 180);// 定义相对玩家的朝向

    private bool isIntroFinished = false; // 标志表示开场是否结束

    // 音频组件
    public AudioClip introSound; // 开场音效
    private AudioSource audioSource;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();

        // 设置初始状态为降落
        animator.SetBool("isFlying", isFlying);

        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
       
        if (!isIntroFinished || !GameObject.FindGameObjectWithTag("Player"))
        {
            return;
        }
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        // 将 isFlying 设置为 true 以开始飞行
        isFlying = true;
        animator.SetBool("isFlying", isFlying);

        // 根据状态来调整高度
        /*if (isFlying)
        {
            // 升高
            if (transform.position.y < maxAltitude)
            {
                transform.position += Vector3.up * flightSpeed * Time.deltaTime;
            }
            else
            {
                // 确保不会超过最大高度
                transform.position = new Vector3(transform.position.x, maxAltitude, transform.position.z);
            }
        }
        else
        {
            // 降落
            if (transform.position.y > minAltitude)
            {
                transform.position -= Vector3.up * landingSpeed * Time.deltaTime;
            }
            else
            {
                // 确保不会低于最小高度
                transform.position = new Vector3(transform.position.x, minAltitude, transform.position.z);
            }
        }*/
        // 计算鸟的目标位置，即玩家位置加上相对偏移量
        Vector3 targetPosition = player.TransformPoint(relativePosition);



        // 平滑地移动到目标位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, flightSpeed * Time.deltaTime);

        // 计算鸟的目标朝向
        Vector3 lookAtPosition = player.TransformPoint(relativeLookDirection);
        Vector3 direction = (lookAtPosition - transform.position).normalized;
        

        // 平滑地旋转到目标朝向
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, flightSpeed * Time.deltaTime);
        }
    }

    void PlayIntro()
    {
        // 播放开场音效
        PlaySound(introSound);

        // 播放开场动画
        animator.SetTrigger("PlayIntro");

        // 设置一个定时器来标志开场结束
        StartCoroutine(IntroCoroutine());
    }
    public void StartIntro()
    {
        // 播放开场动画和音效
        PlayIntro();
    }

    IEnumerator IntroCoroutine()
    {
        // 等待开场音效结束
        yield return new WaitForSeconds(introSound.length);
        isIntroFinished = true;
        animator.SetBool("isIntroFinished", isIntroFinished);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    
}


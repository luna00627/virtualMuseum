using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float speedOne = 0f; //實際速度
    private float speedMax = 100f; //最大速度
    private float speedMin = -20f; //最小速度
    private float speedUp = 20f; //加速加速度
    private float speedDown = 4f; //減速加速度
    private float speedTend = 5f; //無操作時 速度超過0的加速度
    private float speedBack = 1f; //後退加速度
    public ParticleSystem  rightsplash;
    public ParticleSystem rightwave;
    public ParticleSystem leftsplash;
    public ParticleSystem leftwave;
    private Transform m_Transform;
    public GameObject paddle;
    public GameObject idle;
    public ParticleSystem boatwave;
    private Animation m_animation;
    private Animation i_animation;
    AudioSource audiosource;
    public AudioClip wat;
    //public GameObject splash;
    void Start()
    {
        m_Transform = this.transform;
        m_animation = paddle.GetComponent<Animation>();
        i_animation = idle.GetComponent<Animation>();
        audiosource = GetComponent<AudioSource>();
        //splash = splash.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        playerMove();
    }
    public void OnCollisionEnter(Collision collision)
    { //collision為被碰之物件
      //Debug.Log(collision.gameObject.name);
        speedOne = speedOne * -0.5f;
    }


    void playerMove()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            speedOne = 0f;
        }
        if (Input.GetKey(KeyCode.Q) /*&& speedOne < speedMax*/)
        { //前進加速 Q左前 
            speedOne = speedOne + Time.deltaTime * speedUp; //加速
            //m_Transform.Rotate(0, 25f * Time.deltaTime, 0); //轉向
            m_animation.Play("Rotate paddle");
            i_animation.Play("handdle1");
            audiosource.PlayOneShot(wat, 0.7f);
            //gameObject.particleSystem.Play();
            leftsplash.Play();
            leftwave.Play();
            boatwave.Play();
            Invoke(nameof(MethodName1), 0.35f);
        }
        else if (Input.GetKey(KeyCode.W) /*&& speedOne < speedMax*/)
        { //前進加速 W右前
            speedOne = speedOne + Time.deltaTime * speedUp; //加速
            //m_Transform.Rotate(0, -25f * Time.deltaTime, 0); //轉向
            m_animation.Play("Rotate paddle2");
            i_animation.Play("handdle2");
            rightsplash.Play();
            rightwave.Play();
            boatwave.Play();
            Invoke(nameof(MethodName2), 0.35f);
        }
        else if (Input.GetKey(KeyCode.A) /*&& speedOne > speedMin*/)
        { //向後減速 S右後
            // speedOne = Mathf.Lerp(speedOne, 0, 0.4f);
            speedOne = speedOne - Time.deltaTime * speedDown; //減速
            //m_Transform.Rotate(0, -25f * Time.deltaTime, 0); //轉向
            m_animation.Play("Rotate paddle3");
            i_animation.Play("handdle1");
            leftsplash.Play();
            leftwave.Play();
            boatwave.Play();
            Invoke(nameof(MethodName2), 0.35f);
        }
        else if (Input.GetKey(KeyCode.S) /*&& speedOne > speedMin*/)
        { //向後減速 S右後
            // speedOne = Mathf.Lerp(speedOne, 0, 0.4f); 
            speedOne = speedOne - Time.deltaTime * speedDown; //減速
            //m_Transform.Rotate(0, 25f * Time.deltaTime, 0); //轉向
            m_animation.Play("Rotate paddle4");
            i_animation.Play("handdle2");
            rightsplash.Play();
           rightwave.Play();
            boatwave.Play();
            Invoke(nameof(MethodName1), 0.35f);
        }
        //玩家未操作時 速度慢慢回到0
        else if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && speedOne > 0f)
        {
            speedOne = speedOne - Time.deltaTime * speedTend;
        }
        else if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && speedOne < 0f)
        {
            speedOne = speedOne + Time.deltaTime * speedTend;
        }
       // Debug.Log(speedOne); //印出速度
        m_Transform.Translate(Vector3.forward * speedOne * Time.deltaTime); //移動
    }
    void MethodName1()
    {
        m_Transform.Translate(Vector3.forward * speedOne * Time.deltaTime); //移動
        m_Transform.Rotate(0, 25f * Time.deltaTime, 0);
    }
    void MethodName2()
    {
        m_Transform.Translate(Vector3.forward * speedOne * Time.deltaTime); //移動
        m_Transform.Rotate(0, -25f * Time.deltaTime, 0);
    }


    /*void playerMove(){
        if(Input.GetKey(KeyCode.Q)){ //左前
            Debug.Log("Q");
            m_Transform.localRotation = Quaternion.Euler(0, -1, 0);
            m_Transform.Translate(new Vector3(-1, 0, 1)*speed*Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.W)){ //右前
            Debug.Log("W");
            m_Transform.localRotation = Quaternion.Euler(0, 1, 0);
            m_Transform.Translate(new Vector3(1, 0, 1)*speed*Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.A)){ //左後
            Debug.Log("A");
            m_Transform.localRotation = Quaternion.Euler(0, 1, 0);
            m_Transform.Translate(new Vector3(-1, 0, -1)*speed*Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.S)){ //右後
            Debug.Log("S");
            m_Transform.localRotation = Quaternion.Euler(0, 1, 0);
            m_Transform.Translate(new Vector3(1, 0, -1)*speed*Time.deltaTime, Space.World);
        }
    }*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    private static startLanguage instance;
    public static startLanguage getInstance(){
        if(instance==null) instance=new startLanguage();
        return instance;
    }
   public bool flag = true;
    public Text StartTitle;
    public Text StartStartBtn;
    public Text StartSettingBtn;
    public Text LoginTitle;
    public Text LoginBackBtn;
    public Text ChooseSceneTitle;
    public Text ChooseSceneBackBtn;
    public Text SceneInfoTitle;
    public Text SceneInfoContext;
    public Text SceneInfoEnterBtn; 
    public Text SceneInfoBackBtn; 
    public Text SelectTitle; 
    public Text selectChooseBtn; 
    public Text selectBackBtn; 
    public Text settingTitle; 
    public Text settingBack; 

    void Start()
    {
        if(instance==null){ instance =new startLanguage();}
         if(flag){
            Chinese();
        }
        else{
            English();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Chinese(){
        
            StartTitle.text = "和平島公園VR導覽";
            StartStartBtn.text = "開始導覽";
            StartSettingBtn.text = "設定";
            LoginTitle.text = "請選擇登入方式";
            LoginBackBtn.text = "返回";
            ChooseSceneTitle.text = "請選擇場景";
            ChooseSceneBackBtn.text = "返回";
            //SceneInfoTitle.text = "場景名稱";
            //SceneInfoContext.text = "場景介紹";
            SceneInfoEnterBtn.text = "進入場景";
            SceneInfoBackBtn.text = "返回";
            SelectTitle.text = "請選擇角色！";
            selectChooseBtn.text = "選擇";
            selectBackBtn.text = "返回";
            settingBack.text = "返回";
    }
    public void English(){

            StartTitle.text = "Heping Island VR Guide";
            StartStartBtn.text = "Start the tour";
            StartSettingBtn.text = "Settings";
            LoginTitle.text = "Please choose a login method";
            LoginBackBtn.text = "Back";
            ChooseSceneTitle.text = "Please choose a login scene";
            ChooseSceneBackBtn.text = "Back";
            //SceneInfoTitle.text = "";
            //SceneInfoContext.text = "場景介紹";
            SceneInfoEnterBtn.text = "Enter scene";
            SceneInfoBackBtn.text = "Back";
            SelectTitle.text = "Select your role";
            selectChooseBtn.text = "Select";
            selectBackBtn.text = "Back";
            settingBack.text = "Back";
    }
    public void setChinese()
    {
        flag = true;
        if(flag){
            Chinese();
        }
        else{
            English();
        }
    }
    public void setEnglish()
    {
        flag = false;
        if(flag){
            Chinese();
        }
        else{
            English();
        }
    }
}

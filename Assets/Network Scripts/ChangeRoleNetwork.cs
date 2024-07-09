using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ChangeRoleNetwork : NetworkBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Avatar[] avatar;
    public GameObject[] role = new GameObject[6];
    [Networked(OnChanged = nameof(NetworkSelectNum))]
    public int selectedNum { get; set;}
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            int i=0;
            Transform Geometry;
            Geometry=transform.Find("Geometry");
            foreach(Transform child in Geometry){
                role[i++]=child.gameObject;
            }
            selectedNum = characterManager.selectedOption + 1;
            Debug.Log(selectedNum);
            role[0].SetActive(false);
            role[selectedNum].SetActive(true);
            
            animator.avatar = avatar[selectedNum-1];
        }
        else{
            role[0].SetActive(false);
            role[selectedNum].SetActive(true);
            animator.avatar = avatar[selectedNum-1];
        }
        
        Debug.Log(selectedNum);
    }

    private static void NetworkSelectNum(Changed<ChangeRoleNetwork> changed)
    {
        Animator animator = changed.Behaviour.GetComponent<ChangeRoleNetwork>().animator;
        Avatar[] avatar = changed.Behaviour.GetComponent<ChangeRoleNetwork>().avatar;
        int i=0;
        Transform Geometry;
        Geometry = changed.Behaviour.transform.Find("Geometry");
        foreach(Transform child in Geometry){
            if(i==changed.Behaviour.selectedNum){
                child.gameObject.SetActive(true);
                animator.avatar = avatar[changed.Behaviour.selectedNum - 1];
                break;
            }
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}

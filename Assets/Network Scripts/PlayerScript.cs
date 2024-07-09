using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void FixedUpdateNetwork()
    {
        // if(HasStateAuthority==false){
        //     CharacterController characterController=GetComponent<CharacterController>();
        //     if(characterController!=null)
        //     {
        //         Destroy(characterController);
        //     }
        //     ThirdPersonController thirdPersonController=GetComponent<ThirdPersonController>();
        //     if(thirdPersonController!=null)
        //     {
        //         Destroy(thirdPersonController);
        //     }

        //     StarterAssetsInputs starterAssetsInputs=GetComponent<StarterAssetsInputs>();
        //     if(starterAssetsInputs!=null)
        //     {
        //         Destroy(starterAssetsInputs);
        //     }
        //     BasicRigidBodyPush basicRigidBodyPush=GetComponent<BasicRigidBodyPush>();
        //     if(basicRigidBodyPush!=null)
        //     {
        //         Destroy(basicRigidBodyPush);
        //     }
        // }

    }
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            GetComponent<PlayerInput>().enabled = true;
        }else{
            GetComponent<PlayerInput>().enabled = false;
        }
    }
    // // Update is called once per frame
    // void Update()
    // {
    //     GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
    //     foreach(GameObject player in players)
    //     {
    //         if(player.GetComponent<NetworkObject>().HasStateAuthority)
    //         {
    //         }else{

    //         }
    //     }
    // }
    public override void Render()
    {
        Animator _animator=GetComponent<Animator>();
        _animator.SetBool("FreeFall",false);
    }
}

using UnityEngine;

public class ComponentDisabler : MonoBehaviour
{
    //public GameObject[] objectsToControl; 
    public GameObject chest;
    public GameObject selectRoomCanvas;
    private ExhibitInteraction exhibitInteraction;
    private AudioSource audioSource;

    private void Start()
    {
        exhibitInteraction = gameObject.GetComponent<ExhibitInteraction>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        DisableComponents();
    }

    public void DisableComponents()
    {
        if (chest != null)
        {
            var chestComponent = chest.GetComponent("StartGame");
            if (chestComponent != null)
            {
                if (chestComponent is Behaviour behaviour)
                {
                    behaviour.enabled = false;
                }
            }
        }
        selectRoomCanvas.SetActive(false);
        exhibitInteraction.enabled = false;
    }

    public void EnableComponents()
    {
        // foreach (var obj in objectsToControl)
        // {
        //     if (obj != null)
        //     {
        //         var component = obj.GetComponent("ExhibitScript");
        //         if (component != null)
        //         {
        //             if (component is Behaviour behaviour)
        //             {
        //                 behaviour.enabled = true;
        //             }
        //         }
        //     }
        // }
        if (chest != null)
        {
            var chestComponent = chest.GetComponent("StartGame");
            if (chestComponent != null)
            {
                if (chestComponent is Behaviour behaviour)
                {
                    behaviour.enabled = true;
                }
            }
        }
        selectRoomCanvas.SetActive(true);
        exhibitInteraction.enabled = true;
    }
    
}

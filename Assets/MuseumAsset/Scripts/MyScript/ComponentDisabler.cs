using UnityEngine;

public class ComponentDisabler : MonoBehaviour
{
    //public GameObject[] objectsToControl; 
    public GameObject firstChest;
    public GameObject secondChest;
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
        if (firstChest != null)
        {
            var firstChestComponent = firstChest.GetComponent("StartFirstGame");
            if (firstChestComponent != null)
            {
                if (firstChestComponent is Behaviour behaviour)
                {
                    behaviour.enabled = false;
                }
            }
        }

        if (secondChest != null)
        {
            var secondChestComponent = secondChest.GetComponent("StartFirstGame");
            if (secondChestComponent != null)
            {
                if (secondChestComponent is Behaviour behaviour)
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
        if (firstChest != null)
        {
            var firstChestComponent = firstChest.GetComponent("StartFirstGame");
            if (firstChestComponent != null)
            {
                if (firstChestComponent is Behaviour behaviour)
                {
                    behaviour.enabled = true;
                }
            }
        }

        if (secondChest != null)
        {
            var secondChestComponent = secondChest.GetComponent("StartSecondGame");
            if (secondChestComponent != null)
            {
                if (secondChestComponent is Behaviour behaviour)
                {
                    behaviour.enabled = true;
                }
            }
        }
        selectRoomCanvas.SetActive(true);
        exhibitInteraction.enabled = true;
    }
    
}

using UnityEngine;

public class ComponentDisabler : MonoBehaviour
{
    public GameObject manager;
    public GameObject selectRoomCanvas;
    private ExhibitInteraction exhibitInteraction;
    private AudioSource audioSource;
    private RaycastManager raycastManager;

    private void Start()
    {
        exhibitInteraction = gameObject.GetComponent<ExhibitInteraction>();
        audioSource = gameObject.GetComponent<AudioSource>();
        raycastManager = manager.GetComponent<RaycastManager>();
        audioSource.Play();
        DisableComponents();
    }

    public void DisableComponents()
    {
        selectRoomCanvas.SetActive(false);
        if (exhibitInteraction != null)
        {
            exhibitInteraction.enabled = false;
        }
        if (raycastManager != null)
        {
            raycastManager.enabled = false;
        }
    }

    public void EnableComponents()
    {
        selectRoomCanvas.SetActive(true);
        exhibitInteraction.enabled = true;
        raycastManager.enabled = true;
    }
    
}

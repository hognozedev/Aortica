using UnityEngine;

public class ScrapMill : MonoBehaviour, IInteractable
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Interact()
    {
        Debug.Log("yepyepyepyep");
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactPrompt;
    public GameObject popup;
    public PlayerController player;

    public bool CanInteract() => true;

    public void Interact()
    {
        popup.SetActive(true);
    }

    public void Update()
    {
        if (player.cancelAction.WasPerformedThisFrame()) popup.SetActive(false);

        if (player.attackAction.WasPerformedThisFrame() && popup.activeInHierarchy) SceneManager.LoadScene("TD_WaveScene1");

    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);

    }

    public void OnFocusLost()
    {
        interactPrompt.gameObject.SetActive(false);

    }
}

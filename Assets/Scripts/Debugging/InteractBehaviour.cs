using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractBehaviour : MonoBehaviour, IInteractable
{
    public bool CanInteract() => true;
    public GameObject interactPrompt;
    public bool isDoor;
    public LobbyEvents lobby;

    public GameObject warningPopup;
    private bool warned = false;

    public void Interact()
    {
        if (warned == true)
        {
            SceneManager.LoadScene(lobby.LSO.nextWaveScene);
        }

        warningPopup.SetActive(true);
        warned = true;
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

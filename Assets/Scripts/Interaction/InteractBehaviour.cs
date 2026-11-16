using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerData;

public class InteractBehaviour : MonoBehaviour, IInteractable
{
    public bool CanInteract() => true;
    public GameObject interactPrompt;
    public LobbyEvents lobby;
    public GameObject warningPopup;

    public void Interact()
    {
        warningPopup.SetActive(true);
        lobby.player.InMenu();
    }

    public void Continue()
    {
        SceneManager.LoadScene(lobbyData.nextWaveScene);
    }

    public void NotYet()
    {
        interactPrompt.gameObject.SetActive(false);
    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        interactPrompt.gameObject.SetActive(false);
        lobby.player.ExitedMenu();
    }
}
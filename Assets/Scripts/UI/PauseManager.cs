using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public WaveEvents waveEvents;
    public LobbyEvents lobbyEvents;
    public PlayerController player;

    public void Resume()
    {
        player.ExitedMenu();

        gameObject.SetActive(false);
        if(waveEvents != null) waveEvents.menuOpen = false;
        if(lobbyEvents != null) lobbyEvents.menuOpen = false;
        Time.timeScale = 1f;
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static PlayerData;

public class WaveEvents : MonoBehaviour
{
    public PlayerController player;
    public int waveNumber;
    public GameObject pauseMenu;
    public Volume filmFX;

    bool escPressed;
    bool fxOn;
    bool menuOpen;

    public void Awake()
    {
        player.inLobby = false;
        if (filmFX == true) fxOn = true;
    }

    public void Update()
    {
        escPressed = player.escapeAction.WasPerformedThisFrame();
        if (escPressed && !menuOpen) PauseMenu();
        else if (escPressed && menuOpen) Resume();
    }

    public void PauseMenu()
    {
        Time.timeScale = 0;

        player.InMenu();
        pauseMenu.SetActive(true);
        menuOpen = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;

        player.ExitedMenu();
        pauseMenu.SetActive(false);
        menuOpen = false;
    }

    public void ToggleFilter()
    {
        if (fxOn)
        {
            filmFX.enabled = false;
            fxOn = false;
        }

        else if (!fxOn)
        {
            filmFX.enabled = true;
            fxOn = true;
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
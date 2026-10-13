using UnityEngine;
using static PlayerData;

public class WaveEvents : MonoBehaviour
{
    public PlayerController player;
    public GameObject pauseMenu;
    public int currentWave;
    public LobbyData NLD;

    bool escPressed;
    [HideInInspector] public bool menuOpen;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.inLobby = false;

        Wave1Events(currentWave);
        nextLobby = NLD;
    }

    public void Update()
    {
        escPressed = player.escapeAction.WasPressedThisFrame();
        if (escPressed && !menuOpen) PauseMenu();
    }

    public void PauseMenu()
    {
        player.InMenu();
        player.cancelAction.Disable();

        pauseMenu.SetActive(true);
        menuOpen = true;
        Time.timeScale = 0f;
    }

    public void Wave1Events(int WN)
    {
        if (WN == 1) Debug.Log("tutorial stuff");
        else if (WN > 1) Debug.Log("other waves");
    }

}
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class WaveEvents : MonoBehaviour
{
    public PlayerController player;
    public GameObject pauseMenu;

    bool escPressed;
    [HideInInspector] public bool menuOpen;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.inLobby = false;
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
}
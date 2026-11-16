using UnityEngine;
using static PlayerData;

public class WaveEvents : MonoBehaviour
{
    public GameObject pauseMenu;
    public WaveData waveData;
    public int killCon;

    [Header("Script Refs")]
    public PlayerController player;
    public WaifMaster waifs;
    [SerializeField] ScrapMill tutorialMill;
    [SerializeField] GameObject millInteract;

    bool escPressed;
    [HideInInspector] public bool menuOpen;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.inLobby = false;
        waveEnemyKills = 0;

        if(waveData.waveNumber != 1) waifs.StartInvokeSpawn(2);
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

    public void Wave1Events()
    {
        if(thingsSeen >= 3)
        {
            Debug.Log("seen all");
            waifs.StartInvokeSpawn(10);

            tutorialMill.gameObject.layer = LayerMask.NameToLayer("Interactable");
            millInteract.SetActive(false);
        }
    }

    public void WinConCheck()
    {
        int kills = waveEnemyKills;
            //can change this to be all total kills, specific enemies, etc.

        if (kills >= killCon)
        {
            Debug.Log("wave won");
            lobbyData = waveData.nextLobbyData;
                //win screen
        }

        else if(kills < killCon)
        {
            Debug.Log("not enough to win");
        }

    }
}
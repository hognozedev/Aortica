using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static PlayerData;

public class WaveEvents : MonoBehaviour
{
    public PlayerController player;
    public int waveNumber;
    public GameObject pauseMenu;
    public Volume filmFX;
    public GameObject playerStart;
    public GameObject death;

    private WaifAI waif;
    private HoarfrostAI frost;
    private VivisectorAI vivi;
    private nStage1 phage1;
    private nStage2 phage2;
    private nStage3 phage3;
    public GameObject nMaster;

    bool escPressed;
    bool fxOn;
    bool menuOpen;

    public void Awake()
    {
        Cursor.visible = false;
        Time.timeScale = 1;

        player.inLobby = false;
        if (filmFX == true) fxOn = true;
    }

    public void Update()
    {
        escPressed = player.escapeAction.WasPerformedThisFrame();
        if (escPressed && !menuOpen && death.activeInHierarchy == false) PauseMenu();
        else if (escPressed && menuOpen) Resume();

        if (waifsKilled >=1)
        {
            player.PlayerDeath();
        }

        if (vivisectorsKilled >=1)
        {
            player.PlayerDeath();
        }
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

    public void Scene1()
    {
        SceneManager.LoadScene("TD_WaveScene1");
    }
    public void Scene2()
    {
        waifsKilled = 0;
        SceneManager.LoadScene("TD_WaveScene2");
    }
    public void Scene3()
    {
        vivisectorsKilled = 0;
        SceneManager.LoadScene("TD_WaveScene3");
    }

    public void RespawnS1()
    {
        waifsKilled = 0;
        currentHealth = maxHealth;
        player.UpdatePlayerHealth(0);

        player.cc.enabled = false;
        player.transform.position = playerStart.transform.position;
        player.cc.enabled = true;

        waif = FindFirstObjectByType<WaifAI>();
        StartCoroutine(waif.DestroyEnemy(0));

        Time.timeScale = 1;
        player.ExitedMenu();
        player.hasRun = false;

        Cursor.visible = false;
        death.SetActive(false);

    }

    public void RespawnS2()
    {
        vivisectorsKilled = 0;
        currentHealth = maxHealth;
        player.UpdatePlayerHealth(0);

        player.cc.enabled = false;
        player.transform.position = playerStart.transform.position;
        player.cc.enabled = true;

        frost = FindFirstObjectByType<HoarfrostAI>();
        frost.gameObject.SetActive(false);

        vivi = FindFirstObjectByType<VivisectorAI>();
        StartCoroutine(vivi.DestroyEnemy(0));

        Time.timeScale = 1;
        player.ExitedMenu();
        player.hasRun = false;

        Cursor.visible = false;
        death.SetActive(false);

    }

    public void RespawnS3()
    {
        currentHealth = maxHealth;
        player.UpdatePlayerHealth(0);

        player.cc.enabled = false;
        player.transform.position = playerStart.transform.position;
        player.cc.enabled = true;

        phage1 = FindFirstObjectByType<nStage1>();
        if(phage1 != null) Destroy(phage1.gameObject);

        phage2 = FindFirstObjectByType<nStage2>();
        if (phage2 != null) Destroy(phage2.gameObject);

        phage3 = FindFirstObjectByType<nStage3>();
        if (phage3 != null) StartCoroutine(phage3.DestroyEnemy(0));

        Instantiate(nMaster);

        Time.timeScale = 1;
        player.ExitedMenu();
        player.hasRun = false;

        Cursor.visible = false;
        death.SetActive(false);

    }
}
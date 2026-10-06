using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerData;

public class LobbyEvents : MonoBehaviour
{
    public PlayerController player;
    public GameObject pauseMenu;
    public Image blackScreen;
    public TextMeshProUGUI waveText;
    public Dialogue dialogue;

    [HideInInspector] public LobbyData LSO;

    bool escPressed;
    [HideInInspector] public bool menuOpen;

    public void Update()
    {
        escPressed = player.escapeAction.WasPressedThisFrame();
        if (escPressed && !menuOpen) PauseMenu();
    }

    void Start()
    {
        LSO = PlayerData.nextLobby;
        player.inLobby = true;

        waveText.text = LSO.lobbyName;
            //must set in start here, as it is set in player during awake

        waveText.CrossFadeAlpha(0, 5, false);
        blackScreen.CrossFadeAlpha(0, 3, false);
        StartCoroutine(DialogueWait());

    }

    IEnumerator DialogueWait()
    {
        yield return new WaitForSeconds(5);
        dialogue.StartDialogue();
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
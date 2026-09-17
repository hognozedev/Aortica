using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyEvents : MonoBehaviour
{
    public PlayerController playerController;
    public Image blackScreen;
    public TextMeshProUGUI waveText;
    public Dialogue dialogue;
    public int waveNum;

    public void Awake()
    {
        playerController.inLobby = true;
    }

    public void Start()
    {
        if(waveNum == 1)
        {
            waveText.CrossFadeAlpha(0, 3, false);
            blackScreen.CrossFadeAlpha(0, 5, false);
            StartCoroutine(DialogueWait());
        }
    }

    IEnumerator DialogueWait()
    {
        yield return new WaitForSeconds(5);
        dialogue.StartDialogue();
    }
}
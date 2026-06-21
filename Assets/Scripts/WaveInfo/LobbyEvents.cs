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

    public void Awake()
    {
        Debug.Log("this is wave " + "");
        playerController.inLobby = true;

    }

    public void Start()
    {
        waveText.CrossFadeAlpha(0, 3, false);
        blackScreen.CrossFadeAlpha(0, 5, false);
        StartCoroutine(DialogueWait());

        Debug.Log("RESPONSE WILL BE DISPLAYED HERE, PLEASE CHECK.");
        Debug.Log("");
        Debug.Log("");
        Debug.Log("Move - WASD");
        Debug.Log("Interact - E");
        Debug.Log("Sprint - Shift");
        Debug.Log("Aim - RMB");
        Debug.Log("Shoot - LMB");

    }

    IEnumerator DialogueWait()
    {
        yield return new WaitForSeconds(5);
        dialogue.StartDialogue();
    }
}
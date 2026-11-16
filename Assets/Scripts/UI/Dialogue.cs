using System.Collections;
using TMPro;
using UnityEngine;
using static PlayerData;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;

    private int index;
    public bool isClicking = false;
    public PlayerController player;

    void Start()
    {
        if(player.inLobby) lines = lobbyData.dialogueLines;
        dialogueText.text = string.Empty;
    }

    void Update()
    {    
        if(player.clickAction.WasPressedThisFrame())
        {
            if(dialogueText.text == lines[index])
            {
                NextLine();
            }
            
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
                Debug.Log("fill line");
            }
        }
    }

    public void StartDialogue()
    {
        dialogueText.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
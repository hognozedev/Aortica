using System.Collections;
using TMPro;
using UnityEngine;
using static PlayerData;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private string[] lines;
    public float textSpeed;

    private int index;
    public bool isClicking = false;
    private PlayerController player;

    void Start()
    {
        lines = PlayerData.nextLobby.dialogueLines;
      
        player = FindFirstObjectByType<PlayerController>();
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
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
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
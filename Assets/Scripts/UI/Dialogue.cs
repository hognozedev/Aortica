using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;

    private int index;
    public bool isClicking = false;
    public PlayerController playerController;


    void Start()
    {
        dialogueText.text = string.Empty;

    }

    void Update()
    {    
        if(playerController.clickAction.WasPressedThisFrame())
        {           
            Debug.Log("clicked");

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
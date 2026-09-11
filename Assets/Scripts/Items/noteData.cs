using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class noteData : MonoBehaviour, IInteractable
{
    public string noteText;
    public Sprite noteImage;

    public Image noteImageRef;
    public TextMeshProUGUI noteTextRef;
    public GameObject prompts;
    public GameObject noteUI;
    public PlayerController player;

    public void Update()
    {
        if (player.cancelAction.WasPerformedThisFrame())
        {
            OnFocusLost();
        }

    }

    public void Interact()
    {
        player.InMenu();
        noteUI.SetActive(true);

        noteImageRef.sprite = noteImage;
        noteTextRef.text = noteText;
    }

    public void OnFocusGained()
    {
        prompts.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        prompts.gameObject.SetActive(false);
        noteUI.SetActive(false);
        player.ExitedMenu();
    }

}
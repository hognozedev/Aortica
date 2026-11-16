using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerData;

public class InteractDialogue : MonoBehaviour, IInteractable
{
    public bool CanInteract() => true;
    public GameObject interactPrompt;
    [SerializeField] WaveEvents wave;
    [SerializeField] Dialogue dialogue;

    bool firstInt = true;
    [SerializeField] string[] textLines;

    public void Interact()
    {
        if (wave.waveData.waveNumber == 1)
        {
            dialogue.gameObject.SetActive(true);
            dialogue.lines = textLines;
            dialogue.StartDialogue();

            if (firstInt)
            {
                thingsSeen++;
                firstInt = false;
                wave.Wave1Events();
            }
        }
    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        interactPrompt.gameObject.SetActive(false);
    }
}
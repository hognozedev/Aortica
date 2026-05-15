using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactPrompt;
    private bool warned = false;


    public bool CanInteract() => true;

    public void Interact()
    {
        if (warned == true)
        {
            SceneManager.LoadScene("WaveScene1");
        }

        Debug.Log("Start first wave? You can't go back.");
        warned = true;
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

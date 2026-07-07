using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class interactPrompt : MonoBehaviour
{
    public TextMeshProUGUI interactKey;
    public PlayerController playerController;
    private string keyName;
    private InputAction test;

    private void Awake()
    {
        test = playerController.interactAction;
        keyName = test.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
        interactKey.text = keyName;

    }
}
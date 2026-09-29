using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactPrompt : MonoBehaviour
{
    public TextMeshProUGUI interactKey;
    public InputAction interactAction;
    private string keyName;
    private PlayerInput playerInput; 

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];

        keyName = interactAction.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
        interactKey.text = keyName;
    }
}
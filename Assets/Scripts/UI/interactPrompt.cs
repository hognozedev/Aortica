using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactPrompt : MonoBehaviour
{
    public TextMeshProUGUI interactKey;
    private string keyName;
    private PlayerController player;
    private InputAction intA;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        intA = player.interactAction;

        keyName = intA.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
        interactKey.text = keyName;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputStatic;

public class interactPrompt : MonoBehaviour
{
    public TextMeshProUGUI interactKey;
    private string keyName;

    private void Awake()
    {
        keyName = interactAction.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
        interactKey.text = keyName;
    }
}
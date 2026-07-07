using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI flavourText;
    public string[] options = { "" };

    private void Start()
    {
        Cursor.visible = true;
        string displayText = RandomText();
        flavourText.text = displayText;

    }

    private string RandomText()
    {
        string randomWord = options[Random.Range(0, options.Length)];

        return randomWord;
    }
}
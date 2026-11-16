using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerData;

public class DeathScreen : MonoBehaviour
{
    public WaveEvents waveEvents;
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

    public void TryAgain()
    {
        SceneManager.LoadScene("GameLobby");
        lobbyData = waveEvents.waveData.respawnLobbyData;
    }

    public void Exit()
    {
        //save current game state
        SceneManager.LoadScene("");
    }
}
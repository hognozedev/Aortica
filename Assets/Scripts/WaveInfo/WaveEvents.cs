using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerData;

public class WaveEvents : MonoBehaviour
{
    public PlayerController playerController;
    public int waveNumber;

    public void Awake()
    {
        playerController.inLobby = false;
        Debug.Log("");
        Debug.Log("You need to collect salvage to craft items");
        Debug.Log("");
        Debug.Log("Start by reloading.");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
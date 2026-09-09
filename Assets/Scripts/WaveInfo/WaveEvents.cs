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
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
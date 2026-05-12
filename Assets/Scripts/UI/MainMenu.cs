using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameLobby");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

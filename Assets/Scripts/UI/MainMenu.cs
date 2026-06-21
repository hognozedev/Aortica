using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = true;

    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameLobby");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

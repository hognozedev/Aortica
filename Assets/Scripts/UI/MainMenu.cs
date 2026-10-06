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

    public void SettingsMenu()
    {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

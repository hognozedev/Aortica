using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void Return()
    {
        SceneManager.UnloadSceneAsync("SettingsScene");
    }
}
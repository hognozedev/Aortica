using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisclaimerScreen : MonoBehaviour
{
    public Button button;

    void Start()
    {
        StartCoroutine(ShowButton());
    }

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(2);
        button.interactable = true;
    }

    public void NextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
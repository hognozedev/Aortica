using System.Collections;
using UnityEngine;

public class ScrapMill : MonoBehaviour, IInteractable
{
    private PlayerController playerController;

    public float scrapAmount;
    public float millingTime;

    private bool isJammed;

    private void Awake()
    {
        isJammed = true;
        playerController = GetComponent<PlayerController>();

    }

    private void Start()
    {

        isJammed = true;
        playerController = GetComponent<PlayerController>();
    }

    public void Interact()
    {
        Debug.Log("has interacted");

        if(isJammed)
        {
            MillStart();
        }

    }

    private void MillStopped()
    {
        Debug.Log("mill has stopped");

    }

    private void MillStart()
    {
        Debug.Log("mill is running again");

        StartCoroutine(Running());

    }

    private IEnumerator Running()
    {
        Debug.Log("1");

        yield return new WaitForSeconds(millingTime);
        Debug.Log("2");

    }

}

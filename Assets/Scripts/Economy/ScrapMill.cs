using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrapMill : MonoBehaviour, IInteractable
{
    //player variables
    private PlayerController playerController;
    int currentScrap = 0;
    public TextMeshProUGUI scrapCount;

    //scrap variables
    public int scrapAmount;
    private int timeUntilFull;
    private int fullCapacity;

    //machine function
    private bool isJammed = true;
    public int millingTime = 10;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        isJammed = true;
        scrapCount.text = currentScrap.ToString();
    }

    public void Interact()
    {
        if(isJammed == true)
        {
            Debug.Log("jammed");
            MillStart();
        }

        if(isJammed == false)
        {
            Debug.Log("NOT jammed");
            MillCollection();
        }

    }

    private void MillStopped()
    {
        isJammed = true;
        Debug.Log("mill has stopped");

    }

    private void MillStart()
    {
        Debug.Log("mill is running again");

        isJammed = false;
        StartCoroutine(Running());

    }

    private IEnumerator Running()
    {
        yield return new WaitForSeconds(millingTime);
        MillStopped();

    }

    private void MillCollection()
    {
        currentScrap += scrapAmount;
        scrapCount.text = currentScrap.ToString();
    }

}

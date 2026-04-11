using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrapMill : MonoBehaviour, IInteractable
{
    //player variables
    private PlayerController playerController;
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI scrapCount;

    //scrap variables
    private float decimalScrapAmount;
    public int scrapAmount;
    public int playerScrap;

    private float genSpeed = 0.5f;
    public int fullCapacity;

    //machine function
    private bool isJammed;
    public int millDelay = 5;


    private void Awake()
    {


    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        isJammed = true;

        scrapAmount = 0;
        decimalScrapAmount = 0;
    }

    private void Update()
    {
        if(isJammed == false)
        {
            if (scrapAmount < fullCapacity)
            {
                decimalScrapAmount += Time.deltaTime * genSpeed;
                scrapAmount = Mathf.RoundToInt(decimalScrapAmount);

                scrapCount.text = scrapAmount.ToString();
            }

            else
            {
                Debug.Log("full");
                isJammed = true;
            }

        }
    }

    public void Interact()
    {
        if(isJammed == true)
        {
            MillRestart();
        }

        if(isJammed == false)
        {
            Debug.Log("NOT jammed");
            MillCollection();
        }

    }

    private void MillRestart()
    {
        StartCoroutine(Running());

    }

    private IEnumerator Running()
    {
        yield return new WaitForSeconds(millDelay);

        Debug.Log("mill is running again");
        MillCollection();
        isJammed = false;

    }

    private void MillCollection()
    {
        playerScrap += scrapAmount;

        scrapAmount = 0;
        decimalScrapAmount = 0;

        playerCount.text = playerScrap.ToString();

    }
}

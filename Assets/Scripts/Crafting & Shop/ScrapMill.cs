using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrapMill : MonoBehaviour, IInteractable
{
    //player variables
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI salvCount;
    [SerializeField] private GameObject interactPrompt = null;

    //scrap variables
    private float decimalSalvAmount;
    public int salvAmount;
    public int playerSalv;

    //machine function & stats
    private float genSpeed = 0.5f;
    public int fullCapacity;
    private bool isJammed;
    public int millDelay = 5;

    //interact
    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Start()
    {
        isJammed = true;

        salvAmount = 0;
        decimalSalvAmount = 0;
    }

    private void Update()
    {
        if(isJammed == false)
        {
            if (salvAmount < fullCapacity)
            {
                decimalSalvAmount += Time.deltaTime * genSpeed;
                salvAmount = Mathf.RoundToInt(decimalSalvAmount);

                salvCount.text = salvAmount.ToString();
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
        playerSalv += salvAmount;
        salvAmount = 0;
        decimalSalvAmount = 0;
        playerCount.text = playerSalv.ToString();

    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }
    public void OnFocusLost()
    {
        interactPrompt.gameObject.SetActive(false);
    }
}

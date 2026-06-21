using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerData;

public class ScrapMill : MonoBehaviour, IInteractable
{
    //player variables
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI salvCount;
    [SerializeField] private GameObject interactPrompt = null;

    //scrap variables
    private float decimalSalvAmount;
    public int salvAmount;

    //machine function & stats
    private float genSpeed = 0.5f;
    public int fullCapacity;
    private bool isJammed;
    public int millDelay = 5;
    public float jamChancePerc = 0.25f;

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
                Debug.Log("");
                Debug.Log("Mill is jammed!");
                isJammed = true;
            }
        }

    }

    public void Interact()
    {
        if(isJammed == true)
        {
            Debug.Log("");
            Debug.Log("Mill will restart soon.");
            MillRestart();
        }

        if(isJammed == false)
        {
            Debug.Log("");
            Debug.Log("You collected Salvage");

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

        Debug.Log("");
        Debug.Log("Mill restarted");
        MillCollection();
        isJammed = false;
    }

    private void MillCollection()
    {
        PlayerData.playerSalv += salvAmount;
        salvAmount = 0;
        decimalSalvAmount = 0;
        playerCount.text = PlayerData.playerSalv.ToString();

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

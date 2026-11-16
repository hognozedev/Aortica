using System.Collections;
using UnityEngine;
using static PlayerData;

public class ScrapMill : MonoBehaviour, IInteractable
{
    //player variables
    [SerializeField] private GameObject interactPrompt = null;

    //scripts
    public InventorySlot inventorySlot;

    //scrap variables
    private float decimalSalvAmount;
    public int salvAmount;
    public GameObject scrapFill, scrap1, scrap2, scrap3;

    //machine function & stats
    private float genSpeed = 0.5f;
    public int fullCapacity;
    private bool isJammed;
    public int millDelay = 5;
    public float jamChancePerc = 0.25f;


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
            }

            else
            {
                Debug.Log("");
                Debug.Log("Mill is jammed!");
                isJammed = true;
            }

            
            if (salvAmount >= 2)
            {
                scrap1.SetActive(true);

                if (salvAmount >= 8)
                {
                    scrap2.SetActive(true);

                    if (salvAmount >= 12) scrap3.SetActive(true);
                }
            } 
        }
    }

    public void Interact()
    {
        if(salvAmount <= 0)
        {
            scrap1.SetActive(false);
            scrap2.SetActive(false);
            scrap3.SetActive(false);
        }

        if(isJammed == true)
        {
            Debug.Log("Mill will restart soon.");
            MillRestart();
        }

        if(isJammed == false)
        {
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
        inventorySlot.UpdateSalv();

        PlayerData.playerSalv += salvAmount;
        salvAmount = 0;
        decimalSalvAmount = 0;
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
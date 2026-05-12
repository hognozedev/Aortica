using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static PlayerData;

public class CraftManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List <CraftItems> craftItems;
    [SerializeField] private ShopSlot[] shopSlots;

    //interact
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private GameObject playerHUD = null;

    [SerializeField] private ScrapMill scrapMill;

    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Start()
    {
        PopulateCraftItems();
        canvasGroup.gameObject.SetActive(false);

    }

    public void Interact()
    {
        canvasGroup.gameObject.SetActive(true);
        playerHUD.gameObject.SetActive(false);
        Cursor.visible = true;
    }


    public void PopulateCraftItems()
    {
        for (int i = 0; i < craftItems.Count && i < shopSlots.Length; i++)
        {
            CraftItems craftItem = craftItems[i];
            shopSlots[i].Initialize(craftItem.itemData, craftItem.salvCost);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = craftItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemData itemData, int cost)
    {
        if(itemData != null && PlayerData.playerSalv >= cost)
        {
            //CHECK if(HasInventorySpace)
            PlayerData.playerSalv -= cost;
            scrapMill.playerCount.text = PlayerData.playerSalv.ToString();

        }
        //check that the corresponding shop button has a valid itemData attached, and that the player has enough salvage to buy.

        else if(itemData != null && PlayerData.playerSalv <= cost)
        {
            Debug.Log("not enough salv");
        }

    }

    /*
    private bool HasInventorySpace(ItemData itemData)
    {
        //ONLY IF PLAYER HAS INVENTORY SPACE DO LATER
    }
    */








    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        canvasGroup.gameObject.SetActive(false);
        playerHUD.gameObject.SetActive(true);
        interactPrompt.gameObject.SetActive(false);
        Cursor.visible = false;
    }



}


[System.Serializable]
public class CraftItems
{
    public ItemData itemData;
    public int salvCost;
}

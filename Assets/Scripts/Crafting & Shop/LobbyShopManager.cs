using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static PlayerData;

public class LobbyShopManager : MonoBehaviour, IInteractable
{
    public class ShopItems
    {
        public ItemData itemData;
        public int waveCost;
    }

    [SerializeField] private List <ShopItems> shopItems;
    [SerializeField] private ShopSlot[] shopSlots;

    //interact
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Start()
    {
        PopulateShopItems();
        canvasGroup.gameObject.SetActive(false);

    }

    public void Interact()
    {
        canvasGroup.gameObject.SetActive(true);
        Cursor.visible = true;
    }


    public void PopulateShopItems()
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemData, shopItem.waveCost);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
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
            //PlayerData.playerCount.text =PlayerData.playerSalv.ToString();

        }
        //check that the corresponding shop button has a valid itemData attached, and that the player has enough salvage to buy.

        else if(itemData != null && PlayerData.playerSalv <= cost)
        {
            Debug.Log("not enough wave points");
        }

    }


    /*
    private bool HasInventorySpace(ItemData itemData)
    {
        
    }
    */


    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        canvasGroup.gameObject.SetActive(false);
        interactPrompt.gameObject.SetActive(false);
        Cursor.visible = false;
    }



}

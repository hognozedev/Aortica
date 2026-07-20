using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static PlayerData;

[System.Serializable]
public class ShopItems
{
    public ItemData itemData;
    public int enmCost;
}

public class ShopManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List <ShopItems> shopItems;
    [SerializeField] private ShopSlots[] shopSlots;

    //interact
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Start()
    {
        PopulateCraftItems();
        canvasGroup.gameObject.SetActive(false);

    }

    public void Interact()
    {
        playerController.InMenu();

        canvasGroup.gameObject.SetActive(true);
        Cursor.visible = true;
    }


    public void PopulateCraftItems()
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemData, shopItem.enmCost);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemData itemData, int cost)
    {
        if(itemData != null && PlayerData.playerEnm >= cost)
        {
            //check if player has space
            PlayerData.playerEnm -= cost;

            //OnItemTaken?.Invoke(itemData, cost, amount);

        }
    //check that the corresponding shop button has a valid itemData attached, and that the player has enough salvage to buy.

        else if(itemData != null && PlayerData.playerEnm <= cost)
        {
            Debug.Log("");
            Debug.Log("Not enough Enmity");
        }

    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        playerController.ExitedMenu();

        canvasGroup.gameObject.SetActive(false);
        interactPrompt.gameObject.SetActive(false);
        Cursor.visible = false;
    }
}
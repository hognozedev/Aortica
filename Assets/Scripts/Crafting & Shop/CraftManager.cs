using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CraftItems
{
    public ItemData itemData;
}

public class CraftManager : MonoBehaviour, IInteractable
{
    [Header("Items")]
    [SerializeField] private List <CraftItems> craftItems;
    [SerializeField] private CraftSlots[] craftSlots;

    [Header("References")]
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private CanvasGroup canvasGroup;
    public GameObject inventoryUI;
    public PlayerController playerController;


    private void Start()
    {
        PopulateCraftItems();
        canvasGroup.gameObject.SetActive(false);
    }

    public void Interact()
    {
        playerController.InMenu();
        canvasGroup.gameObject.SetActive(true);
        inventoryUI.SetActive(true);
    }

    public void Update()
    {
        if (playerController.cancelAction.WasPerformedThisFrame())
        {
            playerController.ExitedMenu();
            OnFocusLost();
        }

    }


    public void PopulateCraftItems()
    {
        for (int i = 0; i < craftItems.Count && i < craftSlots.Length; i++)
        {
            CraftItems craftItem = craftItems[i];
            craftSlots[i].Initialize(craftItem.itemData, craftItem.itemData.itemCost);
            craftSlots[i].gameObject.SetActive(true);
        }

        for (int i = craftItems.Count; i < craftSlots.Length; i++)
        {
            craftSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemData itemData, int cost)
    {
        if(PlayerData.playerSalv >= cost)
        {
            Debug.Log("bought");

            //CHECK if(HasInventorySpace)
            PlayerData.playerSalv -= cost;        

        }
        //check that the corresponding shop button has a valid itemData attached, and that the player has enough salvage to buy.

        else if (PlayerData.playerSalv < cost)
        {
            Debug.Log("");
            Debug.Log("Not enough Salvage");
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
        inventoryUI.SetActive(false);

    }
}
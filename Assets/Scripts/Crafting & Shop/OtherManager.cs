using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static PlayerData;

[System.Serializable]
public class OtherItems
{
    public ItemData itemData;
    public int enmCost;
}

public class OtherManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List <OtherItems> otherItems;
    [SerializeField] private OtherSlots[] otherSlots;

    //interact
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private GameObject canvasGroup1;
    [SerializeField] private GameObject canvasGroup2;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Start()
    {
        PopulateCraftItems();
        canvasGroup.gameObject.SetActive(false);

    }
    public void Update()
    {
        if (playerController.cancelAction.WasPerformedThisFrame())
        {
            playerController.ExitedMenu();
            OnFocusLost();
        }

    }

    public void Interact()
    {
        playerController.InMenu();

        canvasGroup.gameObject.SetActive(true);
        Cursor.visible = true;
    }


    public void PopulateCraftItems()
    {
        for (int i = 0; i < otherItems.Count && i < otherSlots.Length; i++)
        {
            OtherItems otherItem = otherItems[i];
            otherSlots[i].Initialize(otherItem.itemData, otherItem.enmCost);
            otherSlots[i].gameObject.SetActive(true);
        }

        for (int i = otherItems.Count; i < otherSlots.Length; i++)
        {
            otherSlots[i].gameObject.SetActive(false);
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

    public void ChangePage()
    {
        canvasGroup1.SetActive(true);
        canvasGroup2.SetActive(false);
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
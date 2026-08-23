using NUnit.Framework.Interfaces;
using System;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private PlayerController playerController;
    private ItemUses itemUses;

    public InventorySlot[] invSlots;
    public ModifySlots[] modSlots;
    public GameObject inventoryUI;

    bool invOpen;


    private void OnEnable()
    {
        PlayerData.OnItemTaken += PopItem;
        PlayerData.OnModify += ModItem;
    }

    private void OnDisable()
    {
        PlayerData.OnItemTaken -= PopItem;
        PlayerData.OnModify -= ModItem;

    }


    void Start()
    {
        playerController = GetComponent<PlayerController>();

        foreach (var slot in invSlots)
        {
            slot.UpdateInv();
        }

        foreach (var slotm in modSlots)
        {
            slotm.UpdateMod();
        }

    }


    void Update()
    {
        if (playerController.inventoryAction.WasPressedThisFrame() && !invOpen) 
        {
            playerController.InMenu();
            inventoryUI.gameObject.SetActive(true);
            invOpen = true;
        }

        if (playerController.cancelAction.WasPressedThisFrame() && invOpen)
        {
            invOpen = false;
            playerController.ExitedMenu();
            inventoryUI.gameObject.SetActive(false);
        }

    }


    public void PopItem(ItemData itemData, int cost, int amount)
    {
        foreach (var slot in invSlots)
        {
            if (slot.itemData == itemData && slot.amount < itemData.stackSize)
            {
                int availableSpace = itemData.stackSize - slot.amount;
                int amountToAdd = Mathf.Min(availableSpace, amount);

                slot.amount += amountToAdd;
                amount -= amountToAdd;
                slot.UpdateInv();

                itemData.playerHas += amountToAdd;

                if (amount <= 0) return;

            }

        }

        foreach (var slot in invSlots)
        {
            if (slot.itemData == null)
            {
                int amountToAdd = Mathf.Min(itemData.stackSize, amount);

                slot.itemData = itemData;
                slot.amount = amount;
                slot.UpdateInv();

                itemData.playerHas += amountToAdd;

                return;
            }

        }

    }

    public void ModItem(ItemData itemData)
    {
        foreach (var slot in modSlots)
        {
            if(slot.itemData == null)
            {
                slot.itemData = itemData;
                slot.UpdateMod();
            }

        }

    }

    public void UseItem(InventorySlot slot)
    {
        ItemEffect(slot.itemData, slot);      

        slot.amount--;
        if (slot.amount <= 0) slot.itemData = null;
        slot.UpdateInv();

    }

//put all item effects here!
    public void ItemEffect(ItemData itemData, InventorySlot slot)
    {
        if (slot.itemData.isHealing == true)
        {
            Debug.Log("there was a healing");

        }

    }

}
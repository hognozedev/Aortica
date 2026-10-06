using System.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerController player;
    private ItemUses itemUses;

    public InventorySlot[] invSlots;
    public ModifySlots[] modSlots;
    public GameObject inventoryUI;

    public bool slotSelected;
    public InventorySlot selectedItem;
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
        player = GetComponent<PlayerController>();

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
        if (player.inventoryAction.WasPressedThisFrame() && !invOpen) 
        {
            player.InMenu();
            inventoryUI.gameObject.SetActive(true);
            invOpen = true;
        }

        if (player.cancelAction.WasPressedThisFrame() && invOpen)
        {
            inventoryUI.gameObject.SetActive(false);
            invOpen = false;
            player.ExitedMenu();
        }

        if(invOpen && slotSelected && player.menuInteractAction.WasPressedThisFrame())
        {
            Debug.Log("interacted with");
            UseItem(selectedItem);
            slotSelected = false;
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
        Debug.Log("used " + slot.itemData.itemName);

        if (slot.itemData.useable)
        {
            if (slot.itemData.isHeal)
            {
                //play healing animtaion
                StartCoroutine(ItemEffect(slot));
            }
        }
        else if (!slot.itemData.useable) Debug.Log(slot.itemData.itemName + " cant be used!");
    }

    private IEnumerator ItemEffect(InventorySlot slot2)
    {
        yield return new WaitForSeconds(slot2.itemData.healTime);

        if(slot2 != null) player.UpdatePlayerHealth(-slot2.itemData.healAmount);
        slot2.amount--;
        if (slot2.amount <= 0) slot2.itemData = null;
        slot2.UpdateInv();
    }
}
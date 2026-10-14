using System.Collections;
using System.Linq;
using UnityEngine;
using static PlayerData;

public class PlayerInventory : MonoBehaviour
{
    private PlayerController player;

    public InventorySlot[] invSlots;
    public ModifySlot[] modSlots;
    public HotbarSlot[] hbSlots;
    public GameObject inventoryUI;
    public WeaponHandler handler;

    [HideInInspector] public bool slotSelected;
    [HideInInspector] public InventorySlot selectedItem;
    bool invOpen;


    private void OnEnable()
    {
        PlayerData.OnItemTaken += PopItem;
        PlayerData.OnGunTaken += GunHotb;
        PlayerData.OnModify += ModItem;
    }

    private void OnDisable()
    {
        PlayerData.OnItemTaken -= PopItem;
        PlayerData.OnGunTaken -= GunHotb;
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

        foreach (var slothb in hbSlots)
        {
            slothb.UpdateHotbar();
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

    public void PopItem(ItemData itemData, int amount)
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
                Debug.Log("new line");

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

    public void GunHotb(ItemData itemData)
    {
        int maxAmount = 1;     //there cannot be more than 1 weapon of each type.
        int maxWeapons = hbSlots.ToArray().Length;

        foreach (var slot in hbSlots)
        {
            if (slot.itemData == itemData && itemData.playerHas < maxAmount)
            {
                int availableSpace = itemData.stackSize - maxAmount;
                int amountToAdd = Mathf.Min(availableSpace, maxAmount);

                slot.UpdateHotbar();

                itemData.playerHas += amountToAdd;

                if (maxAmount <= 0) return;
            }
        }

        foreach (var slot in hbSlots)
        {
            if (slot.itemData == null)
            {
                int amountToAdd = Mathf.Min(itemData.stackSize);

                slot.itemData = itemData;
                slot.UpdateHotbar();

                itemData.playerHas += amountToAdd;

                return;


            }
        }

        if (currentWeapons > maxWeapons) PopItem(itemData, 1);

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
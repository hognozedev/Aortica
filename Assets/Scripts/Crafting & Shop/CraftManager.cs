using UnityEngine;
using System.Collections.Generic;
using static PlayerData;

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
    public GunMaster gunMaster;

    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        PopulateCraftItems();
    }

    public void Interact()
    {
        player.InMenu();
        canvasGroup.gameObject.SetActive(true);
        inventoryUI.SetActive(true);
    }

    public void Update()
    {
        if (player.cancelAction.WasPerformedThisFrame())
        {
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

    public void TryBuyItem(ItemData itemData, int cost, int amount)
    {
        if(itemData.isModifier == true && PlayerData.playerSalv >= cost)
        {
            PlayerData.playerSalv -= cost;
            OnModify?.Invoke(itemData);
            return;
        }

        if(!itemData.isModifier && !itemData.isWeapon && PlayerData.playerSalv >= cost)
        {
            PlayerData.playerSalv -= cost;
            OnItemTaken?.Invoke(itemData, amount);

            //gunMaster.UpdateHUD();
        }
        //check that the corresponding shop button has a valid itemData attached, and that the player has enough salvage to buy.

        if (itemData.isWeapon && PlayerData.playerSalv >= cost)
        {
            if(itemData.playerHas >= 1)
            {
                Debug.Log("you already have this");
                return;
            }

            else if (itemData.playerHas < 1)
            {
                PlayerData.playerSalv -= cost;
                itemData.playerHas = 1;
                PlayerData.currentWeapons++;
                OnGunTaken?.Invoke(itemData);
            }
        }

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
        player.ExitedMenu();
        canvasGroup.gameObject.SetActive(false);
        interactPrompt.gameObject.SetActive(false);
        inventoryUI.SetActive(false);
    }
}
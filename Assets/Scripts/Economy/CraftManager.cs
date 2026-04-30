using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List <CraftItems> craftItems;
    [SerializeField] private ShopSlot[] shopSlots;

    //interact
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private GameObject playerHUD = null;

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
            shopSlots[i].Initialize(craftItem.itemData, craftItem.cost);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = craftItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }


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
    public int cost;
}

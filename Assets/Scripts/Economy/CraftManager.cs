using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraftManager : MonoBehaviour
{
    [SerializeField] private List <CraftItems> craftItems;
    [SerializeField] private ShopSlot[] shopSlots;


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


}

[System.Serializable]
public class CraftItems
{
    public ItemData itemData;
    public int cost;
}

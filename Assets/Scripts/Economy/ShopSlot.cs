using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ShopSlot : MonoBehaviour
{
    public ItemData itemData;
    public TMP_Text itemNameText;
    public TMP_Text costText;

    private int cost;

    public void Initialize(ItemData newItemData, int cost)
    {
        itemData = newItemData;
        itemNameText.text = itemData.name;
        this.cost = cost;
        costText.text = cost.ToString();

    }
}

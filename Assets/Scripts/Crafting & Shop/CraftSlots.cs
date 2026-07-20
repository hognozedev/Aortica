using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class CraftSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public ItemData itemData;
    public TMP_Text itemNameText;
    public TMP_Text costText;
    public Image itemImg;

    [SerializeField] private CraftManager craftManager;
    [SerializeField] private ItemPopup itemPopup;

    public void Initialize(ItemData newItemData, int cost)
    {
        itemData = newItemData;
        itemNameText.text = itemData.itemName;
        itemData.itemCost = cost;
        costText.text = cost.ToString();
        itemImg.sprite = itemData.itemImage;

    }

    public void BuyButtonClicked()
    {
        craftManager.TryBuyItem(itemData, itemData.itemCost, itemData.shopAmount);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(itemData != null) itemPopup.ShowItemPopup(itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemPopup.HideItemPopup();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (itemData != null) itemPopup.FollowMouse();

    }
}
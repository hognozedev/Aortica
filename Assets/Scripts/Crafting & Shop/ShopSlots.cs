using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ShopSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public ItemData itemData;
    public TMP_Text itemNameText;
    public TMP_Text costText;

    private int cost;

    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ItemPopup itemPopup;

    public void Initialize(ItemData newItemData, int cost)
    {
        itemData = newItemData;
        itemNameText.text = itemData.name;
        this.cost = cost;
        costText.text = cost.ToString();

    }

    public void BuyButtonClicked()
    {
        shopManager.TryBuyItem(itemData, cost);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemPopup.ShowItemPopup(itemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemPopup.HideItemPopup();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        itemPopup.FollowMouse();
    }
}

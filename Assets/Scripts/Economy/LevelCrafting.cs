using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Collections.Specialized;

public class LevelCrafting : MonoBehaviour
{
    [SerializeField] private CanvasGroup craftingCanvasGroup = null;

    private Transform container;
    private Transform craftItemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        craftItemTemplate = container.Find("craftTemplate");

        craftingCanvasGroup.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(itemStats.ItemType.RifleAmmo, itemStats.);
    }

    private void CreateItemButton(string itemName, int itemCost)
    {
        Transform craftItemTransform = Instantiate(craftItemTemplate, container);
        RectTransform craftItemRectTransform = craftItemTransform.GetComponent<RectTransform>();

        float craftItemHeight = 30f;
        craftItemRectTransform.anchoredPosition = new Vector2(0, -craftItemHeight * positionIndex);

        craftItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        craftItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());


    }

}

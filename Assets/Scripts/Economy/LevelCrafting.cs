using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Collections.Specialized;

public class LevelCrafting : MonoBehaviour, IInteractable
{
    [SerializeField] private CanvasGroup craftingCanvasGroup = null;
    public Transform container;
    public Transform craftItemTemplate;
    public TextMeshProUGUI nameReference;
    public TextMeshProUGUI costReference;


    private void Awake()
    {
        //craftItemTemplate.gameObject.SetActive(false);
        craftingCanvasGroup.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton("Rifle Ammo", itemStats.GetCost(itemStats.ItemType.RifleAmmo), 0);
        CreateItemButton("Bandage", itemStats.GetCost(itemStats.ItemType.Bandage), 1);

    }

    private void CreateItemButton(string itemName, int itemCost, int positionIndex)
    {
        Transform craftItemTransform = Instantiate(craftItemTemplate, container);
        RectTransform craftItemRectTransform = craftItemTransform.GetComponent<RectTransform>();
        float craftItemHeight = 75f;

        craftItemRectTransform.anchoredPosition = new Vector2(0, -craftItemHeight * positionIndex);

        nameReference.text = itemName;
        costReference.text = itemCost.ToString();

    }

    public void Interact()
    {
        craftingCanvasGroup.gameObject.SetActive(true);
    }

}

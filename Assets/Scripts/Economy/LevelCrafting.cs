using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Collections.Specialized;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.InputSystem;
using static itemStats;
using Unity.VisualScripting;

public class LevelCrafting : MonoBehaviour, IInteractable
{
    //UI
    [SerializeField] private CanvasGroup craftCanvasGroup = null;
    [SerializeField] private GameObject playerHUD = null;
    [SerializeField] private GameObject interactPrompt = null;
    public Transform container;
    public Transform craftItemTemplate;
    public TextMeshProUGUI nameReference;
    public TextMeshProUGUI costReference;

    //interact
    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;

    //references
    private IShop shopInterface;
    private PlayerStats playerStats;
    [SerializeField] private Button itembutton;


    private void Awake()
    {
        craftCanvasGroup.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(itemStats.ItemType.RifleAmmo, "Rifle Ammo", itemStats.GetCost(itemStats.ItemType.RifleAmmo), 0);
        CreateItemButton(itemStats.ItemType.Bandage, "Bandage", itemStats.GetCost(itemStats.ItemType.Bandage), 1);

    }

    private void CreateItemButton(itemStats.ItemType itemType, string itemName, int itemCost, int positionIndex)
    {
        Transform craftItemTransform = Instantiate(craftItemTemplate, container);
        RectTransform craftItemRectTransform = craftItemTransform.GetComponent<RectTransform>();
        float craftItemHeight = 75f;

        craftItemRectTransform.anchoredPosition = new Vector2(0, -craftItemHeight * positionIndex);

        nameReference.text = itemName;
        costReference.text = itemCost.ToString();

    }

    public void OnButtonClick()
    {
        TryBuyItem(itemType);
        //reference button somehow?
    }

    private void TryBuyItem(itemStats.ItemType itemType)
    {
        shopInterface.BoughtItem(itemType);
    }



    public void Interact()
    {
        craftCanvasGroup.gameObject.SetActive(true);
        playerHUD.gameObject.SetActive(false);
        Cursor.visible = true;
    }
    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true); 
    }
    public void OnFocusLost()
    {
        craftCanvasGroup.gameObject.SetActive(false);
        playerHUD.gameObject.SetActive(true);
        interactPrompt.gameObject.SetActive(false);
        Cursor.visible = false;
    }

}

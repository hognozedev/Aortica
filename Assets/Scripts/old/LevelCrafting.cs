/*

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static itemStats;

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

    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Awake()
    {
        craftCanvasGroup.gameObject.SetActive(false);
    }


    private void Start()
    {
        CreateItemButton(itemStats.ItemType.RifleAmmo, "Rifle Ammo", itemStats.GetCost(itemStats.ItemType.RifleAmmo), 0);
        CreateItemButton(itemStats.ItemType.Bandage, "Bandage", itemStats.GetCost(itemStats.ItemType.Bandage), 1);

        if (WaveInfo.waveNumber >= 2)
        {
            CreateItemButton(itemStats.ItemType.ShotgunShell, "Shotgun Shell", itemStats.GetCost(itemStats.ItemType.ShotgunShell), 2);

        }
    }


    private void CreateItemButton(itemStats.ItemType itemType, string itemName, int itemCost, int positionIndex)
    {
        Transform craftItemTransform = Instantiate(craftItemTemplate, container);
        RectTransform craftItemRectTransform = craftItemTransform.GetComponent<RectTransform>();
        float craftItemHeight = 75f;

        craftItemRectTransform.anchoredPosition = new Vector2(0, +craftItemHeight * positionIndex);

        nameReference.text = itemName;
        costReference.text = itemCost.ToString();


        craftItemTransform.GetComponent<Button>().onClick = () =>
        {
            TryBuyItem(itemType);
        };
    }
    

    public void TryBuyItem(itemStats.ItemType itemType)
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

*/
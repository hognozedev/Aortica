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

public class LevelCrafting : MonoBehaviour, IInteractable
{
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


    private void Awake()
    {
        craftCanvasGroup.gameObject.SetActive(false);
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

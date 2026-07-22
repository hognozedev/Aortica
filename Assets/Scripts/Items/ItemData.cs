using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;

    public Sprite itemImage;

    public int itemRarity;
    public int itemCost;
    public int stackSize;
    public int shopAmount;
    public int playerHas;

    public bool useable;
    public bool isModifier;
    public bool isHealing;
    public bool isAmmo;
}
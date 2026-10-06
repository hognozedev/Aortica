using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Global")]
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int itemRarity;
    public int itemCost;
    public int stackSize;
    public int shopAmount;
    public int playerHas;

    [Header("Heal")]
    public bool useable;
    public bool isModifier;
    public bool isHeal;
    public int healAmount;
    public int healTime;

    [Header("Weapon")]
    public bool isAmmo;
    public bool isWeapon;
}
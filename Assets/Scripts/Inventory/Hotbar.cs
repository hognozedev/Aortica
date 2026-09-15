using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public ItemData item;
    public Image img;
    public PlayerController player;

    void Start()
    {
        if(player.inLobby == false) img.sprite = item.itemImage;
    }
}
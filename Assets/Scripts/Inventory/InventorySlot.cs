using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ItemData itemData;
    public int amount;
    public TextMeshProUGUI quantityText;
    public Image itemIcon;
    public TextMeshProUGUI salvText;
    public TextMeshProUGUI descText;
    public PlayerInventory playerInventory;


    public void UpdateSalv()
    {
        salvText.text = PlayerData.playerSalv.ToString();

    }

    public void UpdateInv()
    {
        UpdateSalv();

        if (itemData != null)
        {
            itemIcon.sprite = itemData.itemImage;
            itemIcon.gameObject.SetActive(true);
            quantityText.text = amount.ToString();
        }

        else
        {
            itemIcon.gameObject.SetActive(false);
            quantityText.text = "";
        }

    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData != null) descText.text = itemData.itemDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if (amount > 0 && itemData != null && itemData.useable == true)
       {
            playerInventory.UseItem(this);

       }

    }

}
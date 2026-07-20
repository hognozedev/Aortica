using UnityEngine;
using UnityEngine.UI;

public class ItemUses : MonoBehaviour
{
    public Image modIcon;
    public InventorySlot[] modSlots;


    public void ApplyEffect(ItemData itemData)
    {
        Debug.Log("used" + itemData);

        if(itemData.isModifier == true && itemData != null)
        {
            modIcon.sprite = itemData.itemImage;
            modIcon.gameObject.SetActive(true);
        }
    }
}

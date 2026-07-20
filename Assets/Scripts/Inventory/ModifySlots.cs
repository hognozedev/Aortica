using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifySlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData itemData;
    public TextMeshProUGUI descText;
    public Image itemIcon;

    public void UpdateMod()
    {
        if (itemData != null)
        {
            itemIcon.sprite = itemData.itemImage;
            itemIcon.gameObject.SetActive(true);
        }

        else
        {
            itemIcon.gameObject.SetActive(false);
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


}
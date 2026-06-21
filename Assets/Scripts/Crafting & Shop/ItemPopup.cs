using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ItemPopup : MonoBehaviour
{
    public CanvasGroup itemPopup;
    public TextMeshProUGUI itemDescText;

    private RectTransform popupRect;

    private void Awake()
    {
        popupRect = GetComponent<RectTransform>();
    }

    public void ShowItemPopup(ItemData itemData)
    {
        itemPopup.alpha = 1;
        itemDescText.text = itemData.itemDescription;
    }

    public void HideItemPopup()
    {
        itemPopup.alpha = 0;
        itemDescText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 offset = new Vector3(10, -10, 0);

        popupRect.position = mousePosition + offset;
    }
//was different for new input system 

}
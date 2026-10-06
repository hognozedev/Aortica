using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class OtherItems
{
    public ItemData itemData;
    public int enmCost;
}

public class OtherManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List <OtherItems> otherItems;
    [SerializeField] private OtherSlots[] otherSlots;

    //interact
    [SerializeField] private GameObject interactPrompt = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;

    private PlayerController player;

    [SerializeField] private bool isEnabled = true;
    public bool CanInteract() => isEnabled;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        PopulateOtherItems();
        canvasGroup.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (player.cancelAction.WasPressedThisFrame())
        {
            player.ExitedMenu();
            OnFocusLost();
        }
    }

    public void Interact()
    {
        player.InMenu();

        canvasGroup.gameObject.SetActive(true);
        Cursor.visible = true;
    }


    public void PopulateOtherItems()
    {
        for (int i = 0; i < otherItems.Count && i < otherSlots.Length; i++)
        {
            OtherItems otherItem = otherItems[i];
            otherSlots[i].Initialize(otherItem.itemData, otherItem.enmCost);
            otherSlots[i].gameObject.SetActive(true);
        }

        for (int i = otherItems.Count; i < otherSlots.Length; i++)
        {
            otherSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemData itemData, int cost)
    {
        if (itemData != null && PlayerData.playerEnm >= cost)
        {
            PlayerData.playerEnm -= cost;

            //OnItemTaken?.Invoke(itemData, cost, amount);
        }
        //check that the corresponding shop button has a valid itemData attached, and that the player has enough salvage to buy.

        else if (itemData != null && PlayerData.playerEnm <= cost)
        {
            Debug.Log("");
            Debug.Log("Not enough Enmity");
        }
    }

    public void ChangePage1()
    {
        panel1.SetActive(true); panel2.SetActive(false);
    }

    public void ChangePage2()
    {
        panel2.SetActive(true); panel1.SetActive(false);
    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        player.ExitedMenu();

        canvasGroup.gameObject.SetActive(false);
        interactPrompt.gameObject.SetActive(false);
        Cursor.visible = false;
    }
}
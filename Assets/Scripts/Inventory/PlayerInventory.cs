using UnityEngine;
using System.Text;
using static PlayerData;

public class PlayerInventory : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject inventoryUI;
    bool invOpen;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.inventoryAction.WasPressedThisFrame() && !invOpen)
        {
            playerController.InMenu();
            inventoryUI.gameObject.SetActive(true);
            invOpen = true;
        }

        if(playerController.cancelAction.WasPressedThisFrame() && invOpen)
        {
            invOpen= false;
            playerController.ExitedMenu();
            inventoryUI.gameObject.SetActive(false);
        }

    }

}
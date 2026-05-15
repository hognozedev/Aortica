using UnityEngine;
using System.Text;
using static PlayerData;

public class PlayerInventory : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.inventoryAction.WasPressedThisFrame())
        {
            Debug.Log("");
            Debug.Log("");
            Debug.Log("");
            Debug.Log("Rifle Ammo: " + PlayerData.iRifle);
            Debug.Log("You have salvaged " + PlayerData.playerSalv);

        }

    }

    public void AddItem()
    {
        if(PlayerData.playerSalv >= 4)
        {
            PlayerData.iRifle += 5;
            PlayerData.playerSalv -= 4;

        }
        else
        {
            Debug.Log("Not enough salvage");
        }
    }

}

using UnityEngine;
using static PlayerData;

public class WaveEvents : MonoBehaviour
{
    public PlayerController playerController;
    public int waveNumber;

    public void Awake()
    {
        playerController.inLobby = false;
    }

    public void Start()
    {
        if(waveNumber == 1)
        {
            PlayerData.iRifle = 8;
            Debug.Log("Press 'Tab' to see current inventory.");
        }
    }

}

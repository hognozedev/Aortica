using UnityEngine;

public class WaveEvents : MonoBehaviour
{
    public PlayerController playerController;
    public int waveNumber;

    public void Awake()
    {
        Debug.Log("this is wave " + waveNumber);
        playerController.inLobby = false;
    }

}

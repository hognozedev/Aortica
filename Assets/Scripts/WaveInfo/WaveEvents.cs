using UnityEngine;

public class WaveEvents : MonoBehaviour
{
    public PlayerController playerController;

    public void Awake()
    {
        Debug.Log("this is wave " + "");
        playerController.inLobby = false;
    }

}

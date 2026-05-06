using UnityEngine;

public class LobbyEvents : MonoBehaviour
{
    public PlayerController playerController;

    public void Awake()
    {
        Debug.Log("this is wave " + "");
        playerController.inLobby = true;
    }
}

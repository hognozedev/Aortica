using UnityEngine;

[CreateAssetMenu(fileName = "LobbyData", menuName = "Scriptable Objects/LobbyData")]
public class LobbyData : ScriptableObject
{
    public string lobbyName;
    public int lobbyNumber;
    public string[] dialogueLines;
}
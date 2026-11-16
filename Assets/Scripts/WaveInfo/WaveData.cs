using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    [Header("Wave Control")]
    public int waveNumber;
    public int winKillCount;
    public float maxWaveLength;

    [Header("Scenes")]
    public LobbyData respawnLobbyData;
    public LobbyData nextLobbyData;
}
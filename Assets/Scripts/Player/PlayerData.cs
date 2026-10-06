using System;
using UnityEngine;
public static class PlayerData
{
    [Header("Health")]
    public static int maxHealth = 1000, currentHealth;

    [Header("Stamina")]
    public static float maxStamina = 20, staminaLoss = 10, regenSpeed = 5, regenDelay = 2;

    [Header("Movement")]
    public static float sprintSpeed = 6, walkSpeed = 3;
    public static float camShake;

    [Header("Currency")]
    public static int playerSalv = 200, playerEnm;

    [Header("Inventory")]
    public static Action<ItemData, int, int> OnItemTaken;
    public static Action<ItemData> OnModify;

    [Header("Level Data")]
    public static int waveEnemyKills, vivisectorsKilled, waifsKilled, hoarfrostsKilled, necroEggKilled, necroBabyKilled, necrophagesKilled;
    public static LobbyData nextLobby;
}
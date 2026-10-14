using System;
using UnityEngine;
public static class PlayerData
{
    [Header("Health")]
    public static int maxHealth = 250, currentHealth;

    [Header("Stamina")]
    public static float maxStamina = 20, staminaLoss = 10, regenSpeed = 5, regenDelay = 2;

    [Header("Movement")]
    public static float sprintSpeed = 6, walkSpeed = 3;
    public static float camShake;

    [Header("Currency")]
    public static int playerSalv = 200, playerEnm;

    [Header("Inventory")]
    public static Action<ItemData, int> OnItemTaken;
    public static Action<ItemData> OnGunTaken;
    public static Action<ItemData> OnModify;
    public static int currentWeapons;
    //LAYOUT OF INVENTORY/ ITEMS IT CONTAINS

    [Header("Level Data")]
    public static int waveEnemyKills, vivisectorsKilled, waifsKilled, hoarfrostsKilled, necroEggKilled, necroBabyKilled, necrophagesKilled;
    public static LobbyData nextLobby;
}
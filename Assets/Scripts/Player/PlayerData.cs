using System;
using UnityEngine;
public static class PlayerData
{
    [Header("Health")]
    public static int maxHealth = 100, currentHealth;

    [Header("Stamina")]
    public static float maxStamina = 20, staminaLoss = 10, regenSpeed = 5, regenDelay = 2;

    [Header("Movement")]
    public static float sprintSpeed = 6, walkSpeed = 3;
    public static float camShake;

    [Header("Currency")]
    public static int playerSalv = 100, playerEnm;

    [Header("Inventory")]
    public static Action<ItemData, int> OnItemTaken;
    public static Action<ItemData> OnGunTaken;
    public static Action<ItemData> OnModify;
    public static int currentWeapons;

    [Header("Level Data")]
    public static LobbyData lobbyData;
    public static int thingsSeen;

    [Header("Save File Info")]
    //LAYOUT OF INVENTORY/ ITEMS IT CONTAINS
    public static int waveEnemyKills, vivisectorsKilled, waifsKilled, hoarfrostsKilled, necroEggKilled, necroBabyKilled, necrophagesKilled;
    public static float wave1Time, wave2Time, wave3Time, wave4Time, wave5Time, wave6Time, wave7Time, wave8Time, wave9Time, wave10Time, gameCompletionTime;
    public static int mostRecentWave;
}
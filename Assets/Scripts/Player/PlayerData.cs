using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public static class PlayerData
{
    //health
    public static int maxHealth = 2000, currentHealth;

    //stamina
    public static float maxStamina = 20, staminaLoss = 10, regenSpeed = 5, regenDelay = 2;

    //movement
    public static float sprintSpeed = 6, walkSpeed = 3;
    public static float camShake;

    //currency
    public static int playerSalv = 250, playerEnm;

    //inventory
    public static Action<ItemData, int, int> OnItemTaken;
    public static Action<ItemData> OnModify;

    //level data
    public static int waveEnemyKills, vivisectorsKilled, waifsKilled, hoarfrostsKilled, necroEggKilled, necroBabyKilled, necrophagesKilled;

}
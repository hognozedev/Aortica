using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public static class PlayerData
{
    //health
    public static int maxHealth = 100, currentHealth;

    //stamina
    public static float maxStamina = 20, staminaLoss = 10, regenSpeed = 5, regenDelay = 2;

    //movement
    public static float sprintSpeed = 6, walkSpeed = 3;
    public static float camShake;

    //currency
    public static int playerSalv, playerEnm;

    //inventory
    public static int iRifle, iBandage, iRifleAmmo;

    //level data
    public static int vivisectorsKilled, waveEnemyKills;

}
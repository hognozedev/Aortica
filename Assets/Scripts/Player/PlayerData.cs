using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public static class PlayerData
{
    //health
    public static int maxHealth = 100, currentHealth;

    //stamina
    public static float maxStamina = 20;
    public static float staminaLoss = 10;
    public static float regenSpeed = 5;
    public static float regenDelay = 2;
    public static float sprintSpeed = 6;
    public static float walkSpeed = 3;

    //currency
    public static int playerSalv, playerEnm;

    //enemies
    public static int vivisectorsKilled, waveEnemyKills;

    //inventory
    public static int iRifle, iBandage;

    //states
    public static bool playerCanMove = true;

}
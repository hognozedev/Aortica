using System.Collections;
using System;
using UnityEngine;

public static class PlayerData
{    
    
        //health
        public static float maxHealth = 100;
        public static float currentHealth;

        //stamina
        public static float maxStamina = 20;
        public static float staminaLoss = 10;
        public static float regenSpeed = 5;
        public static float regenDelay = 2;
        public static float sprintSpeed = 6;
        public static float walkSpeed = 3;

        //currency
        public static int playerSalv;
        public static int playerEnmity;
    
}
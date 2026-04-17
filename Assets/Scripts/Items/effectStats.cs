using UnityEngine;
using System.Collections;

public class effectStats
{
    public PlayerController playerController;

    public enum StatusEffect
    {
        Fatigue,
        Hunger,
    }

    public static StatusEffect statusEffect;
}

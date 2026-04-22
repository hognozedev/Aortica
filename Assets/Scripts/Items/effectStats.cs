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

    /*
    public static int GetEffect(StatusEffect statusEffect)
    {
        switch (statusEffect)
        {
            case StatusEffect.Fatigue:  return 1;
            case StatusEffect.Hunger:  return 2;
        }
    }

    public void Fatigue()
    {
        Debug.Log("player now has fatigue");
    }

    public void Hunger()
    {
        Debug.Log("player now has fatigue");
    }
    */
}

using UnityEngine;
using System.Collections;

public class effectStats
{
    public PlayerController playerController;

    public enum StatusEffect
    {
        Fatigue,
        Hunger,
        Hatred
    }

    public void Fatigue()
    {
        Debug.Log("player now has fatigue");
    }

    public void Hunger()
    {
        Debug.Log("player now has hunger");
    }

    public void Hatred()
    {
        Debug.Log("player now has hatred");
    }

}
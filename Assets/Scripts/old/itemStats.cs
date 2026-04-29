using UnityEngine;
using System.Collections;

public class itemStats
{
    public enum ItemType
    {
        RifleAmmo,
        Bandage,
        ShotgunShell
    }

    public static int GetCost(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.RifleAmmo:    return 2;
            case ItemType.Bandage:      return 3;    
            case ItemType.ShotgunShell: return 5;    
        }
    }

}
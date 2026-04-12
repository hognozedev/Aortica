using UnityEngine;
using System.Collections;

public class itemStats
{
    public enum ItemType
    {
        RifleAmmo,
        Bandage
    }

    public static int GetCost(ItemType itemType)
    {
        switch(itemType)
        {
            default:
            case ItemType.RifleAmmo:    return 1;
            case ItemType.Bandage:      return 2;    
        }
    }

}

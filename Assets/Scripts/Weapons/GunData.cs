using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public LayerMask targetLayerMask;
    public string gunName;
    public Sprite gunIcon;
    public ItemData ammoType;

    [Header("Fire")]
    public int shootingRange;
    public int fireRate;
    public int bulletDamage;

    public float bulletSpread;
    public float bulletPenetration;
    public float bulletDrop;

    [Header("Reload")]
    public int magSize;
    public float reloadTime;

    [Header("Error")]
    public float jamChance;
    public float jamFix;

}
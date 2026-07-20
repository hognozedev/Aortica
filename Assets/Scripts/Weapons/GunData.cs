using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public LayerMask targetLayerMask;
    public string gunName;
    public Sprite gunIcon;
    public int currentAmmo;
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
    public int jamChance = 15;
    public float jamFix = 2f;

}
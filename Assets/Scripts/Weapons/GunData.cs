using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [Header("Refs")]
    public GameObject inSceneObj;
    public ItemData ammoType;

    public LayerMask targetLayerMask;
    public string gunName;
    public Sprite gunIcon;

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
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public LayerMask targetLayerMask;
    public string gunName;

    [Header("Fire Stats")]
    public int shootingRange;
    public int fireRate;
    public int bulletDamage;

    public float bulletSpread;
    public float bulletPenetration;

    [Header("Reload Stats")]
    public int magSize;
    public float reloadTime;

}

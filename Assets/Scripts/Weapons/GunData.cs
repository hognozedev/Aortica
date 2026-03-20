using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public LayerMask targetLayerMask;
    public string gunName;

    [Header("Fire Stats")]
    public float shootingRange;
    public float fireRate;
    public float bulletDamage;

    //public float bulletSpread;
    //public float bulletPenetration;

    [Header("Reload Stats")]
    public float magSize;
    public float reloadTime;

}

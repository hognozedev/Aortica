using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public LayerMask targetLayerMask;
    public string gunName;

    [Header("Fire Config")]
    public float shootingRange;
    public float fireRate;
    public float bulletDamage;

    [Header("Reload Config")]
    public float magSize;
    public float reloadTime;
}

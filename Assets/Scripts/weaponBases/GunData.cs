using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public LayerMask targetMask;
    public string gunName;

    [Header("Fire Config")]
    public float shootingRange;
    public float fireRate;

    [Header("Reload Config")]
    public float magSize;
    public float reloadTime;
}

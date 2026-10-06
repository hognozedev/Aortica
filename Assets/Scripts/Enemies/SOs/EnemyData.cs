using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public string enemyName;
    public float enemyDamage, enemyHealth, damageRanged, attackWindup, timeBetweenAttacks, attackRange;
}
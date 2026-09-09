using UnityEngine;
using VolFx;

public class EnemyProjectile : MonoBehaviour
{
    public EnemyData enemyData;
    float vDamage;

    public void Start()
    {
        vDamage = enemyData.enemyDamageRanged * Random.Range(0.8f, 1.2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.UpdatePlayerHealth((int)vDamage); Destroy(gameObject);
        }

    //if other enemies get hit then do other things below.

        Destroy(gameObject, 0.5f);
    }
}
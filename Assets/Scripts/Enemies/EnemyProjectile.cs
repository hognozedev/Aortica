using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    int vDamage;
    public int dmgMin, dmgMax;

    public void Start()
    {
        vDamage = Random.Range(dmgMin, dmgMax);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.UpdatePlayerHealth(vDamage); Destroy(gameObject);
        }

    //if other enemies get hit then do other things below.

        Destroy(gameObject, 0.5f);
    }
}
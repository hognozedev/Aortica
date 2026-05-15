using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int vDamage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.UpdatePlayerHealth(vDamage); Destroy(gameObject);
        }
        Destroy(gameObject, 0.5f);
    }
}
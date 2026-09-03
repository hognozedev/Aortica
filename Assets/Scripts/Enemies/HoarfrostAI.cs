using UnityEngine;
using System.Collections;
using static PlayerData;

public class HoarfrostAI : MonoBehaviour
{
    public EnemyData enemyData;

    private int enemyHealth;


    void Start()
    {
        Debug.Log("Spawned!");

        float eHFloat = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eHFloat;

    }

    public void TakeDamage(int damage, Collider col)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0) EnemyDeath();

    }

    private void EnemyDeath()
    {
        //vAnimator.SetTrigger("vDie");
        gameObject.SetActive(false);

        hoarfrostsKilled += 1;
        waveEnemyKills += 1;
    }

}
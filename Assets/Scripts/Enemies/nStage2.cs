using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using static PlayerData;

public class nStage2 : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform enemy;
    public GameObject[] hideyHoles;

    [Header("Variables")]
    public float attackRange;
    public LayerMask groundMask, playerMask;

    bool inAttackRange;
    bool alreadyAttacked;
    private PlayerController player;
    private int enemyHealth;
    private float enemyRadius = 250;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        float eHFloat = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eHFloat;

    }

    private void Update()
    {
        inAttackRange = Physics.CheckSphere(enemy.position, attackRange, playerMask);

        if (!inAttackRange) Chase();
        if (inAttackRange) Attack();

    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);

    }

    private void Attack()
    {
        agent.SetDestination(enemy.position);
        //stop on spot, and look at the player

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(Retreat), Random.Range(0.5f, 2));

            float randomDmg = enemyData.enemyDamage * Random.Range(0.8f, 1.2f);
            player.UpdatePlayerHealth((int)randomDmg);

            Debug.Log(randomDmg);
        }
    }

    private void Retreat()
    {
        Debug.Log("attacked");

        //GameObject closestHide = Physics.CheckSphere(enemy.position, enemyRadius)

        //agent.SetDestination(closestHide.transform.position);

    }

    public void TakeDamage(int damage, Collider col)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0) EnemyDeath();

    }

    private void EnemyDeath()
    {
        //vAnimator.SetTrigger("vDie");
        StartCoroutine(DestroyEnemy());

        necroBabyKilled += 1;
        waveEnemyKills += 1;
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

}
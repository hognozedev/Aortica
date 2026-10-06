using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static PlayerData;

public class WaifAI : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform enemy;
    private PlayerController player;
    [HideInInspector] public GameObject spawnPoint;

    [Header("Variables")]
    //public float attackRange;
    public float sightRange;
    public LayerMask groundMask, playerMask;

    [Header("Body Parts")]
    public Collider head;

    [Header("Animation")]
    public Animator bloodAnim;

    //patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange = 50f;
    private int enemyHealth;
    private WaifMaster waifMaster;

    //attack
    bool alreadyAttacked, windingUp;
    bool inSightRange, inAttackRange;


    private void Start()
    {
        float eHFloat = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eHFloat;

        player = FindFirstObjectByType<PlayerController>();
        waifMaster = FindFirstObjectByType<WaifMaster>();
    }


    private void Update()
    {
        inSightRange = Physics.CheckSphere(enemy.position, sightRange, playerMask);
        inAttackRange = Physics.CheckSphere(enemy.position, enemyData.attackRange, playerMask);

        if (!inSightRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange && !windingUp) AttackWindup();
    //when the enemy should run each different phase
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = enemy.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    //find a walk point, move to it, then repeat
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(enemy.position.x + randomX, enemy.position.y, enemy.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -enemy.up, 2f, groundMask)) walkPointSet = true;
    //get the enemy to go to a random point (that is above ground)
    }

    private void Chase()
    {
        agent.SetDestination(player.transform.position);
    //enemy.LookAt(player); this looks weird maybe use with finished animations ?
    }

    private void AttackWindup()
    {
        windingUp = true;
        agent.SetDestination(enemy.position);

        Invoke(nameof(Attack), enemyData.attackWindup);
    }

    private void Attack()
    {
        if (!alreadyAttacked && inAttackRange)
        {
            Debug.Log("asttack");

            alreadyAttacked = true;

            float randomDmg = enemyData.enemyDamage * Random.Range(0.8f, 1.2f);
            player.UpdatePlayerHealth((int)randomDmg);

            Invoke(nameof(ResetAttack), enemyData.timeBetweenAttacks);
        }

        else if (!inAttackRange)
        {
            Debug.Log("attack anim no damage");

            Invoke(nameof(ResetAttack), enemyData.timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        windingUp = false;
    }

    public void TakeDamage(int damage, Collider col)
    {
        bloodAnim.SetTrigger("PlayHit");

        if (!inSightRange && !inAttackRange) Chase();

        float hsFloat = damage * 0.25f;
        int headshotDmg = (int) hsFloat;

        if (col == head) enemyHealth -= damage + headshotDmg;
        else enemyHealth -= damage;

        if (enemyHealth <= 0) EnemyDeath();
    }

    private void EnemyDeath()
    {
        Debug.Log("enemy death");
        if(waifMaster != null) waifMaster.ResetSpawnPoint(spawnPoint);
        StartCoroutine(DestroyEnemy());

        waifsKilled+=1;
        waveEnemyKills+=1;
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
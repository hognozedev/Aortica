using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using static PlayerData;

public class VivisectorAI : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform enemy;
    public Animator vAnimator;
    public EnemyProjectile vProjectile;
    [HideInInspector] public GameObject spawnPoint;

    [Header("Variables")]
    public float sightRange;
    public LayerMask groundMask, playerMask;
    public Collider critSpot;

    //enemy
    bool isOpen, isClose;
    private int enemyHealth;
    private ViviMaster viviMaster;
    private PlayerController player;

    //patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange = 50f;

    //attack
    bool alreadyAttacked, windingUp;
    bool inSightRange;
    bool inAttackRange;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        viviMaster = FindFirstObjectByType<ViviMaster>();

        float eh = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eh;
    }


    private void Update()
    {
        inSightRange = Physics.CheckSphere(enemy.position, sightRange, playerMask);
        inAttackRange = Physics.CheckSphere(enemy.position, enemyData.attackRange, playerMask);

        if (!inSightRange && !inAttackRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange && !windingUp) AttackWindup();
    //when the enemy should run each different phase

    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = enemy.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    //find a walk point, move to it, then repeat
    }

    private void SearchWalkPoint()
    {
        if (!isClose)
        {
            //vAnimator.SetTrigger("vClose");
            isClose = true;
            isOpen = false;
        }

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(enemy.position.x + randomX, enemy.position.y, enemy.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -enemy.up, 2f, groundMask))
            walkPointSet = true;
    //get the enemy to go to a random point (that is above ground)
    }

    private void Chase()
    {
        agent.SetDestination(player.transform.position);
        enemy.LookAt(player.transform.position);
    //move to the player
    }

    private void AttackWindup()
    {
        windingUp = true;
        agent.SetDestination(enemy.position);
        enemy.LookAt(player.transform);

        Invoke(nameof(Attack), enemyData.attackWindup);
    }

    private void Attack()
    {
        if (!isOpen)
        {
            //vAnimator.SetTrigger("vOpen");
            isOpen = true;
            isClose = false;
        }

        if (!alreadyAttacked && inAttackRange)
        {
            Rigidbody rb = Instantiate(vProjectile, enemy.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(enemy.forward * 32f, ForceMode.Impulse);
            rb.AddForce(enemy.up * 8f, ForceMode.Impulse);

            ///

            alreadyAttacked = true;
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
        if (enemyHealth <= 0) EnemyDeath();

        if (col == critSpot)
        {
            Debug.Log("taking damage");
            enemyHealth -= damage;
        }
    }

    private void EnemyDeath()
    {
        Debug.Log("enemy death");

        if(viviMaster != null) viviMaster.ResetSpawnPoint(spawnPoint);
        StartCoroutine(DestroyEnemy());

        vivisectorsKilled+=1;
        waveEnemyKills+=1;
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
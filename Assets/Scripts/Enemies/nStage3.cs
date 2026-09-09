using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static PlayerData;

public class nStage3 : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform enemy;
    public EnemyProjectile wProjectile;

    [Header("Variables")]
    public float sightRange;
    public float attackRange;
    public float dashRange;
    public float meleeRange;
        //melee range should always be about half of attack range
    public LayerMask groundMask, playerMask;

    [Header("Body Parts")]
    public Collider head;

    //patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange = 50f;
    private int enemyHealth;
    private PlayerController player;

    bool alreadyAttacked, inSightRange, inAttackRange, inMeleeRange, inDashRange;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        float eHFloat = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eHFloat;
    }

    private void Update()
    {
        inSightRange = Physics.CheckSphere(enemy.position, sightRange, playerMask);
        inAttackRange = Physics.CheckSphere(enemy.position, attackRange, playerMask);
        inMeleeRange = Physics.CheckSphere(enemy.position, meleeRange, playerMask);
        inDashRange = Physics.CheckSphere(enemy.position, dashRange, playerMask);

        if (!inSightRange) Patrol();
        if (inSightRange && !inDashRange) Chase();
        if (inAttackRange && !inDashRange) RangeAttack();
        if (inDashRange && !inMeleeRange) Dash();
        //if (inAttackRange && inMeleeRange) MeleeAttack();
    //when the enemy should run each different phase
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = enemy.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {

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

    }

    private void RangeAttack()
    {
        Debug.Log("ranged");

        agent.SetDestination(enemy.position);
        enemy.LookAt(player.transform);
        //stop on spot, and look at the player

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(wProjectile, enemy.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(enemy.forward * 32f, ForceMode.Impulse);
            rb.AddForce(enemy.up * 8f, ForceMode.Impulse);

            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), Random.Range(1f, 2));

        }
    }

    private void Dash()
    {
        Debug.Log("dash");
        agent.SetDestination(player.transform.position);

        InvokeRepeating("MeleeAttack", 0, 0.2f);

    }

    private void MeleeAttack()
    {
        Debug.Log("melee");

        if (inMeleeRange)
        {
            agent.SetDestination(enemy.position);
            //stop on spot, and look at the player

            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), Random.Range(0.5f, 2));

                float randomDmg = enemyData.enemyDamage * Random.Range(0.8f, 1.2f);
                player.UpdatePlayerHealth((int)randomDmg);

                Debug.Log(randomDmg);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(int damage, Collider col)
    {
        if (!inSightRange && !inAttackRange) Chase();

        float hsFloat = damage * 0.25f;
        int headshotDmg = (int)hsFloat;

        if (col == head) enemyHealth -= damage + headshotDmg;
        else enemyHealth -= damage;

        if (enemyHealth <= 0) EnemyDeath();
    }

    private void EnemyDeath()
    {
        //vAnimator.SetTrigger("vDie");
        StartCoroutine(DestroyEnemy());

        necrophagesKilled += 1;
        waveEnemyKills += 1;
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
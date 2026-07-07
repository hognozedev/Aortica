using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using static PlayerData;

public class VivisectorAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform enemy;
    public Animator vAnimator;
    public EnemyProjectile vProjectile;

    [Header("Variables")]
    public int hMin;
    public int hMax;
    public float sightRange, attackRange;
    public LayerMask groundMask, playerMask;

    //enemy
    bool isOpen, isClose;
    private int enemyHealth;

    //patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange = 50f;

    //attack
    bool alreadyAttacked;
    bool inSightRange, inAttackRange;


    private void Start()
    {
        enemyHealth = Random.Range(hMin, hMax);

    }


    private void Update()
    {
        inSightRange = Physics.CheckSphere(enemy.position, sightRange, playerMask);
        inAttackRange = Physics.CheckSphere(enemy.position, attackRange, playerMask);

        if (!inSightRange && !inAttackRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange) Attack();
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
            vAnimator.SetTrigger("vClose");
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
        agent.SetDestination(player.position);
        enemy.LookAt(player);
    //move to the player
    }

    private void Attack()
    {
        if (!isOpen)
        {
            vAnimator.SetTrigger("vOpen");
            isOpen = true;
            isClose = false;
        }

        agent.SetDestination(enemy.position);
        enemy.LookAt(player);
        //stop on spot, and look at the player

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(vProjectile, enemy.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(enemy.forward * 32f, ForceMode.Impulse);
            rb.AddForce(enemy.up * 8f, ForceMode.Impulse);

            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), Random.Range(0.5f, 2));

        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0) EnemyDeath();

    }

    private void EnemyDeath()
    {
        vAnimator.SetTrigger("vDie");
        StartCoroutine(DestroyEnemy());

        vivisectorsKilled+=1;
        waveEnemyKills+=1;
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(transform.parent.gameObject);

    }
}
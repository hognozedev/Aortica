using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;
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
    public float attackRange;
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
    bool alreadyAttacked;
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
        inAttackRange = Physics.CheckSphere(enemy.position, attackRange, playerMask);

        if (!inSightRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange) Attack();
    //when the enemy should run each different phase

    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = enemy.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    //find a walk point, move to it, then repeat
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
        //enemy.LookAt(player); this looks weird maybe use with finished animations ?

    }

    
    private void Attack()
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

    private void ResetAttack()
    {
        alreadyAttacked = false;

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
        StartCoroutine(DestroyEnemy());

        waifsKilled+=1;
        waveEnemyKills+=1;
    }

    IEnumerator DestroyEnemy()
    {
        waifMaster.ResetSpawnPoint(spawnPoint);

        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

}
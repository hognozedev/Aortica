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
    private Vector3 lastKnownPlayerPosition;
    private float lastKnownPositionTolerance = 1f;
    [SerializeField] private LayerMask obstructionLayerMask;
    private Vector3 raycastOffset = new Vector3(0f, -0.5f, 0f);
    private float fieldOfView = 90f;

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

        if (inSightRange && inAttackRange && !windingUp) AttackWindup();
        else if (inSightRange && CanSeePlayer()) { lastKnownPlayerPosition = Vector3.zero; Chase(); }
        else if (lastKnownPlayerPosition != Vector3.zero) ChaseLastKnownPosition();
        else Patrol();
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
        lastKnownPlayerPosition = player.transform.position;
        //enemy.LookAt(player); this looks weird maybe use with finished animations ?
    }

    private void ChaseLastKnownPosition()
    {
        agent.SetDestination(lastKnownPlayerPosition);

        if (Vector3.Distance(enemy.position, lastKnownPlayerPosition) < lastKnownPositionTolerance)
        {
            lastKnownPlayerPosition = Vector3.zero;
        }
    }

    private bool CanSeePlayer()
    {
        // Calculates direction to player
        Vector3 dirToPlayer = player.transform.position - enemy.position;
        float distanceToPlayer = dirToPlayer.magnitude;
        // Is player in fov cone?
        Vector3 flatDir = new Vector3(dirToPlayer.x, 0, dirToPlayer.z).normalized;
        Vector3 flatForward = new Vector3(enemy.forward.x, 0, enemy.forward.z).normalized;
        if (Vector3.Angle(flatForward, flatDir) > fieldOfView / 2f)
        {
            return false;
        }
        // Raycast to see if theres anything between the enemy and the player
        Vector3 raycastOrigin = enemy.position + raycastOffset;
        if (Physics.Raycast(raycastOrigin, dirToPlayer.normalized, out RaycastHit hit, distanceToPlayer, obstructionLayerMask))
        {
            Debug.Log("can't see because of: " + hit.collider.name); //Environment (layer) is blocking LOS to player
            return false;
        }
        return true;
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
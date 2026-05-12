using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class VivisectorAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;

    //patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attack
    bool alreadyAttacked;
    public GameObject projectile;
    public float enemyHealth;

    //ranges
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    //special
    private Animator vAnimator;
    private bool isOpen, isClose;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        vAnimator = GetComponent<Animator>();   

    }

    private void Update()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!inSightRange && !inAttackRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange) Attack();
    //when the enemy should run each different phase

    }


    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

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
        }

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;
    //get the enemy to go to a random point (that is above ground)

    }


    private void Chase()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    //move to the player

    }

    private void Attack()
    {
        if (!isOpen)
        {
            vAnimator.SetTrigger("vOpen");
            isOpen = true;
        }

        agent.SetDestination(transform.position);
        transform.LookAt(player);
        //stop on spot, and look at the player

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

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
        //Debug.Log("took " + damage + " damage");
        enemyHealth -= damage;
        if(enemyHealth <= 0) EnemyDeath();

    }

    private void EnemyDeath()
    {
        vAnimator.SetTrigger("vDie");
        StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }

}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static PlayerData;

public class nStage2 : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("References")]
    public NavMeshAgent agent;
    public Transform enemy;
    public GameObject stage3;
    public MeshRenderer mesh;

    [Header("Variables")]
    public LayerMask groundMask, playerMask, holeMask;

    bool inAttackRange, alreadyAttacked, retreating;
    private PlayerController player;
    private int enemyHealth;
    private GameObject closestHide;
    private List<GameObject> holeList = new List<GameObject>();


    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        float eHFloat = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eHFloat;
    }

    private void Update()
    {
        inAttackRange = Physics.CheckSphere(enemy.position, enemyData.attackRange, playerMask);

        if (!inAttackRange && !retreating) Chase();
        if (inAttackRange && !retreating) WindupAttack();
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    private void WindupAttack()
    {
        agent.SetDestination(enemy.position);

        Debug.Log("windup animation");

        Invoke(nameof(Attack), enemyData.attackWindup);
    }

    private void Attack()
    {
        agent.SetDestination(enemy.position);
    //stop on spot, and look at the player

        if (!alreadyAttacked)
        {
            Debug.Log("attack");

            alreadyAttacked = true;
            Retreat();

            float randomDmg = enemyData.enemyDamage * Random.Range(0.8f, 1.2f);
            player.UpdatePlayerHealth((int)randomDmg);
        }
    }

    private void Retreat()
    {
        holeList = GameObject.FindGameObjectsWithTag("Hole").ToList();
        float lowestDist = Mathf.Infinity;


        for (int i = 0; i < holeList.Count; i++)
        {
            float dist = Vector3.Distance(holeList[i].transform.position, transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                closestHide = holeList[i];
            }
        }
        retreating = true;
        agent.SetDestination(closestHide.transform.position);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            float timeToIncubate = Random.Range(5,10);
            mesh.enabled = false;

            StartCoroutine(Incubate(timeToIncubate));
        }
    }

    IEnumerator Incubate(float wait)
    {
        Debug.Log("running ienum");

        yield return new WaitForSeconds(wait);
        Instantiate(stage3, closestHide.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void TakeDamage(int damage, Collider col)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0) EnemyDeath();
    }

    private void EnemyDeath()
    {
        necroBabyKilled += 1;
        waveEnemyKills += 1;

        Destroy(gameObject);
    }
}
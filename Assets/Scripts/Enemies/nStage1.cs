using UnityEngine;
using static PlayerData;

public class nStage1 : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("Refs")]
    public GameObject stage2;

    [Header("Vars")]
    public int fallChance = 5;

    private int enemyHealth;
    private bool isGrounded = false;
    private Vector3 currentPlace;
    private PlayerController player;


    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        float eHFloat = enemyData.enemyHealth * Random.Range(0.8f, 1.2f);
        enemyHealth = (int)eHFloat;

        InvokeRepeating("CheckFall", 4, 2);
    }

    void CheckFall()
    {
        if (isGrounded == true) return;

        int chanceNum = Random.Range(1, 10);
        if (chanceNum <= fallChance) gameObject.GetComponent<Rigidbody>().useGravity = true;
        else return;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("hit floor");
            isGrounded = true;
            currentPlace = gameObject.transform.position;

            Evolve();

        }
    }

    void Evolve()
    {
        Instantiate(stage2, currentPlace, Quaternion.identity);
        Destroy(gameObject);

    }

    public void TakeDamage(int damage, Collider col)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0) EnemyDeath();
    }

    void EnemyDeath()
    {
        Destroy(gameObject);

        necroEggKilled += 1;
        waveEnemyKills += 1;
    }

}
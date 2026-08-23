using UnityEngine;

public class HoarfrostSP : MonoBehaviour
{
    [Header("References")]
    public GameObject[] spawnPoints;
    public Transform player;

    [Header("Variables")]
    public int maxAmount;
    public LayerMask playerMask;
    public int spawnRate = 2;
    public int startDelay = 5;
    public float enemyRadius;

    private bool inSpawnRange;
    private GameObject[] enemies;
    private int currentEnemies;
    private GameObject chosenEnemy;


    void Start()
    {
        InvokeRepeating("CheckSP", startDelay, spawnRate);
    }

    void CheckSP()
    {
        enemies = GameObject.FindGameObjectsWithTag("Hoarfrost");
        foreach(GameObject gameObject in enemies) currentEnemies++;

        int index = Random.Range(0, spawnPoints.Length);
        chosenEnemy = spawnPoints[index];

        //Debug.Log(chosenEnemy);

        inSpawnRange = Physics.CheckSphere(chosenEnemy.transform.position, enemyRadius, playerMask);

        if (currentEnemies <= maxAmount && !chosenEnemy.activeInHierarchy && inSpawnRange)
        {
            SpawnEnemy(chosenEnemy);
        }

        currentEnemies = 0;
    }
    // pick a random spawn point, see if the player is close enough to it, and if so set gameobj to visible.


    void SpawnEnemy(GameObject chosenRef)
    {
        chosenRef.SetActive(true);
    }

}
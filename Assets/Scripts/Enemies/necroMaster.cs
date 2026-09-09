using UnityEngine;

//the necrophage will attempt to evolve at random intervals with a random chance. eggs can spawn at any spawn point
//regardless of player proximity

public class necroMaster : MonoBehaviour
{
    [Header("Refs")]
    public GameObject[] spawnPoints;
    public GameObject n1Prefab;

    [Header("Vars")]
    public int startDelay = 2;
    public int spawnRate = 2;
    public int maxSpawns;

    private int currentEnemies = 0;

    void Start()
    {
        InvokeRepeating("SpawnNecro", startDelay, spawnRate * Random.Range(0.8f, 1.2f));

    }  

    void SpawnNecro()
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject chosenPoint = spawnPoints[index];

        if (currentEnemies <= maxSpawns && chosenPoint.activeInHierarchy)
        {
            currentEnemies++;
            chosenPoint.SetActive(false);

            GameObject currentInst;
            currentInst = Instantiate(n1Prefab, chosenPoint.transform.position, Quaternion.identity);
            currentInst.TryGetComponent<nStage1>(out nStage1 n1); n1.spawnPoint = chosenPoint;
        }

    }

    public void ResetSpawnPoint(GameObject spawnPoint)
    {
        Debug.Log("reset point");

        currentEnemies--;
        spawnPoint.SetActive(true);
    }

}
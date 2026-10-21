using UnityEngine;

public class WaifMaster : MonoBehaviour
{
    [Header("Refs")]
    public GameObject[] spawnPoints;
    public GameObject prefab;

    [Header("Vars")]
    public int startDelay = 2;
    public int spawnRate = 2;
    public int maxSpawns;

    private int currentEnemies = 0;

    public void StarInvokeSpawn()
    {
        InvokeRepeating("Spawn", startDelay, spawnRate * Random.Range(0.8f, 1.2f));
    }

    void Spawn()
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject chosenPoint = spawnPoints[index];

        if (currentEnemies <= maxSpawns && chosenPoint.activeInHierarchy)
        {
            currentEnemies++;
            chosenPoint.SetActive(false);

            GameObject currentInst;
            currentInst = Instantiate(prefab, chosenPoint.transform.position, Quaternion.identity);
            currentInst.TryGetComponent<WaifAI>(out WaifAI wai); wai.spawnPoint = chosenPoint;

        }
    }

    public void ResetSpawnPoint(GameObject spawnPoint)
    {
        Debug.Log("reset point");

        currentEnemies--;
        spawnPoint.SetActive(true);
    }
}
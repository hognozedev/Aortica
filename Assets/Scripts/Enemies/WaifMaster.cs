using UnityEngine;

public class WaifMaster : MonoBehaviour
{
    [Header("Refs")]
    public GameObject[] spawnPoints;
    public GameObject prefab;

    [Header("Vars")]
    public int startDelay;
    public int spawnRate;
    public int maxSpawns;
    public AudioSource spawnSound;

    private int currentEnemies = 0;

    void Start()
    {
        InvokeRepeating("Spawn", startDelay, spawnRate); //* Random.Range(0.8f, 1.2f));

    }

    void Spawn()
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject chosenPoint = spawnPoints[index];

        if (currentEnemies <= maxSpawns && chosenPoint.activeInHierarchy)
        {
            spawnSound.Play();

            currentEnemies++;
            chosenPoint.SetActive(false);

            GameObject currentInst;
            currentInst = Instantiate(prefab, chosenPoint.transform.position, Quaternion.identity);
            currentInst.TryGetComponent<WaifAI>(out WaifAI wai); wai.spawnPoint = chosenPoint;

        }

    }

    public void ResetSpawnPoint(GameObject spawnPoint)
    {
        currentEnemies--;
        spawnPoint.SetActive(true);
    }
}
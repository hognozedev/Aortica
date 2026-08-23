using UnityEngine;

//the necrophage will attempt to evolve at random intervals with a random chance. eggs can spawn at any spawn point
//regardless of player proximity

public class necroMaster : MonoBehaviour
{
    [Header("Refs")]
    public GameObject n1Prefab;
    public Transform n1SpawnPoints;

    [Header("Vars")]
    public int startDelay = 1;
    public int spawnRate = 2;
    public Vector3 randomPoint;

    //prv
    private Bounds bounds;
    

    void Start()
    {
        InvokeRepeating("SpawnNecro", startDelay, spawnRate * Random.Range(0.8f, 1.2f));

        bounds = new Bounds(n1SpawnPoints.position, Vector3.one);

    }

    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        Debug.Log("started func");

        float minX = bounds.size.x * -0.5f;
        float minY = bounds.size.y * -0.5f;
        float minZ = bounds.size.z * -0.5f;

        n1SpawnPoints.TransformPoint
            (randomPoint = new Vector3(Random.Range(minX, -minX), Random.Range(minY, -minY), Random.Range(minZ, -minZ)));
        //transform from the space in bounds to the world space in the level.

        return randomPoint;

    }

    void SpawnNecro()
    {
        Debug.Log(RandomPointInBounds(bounds).ToString());

        Instantiate(n1Prefab, randomPoint, Quaternion.identity);

    }

}
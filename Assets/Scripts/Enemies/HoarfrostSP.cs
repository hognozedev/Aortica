using UnityEngine;

public class HoarfrostSP : MonoBehaviour
{
    [Header("References")]
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public int spawnRate = 2;

    private GameObject[] enemies;
    private int currentEnemies;

    void Start()
    {
        InvokeRepeating("CheckSP", 2f, spawnRate);

    }


    void Update()
    {

    }


    void CheckSP()
    {
        enemies = GameObject.FindGameObjectsWithTag("Hoarfrost");
        foreach(GameObject gameObject in enemies) currentEnemies++;     

        if(currentEnemies <= 2)
        {
            SpawnEnemy();
        }

        currentEnemies = 0;

    }

    void SpawnEnemy()
    {
        /*
        for (int i = 0; i < craftItems.Count && i < craftSlots.Length; i++)
        {
            CraftItems craftItem = craftItems[i];
            craftSlots[i].Initialize(craftItem.itemData, craftItem.itemData.itemCost);
            craftSlots[i].gameObject.SetActive(true);
        }
        
                for (int i = craftItems.Count; i < craftSlots.Length; i++)
        {
            craftSlots[i].gameObject.SetActive(false);
        }


        //GameObject spawnAt = Random.gameObject(spawnPoints);
        spawnAt.SetActive(true);
        */
    }


}
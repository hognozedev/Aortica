using UnityEngine;

public class nStage1 : MonoBehaviour
{
    [Header("Refs")]
    public EnemyData enemyData;

    [Header("Vars")]
    public int lifespan = 5;


    public void Start()
    {
        Debug.Log("spawned egg");

       //apply physics & then animation when contacts floor layer?
       //play falling animation, spawn nStage2 in exact same location, destroy this self.

    }

}
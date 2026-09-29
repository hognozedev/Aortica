using UnityEngine;

public class DDOL : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");

        if (objs.Length > 1)
        {
            Destroy(objs[1]);
        }

        DontDestroyOnLoad(gameObject);
    }
//keeps the tagged GOs but deletes the old version if it somehow duplicates
}
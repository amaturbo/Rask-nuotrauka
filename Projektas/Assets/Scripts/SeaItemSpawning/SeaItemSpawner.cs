using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaItemSpawner : MonoBehaviour {

    /// <summary>
    /// Sea item spawn location objects
    /// </summary>
    public SeaItemSpawn[] spawns;
   // public Transform[] locations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

    /// <summary>
    /// min and max waiting times for random spawning in of items
    /// </summary>
    public float minWait = 2f;
    public float maxWait = 5f;

    private bool isSpawning;

    void Awake()
    {
        isSpawning = false;

        spawns = new SeaItemSpawn[transform.childCount];
        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i] = transform.GetChild(i).gameObject.GetComponent<SeaItemSpawn>();
        }
    }
    

	void Update () {

        if (!isSpawning)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("Spawn", timer);
            isSpawning = true;
        }		
	}

    public void Spawn()
    {
        int randSpawn = Random.Range(0, spawns.Length);
        int randItem = Random.Range(0, whatToSpawnClone.Length);
        spawns[randSpawn].SpawnItem(whatToSpawnClone[randItem]);

        isSpawning = false;
    }




}

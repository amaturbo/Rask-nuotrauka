using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {
    
    // How long between each spawn.
    public float spawnTime = 3f;
    // An array of the spawn points this enemy can spawn from.
    public Transform[] spawnPoints;        
    // An array of enemy prefabs to spawn
    public GameObject[] enemies;

    public GameObject spawnEffect;

    //time after enemies vanish
    public float deathTime = 60f;

    //min and max number of enemies to spawn at once
    public int minEns = 1;
    public int maxEns = 6;

    //min and max distance from a spawn location for enemies to spawn 
    float minRange = 2f;
    float maxRange = 6f;

    // every "levelIncrease" spawn enemy level increases;
    [SerializeField]
    [Header("levels")]
    public int levelIncrease;
    public int damageIncrease;
    public int speedIncrease;
    public int healthIncrease;
    public int maxLevel;
    int index = 0;

    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;

    float startDamage;
    float startSpeed;
    float startHealth;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);

        enemyMovement = enemies[0].GetComponent<EnemyMovement>();
        startDamage = enemyMovement.damage;
        enemyHealth = enemies[0].GetComponent<EnemyHealth>();
        startHealth = enemyHealth.startHealth;
        nav = enemies[0].GetComponent<NavMeshAgent>();
        startSpeed = nav.speed;
    }

    private void OnDestroy()
    {
        enemyMovement.damage = startDamage;
        enemyHealth.startHealth = startHealth;
        nav.speed = startSpeed;
    }

    void Spawn()
    {
        int level = 0;

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        int count = Random.Range(minEns, maxEns);

        if (maxLevel > level) index++;

        if (index >= levelIncrease)
        {
            level++;
            enemyMovement.damage += damageIncrease;
            enemyHealth.startHealth += healthIncrease;
            nav.speed += speedIncrease;
            index = 0;
        }

        for (int i = 0; i < count; i++)
        {
            int enInd = Random.Range(0, enemies.Length);

            Quaternion rot = spawnPoints[spawnPointIndex].rotation;
            rot.y = Random.rotation.y;

            Vector3 pos = spawnPoints[spawnPointIndex].position;
            pos.x = pos.x + Random.Range(-minRange, maxRange);
            pos.z = pos.z + Random.Range(-minRange, maxRange);


            //  yield WaitForSeconds(0.25);
            //  StartCoroutine(SpawnEffect(pos, rot));
            //SpawnEffect(pos, rot);
            // Invoke("spEffect", 5f);

            var effectInstance = Instantiate(spawnEffect, pos, rot);
        //    Destroy(effectInstance, 5.00f);


            var instance = Instantiate(enemies[enInd], pos, rot);
            instance.GetComponent<NavMeshAgent>().enabled = true;
            //instance.AddComponent<EnemyMovement>();
            Destroy(effectInstance, 5.00f);
            Destroy(instance, deathTime);
        }
    }
    /*

    IEnumerator SpawnEffect(Vector3 pos, Quaternion rot)
    {
        Debug.Log("COROUTINE STARTED MY DUDE");
        GameObject effectInstance = Instantiate(spawnEffect, pos, rot);
        Debug.Log(effectInstance.GetType());
       // Destroy(effectInstance, 5.00f);
        yield return new WaitForSeconds(5f);
    }

    public void spEffect(Vector3 pos, Quaternion rot)
    {
        var effectInstance = Instantiate(spawnEffect, pos, rot);
        Destroy(effectInstance, 5.00f);
    }

    //======================================

    public IEnumerator DoDelay(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }

    public void MyCallBack()
    {
        //this gets called when DoDelay is ready. can instantiate here or whatever.

    }

    void StartDelay()
    { //call this in your calling function...
        StartCoroutine(DoDelay(3.0f, MyCallBack));
    }

    */



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmSpawner : MonoBehaviour {

    public float spawnTimer = 30.0f;//6.0f;
    public float maxSpawnDelay = 20.0f;
    float timer;

    public float destroyDelay = 120.0f;

    const int Max_Items = 4;

    public GameObject whatToSpawn;

    GameObject parent;
    Transform spawn;
XDSfasf
dsgjnsdlkfsdlkslkdgnsdlkgsd
sdlgkndlklsdkgsldkgnsld
sdjgskdjgb
        er;

    public float destroyDelay = 120.0f;

    const int Max_Items = 4;

    public GameObject whatToSpawn;

    GameObject parent;
    Transform spawn;
XDSfasf
er;

    public float destroyDelay = 120.0f;

    const int Max_Items = 4;

    public GameObject whatToSpawn;

    GameObject parent;
    Transform spawn;
XDSfasf
er;

    public float destroyDelay = 120.0f;

    const int Max_Items = 4;

    public GameObject whatToSpawn;

    GameObject parent;
    Transform spawn;
XDSfasf
    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SpawnItem(whatToSpawn);
            timer = spawnTimer + Random.Range(0, maxSpawnDelay);
        }
	}

    public void SetSpawnPositions()
    {
        Vector3 center = spawn.position;
        Vector3 pos0 = new Vector3(-0.002316321f, +0.001066707f, +0.007373331f);
        Vector3 pos1 = new Vector3(-0.001827097f, +0.002459914f, -0.0112914f);
        Vector3 pos2 = new Vector3(-0.0114328f, +0.0004598726f, -0.003606431f);
        Vector3 pos3 = new Vector3(+0.008901863f, +0.001754582f, -0.002937806f);

        spawns[0] = new CoconutSpawnPosition(pos0);
        spawns[1] = new CoconutSpawnPosition(pos1);
        spawns[2] = new CoconutSpawnPosition(pos2);
        spawns[3] = new CoconutSpawnPosition(pos3);
    }

    public void SpawnItem(Object item)
    {
        int k = Random.Range(0, 3);

      //  Debug.Log(k);
      //  Debug.Log(spawn.ToString());
        CoconutSpawnPosition pos = spawns[k];

        Vector3 kazkas = spawn.position + pos.spawnPos;

        GameObject instance = (GameObject)Instantiate(item, kazkas, Random.rotation, spawn);
        instance.transform.SetParent(spawn, false);
        instance.transform.position += pos.spawnPos * 75;
        
        //  GameObject instance = Instantiate(item, spawn, false) as GameObject;
        //instance.transform.SetParent(spawn, false);
        //instance.transform.rotation = Random.rotation;
        //instance.transform.position += kazkas;

        instance.AddComponent<CoconutMovement>();
        Destroy(instance, destroyDelay);
    }

    void OnMouseDown()
    {
        //not implemented yet
         Destroy(this.gameObject);
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}

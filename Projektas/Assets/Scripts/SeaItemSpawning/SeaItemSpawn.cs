using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is used on Sea Item Spawn location;
/// it instantiates a given object
/// </summary>
public class SeaItemSpawn : MonoBehaviour {

    /// <summary>
    /// delay of item destruction time
    /// </summary>
    public float destroyDelay = 300f;

    public void SpawnItem(Object item)
    {
        var instance = Instantiate(item, transform.position, Random.rotation, transform) as GameObject;
        instance.AddComponent<SeaItemMovement>();
   //     instance.AddComponent<Rigidbody>();
        instance.GetComponent<Rigidbody>().useGravity = false;
        //y-position is freezed so it won't fly up/down when it collides with other rigid bodies
        instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        Destroy(instance, destroyDelay);
    }


}

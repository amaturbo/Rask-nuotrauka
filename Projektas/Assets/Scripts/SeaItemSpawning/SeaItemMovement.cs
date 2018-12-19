using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaItemMovement : MonoBehaviour {

    /// <summary>
    /// object movement speed
    /// </summary>
    public float speed = 4f;

    /// <summary>
    /// default destination - usually in the center of the map so the 
    /// moving objects can identify the diretion to move
    /// </summary>
    private Transform DefaultDestination;
    public string DestinationName = "SeaItemWaypoint";

    private Transform spawnLocation;

    /// <summary>
    /// waypoint target
    /// </summary>
    private Transform target;

    /// <summary>
    /// checks whether the object has collided
    /// </summary>
    public bool hasCollided = false;

    /// <summary>
    /// object that will trigger the collision
    /// </summary>
    public GameObject destinationObject;

    Transform water;

    void Start () {
        DefaultDestination = GameObject.Find(DestinationName).transform;
        
        target = DefaultDestination;

        destinationObject = GameObject.Find("island");
        water = GameObject.Find("Water").transform;

        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>());

    }


    void Update()
    {
        if (hasCollided == false)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    /// <summary>
    /// use this method if the object colliders have "isTrigger" attribute set to "true"
    /// </summary>
    /// <param name="col"></param>

    void OnTriggerEnter(Collider col)
    {
      //  Debug.Log("collision triggered!");
        if (col.gameObject == destinationObject || col.gameObject.GetComponent<Rigidbody>() != null)
        {
            hasCollided = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("collided!");
    //    Transform objec = col.transform;
    //    //collision is triggered when object collides with destination object or any other rigidbody
    //    //if (col.gameObject == destinationObject || col.rigidbody != null)
    //    if (col.gameObject == destinationObject || objec == destinationObject || col.rigidbody != null || col.gameObject.GetComponent<Rigidbody>() != null)
    //    {
    //        hasCollided = true;
    //        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    //    }
    //}


}

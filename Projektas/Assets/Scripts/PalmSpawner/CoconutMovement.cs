using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutMovement : MonoBehaviour {

    float autoDropOffset = 5.0f;
    float timeToStop = 0.5f;
    float timeToCompletelyStop = 6;

    bool isItemDropped = false;
    bool actionsFinished = false;

    private void Start()
    {
        if(gameObject.GetComponent<Rigidbody>() == null)
            gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update () {

        if (!actionsFinished)
        {
            autoDropOffset -= Time.deltaTime;

            if (autoDropOffset < 0)
                if (!isItemDropped)
                {
                    Drop();                    
                }

            if (isItemDropped)
            {
                timeToStop -= Time.deltaTime;
                if (timeToStop < 0)
                {
                    EnableCollisions();
                }
            }
        }
        else if (timeToCompletelyStop > 0)
        {
            timeToCompletelyStop -= Time.deltaTime;
            if (timeToCompletelyStop < 0)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
               // GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    public void Drop()
    {
        isItemDropped = true;
        GetComponent<Rigidbody>().useGravity = true;
      //
        if(gameObject.GetComponent<MeshCollider>() == null)  
            gameObject.AddComponent<MeshCollider>();
        gameObject.GetComponent<MeshCollider>().convex = true;
    }

    void EnableCollisions()
    {
        actionsFinished = true;
        transform.parent = null;
    }    

    public void DropOnClick()
    {
        Drop();
        EnableCollisions();
    }


    void OnMouseDown()
    {
        DropOnClick();
       // Destroy(this.gameObject);
    }




}

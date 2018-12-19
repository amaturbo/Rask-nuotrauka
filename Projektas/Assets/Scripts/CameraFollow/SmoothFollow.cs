using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    /// <summary>
    /// x/y/z Distance - camera distance offsets
    /// relative to the player's position
    /// </summary>
    float xDistance = -30;
    float yDistance = +30;
    float zDistance = -30;

    /// <summary>
    /// zoomValue = camera zoom speed
    /// </summary>
    float zoomValue = 1.1f;
    float zoomUpperLimit = 60f;
    float zoomLowerLimit = 10f;
    
    /// <summary>
    /// camera smoothing time
    /// </summary>
    float smoothTime = 1f;//0.05f;
    


    public GameObject player;
    Transform playerPos;
    Vector3 velocity;

    // Use this for initialization
    void Start () {
        velocity = Vector3.zero;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;		
	}
	
	// Update is called once per frame
	void Update () {
        ScrollZoom();
    }

    void LateUpdate()
    {
        MoveCameraSmoothly();
    }

    /// <summary>
    /// This methods moves the camera smoothly
    /// </summary>
    void MoveCameraSmoothly()
    {
        Vector3 needPos = new Vector3(playerPos.transform.position.x + xDistance, playerPos.transform.position.y + yDistance,
            playerPos.transform.position.z + zDistance);
        //  transform.position = Vector3.Lerp(transform.position, needPos, 0.01f);
        transform.position = Vector3.SmoothDamp(transform.position, needPos, ref velocity, smoothTime);
        transform.LookAt(playerPos.transform);
    }

    /// <summary>
    /// This method controls the camera zoom function
    /// </summary>
    void ScrollZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward (zoom in)
        {
            if (yDistance > zoomLowerLimit)
            {
                xDistance /= zoomValue;
                yDistance /= zoomValue;
                zDistance /= zoomValue;
            }            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards (zoom out)
        {
            if (yDistance < zoomUpperLimit)
            {
                xDistance *= zoomValue;
                yDistance *= zoomValue;
                zDistance *= zoomValue;
            }            
        }
    }


}

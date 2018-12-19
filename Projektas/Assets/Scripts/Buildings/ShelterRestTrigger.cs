using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterRestTrigger : MonoBehaviour {

    public bool resting = false;
    public int restSpeed = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
            resting = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
            resting = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour {

    public bool cantBuild = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Unit") || other.transform.gameObject.layer == LayerMask.NameToLayer("Water"))
            cantBuild = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Unit") || other.transform.gameObject.layer == LayerMask.NameToLayer("Water"))
            cantBuild = false;
    }

}

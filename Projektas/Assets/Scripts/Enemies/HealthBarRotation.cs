using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotation : MonoBehaviour {

    Transform t;
    public float rotationx;
    public float rotationy = 0;

    void Start()
    {
        t = transform;
    }

    void Update()
    {
        t.eulerAngles = new Vector3(rotationx, rotationy, 0);
    }
}

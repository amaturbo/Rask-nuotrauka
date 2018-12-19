using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawnPosition {

    public bool isEmpty = false;

    public Vector3 spawnPos { get; set; }

    public CoconutSpawnPosition(Vector3 Position, bool isEmpty)
    {
        this.isEmpty = isEmpty;
        spawnPos = Position;
    }

    public CoconutSpawnPosition(Vector3 Position)
    {
        spawnPos = Position;
    }



}

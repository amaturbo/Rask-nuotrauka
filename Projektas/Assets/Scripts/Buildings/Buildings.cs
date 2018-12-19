using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buildings {

    [Header("Prefabs")]
    public GameObject Prefab;
    public GameObject Red;
    public GameObject Ghost;
    [Header("Costs")]
    public int WoodCost;
    public int ClayCost;
    public int RockCost;
    public int CoalCost;
}

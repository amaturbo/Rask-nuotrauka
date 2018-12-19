using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeBuilding : MonoBehaviour, IBuilding
{
    public GameObject Resources;
    public string BuildingInfo
    {
        get
        {
            return BuildingInfo;
        }
        set
        {
            BuildingInfo = value;
        }
    }

    public int CostClay
    {
        get
        {
            return CostClay;
        }
        set
        {
            CostClay = value;
        }
    }

    public int CostRock
    {
        get
        {
            return CostRock;
        }
        set
        {
            CostRock = value;
        }
    }

    public int CostWood
    {
        get
        {
            return CostWood;
        }
        set
        {
            CostWood = value;
        }
    }

    public bool EnoughClay()
    {
        //return (Resources.getClay() - CostClay);
        return false;
    }

    public bool EnoughRock()
    {
        //return (Resources.getRock() - CostRock);
        return false;
    }

    public bool EnoughWood()
    {
        //return (Resources.getWood() - CostWood);
        return false;
    }

    public int ID
    {
        get
        {
            return ID;
        }
        set
        {
            ID = value;
        }
    }

    public string Name
    {
        get
        {
            return Name;
        }
        set
        {
            Name = value;
        }
    }

    public Color HoverColor
    {
        get
        {
            return HoverColor;
        }

        set
        {
            HoverColor = value;
        }
    }

    public Color StartColor
    {
        get
        {
            return StartColor;
        }
        set
        {
            StartColor = Render.material.color;
        }
    }

    public Renderer Render
    {
        get
        {
            return Render;
        }
        set
        {
            Render = GetComponent<Renderer>();
        }
    }

    public DecorativeBuilding()
    {
        ID = 1;
        Name = "unnamed";
        CostClay = 0;
        CostRock = 0;
        CostWood = 0;
        BuildingInfo = "no info";
    }

    public DecorativeBuilding(int id, string name, int costC, int costR, int costW, string info)
    {
        this.ID = id;
        this.Name = name;
        this.CostClay = costC;
        this.CostRock = costR;
        this.CostWood = costW;
        this.BuildingInfo = info;
    }

    public void Build()
    {
        throw new NotImplementedException();
    }

    public void Deconstruct()
    {
        throw new NotImplementedException();
    }

    public void Upgrade()
    {
        throw new NotImplementedException();
    }
}
